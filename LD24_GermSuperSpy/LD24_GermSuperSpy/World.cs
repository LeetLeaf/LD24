using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24_GermSuperSpy
{
    public class World
    {
        private Texture2D texture;

        public List<Rectangle> collisionSpots;

        public World()
        {
            texture = Game1.Textures["The Room"];

            collisionSpots = new List<Rectangle>();

            collisionSpots.Add(new Rectangle(0, 0, 256, 26));
            collisionSpots.Add(new Rectangle(42, 125, 86 - 42, 175 - 125));
            collisionSpots.Add(new Rectangle(133, 15, 148 - 133, 100 - 23));
            collisionSpots.Add(new Rectangle(133, 77, 240 - 133, 98 - 82));
            collisionSpots.Add(new Rectangle(164, 163, 236 - 164, 194 - 168));

            //top Wall Collision 0,0,256,26
            //pool table 37,125,82,175
            //bar top 131,15,148,100
            //bar botttom 131,79,240,98
            //pinballs 164,163,236,194
        }

        public void Update(GameTime gameTime)
        { 
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
        
    }
}
