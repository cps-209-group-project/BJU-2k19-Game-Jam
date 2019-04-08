﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            HighScoreManager.CreateStringOfScoresAndNames();
        }

        private void InitializeComponent()
        {
        }
    }
}
