using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

/*
 *  [AnimationSprites.cs]  - Classe destinada para exibição de animações de sprites na tela do jogo
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *  
 */

namespace gameutil2d.classes.basic
{
    class AnimationSprites : GameElement
    {
        List<Image> aImage;

        int currentFrame;

        int MAX_FRAMES;

        int index_image;

        ContentManager content;

        bool animationStoped;

        bool playAnimation = false;

        bool isLoop;


        public AnimationSprites(ContentManager c, int x, int y, int width, int height)
        {

            aImage = new List<Image>();
            content = c;
            currentFrame = 0;

            index_image = 0;

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            animationStoped = true;
            isLoop = false;
        }



        public void Add(String pathName)
        {
            aImage.Add(new Image(content, pathName, x, y, width, height));
        }




        public void Draw(SpriteBatch spriteBatch, float angle, int ax, int ay)
        {

            if (!animationStoped)
            {

                currentFrame++;

                if (currentFrame == MAX_FRAMES)
                {


                    currentFrame = 0;


                    index_image++;



                    if (index_image == aImage.Count)
                    {

                        if (isLoop)
                            index_image = 0;
                        else
                        {
                            index_image--;
                            playAnimation = false;
                        }

                    }


                }

            }

            aImage[index_image].Draw(spriteBatch, angle, ax, ay,0.2f);


        }


        public void Draw(SpriteBatch spriteBatch, bool flip_effect)
        {

            if (!animationStoped)
            {

                currentFrame++;

                if (currentFrame == MAX_FRAMES)
                {


                    currentFrame = 0;


                    index_image++;



                    if (index_image == aImage.Count)
                    {

                        if (isLoop)
                            index_image = 0;
                        else
                        {
                            index_image--;
                            playAnimation = false;
                        }

                    }


                }

            }

            if (flip_effect)
                aImage[index_image].Draw(spriteBatch, true);
            else
                aImage[index_image].Draw(spriteBatch);



        }




        public void Start(int frames, bool loop)
        {

            currentFrame = 0;
            MAX_FRAMES = frames;
            animationStoped = false;
            isLoop = loop;

            index_image = 0;

            playAnimation = true;

        }

        public void Stop()
        {
            animationStoped = true;
            playAnimation = false;
        }


        public override void SetX(int x)
        {
            this.x = x;
            //Altera todas as imagens
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].SetX(x);
            }
        }

        public override void SetY(int y)
        {
            this.y = y;
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].SetY(y);
            }
        }

        public override void SetWidth(int w)
        {
            this.width = w;
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].SetWidth(w);
            }
        }

        public override void SetHeight(int h)
        {
            this.height = h;
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].SetHeight(h);
            }
        }

        public override void MoveByX(int value)
        {

            this.x += value;
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].MoveByX(value);
            }

        }

        public override void MoveByY(int value)
        {

            this.y += value;
            for (int index = 0; index < aImage.Count; index++)
            {
                aImage[index].MoveByY(value);
            }

        }

        public bool IsPlaying()
        {

            if (isLoop)
                return true;
            else
            {

                return playAnimation;

            }

        }

        public int GetCount()
        {
            return aImage.Count;
        }


    }
}