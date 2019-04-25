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

namespace Symphony_Sprint
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        public int scorenum;
        public static bool IsPitty = false;
        public GameOverWindow(int scoreNum)
        {
            InitializeComponent();
            scorenum = scoreNum;
            if (scorenum == 0)
            {
                Button btnPitty = new Button();
                btnPitty.Foreground = Brushes.Black;
                btnPitty.FontFamily = new FontFamily("Agency FB");
                btnPitty.FontSize = 10;
                btnPitty.Width = 70;
                btnPitty.Height = 20;
                btnPitty.Content = "Don't click this.";
                goCanvas.Children.Add(btnPitty);
                btnPitty.Click += btnPitty_Click;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (HighScoreManager.IsHighScore(scorenum))
            {
                HighScoreEnter HSEnter = new HighScoreEnter();
                HSEnter.gameScore.Text = Convert.ToString(scorenum);
                HSEnter.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void btnPitty_Click(object sender, RoutedEventArgs e)
        {
            IsPitty = true;
            scorenum += 1000;
            HighScoreEnter hsEnt = new HighScoreEnter();
            hsEnt.gameScore.Text = Convert.ToString(1000);
            hsEnt.Show();
            this.Close();
        }
    }
}
