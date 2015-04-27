using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD24_GermSuperSpy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static SpriteFont terminalFont;
        public static Dictionary<string, Texture2D> Textures {get; set;}
        public static Rectangle window;
        public static World map;
        public static Player player;
        public static Camera camera;
        public static Menu mainMenu;
        public static Mission currentMission;
        
        private bool countDown = false;
        private int tempTime = 300;

        WinScreen winScreen;

        public static Mission mission1;
        public static Mission mission2;
        public static Mission mission3;

        bool winBool = false;
        int winNum = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 256;
            graphics.PreferredBackBufferHeight = 256;
            graphics.PreferMultiSampling = false;
            graphics.IsFullScreen = true;
        }
        static Game1()
        {
            Textures = new Dictionary<string, Texture2D>();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);
            window = Window.ClientBounds;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            terminalFont = Content.Load<SpriteFont>(@"Font\Terminal");
            mainMenu = new Menu();
            Textures.Add("SpyGuy", Content.Load<Texture2D>(@"Character\SpyGuy"));
            Textures.Add("BarGuy", Content.Load<Texture2D>(@"Character\BarGuy"));
            Textures.Add("ClassyGuy", Content.Load<Texture2D>(@"Character\ClassyGuy"));
            Textures.Add("MainGuy", Content.Load<Texture2D>(@"Character\MainGuy"));
            Textures.Add("MustacheGuy", Content.Load<Texture2D>(@"Character\MustacheGuy"));
            Textures.Add("Regular1", Content.Load<Texture2D>(@"Character\Regular1"));
            Textures.Add("Regular2", Content.Load<Texture2D>(@"Character\Regular2"));
            Textures.Add("Regular3", Content.Load<Texture2D>(@"Character\Regular3"));
            Textures.Add("SuitGuy", Content.Load<Texture2D>(@"Character\SuitGuy"));

            Textures.Add("TextBox", Content.Load<Texture2D>(@"TextBox"));
            Textures.Add("The Room", Content.Load<Texture2D>(@"The Room"));

            player = new Player();
            map = new World();
            mission1 = new Mission("  The target is well dressed and enjoys pool but\n  also loves the bar. If you absorb him you gain his power.",
                20, 3,  new Germ(Textures["SuitGuy"], "Target",new Vector2(60,180)));
            mission2 = new Mission("  Mr.TallGuy is world famous for making amazing \n  drinks. He is your target use \"C\" to get past other germs.",
                40, 10, new Germ(Textures["ClassyGuy"], "Target", new Vector2(230, 48)));
            mission3 = new Mission("  Just a Regular Guy he loves games. The Protectors\n  are going to be watching. Try \"Z\" to throw them off.",
                30, 20, new Germ(Textures["Regular1"], "Target", new Vector2(230, 230)));
            currentMission = mission1;

            winScreen =new WinScreen();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if(winScreen.display)
            {
                if (winBool)
                {
                    System.Threading.Thread.Sleep(400);
                    winBool = false;
                }
                if(Keyboard.GetState().IsKeyDown(Keys.Enter)&&currentMission.Equals(mission3)&&currentMission.Finished)
                {
                    System.Threading.Thread.Sleep(400);
                    this.Exit();
                }
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    winScreen.display = false;
                    System.Threading.Thread.Sleep(80);
                   
                }

            }
            else
            {
                if (!mainMenu.closed)
                {
                    mainMenu.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        this.Exit();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        System.Threading.Thread.Sleep(200);
                        mainMenu.closed = true;
                        countDown = false;
                    }

                }
                else
                {

                    //Game Stuff Begin
                    camera.Update(gameTime, player, Window.ClientBounds);

                    player.Update(gameTime);
                    currentMission.Update(gameTime);
                    if (currentMission.Finished)
                    {
                        winScreen.display = true;
                        winBool = true;
                        player.Position = new Vector2(10, 27);
                        if (currentMission.Equals(mission1))
                        {
                            winNum = 1;
                            currentMission = mission2;
                        }
                        if (currentMission.Equals(mission2) && currentMission.Finished)
                        {
                            winNum = 2;
                            currentMission = mission3;
                            currentMission.Finished = false;
                        }
                        if (currentMission.Equals(mission3) && currentMission.Finished)
                        {
                            winNum = 3;
                        }
                        currentMission.Ready = false;
                    }



                    //Game Stuff End

                    //Menu Stuff Start
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {

                        countDown = true;
                    }
                    if (countDown)
                    {
                        tempTime -= gameTime.ElapsedGameTime.Milliseconds;
                        if (tempTime < 0)
                            mainMenu.closed = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        countDown = true;
                    }
                    //Menu Stuff End
                }
                
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            if(winScreen.display)
            {

                spriteBatch.Begin(SpriteSortMode.FrontToBack,
                    BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
                winScreen.Draw(spriteBatch, winNum);
            }
            else
            {
                //Begin Menu
                if (!mainMenu.closed)
                {

                    spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                    mainMenu.Draw(spriteBatch);
                }
                //End Menu

                //Begin Game
            
                if (mainMenu.closed)
                {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                    SamplerState.PointClamp, null, null, null, camera.transform);

                    map.Draw(spriteBatch);

                    currentMission.Draw(spriteBatch);

                    player.Draw(spriteBatch);
                
            }
         

            }
            //End Game
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
