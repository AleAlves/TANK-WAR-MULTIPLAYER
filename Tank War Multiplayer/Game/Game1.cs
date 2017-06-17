using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using gameutil2d.classes;
using gameutil2d.classes.basic;
using System.IO;
using System.Reflection;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Tank_War_Multiplayer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    enum GameState
    {
        Intro,
        Creditos,
        Ajuda,
        Menu,
        JogoOnline,
        JogoOffline,
        Fim,
        win,
        lose
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameTime gameTime;
        Jogo jogoOnline;
        Jogo jogoOffline;
        Intro intro;
        Menu menu;
        About about;
        Help help;
        Image loading;
        Win win;
        Lose lose;
        MouseEventArgs mouse;
        Cursor state;
        int width = 0, height = 0;
        private int load = 0, id = 0;
        private bool onLine = false;
        GameState _state;

        public Game1()
        {
            _state = GameState.Intro;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = false;
            height = 690;
            width = 800;
            graphics.PreferredBackBufferHeight = 690;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
            intro = new Intro(Content);
            menu = new Menu(Content);
            about = new About(Content);
            help = new Help(Content);
            win = new Win(Content);
            lose = new Lose(Content);
            FileStream fs;
            StreamWriter sr;
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\config.txt"))
            {
                fs = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\config.txt", FileMode.OpenOrCreate);
                sr = new StreamWriter(fs);
                sr.WriteLine("http://nodeserver-pipeofwargameserver.44fs.preview.openshiftapps.com/");
                sr.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            intro.Initialize();
            menu.Initialize();
            about.Initialize();
            help.Initialize();
            win.Initialize();
            lose.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            GD.instance = GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            intro.LoadContent(GD.instance, Content, width, height);
            menu.LoadContent(GD.instance, Content, width, height);
            about.LoadContent(GD.instance, Content, width, height);
            help.LoadContent(GD.instance, Content, width, height);
            win.LoadContent(GD.instance, Content, width, height);
            lose.LoadContent(GD.instance, Content, width, height);
            loading = new Image(Content, "Design/Loading", 0, 0, width, height);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        /// 
        private void NewGame()
        {
            jogoOnline = new Jogo(Content, width, height, id);
            jogoOnline.Initialize();
            jogoOnline.LoadContent(GD.instance, spriteBatch);
            _state = GameState.JogoOnline;
            load = 0;
        }
        protected override void Update(GameTime gameTime)
        {

            MouseState state = new MouseState();
            state = Mouse.GetState();
            if (load == 2)
            {
                jogoOnline = new Jogo(Content, width, height, id);
                jogoOnline.Initialize();
                jogoOnline.LoadContent(GD.instance, spriteBatch);
                _state = GameState.JogoOnline;
                load = 0;
            }
            else
                if (load == 3)
                {
                    jogoOffline = new Jogo(Content, width, height, id);
                    jogoOffline.Initialize();
                    jogoOffline.LoadContent(GD.instance, spriteBatch);
                    _state = GameState.JogoOffline;
                    load = 0;
                }
            switch (_state)
            {
                case GameState.Intro:
                    switch (intro.Update(gameTime))
                    {
                        case true:
                            _state = GameState.Menu;
                            break;
                    }
                    break;

                case GameState.Creditos:
                    switch (about.Update(gameTime))
                    {
                        case true:
                            _state = GameState.Menu;
                            break;
                    }
                    break;

                case GameState.Ajuda:
                    switch (help.Update(gameTime))
                    {
                        case true:
                            _state = GameState.Menu;
                            break;
                    }
                    break;

                case GameState.Menu:
                    switch (menu.Update(gameTime, state))
                    {
                        case 1:
                            load = 1;
                            onLine = true;
                            id = 1;
                            break;
                        case 2:
                            load = 1;
                            onLine = true;
                            id = 2;
                            break;
                        case 3:
                            _state = GameState.Ajuda;
                            break;
                        case 4:
                            _state = GameState.Creditos;
                            break;
                        case 5:
                            this.Exit();
                            break;
                        case 6:
                            load = 1;
                            onLine = false;
                            break;
                        case 7:
                            _state = GameState.Intro;
                            break;
                        default:
                            break;
                    }
                    break;
                case GameState.JogoOnline:
                    switch (jogoOnline.Update(gameTime, state))
                    {
                        case 1:
                            _state = GameState.win;
                            break;
                        case 2:
                            _state = GameState.lose;
                            break;
                        case 3:
                            jogoOnline.Gameover = 1;
                            _state = GameState.Menu;
                            break;
                    }
                    break;
                case GameState.JogoOffline:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        this.Exit();
                    }
                    switch (jogoOffline.Update(gameTime, state))
                    {
                        case 1:
                            _state = GameState.win;
                            break;
                        case 2:
                            _state = GameState.lose;
                            break;
                    }
                    break;
                case GameState.win:
                    if (win.Update(gameTime))
                    {
                        try
                        {
                            jogoOnline.Gameover = 1;
                            _state = GameState.Menu;
                        }
                        catch { }
                    }
                    break;
                case GameState.lose:
                    if (lose.Update(gameTime))
                    {
                        try
                        {
                            jogoOnline.Gameover = 1;
                            _state = GameState.Menu;
                        }
                        catch { }
                    }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            switch (_state)
            {
                case GameState.Intro:
                    intro.Draw(gameTime);
                    break;
                case GameState.Menu:
                    menu.Draw(gameTime);
                    break;
                case GameState.JogoOnline:
                    jogoOnline.Draw(gameTime);
                    break;
                case GameState.JogoOffline:
                    jogoOnline.Draw(gameTime);
                    break;
                case GameState.Creditos:
                    about.Draw(gameTime);
                    break;
                case GameState.Ajuda:
                    help.Draw(gameTime);
                    break;
                case GameState.win:
                    win.Draw(gameTime);
                    break;
                case GameState.lose:
                    lose.Draw(gameTime);
                    break;
            }
            if (load == 1)
            {
                if (onLine)
                    load = 2;
                else
                    load = 3;
                spriteBatch.Begin();
                loading.Draw(spriteBatch);
                spriteBatch.End();
            }

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
