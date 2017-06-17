using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gameutil2d.classes.basic;
using Microsoft.Xna.Framework.Graphics;

/*
 *  [Scene.cs]  - Classe que serve para a construir  cenários de um jogo
 *  Ela funciona como um ArrayList que armazena todos os elementos que serão visualizados no jogo
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *  
 */

namespace gameutil2d.classes.scene
{
    class Scene 
    {
        List<GameElement> tela;
	
	public Scene() {
		tela = new List<GameElement>();
	}
	
	public void Add(GameElement element) {
		tela.Add(element);
	}
	
	public GameElement Get(int index) {
		return tela[index];
	}
	
	public List<GameElement> Elements() {
		return tela;
	}
	
	public void Remove(int index) {
        tela.RemoveAt(index);
	}
	
	public void Remove(GameElement element) {
		tela.Remove(element);
	}
	
	public int GetCount() {
		
		return tela.Count();
		
	}
	
	
	public int GetCountByType(String type) {
		
		int c = 0;
		
		foreach(GameElement e in tela)
		{
            
			if(e.GetType().ToString() == type)
				c++;
		}
		
		return c;
		
	}
	
     public int GetCountByTag(String tag) {
		
		int c = 0;
		
		foreach(GameElement e in tela)
		{
			if(e.GetTag() == tag)
				c++;
		}
		
		return c;
			
	}

	
	public void Draw(SpriteBatch spritebatch) {
		
		foreach(GameElement e in tela)
            e.Draw(spritebatch);
	}
	
	public void MoveByX(int value)
	{
		foreach(GameElement element in tela)
		{
			element.MoveByX(value);
		}
	}
	
	public void MoveByY(int value)
	{
        foreach (GameElement element in tela)
		{
			element.MoveByY(value);
		}
	}
	
	public void SetX(int value)
	{
        foreach (GameElement element in tela)
		{
			element.SetX(value);
		}
	}
	
	public void SetY(int value)
	{
        foreach (GameElement element in tela)
		{
			element.SetY(value);
		}
	}

    }
}
