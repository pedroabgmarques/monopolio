using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class UI_Jogadores : UI
    {

        StringBuilder texto;
        private Vector2 offset;

        public UI_Jogadores(string nomeTextura, bool modal)
        {
            base.posicao = Vector2.Zero;
            base.nomeTextura = nomeTextura;
            this.texto = new StringBuilder();
            base.ativa = false;
            base.modal = modal;

            this.offset = new Vector2(490, 0);
        }

        Color cor = Color.White;
        int diferenca = 0;
        public override void Draw(SpriteBatch spriteBatch, Camera camera, SpriteFont arial12, List<Jogador> listaJogadores, Tabuleiro tabuleiro, Jogador jogadorAtual)
        {
            spriteBatch.Draw(base.textura,
                            new Rectangle((int)base.posicao.X + (int)offset.X, (int)base.posicao.Y + (int)offset.Y, textura.Width, textura.Height),
                            null,
                            Color.White,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            1);
            /*
            Foi necessário usar este construtor por causa do ultimo parametro, que define a "camada" em que fica a textura.
            Sem esta definição estrava em conflito com o texto escrito por cima
            */

            texto.Append("Jogadores:");
            texto.AppendLine();
            texto.Append("----------------------------------------------------------------");
            texto.AppendLine();

            int contador = 0;
            foreach (Jogador jogador in listaJogadores)
            {
                texto.Append(jogador.Nome);
                texto.Append(" (");
                texto.Append(jogador.CasaAtual);
                texto.Append(" )");
                texto.AppendLine();
                texto.Append(jogador.Dinheiro.ToString());
                texto.Append(" Euro");
                texto.AppendLine();
                texto.Append(tabuleiro.Casa(jogador.CasaAtual).Nome);
                texto.AppendLine();

                contador++;
                if (contador < listaJogadores.Count)
                {
                    texto.Append("----------------------------------------------------------------");
                }

                if (jogador == jogadorAtual)
                {
                    cor = Color.Green;
                }
                else
                {
                    cor = Color.White;
                }

                if (contador == 1)
                {
                    diferenca = 0;
                }
                else
                {
                    diferenca = (50 * (contador-1)) + 25;
                }
                spriteBatch.DrawString(arial12, texto, new Vector2(base.posicao.X + 27 + offset.X, base.posicao.Y + 25 + offset.Y + diferenca), cor);
                
                texto.Clear();
            }
            diferenca = 0;

            
            
        }
    }
}
