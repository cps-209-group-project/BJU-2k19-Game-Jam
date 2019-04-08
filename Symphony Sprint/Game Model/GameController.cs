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

        //Sets up Level One
        public void LargoLevel()
        {
            var usedPos = new List<int>();

            var positions = new HashSet<int>(); //HashSets cannot contain duplicate items.

            
            for (int i = 0; i < 50; i++)
            {
                //Sets our random numbers each time the loop goes through.
                int img = rand.Next(0, 6);
                int posX = rand.Next(1200, 15000);
                int posY = rand.Next(115, 250);

                
                //Protects against duplicate random positions.
                
                positions.Add(posX); //Hashsets cannot obtain the same value.
                
                var posList = positions.ToList(); //Change our hashset to a list so we can index it.
                
                if (posList.Count < 10 || posList.Count != 0)
                {
                    posX = posList[i];
                } else
                {
                    posX = rand.Next(1200, 15000);
                    posList.Clear();
                }

                GameObject obj = new GameObject(images[img], 50, posX, posY);
                Level.GameObjects.Add(obj);
                
            }

            
        }


        public void ShuffleMe(List<int> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                int value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
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
