﻿using System.Windows.Input;

namespace Symphony_Sprint.Game_Model.World_Objects
{
    public class Player
    {

        public enum movementState { running, jumping, doublejump }
        public movementState State { get; set; }
        //public bool isJumping;

        public string ImgPath { get; set; }
        //public int Height { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }


        public Player(string img)
        {
            ImgPath = img;
            State = movementState.running;
            //Height = 0;
        }

        //Events are registered in MainWindow.xaml.cs
        public void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (this.State == movementState.doublejump)
                {
                    return;
                }
                else
                {
                    if (this.State == movementState.jumping)
                    {
                        this.State = movementState.doublejump;
                    }
                    Jump();
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

        public void Jump()
        {
            this.State = movementState.jumping;

            int currentY = this.PosY; // say 10
            int maxY = currentY + 30; // 30 = jump max height

            while (currentY < maxY) // make sure doesn't go over max
            {
                currentY += 1;
            }

            while (currentY > 10) // 10 = base coordinate on piano keys
            {
                currentY -= 1;
            }
            if (currentY == 10)
            {
                this.State = movementState.running;
            }       
        }       
    }
}