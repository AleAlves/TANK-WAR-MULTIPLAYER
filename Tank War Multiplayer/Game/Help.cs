
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
    public class Help
    {
        Image intro;
        Image control;
        SpriteBatch spriteBatch;
        KeyboardState keyboard;
        SoundEffect click;
        SoundEffectInstance clickInstance;

        public Help(ContentManager Content)
        {
            Content.RootDirectory = "Content";
            keyboard = new KeyboardState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(GraphicsDevice graphic, ContentManager Content, int w, int h)
        {
            GD.instance = graphic;
            spriteBatch = new SpriteBatch(graphic);
            // TODO: use this.Content to load your game content here
            intro = new Image(Content, "Design/fundoDefault", 0, 0, w, h);
            control = new Image(Content, "Design/Control", w/2 - (w - w / 100 * 10)/2, h / 100 * 20, w - w / 100 * 10, h - h / 100 * 20);
            click = Content.Load<SoundEffect>("Sound/clickSom");
            clickInstance = click.CreateInstance();
            clickInstance.IsLooped = false;
        }

        float delay = 0;
        public bool Update(GameTime gameTime)
        {
            delay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
            {
                if (delay > 0.5)
                {
                    if (clickInstance.State != SoundState.Playing)
                        clickInstance.Play();
                    delay = 0;
                    return true;
                }
                return false;
            }
            else
                return false;

            // TODO: Add your update logic here
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code 
            spriteBatch.Begin();
            intro.Draw(spriteBatch);
            control.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

