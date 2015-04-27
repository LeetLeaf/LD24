using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD24_GermSuperSpy
{
    public class Mission
    {
        public List<Germ> Germs;
        private Random random;
        private string targetDescript;
        public List<Germ> Protectors;
        public bool Ready;
        public bool Finished;


        private int timeTillNext;
        private int currentStep;
        private int resetTime;

        private int timeForTyper;
        private bool blink;
        private string typer;
        private bool countDown;
        private int tempTime;
        

        private string displayText = "";

        public Mission(string targetDescript, int numberRandoms, int protectors, Germ target)
        {

            Germs = new List<Germ>();
            random = new Random();
            Ready = false;
            Finished = false;


            currentStep = 0;
            resetTime = 30;
            timeTillNext = resetTime;
            timeForTyper = 300;
            countDown = false;
            tempTime = 300;

            this.targetDescript = targetDescript;
            Germs.Add(target);
            System.Threading.Thread.Sleep(100);
            int germType = 0;
            for (int i = 0; i < numberRandoms; i++)
            {
                germType = random.Next(0, 6);

                switch (germType)
                { 
                    case 0:
                        Germs.Add(new Germ(Game1.Textures["BarGuy"], "BarGuy"));
                        break;
                    case 1:
                        Germs.Add(new Germ(Game1.Textures["ClassyGuy"], "ClassyGuy"));
                        break;
                    case 2:
                        Germs.Add(new Germ(Game1.Textures["MustacheGuy"], "MustacheGuy"));
                        break;
                    case 3:
                        Germs.Add(new Germ(Game1.Textures["SuitGuy"], "SuitGuy"));
                        break;
                    case 4:
                        Germs.Add(new Germ(Game1.Textures["Regular1"], "Regular"));
                        break;
                    case 5:
                        Germs.Add(new Germ(Game1.Textures["Regular2"], "Regular"));
                        break;
                    case 6:
                        Germs.Add(new Germ(Game1.Textures["Regular3"], "Regular"));
                        break;
                     
                }
                System.Threading.Thread.Sleep(80);
            }

                Protectors = new List<Germ>();
                for (int i = 0; i < protectors; i++)
                { 
                    Protectors.Add(new Germ(Game1.Textures["SuitGuy"],"Protectors"));
                    System.Threading.Thread.Sleep(30);
                }
        }

        

        public void Update(GameTime gameTime)
        {
            if (Ready)
            {
                for (int i = 0; i < Germs.Count; i++)
                {
                    Germs[i].Update(gameTime);
                }
                for (int i = 0; i < Protectors.Count; i++)
                {
                    Protectors[i].Update(gameTime);
                }
            }
            if (!Ready)
            {
                if (currentStep <= targetDescript.Length)
                {
                    displayText = targetDescript.Substring(0, currentStep);
                    if (timeTillNext > 0)
                    {
                        timeTillNext -= gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else
                    {
                        currentStep++;
                        timeTillNext = resetTime;
                    }
                }
                if (blink)
                {
                    timeForTyper -= gameTime.ElapsedGameTime.Milliseconds;
                    typer = "_";
                    if (timeForTyper < 0)
                        blink = false;
                }
                else
                {
                    timeForTyper += gameTime.ElapsedGameTime.Milliseconds;
                    typer = "";
                    if (timeForTyper > 300)
                        blink = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {

                    countDown = true;
                }
                if (countDown)
                {
                    tempTime -= gameTime.ElapsedGameTime.Milliseconds;
                    if (tempTime < 0)
                        Ready = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Ready)
            {
                for (int i = 0; i < Germs.Count; i++)
                {
                    Germs[i].Draw(spriteBatch);
                }
                for (int i = 0; i < Protectors.Count; i++)
                {
                    Protectors[i].Draw(spriteBatch);
                }
            }
            else
            {

                spriteBatch.Draw(Game1.Textures["TextBox"], Vector2.Zero, Color.White);
                spriteBatch.DrawString(Game1.terminalFont, displayText + typer,
                    new Vector2(Game1.camera.transform.Left.X,Game1.camera.transform.Down.Y)
                    ,Color.White,0.0f,Vector2.Zero,0.5f,SpriteEffects.None,0);
            }
        }
    }
}
