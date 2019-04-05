using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Symphony_Sprint.Game_Model.World_Objects;

namespace Symphony_Sprint.Game_Model
{
    [TestFixture] // Jason's father helped him create these unit tests, thanks Dad!
    public class TestSerialize
    {
        [Test]
        public void Load_Calls_Deserialize() 
        {
            var moq = new Mock<GameController>();

            moq.Object.Load("testload");

            moq.Verify(game => game.Deserialize(It.IsAny<string>()));
        }

        [Test]
        public void Save_Calls_Serialize() 
        {
            var moq = new Mock<GameController>();

            moq.Object.Save("testload");

            moq.Verify(game => game.Serialize());
        }

        [Test]
        public void Save_Test()
        {
            GameController game = new GameController();
            game.Save("testsave");

            FileAssert.Exists("testsave");
        }

        [Test]
        public void Serialize_Formats_Data()
        {
            var game = new GameController
            {
                Points = 20,
                Notes = 33,
                Player = new Player("img"),
                level = new Level()
            };

            var serializedData = game.Serialize();

            Assert.AreEqual("Points:52|Notes:99|Player:Lives=3,Height=0,PosX=20,PoxY=20,ImgPath=p2.png|" +
                "Level:Difficulty=>EASY&NoteObjective=>90&GameObjects=>Speed=3,PosX=20,PoxY=20," +
                "ImgPath=o1.png;Speed=2,PosX=5,PoxY=10,ImgPath=o2.png", serializedData);
        }

        [Test]
        public void Deserialize_Sets_Properties()
        {
            var serializedData = "Points:52|Notes:99|Player:Lives=3,Height=0,PosX=20,PoxY=20,ImgPath=p2.png|" +
                "Level:Difficulty=>EASY&NoteObjective=>90&GameObjects=>Speed=3,PosX=20,PoxY=20," +
                "ImgPath=o1.png;Speed=2,PosX=5,PoxY=10,ImgPath=o2.png";

            var game = new GameController();
            game.Deserialize(serializedData);

            Assert.AreEqual(52, game.Points);
            Assert.AreEqual(99, game.Notes);
            Assert.IsNotNull(game.Player);
            Assert.IsNotNull(game.Level);
        }
    }
}
