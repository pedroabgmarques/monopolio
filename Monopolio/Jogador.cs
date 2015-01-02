/*
 * Author: Pedro Marques
 * Date: 09/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere um jogador
 * */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Estado, Construtor e Métodos do jogador
    /// </summary>
    public class Jogador
    {

        #region Estado
        /// <summary>
        /// Nome do jogador
        /// </summary>
        private string nome;
        /// <summary>
        /// Devolve o nome do jogador
        /// </summary>
        public string Nome
        {
            get { return nome; }
        }
        
        /// <summary>
        /// Quantidade de dinheiro que o jogador possui
        /// </summary>
        private float dinheiro;
        /// <summary>
        /// Devolve a quantidade de dinheiro que o jogador possui
        /// </summary>
        public float Dinheiro
        {
            get { return dinheiro; }
        }
        /// <summary>
        /// Indice da casa em que o jogador se encontra
        /// </summary>
        private int casaAtual;
        /// <summary>
        /// Devolve o indice da casa em que o jogador se encontra
        /// </summary>
        public int CasaAtual
        {
            get { return casaAtual; }
            set { this.casaAtual = value; }
        }

        /// <summary>
        /// Lista de propriedades que o jogador possui
        /// </summary>
        private List<Propriedade> listaPropriedades;
        /// <summary>
        /// Devolve a lista de propriedades que o jogador possui
        /// </summary>
        public List<Propriedade> ListaPropriedades
        {
            get { return listaPropriedades; }
        }

        private int nVoltas;
        public int NVoltas
        {
            get { return nVoltas; }
            set { nVoltas = value; }
        }

        private bool primeiraVolta;
        public bool PrimeiraVolta
        {
            get { return primeiraVolta; }
            set { primeiraVolta = value; }
        }

        private int ultimoLancamento;
        public int UltimoLancamento
        {
            get { return ultimoLancamento; }
            set { ultimoLancamento = value; }
        }

        private Texture2D token;

        public Texture2D Token
        {
            get { return token; }
            set { token = value; }
        }

        public Vector2 Posicao;

        private Vector2 offsetPosicao;

        public Vector2 OffsetPosicao
        {
            get { return offsetPosicao; }
            set { offsetPosicao = value; }
        }
        

        private int nJogador;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor do Jogador
        /// </summary>
        public Jogador(string nome, int nJogador)
        {
            this.nome = nome;
            this.dinheiro = 2 * 500 + 2 * 100 + 2 * 50 + 6 * 20 + 5 * 10 + 5 * 5 + 5 * 1;
            this.casaAtual = 0;
            this.listaPropriedades = new List<Propriedade>();
            this.nVoltas = 0;
            this.primeiraVolta = true;
            this.nJogador = nJogador;
        }
        #endregion

        #region LoadContent
        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            token = Content.Load<Texture2D>("texturas/tabuleiro/tokens/token"+nJogador);
            this.Posicao = new Vector2(graphics.Viewport.Width / 2,
                                                    graphics.Viewport.Height / 2);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Retira uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a retirar ao jogador</param>
        public void pagar(float valor)
        {
            this.dinheiro -= valor;
        }

        /// <summary>
        /// Adiciona uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a adicionar ao jogador</param>
        public void receber(int valor)
        {
            this.dinheiro += valor;
        }

        /// <summary>
        /// Move o jogador para uma determinada casa
        /// </summary>
        /// <param name="casa">Indice da casa para a qual se pretende mover o jogador</param>
        public void goTo(int casa)
        {
            this.casaAtual = casa;
        }

        /// <summary>
        /// Adiciona uma propriedade à lista de propriedades do jogador
        /// </summary>
        /// <param name="propriedade">Propriedade a adicionar à lista de propriedades do jogador</param>
        public void adicionarPropriedade(Propriedade propriedade)
        {
            propriedade.Dono = this;
            listaPropriedades.Add(propriedade);
            pagar(propriedade.Custo);
            //adicionar propriedade a UI

        }

        /// <summary>
        /// Retira uma propriedade à lista de propriedades do jogador
        /// </summary>
        /// <param name="propriedade">Propriedade a retirar à lista de propriedades do jogador</param>
        public void retirarPropriedade(Propriedade propriedade)
        {
            propriedade.Dono = null;
            listaPropriedades.Remove(propriedade);
        }

        /// <summary>
        /// Devolve o nº de estaçoes que um jogador possui
        /// </summary>
        /// <returns></returns>
        public int nEstacoes()
        {
            int nEstacoes = 0;
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Utilidade)
                {
                    utilidade = (Utilidade)propriedade;
                    if (utilidade.Tipo == Tipo.Estação)
                    {
                        nEstacoes++;
                    }
                }
            }
            return nEstacoes;
        }

        /// <summary>
        /// Devolve o nº de companhias eletricas que um jogador tem
        /// </summary>
        /// <returns></returns>
        Utilidade utilidade;
        public int nEletricasEAguas()
        {
            int nEletricasEAguas = 0;
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Utilidade)
                {
                    utilidade = (Utilidade)propriedade;
                    if (utilidade.Tipo == Tipo.Eletricidade || utilidade.Tipo == Tipo.Água)
                    {
                        nEletricasEAguas++;
                    }
                }
            }
            return nEletricasEAguas;
        }

        /// <summary>
        /// Devolve o total de dinheiro e investimentos de um jogador
        /// </summary>
        /// <returns>Soma total e dinheiro e investimentos</returns>
        public float totalAssets()
        {
            float soma = 0;
            //Somar o dinheiro vivo
            soma += dinheiro;
            //Somar o preço de compra de todas as propriedades, independentemente de estarem ou não hipotecadas
            foreach (Propriedade propriedade in listaPropriedades)
            {
                soma += propriedade.Custo;
            }
            //Somar o preço de compra de todas as casas
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Rua)
                {
                    Rua rua = (Rua)propriedade;
                    soma += rua.NCasas * rua.CustoCasa();
                }
            }

            return soma;
        }


        #endregion

        #region Draw
        /// <summary>
        /// Desenha o token do jogador
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Jogador jogadorAtual)
        {
            if (jogadorAtual == this)
            {
                spriteBatch.Draw(token, Posicao + offsetPosicao, null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            else
            {
                spriteBatch.Draw(token, Posicao + offsetPosicao, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            }
            
        }
        #endregion

    }
}
