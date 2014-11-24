/*
 * Date: 08/11/2014
 * Author: David Gouveia
 * 
 * Implementa uma classe simples para guardar as informações de um sprite
 * http://www.david-gouveia.com/portfolio/2d-camera-with-parallax-scrolling-in-xna/
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Monopolio
{
    public class Sprite
    {
        private string nomeTextura;
        private Texture2D textura;
        private Vector2 posicao;
        private Vector2 offset;
        private Vector2 Velocidade;

        public Sprite(string nomeTextura, Vector2 offset, Vector2 velocidade)
        {
            this.nomeTextura = nomeTextura;
            this.offset = offset;
            this.Velocidade = velocidade;
        }

        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            textura = Content.Load<Texture2D>("texturas/parallax/" + nomeTextura);
            this.posicao = new Vector2(graphics.Viewport.Width / 2 - textura.Width / 2,
                                        graphics.Viewport.Height / 2 - textura.Height / 2) + offset;
        }

        public void Update()
        {
            if (Velocidade != Vector2.Zero)
            {
                this.posicao += Velocidade;
                if (posicao.X < -4096)
                {
                    posicao.X = 4096;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (textura != null)
                spriteBatch.Draw(textura, posicao, Color.White);
        }
    }
}
