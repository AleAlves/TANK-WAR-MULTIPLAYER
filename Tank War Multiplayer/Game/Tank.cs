using gameutil2d.classes.basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tank_War_Multiplayer
{
    class Tank
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        KeyboardState tecaldo;
        SoundEffect esteiraSound;
        SoundEffectInstance esteiraInstance;
        SoundEffect tiroSound;
        List<SoundEffectInstance> tiroInstance;
        ParticleEngine particleEngine;
        private Camera camera;
        private Image torre;
        private AnimationSprites esteira;
        private Image rastroMarca;
        private Image balas;
        private float angulo = 0.0F;
        private float anguloA = 0.0F;
        private float anguloB = 0.0F;
        private float anguloC = 0.0F;
        private int ax = 628;
        private int ay = 530;
        private int balaAx = -111;
        private int balaAy = -111;
        private int explosionAx = -1000;
        private int explosionAy = -1000;
        private int velocidade = 1;
        private int posicao = 1;
        private int movimento = 1;
        private int jogadorID = 0;
        private int screenWidth = 0;
        private int screenHeigth = 0;
        private int tiro = 1;
        private int anguloTiro = 1;
        private float anguloDisparo = 0.0F;
        private int disparo = 0;
        private bool carga = true;
        private int cargaPlantada = 0;
        private int width = 40;
        private int height = 40;
        private int municao = 10;
        private int minas = 1;
        private int vida = 100;
        private Image corpo;
        private Image minaTerrestre;
        private int acionarMina = 0;
        float timeSinceLastShot = 0f;
        float timeUntilRestart = 0f;
        SpriteFont timer;
        private int timerCount = 300;
        int timerOn = 0;
        private float waitTime = 5f;
        private int bulletScout = 0;
        private bool balaOponenteMove = false;
        int parOuImpar = 0;
        SpriteFont holdFlag;

        public int Ax
        {
            get
            {
                return ax;
            }

            set
            {
                ax = value;
            }
        }

        public int Ay
        {
            get
            {
                return ay;
            }

            set
            {
                ay = value;
            }
        }

        public float Angulo
        {
            get
            {
                return angulo;
            }

            set
            {
                angulo = value;
            }
        }

        public int Velocidade
        {
            get
            {
                return velocidade;
            }

            set
            {
                velocidade = value;
            }
        }

        public int Posicao
        {
            get
            {
                return posicao;
            }

            set
            {
                posicao = value;
            }
        }


        public Image Balas
        {
            get
            {
                return balas;
            }

            set
            {
                balas = value;
            }
        }

        public Image Corpo
        {
            get
            {
                return corpo;
            }

            set
            {
                corpo = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public int Municao
        {
            get
            {
                return municao;
            }

            set
            {
                municao = value;
            }
        }

        public int Vida
        {
            get
            {
                return vida;
            }

            set
            {
                vida = value;
            }
        }

        public int JogadorID
        {
            get
            {
                return jogadorID;
            }

            set
            {
                jogadorID = value;
            }
        }

        public int Disparo
        {
            get
            {
                return disparo;
            }

            set
            {
                disparo = value;
            }
        }

        public int AnguloTiro
        {
            get
            {
                return anguloTiro;
            }

            set
            {
                anguloTiro = value;
            }
        }

        public int Movimento
        {
            get
            {
                return movimento;
            }

            set
            {
                movimento = value;
            }
        }

        public float AnguloDisparo
        {
            get
            {
                return anguloDisparo;
            }

            set
            {
                anguloDisparo = value;
            }
        }

        public int Tiro
        {
            get
            {
                return tiro;
            }

            set
            {
                tiro = value;
            }
        }

        public int ExplosionAy
        {
            get
            {
                return explosionAy;
            }

            set
            {
                explosionAy = value;
            }
        }

        public int ExplosionAx
        {
            get
            {
                return explosionAx;
            }

            set
            {
                explosionAx = value;
            }
        }

        public int BalaAx
        {
            get
            {
                return balaAx;
            }

            set
            {
                balaAx = value;
            }
        }

        public int BalaAy
        {
            get
            {
                return balaAy;
            }

            set
            {
                balaAy = value;
            }
        }

        internal Image MinaTerrestre
        {
            get
            {
                return minaTerrestre;
            }

            set
            {
                minaTerrestre = value;
            }
        }

        public int AcionarMina
        {
            get
            {
                return acionarMina;
            }

            set
            {
                acionarMina = value;
            }
        }

        public int Minas
        {
            get
            {
                return minas;
            }

            set
            {
                minas = value;
            }
        }

        public float WaitTime
        {
            get
            {
                return waitTime;
            }

            set
            {
                waitTime = value;
            }
        }

        public int TimerCount
        {
            get
            {
                return timerCount;
            }

            set
            {
                timerCount = value;
            }
        }

        public ParticleEngine ParticleEngine
        {
            get
            {
                return particleEngine;
            }

            set
            {
                particleEngine = value;
            }
        }

        public Camera Camera
        {
            get
            {
                return camera;
            }

            set
            {
                camera = value;
            }
        }

        public int CargaPlantada
        {
            get
            {
                return cargaPlantada;
            }

            set
            {
                cargaPlantada = value;
            }
        }

        public int BulletScout
        {
            get
            {
                return bulletScout;
            }

            set
            {
                bulletScout = value;
            }
        }

        public bool BalaOponenteMove
        {
            get
            {
                return balaOponenteMove;
            }

            set
            {
                balaOponenteMove = value;
            }
        }

        public Tank(ContentManager content, int width, int height, int jogadorID)
        {
            this.screenWidth = width;
            this.screenHeigth = height;
            this.content = content;
            this.jogadorID = jogadorID;
            Position(jogadorID);
        }

        public void Initialize()
        {
            tiroInstance = new List<SoundEffectInstance>();
        }

        public void LoadContent(GraphicsDevice graphic, SpriteBatch spriteBatch)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            this.spriteBatch = spriteBatch;
            GD.instance = graphic;
            timer = content.Load<SpriteFont>("Timer");
            tiroSound = content.Load<SoundEffect>("sound/tiroSom");
            esteiraSound = content.Load<SoundEffect>("sound/esteiraSom");
            esteiraInstance = esteiraSound.CreateInstance();
            Corpo = new Image(content, "corpo" + jogadorID, ax, ay, width, height);
            torre = new Image(content, "torre" + jogadorID, ax, ay, 1, 1);
            minaTerrestre = new Image(content, "mina" + JogadorID, 1000, 1000, 20, 20);
            rastroMarca = new Image(content, "rastro", -1000, -1000, 0, 0);
            esteira = new AnimationSprites(content, ax, ay, 35, 35);
            esteira.Add("esteira1");
            esteira.Add("esteira2");
            balas = new Image(content, "bala", ax, ay, 1, 8);
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("trail"));
            particleEngine = new ParticleEngine(textures, new Vector2(Ax, Ay));
            holdFlag = content.Load<SpriteFont>("HoldFlag");
        }

        public void UnloadContent()
        {
        }


        private void Restart(GameTime gameTime, int id)
        {
            timeUntilRestart += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeUntilRestart >= waitTime)
            {
                vida = 100;
                municao = 10;
                timeUntilRestart = 0f;
                timerCount = 300;
                timerOn = 0;
                waitTime = 5f;
                Position(id);
            }
            else
                timerCount--;
        }

        private void Position(int id)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            if (id == 1)
            {
                switch (random.Next(1, 7))
                {
                    case 1:
                        ax = 75;
                        break;
                    case 2:
                        ax = 166;
                        break;
                    case 3:
                        ax = 257;
                        break;
                    case 4:
                        ax = 348;
                        break;
                    case 5:
                        ax = 439;
                        break;
                    case 6:
                        ax = 530;
                        break;
                    case 7:
                        ax = 621;
                        break;
                }
                ay = 560;
                Angulo = 0.0f;
            }
            else
            {
                if (id == 2)
                {
                    switch (random.Next(1, 7))
                    {
                        case 1:
                            ax = 257;
                            break;
                        case 2:
                            ax = 348;
                            break;
                        case 3:
                            ax = 439;
                            break;
                        case 4:
                            ax = 530;
                            break;
                        case 5:
                            ax = 621;
                            break;
                        case 6:
                            ax = 712;
                            break;
                        case 7:
                            ax = 803;
                            break;
                    }
                    ay = 10;
                    angulo = 3.1419f;
                }
            }
        }

        float zoom = 1.5f;

        public void Update(GameTime gameTime)
        {

            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tecaldo = Keyboard.GetState();

            if (vida == 0)
            {
                timerOn = 1;
                Restart(gameTime, jogadorID);
                anguloA = angulo + 0.01f;
                anguloB = angulo + 0.09f;
                anguloC = angulo + 0.05f;
            }
            else
            {
                anguloA = angulo;
                anguloB = angulo;
                anguloC = angulo;
            }

            if (disparo == 0)
            {
                balas.SetX(Ax);
                balas.SetY(Ay);
                anguloTiro = tiro;
                anguloDisparo = angulo;
            }

            if (municao > 0 && vida > 0)
            {
                if (carga == true && timeSinceLastShot > 1f)
                    if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.F))
                    {
                        timeSinceLastShot = 0;
                        tiroInstance.Add(tiroSound.CreateInstance());
                        tiroInstance.Last().Play();
                        disparo = 1;
                        carga = false;
                        anguloDisparo = angulo;
                        balas.SetX(ax);
                        balas.SetY(ay);
                        municao--;
                        anguloTiro = tiro;
                        GamePad.SetVibration(PlayerIndex.One, 0.1f, 0.1f);
                        bulletScout = 0;
                    }
                if (carga == true && timeSinceLastShot > 1f)
                    if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.E))
                    {
                        bulletScout = 1;
                        timeSinceLastShot = 0;
                        tiroInstance.Add(tiroSound.CreateInstance());
                        tiroInstance.Last().Play();
                        disparo = 1;
                        carga = false;
                        anguloDisparo = angulo;
                        balas.SetX(ax);
                        balas.SetY(ay);
                        municao--;
                        anguloTiro = tiro;
                        GamePad.SetVibration(PlayerIndex.One, 0.1f, 0.1f);
                    }
            }

            if (minas > 0 && vida > 0)
            {
                if (cargaPlantada == 0)
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.Space))
                    {
                        cargaPlantada = 1;
                        minas = 0;
                        minaTerrestre.SetX(Ax - minaTerrestre.GetWidth() / 2);
                        minaTerrestre.SetY(Ay - minaTerrestre.GetHeight() / 2);
                    }
            }

            if (disparo == 1)
            {
                particleEngine.Update(BalaAx, BalaAy, anguloTiro);
                switch (anguloTiro)
                {
                    case 1:
                        balas.MoveByY(-12);
                        break;
                    case 2:
                        balas.MoveByY(12);
                        break;
                    case 3:
                        balas.MoveByX(12);
                        break;
                    case 4:
                        balas.MoveByX(-12);
                        break;
                }

                Random random = new Random(Guid.NewGuid().GetHashCode());

                if (balas.GetX() <= -100 || balas.GetX() >= 1000 || balas.GetY() <= -100 || balas.GetY() >= 1000)
                {
                    GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
                    balas.SetX(-random.Next(100, 500));
                    balas.SetY(-random.Next(100, 500));
                    carga = true;
                    disparo = 0;
                    bulletScout = 0;
                    particleEngine.Clear();
                    BalaOponenteMove = false;
                }
            }

            if (vida > 0)
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.Up))
                {
                    tiro = 1;
                    angulo = 0.0f;
                    if (ay > 0)
                        ay -= velocidade;
                    posicao = 1;
                    movimento = 1;
                    if (!esteira.IsPlaying())
                        esteira.Start(3, false);
                    if (parOuImpar % 5 == 0)
                    {
                        rastroMarca.SetX(Ax);
                        rastroMarca.SetY(Ay);
                        rastroMarca.SetWidth(Width);
                        rastroMarca.SetHeight(Height);
                        rastroMarca.SetAngle(AnguloDisparo);
                    }
                    parOuImpar++;
                }
                else
            if (vida > 0)
                    if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.Down) && vida > 0)
                    {
                        tiro = 2;
                        angulo = 3.1419f;
                        if (ay < screenHeigth)
                            ay += velocidade;
                        posicao = 2;
                        movimento = 1;
                        if (!esteira.IsPlaying())
                            esteira.Start(3, false);
                    }
                    else
                if (vida > 0)
                        if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.Right) && vida > 0)
                        {
                            tiro = 3;
                            angulo = 1.571f;
                            if (ax < screenWidth)
                                ax += velocidade;
                            posicao = 3;
                            movimento = 1;
                            if (!esteira.IsPlaying())
                                esteira.Start(3, false);
                        }
                        else
                    if (vida > 0)
                            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || tecaldo.IsKeyDown(Keys.Left) && vida > 0)
                            {
                                tiro = 4;
                                angulo = 4.712f;
                                if (ax > 0)
                                    ax -= velocidade;
                                posicao = 4;
                                movimento = 1;
                                if (!esteira.IsPlaying())
                                    esteira.Start(3, false);
                            }

            if (!esteira.IsPlaying())
            {
                movimento = 0;
                if (esteiraInstance.State == SoundState.Playing)
                    esteiraInstance.Stop();
            }
            else
            {
                if (esteiraInstance.State != SoundState.Playing)
                    esteiraInstance.Play();
            }

            BalaAx = balas.GetX();
            BalaAy = balas.GetY();
        }

        int bandeiraPosse = 0;
        int jogadorBposicao = 0;
        int jogadorBmovimento = 0;
        int jogadorBdisparo = 0;
        int jogadorBtiro = 0;
        int jogadorBanguloTiro = 0;
        int jogadorBcargaPlantada = 0;
        int jogadorBvida = 0;

        public void Update(GameTime gameTime, string dados, int bandeira, int ID)
        {
            this.bandeiraPosse = bandeira;
            try
            {
                ay = int.Parse(dados[1] + "" + dados[2] + "" + dados[3]);
                ax = int.Parse(dados[4] + "" + dados[5] + "" + dados[6]);
                jogadorBposicao = int.Parse("" + dados[7]);
                jogadorBmovimento = int.Parse("" + dados[8]);
                jogadorBdisparo = int.Parse("" + dados[9]);
                jogadorBtiro = int.Parse("" + dados[10]);
                jogadorBanguloTiro = int.Parse("" + dados[11]);
                jogadorBcargaPlantada = int.Parse("" + dados[12]);
                jogadorBvida = int.Parse(dados[13] + "" + dados[14] + "" + dados[15]);
                Vida = jogadorBvida;

                if (vida == 0)
                {
                    timerOn = 1;
                    Restart(gameTime, jogadorID);
                    anguloA = angulo + 0.01f;
                    anguloB = angulo + 0.09f;
                    anguloC = angulo + 0.05f;
                }
                else
                {
                    anguloA = angulo;
                    anguloB = angulo;
                    anguloC = angulo;
                }

                switch (jogadorBposicao)
                {
                    case 1:
                        angulo = 0.0f;
                        break;
                    case 2:
                        angulo = 3.12f;
                        break;
                    case 3:
                        angulo = 1.56f;
                        break;
                    case 4:
                        angulo = 4.68f;
                        break;
                }

                if (jogadorBmovimento == 1)
                {
                    if (!esteira.IsPlaying())
                        esteira.Start(5, false);
                }

                switch (jogadorBanguloTiro)
                {
                    case 1:
                        anguloDisparo = 0.0f;
                        break;
                    case 2:
                        anguloDisparo = 3.1419f;
                        break;
                    case 3:
                        anguloDisparo = 1.571f;
                        break;
                    case 4:
                        anguloDisparo = 4.712f;
                        break;
                }

                if (jogadorBdisparo == 1 && balaOponenteMove == false)
                    balaOponenteMove = true;

                if (balas.GetX() >= -100 || balas.GetX() <= 1000 || balas.GetY() >= -100 || balas.GetY() <= 1000)
                    if (balaOponenteMove)
                    {
                        switch (anguloTiro)
                        {
                            case 1:
                                balas.MoveByY(-15);
                                break;
                            case 2:
                                balas.MoveByY(15);
                                break;
                            case 3:
                                balas.MoveByX(15);
                                break;
                            case 4:
                                balas.MoveByX(-15);
                                break;
                        }
                    }
                    else
                    {
                        balas.SetX(ax);
                        balas.SetY(ay);
                    }

                anguloTiro = jogadorBanguloTiro;

                if (jogadorBdisparo == 1)
                {
                    particleEngine.Update(balas.GetX(), balas.GetY(), jogadorBanguloTiro);

                }

                if (jogadorBdisparo == 1 && disparo == 0)
                {
                    disparo = 1;
                    tiroInstance.Add(tiroSound.CreateInstance());
                    tiroInstance.Last().Play();
                }
                else
                {
                    particleEngine.Clear();
                }

                if (jogadorBdisparo == 1 && disparo == 1)
                {

                    particleEngine.Update(Balas.GetX(), Balas.GetY(), anguloTiro);

                    if (jogadorBdisparo == 0)
                    {
                        particleEngine.Clear();
                        disparo = 0;
                    }
                }

                if (balas.GetX() <= -100 || balas.GetX() >= 1000 || balas.GetY() <= -100 || balas.GetY() >= 1000)
                {
                    balas.SetX(90000);
                    balas.SetY(90000);
                    particleEngine.Clear();
                    BalaOponenteMove = false;
                }

                if (jogadorBcargaPlantada == 1 && cargaPlantada == 0)
                {
                    cargaPlantada = 1;
                    minaTerrestre.SetX(Ax - minaTerrestre.GetWidth() / 2);
                    minaTerrestre.SetY(Ay - minaTerrestre.GetHeight() / 2);
                }
                if (jogadorBcargaPlantada == 0 && minaTerrestre.GetX() > 1000 && minaTerrestre.GetY() > 1000)
                {
                    cargaPlantada = 0;
                }
            }
            catch { }
        }

        public void Draw(GameTime gameTime, float capturarBandeira, bool overFlag)
        {
            minaTerrestre.Draw(spriteBatch);
            if (disparo == 1)
            {
                particleEngine.Draw(spriteBatch);
                balas.Draw(spriteBatch, anguloDisparo, balas.GetX(), balas.GetY(), 0.1f);
            }
            esteira.Draw(spriteBatch, anguloA, ax, ay);
            Corpo.Draw(spriteBatch, anguloC, ax, ay, 0.2f);
            torre.Draw(spriteBatch, anguloB, ax, ay, 0.25f);
            if (timerOn == 1)
                spriteBatch.DrawString(timer, (timerCount / 60).ToString(), new Vector2(Ax - Corpo.GetWidth() / 2, Ay - Corpo.GetHeight() / 2), Color.White);
            if (overFlag)
                spriteBatch.DrawString(holdFlag, Math.Round(capturarBandeira).ToString(), new Vector2(Ax , Ay), Color.White);
        }

        public void Draw(GameTime gameTime, string oponente)
        {
            if (bandeiraPosse == 1)
                minaTerrestre.Draw(spriteBatch);
            balas.Draw(spriteBatch, anguloDisparo, balas.GetX(), balas.GetY(), 0.1f);
            esteira.Draw(spriteBatch, anguloA, ax, ay);
            Corpo.Draw(spriteBatch, anguloB, ax, ay, 0.2f);
            torre.Draw(spriteBatch, anguloC, ax, ay, 0.25f);
            particleEngine.Draw(spriteBatch);
        }
    }
}
