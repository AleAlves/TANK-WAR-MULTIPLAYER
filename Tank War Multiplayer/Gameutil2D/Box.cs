using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *  [Box.cs]  - Classe que funciona como ponto de colisão para personagens em um jogo,
 *  utilizado em conjunto com objetos de instância de classe Scene e Character
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *  
 */

namespace gameutil2d.classes.basic
{
    class Box : GameElement
    {
        public Box(int x, int y, int w, int h)
        {
            SetBounds(x, y, w, h);
        }

        public Box(int x, int y, int w, int h, String tag)
        {
            SetBounds(x, y, w, h);
            SetTag(tag);
        }
    }
}
