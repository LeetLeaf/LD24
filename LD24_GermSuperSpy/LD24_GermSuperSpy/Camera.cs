using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LD24_GermSuperSpy
{
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 center;

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public void Update(GameTime gameTime, Player target, Rectangle window)
        {
            center = new Vector2(target.Position.X + (target.FrameSize.X / 2 - window.Width / 2),
                target.Position.Y + (target.FrameSize.Y / 2 - window.Height / 2));
            if (center.X > window.Width /4)
                center.X = window.Width / 4;
            if (center.X < -(window.Width / 4))
                center.X = -(window.Width / 4);
            if (center.Y < -(window.Height / 4))
                center.Y = -(window.Height / 4);
            if (center.Y > window.Height / 4)
                center.Y = window.Height / 4;
            transform = Matrix.CreateScale(new Vector3(2, 2, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X * 2 - window.Width / 2, -center.Y * 2 - window.Height / 2, 0));
           
        }
    }
}
