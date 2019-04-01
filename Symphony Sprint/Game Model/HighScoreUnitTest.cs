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
        public void CreateStringOfScoresAndName_ReturnsString_Succesful()
        {
            HighScore hs = new HighScore();
            hs.HSList = [3000, 2300, 500];
            hs.Names = ["billybob", "bobbybill", "billybobbybillbob"];
            Assert.IsTrue(hs.CreateStringOfScoresAndNames() == "billybob......3000, bobbybill......2300, billybobbybillbob......500");
        }
    }
}