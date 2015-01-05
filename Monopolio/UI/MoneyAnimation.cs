using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    public class MoneyAnimation
    {
        private Jogador jogador;

        public Jogador Jogador
        {
            get { return jogador; }
        }
        
        private float alpha;
        private bool viva;
        private float valor;
        public bool Viva
        {
            get { return viva; }
            set { viva = value; }
        }


        public MoneyAnimation(Jogador jogador, float valor)
        {
            this.jogador = jogador;
            this.alpha = 1;
            this.viva = true;
            this.valor = valor;
        }

        public void Update()
        {
            if (viva)
            {
                alpha -= 0.001f;
                if (alpha <= 0)
                {
                    viva = false;
                }
            }
        }

        public void Draw(SpriteFont fonte, SpriteBatch spriteBatch)
        {
            if (viva)
            {
                if (valor > 0)
                {
                    spriteBatch.DrawString(fonte, "+"+valor.ToString() + "Euro", new Vector2(jogador.PosicaoStats.X + 200, jogador.PosicaoStats.Y + 25), Color.Green * alpha);
                }
                else
                {
                    spriteBatch.DrawString(fonte, valor.ToString() + "Euro", new Vector2(jogador.PosicaoStats.X + 200, jogador.PosicaoStats.Y + 25), Color.Red * alpha);
                }
                
            }
        }
    }
}
