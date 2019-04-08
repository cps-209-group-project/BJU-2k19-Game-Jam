using System.Windows.Input;

namespace Symphony_Sprint.Game_Model.World_Objects
{
    public class Player : ISerialize
    {

        public enum movementState { running, jumping, doublejump, decending, decending2 }
        public movementState State { get; set; }
        //public bool isJumping;

        public string ImgPath { get; set; }
        public int Lives { get; set; }       
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int jumpceiling1 { get; set; }
        public int jumpceiling2 { get; set; }


        public Player(string img)
        {
            ImgPath = img;
            State = movementState.running;
            Lives = 5;
            jumpceiling1 = 200;
            jumpceiling2 = 0;
        }

        //Events are registered in MainWindow.xaml.cs
        public void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (this.State != movementState.jumping && this.State != movementState.doublejump && this.State != movementState.decending && this.State != movementState.decending2)
                {
                    this.State = movementState.jumping;
                }
                else if (this.State == movementState.doublejump || this.State == movementState.decending2)
                {
                    return;
                }

                else if (this.State == movementState.jumping && this.State != movementState.doublejump && this.State != movementState.decending2 || this.State == movementState.decending )
                {
                    jumpceiling2 = this.PosY + 100;
                    this.State = movementState.doublejump;
                }

            }
        }

        public void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                //isJumping = false;
            }
        }

        public string Serialize()
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(string data)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePosition()
        {

            if (this.PosY == jumpceiling1 && this.State != movementState.doublejump || this.State != movementState.doublejump && this.State != movementState.running && this.State != movementState.jumping && this.State != movementState.decending2) // need to check not running so doesn't decend on game start
            {
                this.State = movementState.decending;
            }
            if (this.State == movementState.jumping)
            {
                this.PosY += 10;
            }
            if (this.State == movementState.doublejump)
            {
                this.PosY += 10;
            }
            if (this.State == movementState.decending && this.PosY != 60)
            {
                this.PosY -= 10;
            }
            if (this.State == movementState.decending && this.PosY <= 60)
            {
                this.PosY = 50;
                this.State = movementState.running;
            }
            if (this.State == movementState.doublejump && this.PosY == jumpceiling2)
            {
                this.State = movementState.decending2;
            }
            if (this.State == movementState.decending2 && this.PosY != 60)
            {
                this.PosY -= 10;
            }
            if (this.State == movementState.decending2 && this.PosY <= 60)
            {
                this.PosY = 50;
                this.State = movementState.running;
            }
        }
    }
}