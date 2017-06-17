using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 *  [GameElement.cs]  - Classe genérica, utilizada como classe base para a construção
 *  da maioria das classes presentes dentro dessa framework
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *  
 */

namespace gameutil2d.classes.basic
{
    abstract class GameElement
    {
        public int x;

        public int y;

        protected int width;

        protected int height;

        protected string tag;

        private float angle;

        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        public virtual void SetX(int x)
        {
            this.x = x;

        }

        public virtual void SetY(int y)
        {
            this.y = y;

        }

        public virtual void SetWidth(int width)
        {
            this.width = width;

        }

        public virtual void SetHeight(int height)
        {
            this.height = height;

        }

        public virtual void MoveByX(int x)
        {

            this.x += x;

        }

        public virtual void MoveByY(int y)
        {

            this.y += y;

        }


        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

        }

        public void SetTag(String tag)
        {
            this.tag = tag;
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public int GetX() { return x; }

        public int GetY() { return y; }

        public int GetWidth() { return width; }

        public int GetHeight() { return height; }

        public string GetTag() { return tag; }

        public float GetAngle() { return angle; }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public bool IsClicked(int posx, int posy)
        {

            if ((posx >= x) && (posx <= x + width)
            && (posy >= y) && (posy <= y + height))
                return true;
            else
                return false;
        }




    }
}
