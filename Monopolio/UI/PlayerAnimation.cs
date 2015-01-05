using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    class PlayerAnimation
    {

        private Jogador jogador;
        private int alpha;
        private bool viva;
        private Vector2 posicao;
        public bool Viva
        {
            get { return viva; }
        }


        public PlayerAnimation(Jogador jogador, GraphicsDevice graphics)
        {
            this.jogador = jogador;
            this.alpha = 255;
            this.viva = true;
            this.posicao.X = (graphics.Viewport.Width / 2) - (jogador.PlayerSplash.Width / 2);
            this.posicao.Y = ((graphics.Viewport.Height - jogador.PlayerSplash.Height) / 2) - 200;
        }

        public void Update()
        {
            if (viva)
            {
                alpha -= 2;
                if (alpha <= 0)
                {
                    viva = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (viva)
            {
                spriteBatch.Draw(jogador.PlayerSplash, this.posicao, new Color(0, 0, 0, alpha));
            }
        }
    }
}
