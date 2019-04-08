using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Symphony_Sprint.Game_Model
{
    [TestFixture]
    public class HighScoreUnitTest
    {
        [Test]
        public void Testcreatestringofscoresandname_returnsstring_succesful()
        {
            string name = "billybobbybillbob";
            int highscore = 3000;
            HighScoreManager hs = new HighScoreManager();
            HighScoreManager.AddNameAndScore(name, highscore);
            HighScoreManager.CreateStringOfScoresAndNames();
            Assert.IsTrue(HighScoreManager.HighScoreText == "billybobbybillbob......3000");
        }

        [Test]
        public void Testload_returnstext_succesful()
        {
            HighScoreManager hs = new HighScoreManager();
            hs.LoadScore();
            Assert.IsTrue(HighScoreManager.HighScoreList.Count > 0);
        }

        [Test]
        public void Testsave_returnsnothings_succesful()
        {
            HighScoreManager hs = new HighScoreManager();
            hs.SaveScore();
            Assert.IsTrue(File.Exists(Directory.GetCurrentDirectory() + "SymphonySprintHighScores.txt"));
        }

    }
}