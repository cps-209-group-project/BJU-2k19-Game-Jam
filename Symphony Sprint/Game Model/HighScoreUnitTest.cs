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
            hs.AddNameandScore(name, highscore);
            Assert.IsTrue(hs.CreateStringOfScoresAndNames() == "billybobbybillbob......3000");
        }

        [Test]
        public void Testload_returnstext_succesful()
        {
           string filename = "/highscoresfile.txt";
            HighScoreManager hs = new HighScoreManager();
            hs.LoadScore(filename);
            Assert.IsTrue(hs.HighScoreList.Count > 0);
        }

        [Test]
        public void Testsave_returnsnothings_succesful()
        {
            string filename = "highscoresfile.txt";
            HighScoreManager hs = new HighScoreManager();
            hs.SaveScore(filename);
            Assert.IsTrue(File.Exists(Directory.GetCurrentDirectory() + filename));
        }

    }
}