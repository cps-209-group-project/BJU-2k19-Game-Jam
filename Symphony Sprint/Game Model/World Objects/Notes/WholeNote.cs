using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symphony_Sprint.Game_Model.World_Objects.Notes
{
    class WholeNote : GameObject
    {
        public int pointValue;

        public string imgPath;
        public int speed;
        public int posX;
        public int posY;

        public int PointValue { get { return pointValue; } set { pointValue = value; } }
        public override string ImgPath { get { return imgPath; } set { imgPath = value; } }
        public override int Speed { get { return speed; } set { speed = value; } }
        public override int PosX { get { return posX; } set { posX = value; } }
        public override int PosY { get { return posY; } set { posY = value; } }

        public override string Serialize()
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(string data)
        {
            throw new NotImplementedException();
        }
    }
}
