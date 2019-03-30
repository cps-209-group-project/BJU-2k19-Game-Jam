using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symphony_Sprint.Game_Model.World_Objects.Enemies
{
    class Flat : GameObject
    {
        public int damage;

        public string imgPath;
        public int speed;
        public int posX;
        public int posY;

        public int Damage { get { return damage; } set { damage = value; } }

        public override string ImgPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int PosX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int PosY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
