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
using static Symphony_Sprint.Game_Model.Level;

namespace Symphony_Sprint
{
    /// <summary>
    /// Interaction logic for ChooseDifficulty.xaml
    /// </summary>
    public partial class ChooseDifficulty : Window
    {
        System.Media.SoundPlayer mPlayer;
        public ChooseDifficulty(System.Media.SoundPlayer sndPlayer)
        {
            InitializeComponent();
            mPlayer = sndPlayer;
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            Level.Difficulty = Level.DifficultyEnum.EASY;
            GameWindow gwin = new GameWindow();
            gwin.Show();
            //mPlayer.Stop();
            this.Close();
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            Level.Difficulty = Level.DifficultyEnum.MEDIUM;
            GameWindow gwin = new GameWindow();
            gwin.Show();
            //mPlayer.Stop();
            this.Close();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            Level.Difficulty = Level.DifficultyEnum.HARD;
            GameWindow gwin = new GameWindow();
            gwin.Show();
            //mPlayer.Stop();
            this.Close();
        }

        private void Insane_Click(object sender, RoutedEventArgs e)
        {
            Level.Difficulty = Level.DifficultyEnum.INSANE;
            GameWindow gwin = new GameWindow();
            gwin.Show();
            //mPlayer.Stop();
            this.Close();
        }
    }
}
