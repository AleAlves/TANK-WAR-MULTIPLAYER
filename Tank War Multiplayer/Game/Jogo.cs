using gameutil2d.classes.basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework.Audio;
using Quobject.SocketIoClientDotNet.Client;
using System.Diagnostics;


namespace Tank_War_Multiplayer
{
    class Jogo
    {
        //private ConnectedSocket socket;
        private int gameover = 0;
        private ContentManager content;
        private AnimationSprites explosaoA;
        private AnimationSprites explosaoB;
        private int explosaoAAx = -100;
        private int explosaoAAy = -100;
        private int explosaoBAx = -100;
        private int explosaoBAy = -100;
        private int jogador;
        private int oponente;
        private int h = 0;
        private int pegarBandeiraA = 0;
        private int pegarBandeiraB = 0;
        private bool missaoCumprida = false;
        private bool missaoFracassada = false;
        private bool gameon = false;
        private Random random;
        private DateTime date;
        private Image hintEnemy;
        private Image hintFlag;
        private Image compassA;
        private Image compassB;
        private Image crateraA;
        private Image crateraB;
        SpriteBatch spriteBatch;
        Tank jogadorA;
        Tank jogadorB;
        Cenario cenario;
        Status status;
        Image wait;
        SoundEffect morseSom;
        SoundEffectInstance morseInstance;
        SoundEffect clangSound;
        List<SoundEffectInstance> clangInstance;
        SoundEffect reload;
        SoundEffectInstance reloadInstance;
        SpriteFont pingFont;
        Image falhaconexao;
        Image oponentOffline;
        Image full;
        Image timeup;
        Image waitGetLong;
        Image waitingPlayer;
        Image waitingAnimation;
        private Image barraVidaOponente;
        private Image barraVidaFixaOponente;
        float morseGetFlag = 0f;
        float morseWinBattle = 0f;
        float morseLoseBattle = 0f;
        float capturarBandeira = 10f;
        float perderBandeira = 10f;
        int server;
        int velocidade = 2;
        double anguleA;
        double anguleB;
        double enemyAngle = 0.0f;
        Camera _camera;
        int width = 0;
        int height = 0;
        float zoom = 2.0f;
        int min = 2;
        int sec = 59;
        int timeToWait = 30;
        float sectimeTowait = 0f;
        float timerCount = 1f;
        float waitingRotation = 0f;
        SpriteFont timer;
        Socket socket;
        bool conectado = false;
        bool oponenteOnline = true;
        bool serverfull = false;
        bool twoPlayers = false;
        string dadosOponente = "000000000000000000000000000000";
        String gameServer;
        String oponenteId;
        bool overFlag = false;

        public Jogo(ContentManager content, int width, int height, int id)
        {
            FileStream fStream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\config.txt", FileMode.Open);
            StreamReader sReader = new StreamReader(fStream);
            gameServer = sReader.ReadLine();
            Console.WriteLine("Trying get: " + gameServer);
            fStream.Close();
            sReader.Close();

            try
            {
                var options = new IO.Options() { ForceNew = true, Reconnection = false, IgnoreServerCertificateValidation = true, Timeout = 5000 };
                socket = IO.Socket(gameServer, options);
                conectado = true;
            }
            catch { Debug.WriteLine("Erro na conexão"); conectado = false; }

            try
            {
                socket.On("id", (data) =>
                {
                    server = Convert.ToInt32(data);
                    Console.WriteLine("Server given ID: " + server);
                });
            }
            catch { conectado = false; }

            this.width = width;
            this.height = height;
            this.content = content;
            this.h = height;
            if (id == 1)
            {
                this.jogador = 1;
                this.oponente = 2;
            }
            else
            {
                this.jogador = 2;
                this.oponente = 1;
            }

            status = new Status(this.content, width, height);
            jogadorB = new Tank(this.content, width, height, oponente);
            jogadorA = new Tank(this.content, width, height, jogador);
            cenario = new Cenario(this.content, width, height, id);
        }


        public Image BarraVidaOponente
        {
            get
            {
                return barraVidaOponente;
            }

            set
            {
                barraVidaOponente = value;
            }
        }

        public Image BarraVidaFixaOponente
        {
            get
            {
                return barraVidaFixaOponente;
            }

            set
            {
                barraVidaFixaOponente = value;
            }
        }

        public void UnloadContent()
        {
        }

        public int Gameover
        {
            get
            {
                return gameover;
            }

            set
            {
                gameover = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public void Initialize()
        {
            jogadorA.Initialize();
            jogadorB.Initialize();
            cenario.Initialize();
            status.Initialize();
            clangInstance = new List<SoundEffectInstance>();
        }

        public void LoadContent(GraphicsDevice graphic, SpriteBatch spriteBatch)
        {
            falhaconexao = new Image(content, "Error/error1", 0, 0, width, height);
            oponentOffline = new Image(content, "Error/error2", 0, 0, width, height);
            full = new Image(content, "Error/error3", 0, 0, width, height);
            waitGetLong = new Image(content, "Error/error4", 0, 0, width, height);
            timeup = new Image(content, "Error/error5", 0, 0, width, height);
            waitingPlayer = new Image(content, "waitingPlayer", width / 100 * 30, height / 2, width / 100 * 50, height / 100 * 20);
            waitingAnimation = new Image(content, "waitingAnimation", width / 100 * 55, waitingPlayer.GetY() + height / 100 * 12, width / 100 * 20, height / 100 * 20);
            _camera = new Camera(graphic.Viewport);
            this.spriteBatch = spriteBatch;
            GD.instance = graphic;
            date = new DateTime();
            jogadorA.LoadContent(graphic, spriteBatch);
            jogadorB.LoadContent(graphic, spriteBatch);
            cenario.LoadContent(graphic, spriteBatch);
            status.LoadContent(graphic, spriteBatch);
            clangSound = content.Load<SoundEffect>("Sound/clangSom");
            morseSom = content.Load<SoundEffect>("Sound/morseSom");
            morseInstance = morseSom.CreateInstance();
            wait = new Image(content, "wait", 0, 0, 800, h - 100);
            timer = content.Load<SpriteFont>("ping");
            hintEnemy = new Image(content, "hintEnemy", 0, 0, 20, 20);
            hintFlag = new Image(content, "hintFlag", 0, 0, 20, 20);
            compassA = new Image(content, "compass", width / 2 - 25, height - 120, 50, 50);
            compassB = new Image(content, "compass", width / 2 + 25, height - 120, 50, 50);

            explosaoA = new AnimationSprites(content, -400, -4000, 100, 100);
            for (int i = 1; i < 7; i++)
                explosaoA.Add("Explosao/boom" + i);

            explosaoB = new AnimationSprites(content, -400, -4000, 100, 100);
            for (int i = 1; i < 7; i++)
                explosaoB.Add("Explosao/boom" + i);

            date = DateTime.Now;
            status.Situacao = "";
            status.Situacao = "[Mission]: Get the enemy flag. \n *Careful with landmines";

            reload = content.Load<SoundEffect>("Sound/clickSom");
            reloadInstance = reload.CreateInstance();
            reloadInstance.IsLooped = false;

            barraVidaOponente = new Image(content, "Menu/barra", 200, 200, 100, 5);
            barraVidaFixaOponente = new Image(content, "Menu/barraFixa", -100, -100, 33, 5);

            crateraA = new Image(content, "cratera", -1000, -1000, 50, 50);
            crateraB = new Image(content, "cratera", -1000, -1000, 50, 50);
            pingFont = content.Load<SpriteFont>("Ping");
        }

        float xDiff = 0.0f;
        float yDiff = 0.0f;
        float flagxDiff = 0.0f;
        float flagyDiff = 0.0f;
        double flagAngle = 0.0;

        public int Update(GameTime gameTime, MouseState state)
        {
     
            if (twoPlayers == false)
            socket.On("gameOn", (data) =>
            {
                Console.WriteLine("GameOn:  " + data);
                oponenteId = data.ToString();
                twoPlayers = true;
            });

            if (twoPlayers)
                timerCount -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                waitingRotation += 0.1f;
                sectimeTowait += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (sectimeTowait >= 1)
                {
                    timeToWait--;
                    sectimeTowait = 0f;
                }
                if (timeToWait <= 0)
                {
                    socket.Disconnect();
                    conectado = false;
                }
            }

            if (timerCount <= 0f)
            {
                timerCount = 1f;
                if (sec > 0)
                    sec--;
                if (sec == 0 && min > 0)
                {
                    sec = 59;
                    min--;
                }
            }


            if (min <= 0 && sec <= 0)
                socket.Disconnect();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                socket.Disconnect();
                return 3;
            }


            if (conectado)
            {

                if (jogadorA.BulletScout == 0)
                {
                    _camera.Zoom = 2f;
                    _camera.Position = new Vector2(((jogadorA.Ax - width / 2) - jogadorA.Width) + 25, ((jogadorA.Ay - height / 2) - jogadorA.Height) + 50);
                }
                else
                    if (jogadorA.BulletScout == 1)
                {
                    _camera.Zoom += 0.05f;
                    _camera.Position = new Vector2(((jogadorA.BalaAx - width / 2) - jogadorA.Width) + 25, ((jogadorA.BalaAy - height / 2) - jogadorA.Height) + 50);
                }
                cenario.Update(gameTime);
                xDiff = jogadorA.Ax - jogadorB.Ax;
                yDiff = jogadorA.Ay - jogadorB.Ay;
                enemyAngle = (float)(Math.Atan2(yDiff, xDiff));

                if (pegarBandeiraB == 0)
                {
                    flagxDiff = jogadorA.Ax - cenario.BandeirBAx;
                    flagyDiff = jogadorA.Ay - cenario.BandeirBAy;
                }
                else
                {
                    flagxDiff = jogadorA.Ax - cenario.BandeirAAx;
                    flagyDiff = jogadorA.Ay - cenario.BandeirAAy;
                }
                flagAngle = (float)(Math.Atan2(flagyDiff, flagxDiff));

                if (GamePad.GetState(PlayerIndex.One).Buttons.BigButton == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    jogadorA.Vida = 0;
                }

                random = new Random(Guid.NewGuid().GetHashCode());
                anguleA = random.NextDouble() * (4.0 - 0.0) + 0.0;
                anguleB = random.NextDouble() * (4.0 - 0.0) + 0.0;

                gameon = true;

                if (twoPlayers)
                    jogadorA.Update(gameTime);

                try
                {
                    if (oponenteId != null)
                        socket.Emit("update", oponenteId, server + jogadorA.Ay.ToString().PadLeft(3, '0') + jogadorA.Ax.ToString().PadLeft(3, '0') + jogadorA.Posicao + jogadorA.Movimento + jogadorA.Disparo + jogadorA.Tiro + jogadorA.AnguloTiro + jogadorA.CargaPlantada + jogadorA.Vida.ToString().PadLeft(3, '0'));
                }
                catch { }
                try
                {
                    socket.On("full", (data) =>
                    {
                        socket.Disconnect();
                        serverfull = true;
                    });
                    socket.On("off", (data) =>
                    {
                        socket.Disconnect();
                        oponenteOnline = false;
                    });
                    socket.On("serverData", (data) =>
                        {
                            dadosOponente = data.ToString();
                        });
                }
                catch { }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    socket.Disconnect();
                    conectado = false;
                }

                jogadorB.Update(gameTime, dadosOponente, pegarBandeiraB, jogadorA.JogadorID);

                cenario.Update(gameTime);
                status.Update(jogadorA.Municao, jogadorA.Minas, jogadorB.Vida, gameTime);
                status.BarraVida.SetWidth(jogadorA.Vida);
                BarraVidaOponente.SetWidth(jogadorB.Vida / 3);
                BarraVidaOponente.SetX(jogadorB.Ax);
                BarraVidaOponente.SetY(jogadorB.Ay - jogadorB.Height);
                BarraVidaFixaOponente.SetX(jogadorB.Ax);
                BarraVidaFixaOponente.SetY(jogadorB.Ay - jogadorB.Height);

                foreach (Image i in cenario.Casas)
                    if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, i))
                    {
                        switch (jogadorA.Posicao)
                        {
                            case 1:
                                jogadorA.Ay += velocidade;
                                break;
                            case 2:
                                jogadorA.Ay -= velocidade;
                                break;
                            case 3:
                                jogadorA.Ax -= velocidade;
                                break;
                            case 4:
                                jogadorA.Ax += velocidade;
                                break;
                        }
                    }

                foreach (Image i in cenario.BaseMuros)
                    if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, i))
                    {
                        switch (jogadorA.Posicao)
                        {
                            case 1:
                                jogadorA.Ay += velocidade;
                                break;
                            case 2:
                                jogadorA.Ay -= velocidade;
                                break;
                            case 3:
                                jogadorA.Ax -= velocidade;
                                break;
                            case 4:
                                jogadorA.Ax += velocidade;
                                break;
                        }
                    }

                foreach (Image i in cenario.Portao)
                    if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, i))
                    {
                        switch (jogadorA.Posicao)
                        {
                            case 1:
                                jogadorA.Ay += velocidade;
                                break;
                            case 2:
                                jogadorA.Ay -= velocidade;
                                break;
                            case 3:
                                jogadorA.Ax -= velocidade;
                                break;
                            case 4:
                                jogadorA.Ax += velocidade;
                                break;
                        }
                    }

                foreach (Image i in cenario.Casas)
                    if (Collision.Check(jogadorA.Balas, i))
                    {
                        jogadorA.BulletScout = 0;
                        jogadorA.ParticleEngine.Clear();
                        jogadorA.Balas.SetX(random.Next(900, 999));
                        jogadorA.Balas.SetY(random.Next(900, 999));
                        cenario.ExplosaoInstance.Add(cenario.ExplosaoSound.CreateInstance());
                        cenario.ExplosaoInstance.Last().Play();
                        explosaoAAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoAAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoA.Start(2, false);
                        cenario.Casas.Remove(i);
                        break;
                    }

                foreach (Image i in cenario.Casas)
                    if (Collision.Check(jogadorB.Balas, i))
                    {
                        jogadorB.ParticleEngine.Clear();
                        jogadorB.BalaOponenteMove = false;
                        jogadorB.Balas.SetX(random.Next(900, 999));
                        jogadorB.Balas.SetY(random.Next(900, 999));
                        cenario.ExplosaoInstance.Add(cenario.ExplosaoSound.CreateInstance());
                        cenario.ExplosaoInstance.Last().Play();
                        explosaoBAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoBAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoB.Start(2, false);
                        cenario.Casas.Remove(i);
                        break;
                    }

                foreach (Image i in cenario.BaseMuros)
                    if (Collision.Check(jogadorA.Balas, i))
                    {
                        jogadorA.BulletScout = 0;
                        jogadorA.ParticleEngine.Clear();
                        jogadorA.Balas.SetX(random.Next(900, 999));
                        jogadorA.Balas.SetY(random.Next(900, 999));
                        cenario.ExplosaoInstance.Add(cenario.ExplosaoSound.CreateInstance());
                        cenario.ExplosaoInstance.Last().Play();
                        explosaoAAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoAAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoA.Start(2, false);
                        cenario.BaseMuros.Remove(i);
                        break;
                    }

                foreach (Image i in cenario.BaseMuros)
                    if (Collision.Check(jogadorB.Balas, i))
                    {
                        jogadorB.ParticleEngine.Clear();
                        jogadorB.BalaOponenteMove = false;
                        jogadorB.Balas.SetX(random.Next(900, 999));
                        jogadorB.Balas.SetY(random.Next(900, 999));
                        cenario.ExplosaoInstance.Add(cenario.ExplosaoSound.CreateInstance());
                        cenario.ExplosaoInstance.Last().Play();
                        explosaoBAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoBAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoB.Start(2, false);
                        cenario.BaseMuros.Remove(i);
                        break;
                    }

                foreach (Image i in cenario.Portao)
                    if (Collision.Check(jogadorA.Balas, i))
                    {
                        jogadorA.BulletScout = 0;
                        jogadorA.ParticleEngine.Clear();
                        jogadorA.Balas.SetX(random.Next(900, 999));
                        jogadorA.Balas.SetY(random.Next(900, 999));
                        clangInstance.Add(clangSound.CreateInstance());
                        clangInstance.Last().Play();
                        explosaoAAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoAAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoA.Start(4, false);
                        cenario.Portao.Remove(i);
                        break;
                    }

                foreach (Image i in cenario.Portao)
                    if (Collision.Check(jogadorB.Balas, i))
                    {
                        jogadorB.ParticleEngine.Clear();
                        jogadorB.BalaOponenteMove = false;
                        jogadorB.Balas.SetX(random.Next(900, 999));
                        jogadorB.Balas.SetY(random.Next(900, 999));
                        clangInstance.Add(clangSound.CreateInstance());
                        clangInstance.Last().Play();
                        explosaoBAx = (i.GetX() + i.GetWidth() / 2);
                        explosaoBAy = (i.GetY() + i.GetHeight() / 2);
                        explosaoB.Start(2, false);
                        cenario.Portao.Remove(i);
                        break;
                    }

                if (Collision.Check(jogadorB.Corpo, jogadorB.Ax, jogadorB.Ay, jogadorA.Balas))
                {
                    jogadorA.BulletScout = 0;
                    jogadorA.ParticleEngine.Clear();
                    jogadorA.Balas.SetX(random.Next(900, 999));
                    jogadorA.Balas.SetY(random.Next(900, 999));
                    clangInstance.Add(clangSound.CreateInstance());
                    clangInstance.Last().Play();
                    explosaoAAx = (jogadorB.Ax);
                    explosaoAAy = (jogadorB.Ay);
                    explosaoA.Start(2, false);
                    if (jogadorB.Vida > 0)
                        jogadorB.Vida -= 25;
                }

                if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, jogadorB.Balas))
                {
                    jogadorB.ParticleEngine.Clear();
                    jogadorB.BalaOponenteMove = false;
                    jogadorB.Balas.SetX(random.Next(900, 999));
                    jogadorB.Balas.SetY(random.Next(900, 999));
                    clangInstance.Add(clangSound.CreateInstance());
                    clangInstance.Last().Play();
                    explosaoBAx = (jogadorA.Ax);
                    explosaoBAy = (jogadorA.Ay);
                    explosaoB.Start(2, false);
                    if (jogadorA.Vida > 0)
                        jogadorA.Vida -= 25;
                }

                if (Collision.Check(jogadorA.Balas, jogadorB.Balas))
                {
                    jogadorA.BulletScout = 0;
                    jogadorA.ParticleEngine.Clear();
                    jogadorB.ParticleEngine.Clear();
                    jogadorB.BalaOponenteMove = false;
                    jogadorA.Balas.SetX(random.Next(900, 999));
                    jogadorA.Balas.SetY(random.Next(900, 999));
                    jogadorB.Balas.SetX(random.Next(900, 999));
                    jogadorB.Balas.SetY(random.Next(900, 999));
                    clangInstance.Add(clangSound.CreateInstance());
                    clangInstance.Last().Play();
                    explosaoA.SetX(jogadorA.Balas.GetX());
                    explosaoA.SetY(jogadorA.Balas.GetY());
                    explosaoA.Start(4, false);
                    explosaoB.SetX(jogadorB.Balas.GetX());
                    explosaoB.SetY(jogadorB.Balas.GetY());
                    explosaoA.Start(2, false);
                }

                if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, cenario.Repair))
                {
                    if (jogadorA.Municao < 10)
                    {
                        if (reloadInstance.State != SoundState.Playing)
                            reloadInstance.Play();
                        jogadorA.Municao = 10;
                    }
                }

                if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, cenario.BandeiraB) && missaoCumprida == false && jogadorA.Vida > 0)
                {
                    overFlag = true;
                    if (pegarBandeiraB == 0)
                        capturarBandeira -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (capturarBandeira < 0f)
                    {
                        pegarBandeiraB = 1;
                        capturarBandeira = 10f;
                    }
                    else
                    {
                        status.Situacao = "";
                        status.Situacao = "[Mission]: Good job,\n now hold on a few seconds.";
                    }
                }
                else
                {
                    pegarBandeiraB = 0;
                    overFlag = false;
                }

                if (pegarBandeiraB == 1)
                {
                    morseGetFlag += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (morseGetFlag < 1f)
                    {
                        if (morseInstance.State != SoundState.Playing)
                            morseInstance.Play();
                        date = DateTime.Now;
                    }
                    status.Situacao = "";
                    status.Situacao = "[Mission]: Bring it to your HQ.";
                    cenario.BandeiraB.SetX(jogadorA.Ax);
                    cenario.BandeiraB.SetY(jogadorA.Ay);
                    cenario.BandeiraB.SetWidth(1);
                    cenario.BandeiraB.SetHeight(1);
                    if (Collision.Check(cenario.CheckpointA, cenario.BandeiraB) && pegarBandeiraA == 0)
                    {
                        overFlag = true;
                        capturarBandeira -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (capturarBandeira < 0f)
                            missaoCumprida = true;
                        status.Situacao = "";
                        status.Situacao = "[Mission]: Almost there,\n hold it a bit more.";
                    }
                    else
                        overFlag = false;
                }
                else
                {
                    cenario.BandeiraB.SetX(cenario.CheckpointB.GetX() + 10);
                    cenario.BandeiraB.SetY(cenario.CheckpointB.GetY() + 10);
                    cenario.BandeiraB.SetWidth(20);
                    cenario.BandeiraB.SetHeight(20);
                }
                if (missaoCumprida)
                {
                    morseWinBattle += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (morseWinBattle < 1f)
                    {
                        if (morseInstance.State != SoundState.Playing)
                            morseInstance.Play();
                        date = DateTime.Now;
                    }
                    status.Situacao = "";
                    status.Situacao = "[Mission]: You've win the battle.";
                    cenario.BandeiraB.SetX(cenario.CheckpointA.GetX());
                    cenario.BandeiraB.SetY(cenario.CheckpointA.GetY());
                    cenario.BandeiraB.SetWidth(cenario.CheckpointA.GetWidth());
                    cenario.BandeiraB.SetHeight(cenario.CheckpointA.GetHeight());
                    pegarBandeiraB = 1;
                    return 1;
                }


                if (Collision.Check(jogadorB.Corpo, jogadorB.Ax, jogadorB.Ay, cenario.BandeiraA))
                {
                    pegarBandeiraA = 1;
                }
                else
                {
                    pegarBandeiraA = 0;
                }

                if (pegarBandeiraA == 1)
                {
                    status.Situacao = "";
                    status.Situacao = "Your Flag has been captured!!!\n You must recover it to win";
                    cenario.BandeiraA.SetX(jogadorB.Ax);
                    cenario.BandeiraA.SetY(jogadorB.Ay);
                    cenario.BandeiraA.SetWidth(1);
                    cenario.BandeiraA.SetHeight(1);
                    if (Collision.Check(cenario.CheckpointB, cenario.BandeiraA) && pegarBandeiraB == 0)
                    {
                        perderBandeira -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (perderBandeira < 0f)
                            missaoFracassada = true;
                    }
                }
                else
                {
                    cenario.BandeiraA.SetX(cenario.CheckpointA.GetX() + 10);
                    cenario.BandeiraA.SetY(cenario.CheckpointA.GetY() + 10);
                    cenario.BandeiraA.SetWidth(20);
                    cenario.BandeiraA.SetHeight(20);
                    perderBandeira = 10f;
                }
                if (missaoFracassada)
                {
                    morseLoseBattle += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (morseLoseBattle < 1f)
                    {
                        if (morseInstance.State != SoundState.Playing)
                            morseInstance.Play();
                        date = DateTime.Now;
                    }
                    status.Situacao = "";
                    status.Situacao = "[Mission]:You've Lost the battle.";
                    cenario.BandeiraA.SetX(cenario.CheckpointB.GetX());
                    cenario.BandeiraA.SetY(cenario.CheckpointB.GetY());
                    cenario.BandeiraA.SetWidth(cenario.CheckpointB.GetWidth());
                    cenario.BandeiraA.SetHeight(cenario.CheckpointB.GetHeight());
                    pegarBandeiraA = 1;
                    return 2;
                }


                if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, jogadorB.MinaTerrestre))
                {
                    jogadorA.WaitTime = 10f;
                    jogadorA.TimerCount = 600;
                    clangInstance.Add(clangSound.CreateInstance());
                    clangInstance.Last().Play();
                    explosaoBAx = (jogadorA.Ax);
                    explosaoBAy = (jogadorA.Ay);
                    explosaoB.Start(4, false);
                    jogadorB.MinaTerrestre.SetX(random.Next(1200, 1300));
                    jogadorB.MinaTerrestre.SetY(random.Next(1200, 1300));
                    crateraA.SetX(jogadorA.Ax - crateraA.GetWidth() / 2);
                    crateraA.SetY(jogadorA.Ay - crateraA.GetHeight() / 2);
                    jogadorA.Vida = 0;
                    date = DateTime.Now;
                    status.Situacao = "";
                    status.Situacao = "You've Killed by enemy Landmine.\n It will take more time to repair";
                }


                if (Collision.Check(jogadorB.Corpo, jogadorB.Ax, jogadorB.Ay, jogadorA.MinaTerrestre))
                {
                    clangInstance.Add(clangSound.CreateInstance());
                    clangInstance.Last().Play();
                    explosaoAAx = (jogadorB.Ax);
                    explosaoAAy = (jogadorB.Ay);
                    explosaoA.Start(4, false);
                    jogadorA.MinaTerrestre.SetX(random.Next(1200, 1300));
                    jogadorA.MinaTerrestre.SetY(random.Next(1200, 1300));
                    crateraB.SetX(jogadorB.Ax - crateraB.GetWidth() / 2);
                    crateraB.SetY(jogadorB.Ay - crateraB.GetHeight() / 2);
                    jogadorB.Vida = 0;
                    date = DateTime.Now;
                    status.Situacao = "";
                    status.Situacao = "You Killed enemy with landmine.\n It'll take more time to repair";
                    jogadorA.Minas = 1;
                    jogadorA.CargaPlantada = 0;
                }

                if (Collision.Check(jogadorA.Corpo, jogadorA.Ax, jogadorA.Ay, jogadorA.Width, jogadorA.Height, jogadorB.Corpo, jogadorB.Ax, jogadorB.Ay, jogadorB.Width, jogadorB.Height))
                {
                    pegarBandeiraA = 0;

                    switch (jogadorA.Posicao)
                    {
                        case 1:
                        case 2:
                            switch (jogadorB.Posicao)
                            {
                                case 1:
                                case 2:
                                    if (jogadorA.Ay < jogadorB.Ay)
                                    {
                                        jogadorA.Ay -= velocidade;
                                    }
                                    else
                                    if (jogadorA.Ay > jogadorB.Ay)
                                    {
                                        jogadorA.Ay += velocidade;
                                    }
                                    break;
                                case 3:
                                case 4:
                                    if (jogadorA.Ay < jogadorB.Ay)
                                    {
                                        jogadorA.Ay -= velocidade;
                                    }
                                    else
                                    if (jogadorA.Ay > jogadorB.Ay)
                                    {
                                        jogadorA.Ay += velocidade;
                                    }
                                    break;

                            }
                            break;
                        case 3:
                        case 4:
                            switch (jogadorB.Posicao)
                            {
                                case 1:
                                case 2:
                                    if (jogadorA.Ax < jogadorB.Ax)
                                    {
                                        jogadorA.Ax -= velocidade;
                                    }
                                    else
                                    if (jogadorA.Ax > jogadorB.Ax)
                                    {
                                        jogadorA.Ax += velocidade;
                                    }
                                    break;
                                case 3:
                                case 4:
                                    if (jogadorA.Ax < jogadorB.Ax)
                                    {
                                        jogadorA.Ax -= velocidade;
                                    }
                                    else
                                    if (jogadorA.Ax > jogadorB.Ax)
                                    {
                                        jogadorA.Ax += velocidade;
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
            return 0;
        }

        public void Draw(GameTime gameTime)
        {
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            cenario.Draw(gameTime);
            crateraA.Draw(spriteBatch);
            crateraB.Draw(spriteBatch);
            jogadorA.Draw(gameTime,capturarBandeira, overFlag);
            jogadorB.Draw(gameTime, "Oponente");
            explosaoB.Draw(spriteBatch, Convert.ToSingle(anguleB), explosaoBAx, explosaoBAy);
            explosaoA.Draw(spriteBatch, Convert.ToSingle(anguleA), explosaoAAx, explosaoAAy);
            barraVidaFixaOponente.Draw(spriteBatch);
            barraVidaOponente.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            status.Draw(gameTime, date);
            hintEnemy.Draw(spriteBatch, Convert.ToSingle(enemyAngle), compassB.GetX() + compassB.GetWidth() / 2, compassB.GetY() + compassB.GetHeight() / 2);
            hintFlag.Draw(spriteBatch, Convert.ToSingle(flagAngle), compassA.GetX() + compassA.GetWidth() / 2, compassA.GetY() + compassA.GetWidth() / 2);
            compassA.Draw(spriteBatch);
            compassB.Draw(spriteBatch);
            spriteBatch.DrawString(pingFont, "", new Vector2(100, 600), Color.Black);
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(timer, string.Format("FPS:{0} Remaining Time:{1}", Math.Round(frameRate), " " + min.ToString().PadLeft(2, '0') + ":" + sec.ToString().PadLeft(2, '0')), new Vector2(10.0f, 20.0f), Color.White);
            if (!twoPlayers)
            {
                waitingPlayer.Draw(spriteBatch);
                waitingAnimation.Draw(spriteBatch, waitingRotation, waitingAnimation.GetX(), waitingAnimation.GetY(), 0.4f);
            }
            if (!conectado) { falhaconexao.Draw(spriteBatch); }
            if (!oponenteOnline) { oponentOffline.Draw(spriteBatch); }
            if (serverfull) { full.Draw(spriteBatch); }
            if (min == 0 && sec == 0) { timeup.Draw(spriteBatch); }
            if (timeToWait <= 0) { waitGetLong.Draw(spriteBatch); }
            spriteBatch.End();
        }
    }
}
