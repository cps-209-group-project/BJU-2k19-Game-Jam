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
        //[Test]
        //public void Load_Calls_Deserialize() 
        //{
        //    var moq = new Mock<GameController>();

        //    moq.Object.Load("testload");

        //    moq.Verify(game => game.Deserialize(It.IsAny<string>()));
        //}

        //[Test]
        //public void Save_Calls_Serialize() 
        //{
        //    var moq = new Mock<GameController>();

        //    moq.Object.Save("testload");

        //    moq.Verify(game => game.Serialize());
        //}

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
                Points = 52,
                Notes = 99,
                Player = new Player("p2.png")
                {
                    Lives = 3,
                    PosX = 20,
                    PosY = 20
                },
                Level = new Level
                {
                    Difficulty = Level.DifficultyEnum.EASY,
                    NoteObjective = 90,
                    gameObjs = new List<GameObject>
                    {
                        new GameObject("o1.png", 3, 20, 20),
                        new GameObject("o2.png", 2, 5, 10)
                    }
                }
            };

            var serializedData = game.Serialize();

            string expectedData = @"Points:52
Notes:99
Player:Lives=3,PosX=20,PosY=20,ImgPath=p2.png
Level:Difficulty=EASY,NoteObjective=90
GameObjects:
Speed=3,PosX=20,PosY=20,ImgPath=o1.png
Speed=2,PosX=5,PosY=10,ImgPath=o2.png";

            Assert.AreEqual(expectedData.Replace("\n", "\r\n").Replace("\r\r", "\r"), serializedData);
        }

        [Test]
        public void Deserialize_Sets_Properties()
        {
            var serializedData = @"Points:52
Notes:99
Player:Lives=3,PosX=20,PosY=20,ImgPath=p2.png
Level:Difficulty=EASY,NoteObjective=90
GameObjects:
Speed=3,PosX=20,PosY=20,ImgPath=o1.png
Speed=2,PosX=5,PosY=10,ImgPath=o2.png";

            var game = new GameController();
            game.Deserialize(serializedData);

            Assert.AreEqual(52, game.Points);
            Assert.AreEqual(99, game.Notes);
            Assert.IsNotNull(game.Player);
            Assert.AreEqual(3, game.Player.Lives);
            Assert.AreEqual(20, game.Player.PosX);
            Assert.AreEqual(20, game.Player.PosY);
            Assert.AreEqual("p2.png", game.Player.ImgPath);
            Assert.IsNotNull(game.Level);
            Assert.AreEqual(Level.DifficultyEnum.EASY, game.Level.Difficulty);
            Assert.AreEqual(90, game.Level.NoteObjective);
            Assert.IsNotNull(game.Level.GameObjects);
            Assert.AreEqual(3, game.Level.GameObjects[0].Speed);
            Assert.AreEqual(20, game.Level.GameObjects[0].posX);
            Assert.AreEqual(20, game.Level.GameObjects[0].posY);
            Assert.AreEqual("o1.png", game.Level.GameObjects[0].ImgPath);
            Assert.AreEqual(2, game.Level.GameObjects[1].Speed);
            Assert.AreEqual(5, game.Level.GameObjects[1].posX);
            Assert.AreEqual(10, game.Level.GameObjects[1].posY);
            Assert.AreEqual("o2.png", game.Level.GameObjects[1].ImgPath);
        }

    }
}
