using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Monopolio
{

    public enum OrientacaoOpcoes
    {
        Horizontal,
        Vertical
    }

    public class UI_Centrado : UI
    {

        /// <summary>
        /// Stringbuilder utilizado para 
        /// </summary>
        protected StringBuilder texto;

        /// <summary>
        /// Variáveis utilizadas para calcular as bordas entre botões
        /// </summary>
        private int bordaLateral, bordaSuperior, diferencaHorizontal = 0, diferencaVertical = 0;

        /// <summary>
        /// Orientação dos botões - vertical ou horizontal
        /// </summary>
        protected OrientacaoOpcoes orientacaOpcoes;

        public UI_Centrado()
        {
        }

        public UI_Centrado(string nomeTextura, bool ativa, bool modal, StringBuilder texto, List<Opcao> listaOpcoes, OrientacaoOpcoes orientacaOpcoes)
        {
            base.nomeTextura = nomeTextura;
            base.ativa = ativa;
            base.modal = modal;
            this.texto = texto;
            base.listaOpcoes = listaOpcoes;
            this.orientacaOpcoes = orientacaOpcoes;

            this.bordaLateral = 27;
            this.bordaSuperior = 25;
            this.diferencaHorizontal = 0;
            this.diferencaVertical = 0;
        }

        Texture2D botao;
        public void gerarRectangulosBotoes()
        {
            if (listaOpcoes != null)
            {
                //Se este componente de UI tem lista de opções, percorrê-las e criar-lhes um rectângulo que permita
                //detetar intersecções com o rato
                foreach (Opcao opcao in listaOpcoes)
                {
                    botao = base.getBotao(opcao.TipoOpcao, opcao.Hover, opcao.Clique);
                    if (orientacaOpcoes == OrientacaoOpcoes.Horizontal)
                    {

                        opcao.rectangulo = new Rectangle(
                                        (int)base.posicao.X + bordaLateral + diferencaHorizontal,
                                        (int)base.posicao.Y + base.textura.Height - botao.Height - (bordaSuperior + 4),
                                        botao.Width,
                                        botao.Height);
                        diferencaHorizontal += botao.Width + (bordaLateral - 10);
                    }
                    else
                    {
                        opcao.rectangulo = new Rectangle(
                                        (int)base.posicao.X + base.textura.Width - (botao.Width + bordaLateral),
                                        (int)base.posicao.Y + bordaSuperior + diferencaVertical,
                                        botao.Width,
                                        botao.Height);
                        diferencaVertical += botao.Height + (bordaSuperior - 10);
                    }
                }
            }
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
                            1);
            /*
            Foi necessário usar este construtor por causa do ultimo parametro, que define a "camada" em que fica a textura.
            Sem esta definição estrava em conflito com o texto escrito por cima
            */

            
            //Desenhar os botões para as opcoes
            foreach(Opcao opcao in base.listaOpcoes)
            {

                botao = base.getBotao(opcao.TipoOpcao, opcao.Hover, opcao.Clique);

                spriteBatch.Draw(botao, opcao.rectangulo,
                            null,
                            Color.White,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            0.5f);

                spriteBatch.DrawString(arial12, opcao.TextoOpcao.ToString(), new Vector2(
                                                                                opcao.rectangulo.X + 8,
                                                                                opcao.rectangulo.Y + (botao.Height/2 - 7)), 
                                                                              Color.White);

                
            }

            spriteBatch.DrawString(arial12, this.texto, new Vector2(base.posicao.X + bordaLateral, base.posicao.Y + bordaSuperior), Color.White);

        }

    }
}
