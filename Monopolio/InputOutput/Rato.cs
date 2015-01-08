/*
 * Author: Pedro Marques
 * Date: 07/11/204
 * Email: pedroabgmarques@gmail.com / a10855@alunos.ipca.pt
 * Description:
 * 
 * Guarda o estado e disponibiliza todas as funcionalidades necessárias para o jogo que envolvem o rato.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Monopolio
{
    /// <summary>
    /// Enumerador para os botões do rato
    /// </summary>
    public enum botao
    {
        /// <summary>
        /// Botão esquerdo do rato
        /// </summary>
        Esquerdo,
        /// <summary>
        /// Botão direito do rato
        /// </summary>
        Direito
    }

    /// <summary>
    /// Classe para o evento de clique no botao esquerdo
    /// Devolve a posicao do rato aquando do clique
    /// </summary>
    public class Clique : EventArgs
    {
        private Vector2 posicao;
        /// <summary>
        /// Posição em que se deu o clique
        /// </summary>
        public Vector2 Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }
        private botao botaoClicado;
        /// <summary>
        /// Botão que foi clicado
        /// </summary>
        public botao BotaoClicado
        {
            get { return botaoClicado; }
            set { botaoClicado = value; }
        }

        private botao botaoDesclicado;
        /// <summary>
        /// Getter / Setter do botão que foi desclicado
        /// </summary>
        public botao BotaoDesclicado
        {
            get { return botaoDesclicado; }
            set { botaoDesclicado = value; }
        }
    }

    /// <summary>
    /// Estado, Construtor e Métodos do rato
    /// </summary>
    class Rato
    {

        #region Estado
        private MouseState estado;
        private MouseState estado_anterior;

        private Vector2 posicao;
        public Vector2 Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }

        private bool blocked;
        /// <summary>
        /// Impede os double-clicks
        /// </summary>
        public bool Blocked
        {
            get { return blocked; }
            set { blocked = value; }
        }
        
        
        private Texture2D seta;
        #endregion

        #region Events and Delegates
        public delegate void cliqueHandler(Clique clique);
        public event cliqueHandler clique;

        public delegate void DescliqueHandler(Clique clique);
        public event DescliqueHandler desclique;
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public Rato()
        {
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// Loading de assets
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            seta = Content.Load<Texture2D>("texturas/componentes/seta_rato");
        }
        #endregion

        #region Update
        public void Update()
        {
            //Guardamos o estado anterior do rato para sabermos quando houve um click
            estado_anterior = estado;

            //Atualizar a posicao atual do rato
            estado = Mouse.GetState();
            posicao.X = estado.X;
            posicao.Y = estado.Y;

            /* Detetar cliques no rato
             * Algoritmo:
             * Se (botao esquerdo carregado) E (botão esquerdo não estava carregar no frame anterior) ENTÃO
             * */   
            if (estado.LeftButton == ButtonState.Pressed && estado_anterior.LeftButton != ButtonState.Pressed)
            {
                //Disparar o evento de clique no botão, passando a posicao em que aconteceu o clique e qual o botao
                gerarEventoCliqueBotao(botao.Esquerdo, posicao);
            }
            if (estado.RightButton == ButtonState.Pressed && estado_anterior.RightButton != ButtonState.Pressed)
            {
                //Disparar o evento de clique no botão, passando a posicao em que aconteceu o clique e qual o botao
                gerarEventoCliqueBotao(botao.Direito, posicao);
            }

            /* Detetar descliques no rato */
            if (estado.LeftButton != ButtonState.Pressed && estado_anterior.LeftButton == ButtonState.Pressed)
            {
                //Disparar o evento de desclique no botão, passando a posicao em que aconteceu o clique e qual o botao
                gerarEventoDescliqueBotao(botao.Esquerdo, posicao);
            }
            if (estado.RightButton != ButtonState.Pressed && estado_anterior.RightButton == ButtonState.Pressed)
            {
                //Disparar o evento de clique no botão, passando a posicao em que aconteceu o clique e qual o botao
                gerarEventoDescliqueBotao(botao.Direito, posicao);
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(seta, posicao, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        #endregion

        #region Helpers
        private void gerarEventoCliqueBotao(botao botaoClicado, Vector2 posicao)
        {
            if (!blocked)
            {
                Clique click = new Clique();
                click.Posicao = posicao;
                click.BotaoClicado = botaoClicado;
                clique(click);
            }
        }

        private void gerarEventoDescliqueBotao(botao botaoDesclicado, Vector2 posicao)
        {
            if (!blocked)
            {
                Clique click = new Clique();
                click.Posicao = posicao;
                click.BotaoDesclicado = botaoDesclicado;
                desclique(click);
            }
        }
        #endregion

    }
}
