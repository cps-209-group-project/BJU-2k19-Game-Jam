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
        public int time = 100;


        public GameWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, EventArgs e)
        {
            gameTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
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
            --time;
            Console.WriteLine("Working");

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
            GameCanvas.Children.Clear();

            var playerSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", GameController.Instance.Player.ImgPath), UriKind.Relative));
            var playerImg = new Image();
            playerImg.Height = 60;
            GameController.Instance.Player.PosX = 100;
            GameController.Instance.Player.PosY = 50;

            Canvas.SetLeft(playerImg, GameController.Instance.Player.PosX);
            Canvas.SetBottom(playerImg, GameController.Instance.Player.PosY);

            ImageBehavior.SetAnimatedSource(playerImg, playerSource);
            GameCanvas.Children.Add(playerImg);
            
            //Loops through each game object and sets there custom position.
            foreach (GameObject obj in GameController.Instance.Level.GameObjects)
            {
                var objectSource = new BitmapImage(new Uri(String.Format("/Graphics/{0}", obj.ImgPath), UriKind.Relative));
                var img = new Image();
                

                ImageBehavior.SetAnimatedSource(img, objectSource);

                img.Height = 40;
                GameCanvas.Children.Add(img);
                obj.posX -= obj.Speed;

                Canvas.SetLeft(img, obj.posX);
                Canvas.SetTop(img, obj.posY);

                
            }
        }

        
    }
}
