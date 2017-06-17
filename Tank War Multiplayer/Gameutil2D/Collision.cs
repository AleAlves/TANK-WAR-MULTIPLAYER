using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gameutil2d.classes.scene;

/*
 *  [Collision.cs]  - Classe que oferece funções para detecção de colisão entre objetos no jogo
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *   
 */


namespace gameutil2d.classes.basic
{
    class Collision
    {
        public static bool Check(GameElement obj1, GameElement obj2)
        {

            if (((obj1.GetX() + obj1.GetWidth()) >= obj2.GetX()) &&

                    ((obj1.GetX() <= (obj2.GetX() + obj2.GetWidth())) &&

                    ((obj1.GetY() + obj1.GetHeight()) >= obj2.GetY()) &&

                    ((obj1.GetY() <= (obj2.GetY() + obj2.GetHeight())))))

                return true;
            else
                return false;


        }

        public static bool Check(GameElement obj1, int xI, int yI, int widthI, int heightI, GameElement obj2, int xII, int yII, int widthII, int heightII)
        {
            xI = xI - widthI;
            yI = yI - heightI;
            xII = xII - widthII;
            yII = yII - heightII;

            if (((xI + widthI >= xII) &&((xI <= (xII + widthII)) &&((yI + heightI) >= yII) &&((yI <= (yII + heightII))))))
                return true;
            else
                return false;


        }

        public static bool Check(GameElement obj1, int xI, int yI, GameElement obj2)
        {
            xI = xI - (obj1.GetWidth() / 2);
            yI = yI - (obj1.GetHeight() / 2);
            int auxWidth1 = obj1.GetWidth();
            int auxHeigth1 = obj1.GetHeight();
            if (((xI + auxWidth1) >= obj2.GetX()) &&
                ((xI <= (obj2.GetX() + obj2.GetWidth())) &&
                ((yI + auxHeigth1) >= obj2.GetY()) &&
                ((yI <= (obj2.GetY() + obj2.GetHeight())))))
                return true;
            else
                return false;


        }

        public static bool Check(GameElement obj, Scene scene, String any_object_with_tag)
        {
            bool anyCollision = false;

            foreach (GameElement e in scene.Elements())
            {
                if (e.GetTag() == any_object_with_tag)
                {
                    if (Check(obj, e))
                    {
                        anyCollision = true;
                        break;
                    }
                }
            }

            return anyCollision;
        }

        public static GameElement CheckAndReturn(GameElement obj, Scene scene, String any_object_with_tag)
        {
            GameElement hitObject = null;

            foreach (GameElement e in scene.Elements())
            {
                if (e.GetTag() == any_object_with_tag)
                {
                    if (Check(obj, e))
                    {
                        hitObject = e;
                        break;
                    }
                }
            }

            return hitObject;
        }

    }
}
