using Symphony_Sprint.Game_Model.World_Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symphony_Sprint.Game_Model
{
    public class GameController : ISerialize
    {

        public Level level;
        public Player player;
        public int points;
        public int notes;
        public bool isGameOver = false;

        public string[] images = { "wholeNote.gif", "quarterNote.gif", "eigthNote.gif", "trebleClef.gif", "flat.gif", "sharp.gif", "halfNote.gif" };

        Random rand = new Random();

        private static GameController instance = new GameController();

        public static GameController Instance
        {
            get
            {
                return instance;
            }
        }

        public Level Level { get { return level; } set { level = value; } }
        public Player Player { get { return player; } set { player = value; } }
        public int Points { get { return points; } set { points = value; } }
        public int Notes { get { return notes; } set { notes = value; } }

        public GameController()
        {
            Player = new Player("robin.png");
            level = new Level();
        }

        public void Update()
        {

        }

        public void LargoLevel()
        {

            for (int i = 0; i < 20; i++)
            {
                int img = rand.Next(0, 6);
                int space = rand.Next(10, 20);
                int posX = rand.Next(1200, 1500);
                int posY = rand.Next(100, 400);
                GameObject obj = new GameObject(images[img], 20, posX, posY);
                Level.GameObjects.Add(obj);
            }

            
        }

        public void Save(string filename)
        {
            string serializedData = Serialize();
            File.WriteAllText(filename, serializedData);
        }

        public GameController Load(string filename)
        {
            string serializedData = File.ReadAllText(filename);
            var game = new GameController();
            game.Deserialize(serializedData);
            return game;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(string data)
        {
            throw new NotImplementedException();
        }
    }
}
