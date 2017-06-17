using gameutil2d.classes.basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Tank_War_Multiplayer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Menu
    {
        Image menu;
        Image sair;
        SpriteBatch spriteBatch;
        Image allies;
        Image axis;
        Image help;
        Image about;
        Image title;
        Image logo;
        SoundEffect click;
        SoundEffectInstance clickInstance;
        Image seta;

        private int heigth = 0;
        private bool cursorFlagA = false;
        private bool cursorFlagB = false;
        private bool cursorAbout = false;
        private bool cursorHelp = false;
        private bool cursorExit = false;

        public Menu(ContentManager Content)
        {
            Content.RootDirectory = "Content";

        }

        public void Initialize()
        {

        }

        public void LoadContent(GraphicsDevice graphic, ContentManager Content, int w , int h)
        {
            this.heigth = h;
            GD.instance = graphic;
            spriteBatch = new SpriteBatch(graphic);
            menu = new Image(Content, "Design/fundoDefault", 0, 0, w, h);
            allies = new Image(Content, "Screen/Menu/buttonAllies", 200, 500, 140, 90);
            axis = new Image(Content, "Screen/Menu/buttonAxis", 380, 500, 140, 90);
            help = new Image(Content, "Screen/Menu/Help", 560, 500, 100, 25);
            about = new Image(Content, "Screen/Menu/About", 560, 535, 100, 25);
            sair = new Image(Content, "Screen/Menu/Exit", 560, 570, 100, 25);
            title = new Image(Content, "Design/Title", (w) - (w / 100 * 80), 100, w / 100 * 60, h / 100 * 20);
            logo = new Image(Content, "Design/Pipe", (w / 2) - (w / 100 * 35) / 2, h / 2 - (h / 100 * 30) / 2, w / 100 * 40, h / 100 * 35);
            seta = new Image(Content, "Screen/Menu/seta", 500, 450, 20, 20);
            click = Content.Load<SoundEffect>("Sound/clickSom");
            clickInstance = click.CreateInstance();
            clickInstance.IsLooped = false;
        }

        float delay = 0;

        public int Update(GameTime gameTime, MouseState state)
        {
            cursorFlagA = false;
            cursorFlagB = false;
            cursorAbout = false;
            cursorExit = false;
            cursorHelp = false;

            KeyboardState teclado = Keyboard.GetState();
            seta.SetX(state.Position.X);
            seta.SetY(state.Position.Y);

            delay += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (delay > 0.5)
            {

                if (Collision.Check(seta, allies) || teclado.IsKeyDown(Keys.B))
                {
                    cursorFlagA = true;
                    if (state.LeftButton == ButtonState.Pressed || teclado.IsKeyDown(Keys.B))
                    {
                        Console.WriteLine("Brasil");
                        if (clickInstance.State != SoundState.Playing)
                            clickInstance.Play();
                        delay = 0;
                        return 1;
                    }
                }
                if (Collision.Check(seta, axis) || teclado.IsKeyDown(Keys.I))
                {
                    cursorFlagB = true;
                    if (state.LeftButton == ButtonState.Pressed || teclado.IsKeyDown(Keys.I))
                    {
                        Console.WriteLine("Italia");
                        if (clickInstance.State != SoundState.Playing)
                            clickInstance.Play();
                        delay = 0;
                        return 2;
                    }
                }
                if (Collision.Check(seta, help) || teclado.IsKeyDown(Keys.H))
                {
                    cursorHelp = true;
                    if (state.LeftButton == ButtonState.Pressed || teclado.IsKeyDown(Keys.H))
                    {
                        if (clickInstance.State != SoundState.Playing)
                            clickInstance.Play();
                        delay = 0;
                        return 3;
                    }
                }
                if (Collision.Check(seta, about) || teclado.IsKeyDown(Keys.A))
                {
                    cursorAbout = true;
                    if (state.LeftButton == ButtonState.Pressed || teclado.IsKeyDown(Keys.A))
                    {
                        if (clickInstance.State != SoundState.Playing)
                            clickInstance.Play();
                        delay = 0;
                        return 4;
                    }
                }
                if (Collision.Check(seta, sair) || teclado.IsKeyDown(Keys.E))
                {
                    cursorExit = true;
                    if (state.LeftButton == ButtonState.Pressed || teclado.IsKeyDown(Keys.E))
                    {
                        if (clickInstance.State != SoundState.Playing)
                            clickInstance.Play();
                        delay = 0;
                        return 5;
                    }
                }
                if (teclado.IsKeyDown(Keys.O))
                {
                    if (clickInstance.State != SoundState.Playing)
                        clickInstance.Play();
                    delay = 0;
                    return 6;
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || teclado.IsKeyDown(Keys.Escape))
                {
                    if (clickInstance.State != SoundState.Playing)
                        clickInstance.Play();
                    delay = 0;
                    return 7;
                }
            }
            return 0;
        }




        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code 
            spriteBatch.Begin();
            menu.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            allies.Draw(spriteBatch, cursorFlagA, 1);
            axis.Draw(spriteBatch, cursorFlagB, 1);
            help.Draw(spriteBatch,cursorHelp,1);
            about.Draw(spriteBatch, cursorAbout, 1);
            sair.Draw(spriteBatch, cursorExit, 1);
            title.Draw(spriteBatch,false,1);
            logo.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            seta.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
