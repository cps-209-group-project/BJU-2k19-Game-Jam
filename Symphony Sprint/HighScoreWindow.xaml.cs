using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Symphony_Sprint
{
    /// <summary>
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        public HighScoreWindow()
        {
            InitializeComponent();
            HighScoreManager.LoadScore("HighScoresFile.txt");
            foreach (var item in HighScoreManager.HighScoreList)
            {
                TextBlock nameBlock = new TextBlock();
                nameBlock.FontSize = 25;
                nameBlock.TextAlignment = TextAlignment.Center;
                nameBlock.Foreground = Brushes.LightGoldenrodYellow;
                nameBlock.Margin = new Thickness(0,5,0,0);
                nameBlock.Height = 33;
                nameBlock.FontFamily = new FontFamily("SH Pinscher");
                namePanel.Children.Add(nameBlock);

                TextBlock scoreBlock = new TextBlock();
                scoreBlock.FontSize = 25;
                scoreBlock.TextAlignment = TextAlignment.Center;
                scoreBlock.Foreground = Brushes.LightGoldenrodYellow;
                scoreBlock.Margin = new Thickness(0, 5, 0, 0);
                scoreBlock.Height = 33;
                scoreBlock.FontFamily = new FontFamily("SH Pinscher");
                scorePanel.Children.Add(scoreBlock);

                nameBlock.FontFamily = new FontFamily("SH Pinscher");
                string name = item.Name.ToString();
                string score = item.Score.ToString();
                nameBlock.Text = name;
                scoreBlock.Text = score;
            }
        }
    }
}
