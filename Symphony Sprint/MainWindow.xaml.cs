using Symphony_Sprint.Properties;
using System.Windows;

namespace Symphony_Sprint
{

    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer sPlayerMW;
        public MainWindow()
        {
            InitializeComponent();
            sPlayerMW = new System.Media.SoundPlayer(Properties.Resources.audio_hero_On_The_Ball_SIPML_K_04_25_01);
            sPlayerMW.Play();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ChooseDifficulty dWin = new ChooseDifficulty(sPlayerMW);
            dWin.Show();
            //GameWindow gwin = new GameWindow();
            //gwin.Show();
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow();
            highScoreWindow.Show();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help helpWindow = new Help();
            helpWindow.Show();
        }       

        private void btnAbout_CLick(object sender, RoutedEventArgs e)
        {
            About aboutWin = new About();
            aboutWin.Show();
        }
    }
}
