
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
    public class Intro
    {
        Image intro;
        Image title;
        Image logo;
        Image torreA;
        Image corpoA;
        AnimationSprites esteiraA;
        Image torreB;
        Image corpoB;
        AnimationSprites esteiraB;
        SpriteBatch spriteBatch;
        KeyboardState keyboard;
        SoundEffect click;
        SoundEffectInstance clickInstance;
        int ax = 0;
        int ay = 0;
        int axB = 0;
        int ayB = 0;
        int w = 0;
        int h = 0;
        bool direita = true;
        bool esquerda = false;
        bool direitaB = true;
        bool esquerdaB = false;
        float angulo = 1.571f;
        float anguloB = 1.571f;

        public Intro(ContentManager Content)
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
            this.w = w;
            this.h = h;
            GD.instance = graphic;
            spriteBatch = new SpriteBatch(graphic);
            // TODO: use this.Content to load your game content here
            intro = new Image(Content, "Design/fundoDefault", 0, 0, w, h);
            title = new Image(Content, "Design/Title", (w) - (w / 100 * 80), 100, w / 100 * 60, h / 100 * 20);
            logo = new Image(Content, "Design/Pipe", (w/2) - (w / 100 * 35)/2, h/2 - (h / 100 * 30)/2, w / 100 * 40, h / 100 * 35);
            click = Content.Load<SoundEffect>("Sound/clickSom");
            corpoA = new Image(Content, "corpo" + 1, w / 2, h / 2, 50, 50);
            torreA = new Image(Content, "torre" + 1, corpoA.GetX(), corpoA.GetY(), 100, 100);
            esteiraA = new AnimationSprites(Content, corpoA.GetX(), corpoA.GetY(), 100, 100);
            esteiraA.Add("esteira1");
            esteiraA.Add("esteira2");
            esteiraA.Start(2, true);
            corpoB = new Image(Content, "corpo" + 2, w / 2, h / 2, 50, 50);
            torreB = new Image(Content, "torre" + 2, corpoB.GetX(), corpoB.GetY(), 100, 100);
            esteiraB = new AnimationSprites(Content, corpoB.GetX(), corpoB.GetY(), 100, 100);
            esteiraB.Add("esteira1");
            esteiraB.Add("esteira2");
            esteiraB.Start(2, true);
            clickInstance = click.CreateInstance();
            clickInstance.IsLooped = false;
            ax = w - w / 100 * 20;
            ay = h - h / 100 * 10;
            axB = w / 100 * 20;
            ayB = h - h / 100 * 20;
        }

        float delay = 0;

        public bool Update(GameTime gameTime)
        {

            keyboard = Keyboard.GetState();
            delay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ax <= w - w / 100 * 20 && direita == true)
            {
                ax += 2;
            }
            else
            {
                esquerda = true;
                direita = false;
                angulo = 4.712f;
                if (ax >= w / 100 * 20 && esquerda == true)
                {
                    ax -= 2;
                }
                else { direita = true; angulo = 1.571f; }

            }

            if (axB <= w - w / 100 * 20 && direitaB == true)
            {
                axB += 2;
            }
            else
            {
                esquerdaB = true;
                direitaB = false;
                anguloB = 4.712f;
                if (axB >= w / 100 * 20 && esquerdaB == true)
                {
                    axB -= 2;
                }
                else { direitaB = true; anguloB = 1.571f; }

            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
            {
                if (delay > 0.5)
                {
                    if (clickInstance.State != SoundState.Playing)
                        clickInstance.Play();
                    delay = 0;
                    return true;
                }
                else
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
            title.Draw(spriteBatch,false,1);
            logo.Draw(spriteBatch);
            esteiraA.Draw(spriteBatch, angulo, ax, ay);
            corpoA.Draw(spriteBatch, angulo, ax, ay, 0.2f);
            torreA.Draw(spriteBatch, angulo, ax, ay, 0.25f);
            esteiraB.Draw(spriteBatch, angulo, axB, ayB);
            corpoB.Draw(spriteBatch, anguloB, axB, ayB, 0.2f);
            torreB.Draw(spriteBatch, anguloB, axB, ayB, 0.25f);
            spriteBatch.End();
        }
    }
}

