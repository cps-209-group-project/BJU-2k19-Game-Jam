using Microsoft.Win32;
using Symphony_Sprint.Game_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace Symphony_Sprint
{
    
    public partial class GameWindow : Window
    {
        public static DispatcherTimer gameTimer;
        public int seconds = 0;
        

        public GameWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(GameController.Instance.Player.KeyIsDown);
            GameController.Instance.Player.PosX = 100;
            GameController.Instance.Player.PosY = 50;
        }

        public void Window_Loaded(object sender, EventArgs e)
        {
            //Load images on screen
            

            var source = new BitmapImage(new Uri("/Graphics/heart-1.png.png", UriKind.Relative));

            time.Source = new BitmapImage(new Uri("/Graphics/time-1.png.png", UriKind.Relative));
            lives.Source = new BitmapImage(new Uri("/Graphics/lives-1.png.png", UriKind.Relative));
            heart1.Source = source;
            heart2.Source = source;
            heart3.Source = source;


            gameTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 5) };
            gameTimer.Tick += GameTimer_Tick;

            GameController.Instance.LargoLevel();
            //SetupGame();
            UpdateScreen();
      
            gameTimer.Start();
        }

        //Check if the game is over or not..
        //If is not over then update screen...
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            

            if (GameController.Instance.isGameOver == false)
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

            seconds++;
            timeNum.Content = seconds.ToString();

            
            GameCanvas.Children.Clear();

            //Piano
            var piano = new Image();
            piano.Source = new BitmapImage(new Uri("/Graphics/pianoDouble-1.png.png", UriKind.Relative));
            piano.Height = 54;
            piano.Stretch = Stretch.UniformToFill;
            Canvas.SetBottom(piano, 0);
            GameCanvas.Children.Add(piano);

            //Player
            var playerSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", GameController.Instance.Player.ImgPath), UriKind.Relative));
            var playerImg = new Image();
            playerImg.Height = 60;

            Canvas.SetLeft(playerImg, GameController.Instance.Player.PosX);


            ImageBehavior.SetAnimatedSource(playerImg, playerSource);
            GameCanvas.Children.Add(playerImg);


            //Sets the players position depending on its state. 

            Canvas.SetBottom(playerImg, GameController.Instance.Player.PosY);
            GameController.Instance.Player.UpdatePosition();
            //End of player code


            //Game Object Code
            //Loops through each game object and sets there custom position.
            foreach (GameObject obj in GameController.Instance.Level.GameObjects.ToList())
            {
                var objectSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", obj.ImgPath), UriKind.Relative));
                var objImg = new Image();
                

                ImageBehavior.SetAnimatedSource(objImg, objectSource);

                if (obj.posX > 1190)
                {
                    objImg.Visibility = Visibility.Hidden;
                }

                if(obj.posX < 10)
                {
                    //objImg.Visibility = Visibility.Hidden;
                    GameController.Instance.Level.GameObjects.Remove(obj);
                }
               
                if (obj.ImgPath == "trebleClef-7.png.png")
                {
                    objImg.Height = 60;
                }
                else
                {
                    objImg.Height = 40;
                }

                if (DetectCollision(objImg, playerImg) == null)
                {
                    //objImg.Visibility = Visibility.Hidden;
                    Console.WriteLine("Collision");
                }


                //if (Canvas.GetLeft(objImg) == GameController.Instance.Player.PosX && Canvas.GetBottom(objImg) == GameController.Instance.Player.PosY)
                //{
                //    objImg.Visibility = Visibility.Hidden;
                //}

                //if (obj.posX == GameController.Instance.Player.PosX && obj.posY == GameController.Instance.Player.PosY)
                //{
                //    objImg.Visibility = Visibility.Hidden;
                    
                //}

                objImg.Uid = "GameObject";
                GameCanvas.Children.Add(objImg);
                obj.posX -= obj.Speed;

                Canvas.SetLeft(objImg, obj.posX);
                Canvas.SetBottom(objImg, obj.posY);
                //End of Game Object Code
                
            }
 
        }

        private Rect DetectCollision(FrameworkElement rect1, FrameworkElement rect2)
        {
            var r1 = new Rect(Canvas.GetLeft(rect1), Canvas.GetTop(rect1), rect1.ActualWidth, rect1.ActualHeight);
            var r2 = new Rect(Canvas.GetLeft(rect2), Canvas.GetTop(rect2), rect2.ActualWidth, rect2.ActualHeight);
            r1.Intersect(r2);
            return r1;
        }

    }
}
