using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

// Refeito por Aleson Alves


namespace gameutil2d.classes.basic
{
    class Image : GameElement
    {
        Texture2D image, image_flipped;

        public Image(ContentManager content, String image_name, int x, int y, int w, int h)
        {
            
            image = content.Load<Texture2D>(image_name);

            SetBounds(x, y, w, h);

        }

        public Image(ContentManager content, String image_name, int x, int y, int w, int h, string tag)
        {


            image = content.Load<Texture2D>(image_name);

            SetBounds(x, y, w, h);

            SetTag(tag);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Rectangle(x, y, width, height), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, bool cursor,int z)
        {
            var alpha = 1000000;
            if (cursor)
                alpha = 2500000;
            spriteBatch.Draw(image, new Rectangle(x, y, width, height), Color.FromNonPremultiplied(100,100,100,alpha));
        }

        public void Draw(SpriteBatch spriteBatch, float angle, int ax, int ay)
        {
            Vector2 location = new Vector2(image.Width / 2, image.Height / 2);
            Rectangle sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Vector2 origin = new Vector2(ax, ay);
            spriteBatch.Draw(image, origin, null, Color.White, angle, location, 0.25f, SpriteEffects.None, 0);
        }


        public void Draw(SpriteBatch spriteBatch, float angle, int ax, int ay, float scale)
        {
            Vector2 location = new Vector2(image.Width / 2, image.Height / 2);
            Rectangle sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Vector2 origin = new Vector2(ax, ay);
            spriteBatch.Draw(image, origin, null, Color.White, angle, location, scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, bool flip_horizontal)
        {
            if (flip_horizontal)
                spriteBatch.Draw(image_flipped, new Rectangle(x, y, width, height), Color.White);
            else
                spriteBatch.Draw(image, new Rectangle(x, y, width, height), Color.White);
        }

    }

}
