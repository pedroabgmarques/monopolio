using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monopolio
{
    public class UI_Mortgages : UI_Centrado
    {

        private StringBuilder textoIntrodutorio;
        private int bordaLateral, bordaSuperior;
        new private List<Opcao> listaOpcoes;
        private List<Rua> listaRuas;


        public UI_Mortgages(string nomeTextura, bool ativa, List<Opcao> listaOpcoes, OrientacaoOpcoes orientacao, Jogador jogador)
        {
            base.nomeTextura = nomeTextura;
            base.ativa = ativa;
            base.modal = true;
            base.listaOpcoes = listaOpcoes;
            base.orientacaOpcoes = orientacao;
            this.listaOpcoes = listaOpcoes;
            this.orientacaOpcoes = orientacao;
            textoIntrodutorio = new StringBuilder();
            textoIntrodutorio.Append("Below is a list of your properties that can be mortgaged or bought back from the bank.");
            this.listaRuas = jogador.listaRuas();

            this.bordaLateral = 27;
            this.bordaSuperior = 25;

        }

        bool offsetCriado = false;
        public override void Draw(SpriteBatch spriteBatch, Camera camera, SpriteFont arial12, List<Jogador> listaJogadores, Tabuleiro tabuleiro, Jogador jogadorAtual)
        {
            spriteBatch.Draw(base.textura,
                            new Rectangle((int)base.posicao.X, (int)base.posicao.Y, textura.Width, textura.Height),
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


            Texture2D botao;
            StringBuilder tempTexto = new StringBuilder();
            //Desenhar os botões para as opcoes
            foreach (Opcao opcao in this.listaOpcoes)
            {

                botao = base.getBotao(opcao.TipoOpcao, opcao.Hover, opcao.Clique);

                if (!offsetCriado)
                {
                    opcao.rectangulo.X += 30;
                    opcao.rectangulo.Y -= 30;
                    offsetCriado = true;
                }
                

                spriteBatch.Draw(botao, opcao.rectangulo,
                            null,
                            Color.White,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            0.5f);

                spriteBatch.DrawString(arial12, opcao.TextoOpcao.ToString(), new Vector2(
                                                                                opcao.rectangulo.X + 8,
                                                                                opcao.rectangulo.Y + (botao.Height / 2 - 7)),
                                                                              Color.White);


            }

            int contadorRuas = 1;
            foreach (Rua rua in listaRuas)
            {
                tempTexto.Clear();
                tempTexto.Append(rua.Nome);
                spriteBatch.DrawString(arial12, tempTexto, new Vector2(base.posicao.X + bordaLateral, base.posicao.Y + bordaSuperior + 20 * contadorRuas), Color.White);
                contadorRuas++;
            }

            spriteBatch.DrawString(arial12, textoIntrodutorio, new Vector2(base.posicao.X + bordaLateral, base.posicao.Y + bordaSuperior), Color.White);

        }

    }
}

