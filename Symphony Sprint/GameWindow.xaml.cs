 using Symphony_Sprint.Game_Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            ImageBehavior.SetAnimatedSource(playerImg, playerSource);
            GameCanvas.Children.Add(playerImg);


            //Sets the players position depending on its state. 
            GameController.Instance.Player.UpdatePosition();
            Canvas.SetLeft(playerImg, GameController.Instance.Player.PosX);
            Canvas.SetBottom(playerImg, GameController.Instance.Player.PosY);
            //End of player code


            //Game Object Code
            //Loops through each game object and sets there custom position.
            foreach (GameObject obj in GameController.Instance.Level.GameObjects.ToList())
            {
                var objectSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", obj.ImgPath), UriKind.Relative));
                Image objImg = new Image();
                objImg.Source = objectSource;
                



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

                
                
                objImg.Uid = "GameObject";
                GameCanvas.Children.Add(objImg);
                obj.posX -= obj.Speed;

                Canvas.SetLeft(objImg, obj.posX);
                Canvas.SetBottom(objImg, obj.posY);
                //End of Game Object Code

                //Collision Code Start
                var objects = new System.Drawing.Rectangle(

                    Convert.ToInt32(Canvas.GetLeft(objImg)),
                        Convert.ToInt32(Canvas.GetBottom(objImg)),
                        Convert.ToInt32(objImg.ActualWidth),
                        Convert.ToInt32(objImg.ActualHeight)

                    );

                

                var player = new System.Drawing.Rectangle(
                        Convert.ToInt32(Canvas.GetLeft(playerImg)),
                        Convert.ToInt32(Canvas.GetBottom(playerImg)),
                        Convert.ToInt32(playerImg.ActualWidth),
                        Convert.ToInt32(playerImg.ActualHeight)
                    );

                //if (objects.Left < player.Left && objects.Right > player.Left && objects.Top > player.Top && objects.Bottom < player.Top || objects.Left < player.Right && objects.Right > player.Right && objects.Top > player.Bottom && objects.Bottom < player.Bottom || objects.Left < player.Top && objects.Right > player.Top && objects.Top > player.Right && objects.Bottom < player.Right || objects.Left < player.Bottom && objects.Right > player.Bottom && objects.Top > player.Left && objects.Bottom < player.Left || player.Left < objects.Left && player.Right > objects.Left && player.Top > objects.Top && player.Bottom < objects.Top || player.Left < objects.Right && player.Right > objects.Right && player.Top > objects.Bottom && player.Bottom < objects.Bottom || player.Left < objects.Top && player.Right > objects.Top && player.Top > objects.Right && player.Bottom < objects.Right || player.Left < objects.Bottom && player.Right > objects.Bottom && player.Top > objects.Left && player.Bottom < objects.Left)
                //{

                //    Debug.WriteLine(objects.X + "and" + objects.Y);
                //    GameController.Instance.Level.GameObjects.Remove(obj);
                //}

                if (objects.Left == player.Right)
                {
                    GameController.Instance.Level.GameObjects.Remove(obj);
                }
                
                if (objects.IntersectsWith(player))
                {
                    Debug.WriteLine("Collision!");
                }
                //Collision code end
            }

        }

        

    }
}
