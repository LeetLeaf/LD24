using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24_GermSuperSpy
{
    public class WinScreen
    {
        public bool display;


        public WinScreen()
        {
            display = false;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch,int num)
        {
            if (num == 3)
            {
                spriteBatch.DrawString(Game1.terminalFont,"You WIN! Thanks You So Much For Playing!!", Vector2.Zero, Color.White);
            }
            else
            spriteBatch.DrawString(Game1.terminalFont, "Mission " + num + " Complete!!", Vector2.Zero, Color.White);
            
        }

    }
}
