﻿using Symphony_Sprint.Game_Model.World_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symphony_Sprint.Game_Model
{
    class GameController
    {

        public Level level;
        public Player player;
        public int points;
        public int notes;
        public bool isGameOver = false;

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
            Player = new Player("/Graphics/stone.png");
            level = new Level();
        }

        public void Update()
        {

        }

        public void Largo()
        {
            GameObject wholeNote = new GameObject("wholeNote.gif", 20, 800, 5);

            Level.GameObjects.Add(wholeNote);
        }

        public void Load(string filename)
        {
            throw new NotImplementedException();
        }

        public string Save()
        {
            throw new NotImplementedException();
        }

    }
}
