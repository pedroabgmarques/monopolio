using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Descreve uma animação de entrada/saída de dinheiro
    /// </summary>
    public class MoneyAnimation
    {
        private Jogador jogador;

        /// <summary>
        /// Getter / Setter do jogador que recebeu / pagou
        /// </summary>
        public Jogador Jogador
        {
            get { return jogador; }
        }
        
        private float alpha;
        private bool viva;
        private float valor;
        /// <summary>
        /// Indica se animação ainda está a decorrer
        /// </summary>
        public bool Viva
        {
            get { return viva; }
            set { viva = value; }
        }

        /// <summary>
        /// Construtor de uma nova animação de entrada / saída de dinheiro
        /// </summary>
        /// <param name="jogador">Jogador</param>
        /// <param name="valor">Valor transferido</param>
        public MoneyAnimation(Jogador jogador, float valor)
        {
            this.jogador = jogador;
            this.alpha = 1;
            this.viva = true;
            this.valor = valor;
        }

        /// <summary>
        /// Atualiza a animação de entrada / saida de dinheiro (fade-out)
        /// </summary>
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

        /// <summary>
        /// Desenha uma animação de entrada / saida de dinheiro
        /// </summary>
        /// <param name="fonte">Fonte a utilizar</param>
        /// <param name="spriteBatch">Instância do spritebatch</param>
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
