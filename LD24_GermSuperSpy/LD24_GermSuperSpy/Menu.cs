using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24_GermSuperSpy
{
    public class Menu
    {

        private int timeTillNext;
        private int currentStep;
        private int resetTime;
        private static string mainText = "Germ Super Spy \n\n   Start: Press Enter\n\n   Exit: Press Escape";
        private string displayText;

        private int timeForTyper;
        private bool blink;
        private string typer;

        public bool closed { get; set; }

        public Menu()
        {
            currentStep = 0;
            resetTime = 60;
            timeTillNext = resetTime;
            timeForTyper = 300;
            closed = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!closed)
            {
                if (currentStep <= mainText.Length)
                {
                    displayText = mainText.Substring(0, currentStep);
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
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!closed)
            {
                spriteBatch.DrawString(Game1.terminalFont, displayText + typer, Vector2.Zero, Color.White);
            }
        }
    }
}
