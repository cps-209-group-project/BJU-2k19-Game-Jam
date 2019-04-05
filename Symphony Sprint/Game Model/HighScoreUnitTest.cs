using System;
using System.Collections.Generic;
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
        public void TestCreateStringOfScoresAndName_ReturnsString_Succesful()
        {
            string name = "billybobbybillbob";
            int highScore = 3000;
            HighScoreManager hs = new HighScoreManager();
            hs.AddNameandScore(name, highScore);
            Assert.IsTrue(hs.CreateStringOfScoresAndNames() == "billybobbybillbob......3000");
        }
        [Test]
        public void TestLoad_ReturnsText_Succesful()
        {
            string filename = "highscoresfile.txt";
            HighScoreManager hs = new HighScoreManager();
            string results = hs.LoadScore(filename);
            Assert.IsTrue(results == "player1......4000, player2......400, player3......40, player4......4");
        }
    }
}