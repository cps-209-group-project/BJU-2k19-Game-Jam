﻿//-----------------------------------------------------------
//File:   Symphunny Sprint
//Desc:   This class handles game play screen
//        
//-----------------------------------------------------------

using Microsoft.Win32;
using Symphony_Sprint.Game_Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace Symphony_Sprint
{
    public partial class GameWindow : Window
    {
        GameController gc;

        public static DispatcherTimer gameTimer;
        public static DispatcherTimer displayTimer;


        //public int livesLeft = 3;
        private OpenFileDialog loadDialog = new OpenFileDialog();
        private SaveFileDialog saveDialog = new SaveFileDialog();

        BitmapImage source1;
        BitmapImage source2;
        BitmapImage source3;

        System.Media.SoundPlayer sPlayer;
        System.Media.SoundPlayer GOPlayer;
        System.Media.SoundPlayer victoryPlayer;
        System.Media.SoundPlayer gStartPlayer;

        public GameWindow()
        {
            gc = new GameController();
            InitializeComponent();
            DataContext = gc.Level;

            this.KeyDown += new KeyEventHandler(gc.Player.KeyIsDown);
            this.KeyDown += new KeyEventHandler(this.KeyIsDown);
            gc.Player.PosX = 200;
            gc.Player.PosY = 50;
            gc.Player.Lives = 3;
            gc.Level.NoteObjective = 10; //The first level, Largo, has 10 notes to collect.

            GOPlayer = new System.Media.SoundPlayer(Properties.Resources.zapsplat_cartoon_fail_negative_descending_musical_tuba_marimba_oboe_18126);
            gStartPlayer = new System.Media.SoundPlayer(Properties.Resources.musical_air_horn_dancehall_style_001__AudioTrimmer_com_);
            victoryPlayer = new System.Media.SoundPlayer(Properties.Resources.Advent_Chamber_Orchestra___04___Mozart___Eine_Kleine_Nachtmusik_allegro);
            this.Closing += GameWindow_Closing;
        }

        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop();
            displayTimer.Stop();
        }

        public void Window_Loaded(object sender, EventArgs e)
        {
            gStartPlayer.Play();
            //Load images on screen
            source1 = new BitmapImage(new Uri("/Graphics/heart-1.png.png", UriKind.Relative));
            source2 = new BitmapImage(new Uri("/Graphics/heart-1.png.png", UriKind.Relative));
            source3 = new BitmapImage(new Uri("/Graphics/heart-1.png.png", UriKind.Relative));

            noteImg.Source = new BitmapImage(new Uri("/Graphics/notes-1.png", UriKind.Relative));

            lives.Source = new BitmapImage(new Uri("/Graphics/lives-1.png.png", UriKind.Relative));
            time.Source = new BitmapImage(new Uri("/Graphics/time-1.png.png", UriKind.Relative));
            heart1.Source = source1;
            heart2.Source = source2;
            heart3.Source = source3;
            scoreImg.Source = new BitmapImage(new Uri("/Graphics/score-1.png.png", UriKind.Relative));
            //noteImg.Source = new BitmapImage(new Uri("/Graphics/notes-1.png.png", UriKind.Relative));
            gameTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 5) };
            gameTimer.Tick += GameTimer_Tick;

            displayTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            displayTimer.Tick += DisplayTimer_Tick;
            UpdateScreen();

            gc.LargoLevel();
            UpdateScreen();

            gameTimer.Start();
            displayTimer.Start();
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            gc.Level.Seconds++;
        }

        //Check if the game is over or not..
        //If is not over then update screen...
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gc.isGameOver == false)
            {
                UpdateScreen();
            }
        }

        private void UpdateScreen()
        {
            //Update Points
            //Update Health
            //Update NoteObjective
            //Update Level when needed.

            if (gc.Player.Lives == 2)
            {
                heart1.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
            }
            else if (gc.Player.Lives == 1)
            {
                heart1.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
                heart2.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
            }
            else if (gc.Player.Lives == 0)
            {
                heart1.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
                heart2.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
                heart3.Source = new BitmapImage(new Uri("/Graphics/heartDead-1.png.png", UriKind.Relative));
                GameOverWindow GOWin = new GameOverWindow(Convert.ToInt32(scoreNum.Content));
                gc.isGameOver = true;
                this.Close();
                GOWin.Show();
                GOPlayer.Play();
            }

            //Timer
            if (gc.Level.Seconds < 10)
            {
                timeNum.Content = gc.Level.Min + ":0" + gc.Level.Seconds;
            }
            else if (gc.Level.Seconds > 60)
            {
                gc.Level.Min++;
                gc.Level.Seconds = 0;
                timeNum.Content = gc.Level.Min + ":" + gc.Level.Seconds;
            }
            else
            {
                timeNum.Content = gc.Level.Min + ":" + gc.Level.Seconds;
            }

            CheckGameState();
            GameCanvas.Children.Clear();

            //Piano
            var piano = new Image();
            piano.Source = new BitmapImage(new Uri("/Graphics/pianoDouble-1.png.png", UriKind.Relative));
            piano.Height = 54;
            piano.Stretch = Stretch.UniformToFill;
            Canvas.SetBottom(piano, 0);
            GameCanvas.Children.Add(piano);

            //Player
            var playerSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", gc.Player.ImgPath), UriKind.Relative));
            var playerImg = new Image();
            playerImg.Height = 60;
            playerImg.Width = 50;
            playerImg.Stretch = Stretch.Fill;
            ImageBehavior.SetAnimatedSource(playerImg, playerSource);
            GameCanvas.Children.Add(playerImg);

            //Sets the players position depending on its state. 
            gc.Player.UpdatePosition();
            Canvas.SetLeft(playerImg, gc.Player.PosX);
            Canvas.SetBottom(playerImg, gc.Player.PosY);
            //End of player code

            //Sparkle Image
            var sparkle = new Image();
            var sparkleSource = new BitmapImage(new Uri("/Graphics/sparkle.gif", UriKind.Relative));
            ImageBehavior.SetAnimatedSource(sparkle, sparkleSource);
            //End of Sparkle
            //Game Object Code
            //Loops through each game object and sets there custom position.
            foreach (GameObject obj in gc.Level.GameObjects.ToList())
            {
                var objectSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", obj.ImgPath), UriKind.Relative));
                Image objImg = new Image();
                objImg.Source = objectSource;

                if (obj.posX > 1190)
                {
                    objImg.Visibility = Visibility.Hidden;
                }

                if (obj.posX < 10)
                {
                    //objImg.Visibility = Visibility.Hidden;
                    gc.Level.GameObjects.Remove(obj);
                }

                if (obj.ImgPath == "trebleClef-7.png.png")
                {
                    objImg.Height = 60;
                    objImg.Width = 100;
                }
                else if (obj.ImgPath == "organ-1.png.png")
                {
                    objImg.Height = 60;
                    objImg.Width = 60;
                }
                else
                {
                    objImg.Height = 40;
                    objImg.Width = 60;
                }

                objImg.Uid = "GameObject";
                GameCanvas.Children.Add(objImg);
                obj.posX -= obj.Speed;

                Canvas.SetLeft(objImg, obj.posX);
                Canvas.SetBottom(objImg, obj.posY);
                //End of Game Object Code

                //Collision Code Start
                var objects = new Rect(

                    Convert.ToDouble(Canvas.GetLeft(objImg)),
                        Convert.ToDouble(Canvas.GetBottom(objImg)),
                        Convert.ToDouble(objImg.Width),
                        Convert.ToDouble(objImg.Height)
                    );

                var player = new Rect(
                        Convert.ToDouble(Canvas.GetLeft(playerImg)),
                        Convert.ToDouble(Canvas.GetBottom(playerImg)),
                        Convert.ToDouble(playerImg.Width),
                        Convert.ToDouble(playerImg.Height)
                    );



                if (objects.Left <= player.Right && objects.Right >= player.Left && objects.Bottom >= player.Top && objects.Top <= player.Bottom)
                {
                    if (gc.Player.State == Game_Model.World_Objects.movementState.decending || gc.Player.State == Game_Model.World_Objects.movementState.decending2)
                    {
                        if ((objects.Bottom + 5) <= player.Bottom)
                        {
                            if (obj.ImgPath == "organ-1.png.png")
                            {
                                Canvas.SetBottom(playerImg, Canvas.GetBottom(objImg) + 50);
                                gc.Player.State = Game_Model.World_Objects.movementState.running;

                            }
                        }

                    }

                    if (obj.ImgPath == "flat-1.png.png" || obj.ImgPath == "sharp-1.png.png")
                    {
                        //Checks if cheat mode is enabled.
                        if (gc.isCheatEnabled == true)
                        {
                            return;
                        }
                        //Takes a life and removes enemy from game.
                        Debug.WriteLine(obj.posX + " and " + obj.posY + " player: x: " + gc.Player.PosX + " y: " + gc.Player.PosY);
                        Debug.WriteLine("Rect: Object X: " + objects.X + " and " + objects.Y + " Player: " + player.X + " and " + player.Y);
                        Debug.WriteLine(obj.ImgPath);
                        gc.Player.Lives--;
                        gc.Level.GameObjects.Remove(obj);
                        sPlayer = new System.Media.SoundPlayer(Properties.Resources.zapsplat_musical_karimba_tine_pluck_004_15326);
                        sPlayer.Play();
                        GameCanvas.Children.Remove(objImg);
                    }
                    else if (obj.ImgPath != "organ-1.png.png")
                    {
                        GameCanvas.Children.Remove(objImg);
                        gc.Level.GameObjects.Remove(obj);
                        Debug.WriteLine("Collision: " + "Rect: Object X: " + objects.X + " and " + objects.Y + " Player: " + player.X + " and " + player.Y);
                        gc.Level.NoteObjective--;
                        if (obj.ImgPath == "wholeNote-1.png.png")
                        {
                            gc.Points += 40;
                            sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_small_gamelan_single_mallet_hit_high_004_543);
                            sPlayer.Play();
                        }
                        else if (obj.ImgPath == "halfNote-1.png.png")
                        {
                            gc.Points += 30;
                            sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_small_gamelan_single_mallet_hit_high_003_542);
                            sPlayer.Play();
                        }
                        else if (obj.ImgPath == "quarterNote-1.png.png")
                        {
                            gc.Points += 20;
                            sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_small_gamelan_single_mallet_hit_high_002_541);
                            sPlayer.Play();
                        }
                        if (obj.ImgPath == "eigthNote-1.png.png")
                        {
                            gc.Points += 10;
                            sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_gamelan_single_mallet_hit_high_mid_005_493);
                            sPlayer.Play();
                        }
                        if (obj.ImgPath == "trebleClef-7.png.png")
                        {
                            if (gc.Points == 0)
                            {
                                gc.Points += 10;
                                sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_gamelan_single_mallet_hit_mid_low_004_502);
                                sPlayer.Play();
                            }
                            gc.Points += gc.Points * 2;
                            sPlayer = new System.Media.SoundPlayer(Properties.Resources.ftus_musical_instrument_gender_gamelan_single_mallet_hit_mid_low_004_502);
                            sPlayer.Play();
                        }
                        scoreNum.Content = gc.Points;
                        noteObj.Content = gc.Level.NoteObjective;

                        // If Note Objective is 0, we need to change the level.
                        if (gc.Level.NoteObjective == 0)
                        {
                            ChangeLevelHandler();
                        }
                    }
                }

                //Collision code end
            }
        }
        public void ChangeLevelHandler()
        {
            if (gc.Level.levelName == "largo")
            {
                gc.AndanteLevel();
                gc.Level.NoteObjective = 20;

                gc.Level.GameObjects.RemoveAll(o => o.posX <= 1190); // Remove all objects from the screen because it's a new level
            }
            else if (gc.Level.levelName == "andante")
            {
                gc.ModeratoLevel();
                gc.Level.NoteObjective = 30;

                gc.Level.GameObjects.RemoveAll(o => o.posX <= 1190); // Remove all objects from the screen because it's a new level
            }
            else if (gc.Level.levelName == "moderato")
            {
                gc.AllegroLevel();
                gc.Level.NoteObjective = 40;

                gc.Level.GameObjects.RemoveAll(o => o.posX <= 1190); // Remove all objects from the screen because it's a new level
            }
            else if (gc.Level.levelName == "allegro")
            {
                gc.PrestoLevel();
                gc.Level.NoteObjective = 40;

                gc.Level.GameObjects.RemoveAll(o => o.posX <= 1190); // Remove all objects from the screen because it's a new level
            }
            else if (gc.Level.levelName == "presto")
            {
                VictoryWin victoryWindow = new VictoryWin(Convert.ToInt32(scoreNum.Content));
                gc.isGameOver = true;
                this.Close();
                victoryWindow.Show();
                victoryPlayer.Play();
            }

            SetBackgroundColorFromLevelName(); // Sets the new background color
        }

        public void SetBackgroundColorFromLevelName()
        {
            if (gc.Level.levelName == "largo")
            {
                primaryColor.Color = (Color)ColorConverter.ConvertFromString("#48c6ef");
                secondaryColor.Color = (Color)ColorConverter.ConvertFromString("#6f86d6");
            }
            else if (gc.Level.levelName == "andante")
            {
                primaryColor.Color = (Color)ColorConverter.ConvertFromString("#FF48EF9B");
                secondaryColor.Color = (Color)ColorConverter.ConvertFromString("#FFF1F0D0");
            }
            else if (gc.Level.levelName == "moderato")
            {
                primaryColor.Color = (Color)ColorConverter.ConvertFromString("#FFE6E63D");
                secondaryColor.Color = (Color)ColorConverter.ConvertFromString("#FFEAC39D");
            }
            else if (gc.Level.levelName == "allegro")
            {
                primaryColor.Color = (Color)ColorConverter.ConvertFromString("#FFEF48C9");
                secondaryColor.Color = (Color)ColorConverter.ConvertFromString("#FF48D9F0");
            }
            else if (gc.Level.levelName == "presto")
            {
                primaryColor.Color = (Color)ColorConverter.ConvertFromString("#FFFDFDFD");
                secondaryColor.Color = (Color)ColorConverter.ConvertFromString("#FF6F84CD");
            }
        }

        public void CheckGameState()
        {
            scoreNum.Content = gc.Points;
            noteObj.Content = gc.Level.NoteObjective;
            gc.Player.Lives = gc.Player.Lives;
        }

        public void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                if (gc.isCheatEnabled == false)
                {
                    gc.isCheatEnabled = true;
                }
                else
                {
                    gc.isCheatEnabled = false;
                }
            }
            else if (e.Key == Key.S)
            {
                keyS_press();
            }
            else if (e.Key == Key.L)
            {
                keyL_press();
            }
        }

        private void keyS_press()
        {
            gameTimer.Stop();
            displayTimer.Stop();

            saveDialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameSaves");
            if (saveDialog.ShowDialog() == true)
            {
                gc.Save(saveDialog.FileName);
                loadDialog.FileName = saveDialog.FileName;
            }

            gameTimer.Start();
            displayTimer.Start();
        }

        private void keyL_press()
        {
            gameTimer.Stop();
            displayTimer.Stop();

            this.KeyDown -= new KeyEventHandler(gc.Player.KeyIsDown);

            loadDialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameSaves");
            if (loadDialog.ShowDialog() == true)
            {
                gc.Load(loadDialog.FileName);
                SetBackgroundColorFromLevelName();
            }

            this.KeyDown += new KeyEventHandler(gc.Player.KeyIsDown);

            gameTimer.Start();
            displayTimer.Start();
        }
    }
}
