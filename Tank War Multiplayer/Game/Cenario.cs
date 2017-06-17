using gameutil2d.classes.basic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Tank_War_Multiplayer
{
    class Cenario
    {
        private static ContentManager content;
        private SpriteBatch spriteBatch;
        private Image chao;
        private List<Image> casas;
        private List<Image> casasDestruidas;
        private int screenWidht = 0;
        private int screenHeight = 0;
        private int id = 0;
        private Image baseSolo;
        private Image repair;
        private List<Image> baseMuros;
        private Image bandeiraA;
        private Image bandeiraB;
        private List<Image> portao;
        public List<Image> rua;
        public List<Image> calcada;
        private Image chaoBaseEixo;
        private Image chaoBaseAliado;
        private Image checkpointA;
        private Image checkpointB;
        private SoundEffect explosaoSound;
        private List<SoundEffectInstance> explosaoInstance;
        private int bandeirBAx;
        private int bandeirBAy;
        private int bandeirAAx;
        private int bandeirAAy;

        public List<Image> BaseMuros
        {
            get
            {
                return this.baseMuros;
            }
            set
            {
                this.baseMuros = value;
            }
        }

        public Image Repair
        {
            get
            {
                return this.repair;
            }
            set
            {
                this.repair = value;
            }
        }



        internal List<Image> Casas
        {
            get
            {
                return casas;
            }

            set
            {
                casas = value;
            }
        }

        internal List<Image> Portao
        {
            get
            {
                return portao;
            }

            set
            {
                portao = value;
            }
        }

        internal Image BandeiraA
        {
            get
            {
                return bandeiraA;
            }

            set
            {
                bandeiraA = value;
            }
        }

        internal Image BandeiraB
        {
            get
            {
                return bandeiraB;
            }

            set
            {
                bandeiraB = value;
            }
        }

        internal Image CheckpointA
        {
            get
            {
                return checkpointA;
            }

            set
            {
                checkpointA = value;
            }
        }

        internal Image CheckpointB
        {
            get
            {
                return checkpointB;
            }

            set
            {
                checkpointB = value;
            }
        }

        public SoundEffect ExplosaoSound
        {
            get
            {
                return explosaoSound;
            }

            set
            {
                explosaoSound = value;
            }
        }

        public List<SoundEffectInstance> ExplosaoInstance
        {
            get
            {
                return explosaoInstance;
            }

            set
            {
                explosaoInstance = value;
            }
        }

        public int BandeirBAy
        {
            get
            {
                return bandeirBAy;
            }

            set
            {
                bandeirBAy = value;
            }
        }

        public int BandeirBAx
        {
            get
            {
                return bandeirBAx;
            }

            set
            {
                bandeirBAx = value;
            }
        }

        public int BandeirAAx
        {
            get
            {
                return bandeirAAx;
            }

            set
            {
                bandeirAAx = value;
            }
        }

        public int BandeirAAy
        {
            get
            {
                return bandeirAAy;
            }

            set
            {
                bandeirAAy = value;
            }
        }

        public Cenario(ContentManager Content, int width, int height, int id)
        {
            casas = new List<Image>();
            casasDestruidas = new List<Image>();
            baseMuros = new List<Image>();
            rua = new List<Image>();
            calcada = new List<Image>();
            portao = new List<Image>();
            explosaoInstance = new List<SoundEffectInstance>();
            screenWidht = width;
            screenHeight = height;
            content = Content;
            this.id = id;
        }

        public void Initialize()
        {
        }

        public void LoadContent(GraphicsDevice graphic, SpriteBatch spriteBatch)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            this.spriteBatch = spriteBatch;
            GD.instance = graphic;
            explosaoSound = content.Load<SoundEffect>("Sound/impactoSom");
            chao = new Image(content, "Cenario/calcada", -100, -100, screenWidht, screenHeight);
            chaoBaseEixo = new Image(content, "Cenario/Base/concreto", 0, 0, 120, 100);
            chaoBaseAliado = new Image(content, "Cenario/Base/concreto", 670, 490, 120, 100);

            switch (id)
            {
                case 1:
                    bandeiraA = new Image(content, "Cenario/Base/flag1", 727, 555, 20, 20);
                    bandeiraB = new Image(content, "Cenario/Base/flag2", 0, 0, 20, 20);
                    checkpointB = new Image(content, "Cenario/Base/checkpoint", 40, 5, 40, 40);
                    checkpointA = new Image(content, "Cenario/Base/checkpoint", 720, 545, 40, 40);
                    break;
                case 2:
                    bandeiraA = new Image(content, "Cenario/Base/flag2", 20, 20, 20, 20);
                    bandeiraB = new Image(content, "Cenario/Base/flag1", 725, 553, 20, 20);
                    checkpointA = new Image(content, "Cenario/Base/checkpoint", 40, 5, 40, 40);
                    checkpointB = new Image(content, "Cenario/Base/checkpoint", 720, 545, 40, 40);
                    break;
            }
            portao.Add(new Image(content, "Cenario/Base/portao", 90, 40, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portao", 100, 40, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portao", 110, 40, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portao", 690, 505, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portao", 680, 505, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portao", 670, 505, 10, 45));
            portao.Add(new Image(content, "Cenario/Base/portaoH", 330, 220, 50, 10));
            portao.Add(new Image(content, "Cenario/Base/portaoH", 410, 360, 50, 10));
            int x = 10;
            int y = 0;
            int contador = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    bool flag = contador != 0 && contador != 31 && contador != 62 && contador != 1 && contador != 9 && contador != 10 && contador != 52 && contador != 53 && contador != 61;
                    if (flag)
                    {
                        casas.Add(new Image(content, "Cenario/casa" + random.Next(1, 6), x, y, 35, 35));
                        casasDestruidas.Add(new Image(content, "Cenario/casaDestruida", x, y, 40, 40));
                    }
                    x += 92;
                    contador++;
                }
                y += 92;
                x = 10;
            }
            x = 320;
            y = 220;
            for (int k = 0; k < 15; k++)
            {
                baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                y += 10;
            }
            x = 460;
            y = 220;
            for (int l = 0; l < 15; l++)
            {
                baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                y += 10;
            }
            x = 380;
            y = 220;
            for (int m = 0; m < 9; m++)
            {
                baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                x += 10;
            }
            x = 322;
            y = 358;
            for (int n = 0; n < 9; n++)
            {
                baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                x += 10;
            }
            baseSolo = new Image(content, "Cenario/Base/concreto", 330, 230, 130, 130);
            repair = new Image(content, "Cenario/Base/checkpointAmmo", 380, 280, 40, 40);
            x = 90;
            y = 0;
            for (int x4 = 0; x4 < 4; x4++)
            {
                for (int x5 = 0; x5 < 3; x5++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    x += 10;
                }
                x = 90;
                y += 10;
            }
            x = 0;
            y = 85;
            for (int x6 = 0; x6 < 12; x6++)
            {
                for (int x7 = 0; x7 < 3; x7++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    y += 10;
                }
                y = 85;
                x += 10;
            }


            x = 0;
            y = -10;
            for (int x8 = 0; x8 < 3; x8++)
            {
                for (int x9 = 0; x9 < 12; x9++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    x += 10;
                }
                x = 0;
                y -= 10;
            }

            x = 0;
            y = 0;
            for (int x8 = 0; x8 < 9; x8++)
            {
                for (int x9 = 0; x9 < 3; x9++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    x += 10;
                }
                x = 0;
                y += 10;
            }


            x = 690;
            y = 550;
            for (int x10 = 0; x10 < 4; x10++)
            {
                for (int x11 = 0; x11 < 3; x11++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    x -= 10;
                }
                x = 690;
                y += 10;
            }
            x = 670;
            y = 495;
            for (int x12 = 0; x12 < 13; x12++)
            {
                for (int x13 = 0; x13 < 3; x13++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    y -= 10;
                }
                y = 495;
                x += 10;
            }
            x = 790;
            y = 495;
            for (int x14 = 0; x14 < 10; x14++)
            {
                for (int x15 = 0; x15 < 3; x15++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    x -= 10;
                }
                x = 790;
                y += 10;
            }

            x = 670;
            y = 610;
            for (int x12 = 0; x12 < 13; x12++)
            {
                for (int x13 = 0; x13 < 3; x13++)
                {
                    baseMuros.Add(new Image(content, "Cenario/Base/muro", x, y, 10, 10));
                    y -= 10;
                }
                y = 610;
                x += 10;
            }

            x = 155;
            y = 40;
            int w = 50;
            int h = 40;
            for (int x16 = 0; x16 < 16; x16++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 0;
            y = 130;
            for (int x17 = 0; x17 < 17; x17++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 0;
            y = 223;
            for (int x18 = 0; x18 < 5; x18++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 515;
            y = 223;
            for (int x19 = 0; x19 < 7; x19++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 0;
            y = 316;
            for (int y0 = 0; y0 < 5; y0++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 515;
            y = 316;
            for (int y1 = 0; y1 < 7; y1++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 0;
            y = 409;
            for (int y2 = 0; y2 < 17; y2++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            x = 0;
            y = 501;
            for (int y3 = 0; y3 < 13; y3++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                x += 50;
            }
            w = 40;
            h = 50;
            x = 55;
            y = 150;
            for (int y4 = 0; y4 < 9; y4++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 90;
            y = 0;
            for (int y5 = 0; y5 < 17; y5++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = 0;
            for (int y6 = 0; y6 < 17; y6++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = -30;
            for (int y7 = 0; y7 < 5; y7++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            y = 410;
            for (int y8 = 0; y8 < 5; y8++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = -30;
            for (int y9 = 0; y9 < 4; y9++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            y = 370;
            for (int contador0 = 0; contador0 < 5; contador0++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = 0;
            for (int contador1 = 0; contador1 < 13; contador1++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = 0;
            for (int contador2 = 0; contador2 < 13; contador2++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x += 93;
            y = 0;
            for (int contador3 = 0; contador3 < 9; contador3++)
            {
                rua.Add(new Image(content, "Cenario/rua", x, y, w, h));
                y += 50;
            }
            x = -300;
            y = -300;
            w = 200;
            h = 200;
            for (int contador4 = 0; contador4 < 8; contador4++)
            {
                for (int contador5 = 0; contador5 < 8; contador5++)
                {
                    calcada.Add(new Image(content, "Cenario/calcada", x, y, w, h));
                    x += 200;
                }
                x = -300;
                y += 200;
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            BandeirBAx = bandeiraB.GetX();
            BandeirBAy = bandeiraB.GetY();
            BandeirAAx = BandeiraA.GetX();
            BandeirAAy = BandeiraA.GetY();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Image current in calcada)
            {
                current.Draw(spriteBatch);
            }
            baseSolo.Draw(spriteBatch);
            chaoBaseEixo.Draw(spriteBatch);
            chaoBaseAliado.Draw(spriteBatch);
            checkpointA.Draw(spriteBatch);
            checkpointB.Draw(spriteBatch);
            repair.Draw(spriteBatch);
            foreach (Image i in rua)
            {
                i.Draw(spriteBatch);
            }

            foreach (Image i in portao)
            {
                i.Draw(spriteBatch);
            }

            foreach (Image i in casasDestruidas)
            {
                i.Draw(spriteBatch);
            }
            foreach (Image i in this.casas)
            {
                i.Draw(this.spriteBatch);
            }
            foreach (Image i in baseMuros)
            {
                i.Draw(spriteBatch);
            }

            bandeiraB.Draw(spriteBatch);
            bandeiraA.Draw(spriteBatch);
        }
    }
}
