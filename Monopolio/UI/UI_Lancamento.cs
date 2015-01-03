using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monopolio
{
    public class UI_Lancamento : UI_Centrado
    {

        private string texto;

        private int dado1;
        private int dado2;

        private int bordaLateral, bordaSuperior;

        private int contador, diferenca;

        private Lancamento lancamento;

        public UI_Lancamento(string nomeTextura, bool ativa)
        {
            base.nomeTextura = nomeTextura;
            base.ativa = ativa;
            base.modal = true;
            this.texto = "Your dice are rolling..";

            this.bordaLateral = 27;
            this.bordaSuperior = 25;

            dado1 = 0;
            dado2 = 0;

            contador = 0;
            diferenca = 1;

            lancamento = new Lancamento();
            lancamento.dado1 = 0;
            lancamento.dado2 = 0;
        }

        
        public override Lancamento Update(Random geradorRandom)
        {
            if (diferenca < 10)
            {
                if (contador > 2 * diferenca)
                {
                    dado1 = geradorRandom.Next(1, 7);
                    dado2 = geradorRandom.Next(1, 7);
                    contador = 0;
                    diferenca++;
                }
                contador++;
            }
            else
            {
                if (diferenca > 100)
                {

                    lancamento.dado1 = dado1;
                    lancamento.dado2 = dado2;
                }
                else
                {
                    diferenca++;
                }
            }

            //lancamento.dado1 = 1;
            //lancamento.dado2 = 1;
            return lancamento;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera, SpriteFont arial12, List<Jogador> listaJogadores, Tabuleiro tabuleiro, Jogador jogadorAtual)
        {
            spriteBatch.Draw(base.textura,
                            new Rectangle((int)base.posicao.X, (int)base.posicao.Y, textura.Width, textura.Height),
                            null,
                            Color.White,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            1f);
            /*
            Foi necessário usar este construtor por causa do ultimo parametro, que define a "camada" em que fica a textura.
            Sem esta definição estrava em conflito com o texto escrito por cima
            */

            spriteBatch.DrawString(arial12, this.texto, new Vector2(base.posicao.X + bordaLateral, base.posicao.Y + bordaSuperior), Color.White);

            spriteBatch.DrawString(arial12, dado1.ToString(), new Vector2(base.posicao.X + bordaLateral, base.posicao.Y + bordaSuperior * 3), Color.White);
            spriteBatch.DrawString(arial12, dado2.ToString(), new Vector2(base.posicao.X + bordaLateral * 2, base.posicao.Y + bordaSuperior * 3), Color.White);

        }

    }
}
