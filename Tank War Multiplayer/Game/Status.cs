using gameutil2d.classes.basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War_Multiplayer
{
    class Status
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        private Image statusTable; 
        private Image barraVida;
        private Image barraVidaFixa;
        int width = 0;
        int heigth = 0;
        int municao = 0;
        int minas = 0;
        SpriteFont municaoFont;
        SpriteFont minasFont;
        SpriteFont situacaoFont;
        private string situacao = "";

        public Image BarraVida
        {
            get
            {
                return barraVida;
            }

            set
            {
                barraVida = value;
            }
        }

        public string Situacao
        {
            get
            {
                return situacao;
            }

            set
            {
                situacao = value;
            }
        }


        public Status(ContentManager content, int width, int height)
        {
            this.width = width;
            this.heigth = height;
            this.content = content;
        }

        public void Initialize()
        {

        }

        public void LoadContent(GraphicsDevice graphic, SpriteBatch spriteBatch)
        { 
            this.spriteBatch = spriteBatch;
            GD.instance = graphic;
            statusTable = new Image(content, "Menu/statusBar", 0, heigth - 70, 800, 70);
            barraVida = new Image(content, "Menu/barra", 100, heigth-45,100,15);
            barraVidaFixa = new Image(content, "Menu/barraFixa", 100, heigth - 45, 100, 15);
            municaoFont = content.Load<SpriteFont>("Municao");
            situacaoFont = content.Load<SpriteFont>("status");
            minasFont = content.Load<SpriteFont>("Municao");
        }

        public void UnloadContent()
        {

        }

        public void Update(int municao, int minas, int vida,GameTime gameTime)
        {
            this.municao = municao;
            this.minas = minas;
        }

        public void Draw(GameTime gameTime, DateTime date)
        {
            statusTable.Draw(spriteBatch);
            barraVidaFixa.Draw(spriteBatch);
            barraVida.Draw(spriteBatch);
            spriteBatch.DrawString(municaoFont, municao.ToString(), new Vector2(470, heigth - 45), Color.Black);
            spriteBatch.DrawString(municaoFont, minas.ToString(), new Vector2(305, heigth - 45), Color.Black);
            spriteBatch.DrawString(situacaoFont,"Report:"+date.Day+"/"+date.Month+"/1945 "+date.Hour+":"+date.Minute, new Vector2(610, heigth - 55), Color.Black);
            spriteBatch.DrawString(situacaoFont, situacao, new Vector2(570, heigth - 40), Color.Black);
        }
    }
}
