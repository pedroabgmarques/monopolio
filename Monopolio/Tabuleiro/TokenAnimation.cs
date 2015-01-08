using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Descreve a animação de um token de utilizador
    /// </summary>
    public class TokenAnimation
    {

        #region Estado

        /// <summary>
        /// Posição para onde será movido o token
        /// </summary>
        private Vector2 posicaoTarget;
        /// <summary>
        /// Posição desejada para o token
        /// </summary>
        public Vector2 PosicaoTarget
        {
            get { return posicaoTarget; }
            set { posicaoTarget = value; }
        }

        private Jogador jogador;
        /// <summary>
        /// Getter / Setter do jogador que estamos a animar
        /// </summary>
        public Jogador Jogador
        {
            get { return jogador; }
            set { jogador = value; }
        }
        

        /// <summary>
        /// Velocidade da mudança de posição
        /// </summary>
        float posicaoStep = 0.135f;

        /// <summary>
        /// Flag de movimento
        /// </summary>
        private bool moving;
        /// <summary>
        /// Getter da flag de movimento
        /// </summary>
        public bool Moving
        {
            get { return moving; }
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor de uma animação de token
        /// </summary>
        /// <param name="posicaoTarget">Posição para onde se deseja mover o token</param>
        /// <param name="jogador">Instância do jogador</param>
        public TokenAnimation(Vector2 posicaoTarget, Jogador jogador)
        {
            PosicaoTarget = posicaoTarget;
            Jogador = jogador;
            this.moving = true;
        }

        #endregion

        #region Update

        /// <summary>
        /// Atualiza a animação de um token de jogador
        /// </summary>
        /// <param name="tabuleiro">Instância do tabuleiro de jogo</param>
        public void Update(Tabuleiro tabuleiro)
        {

            if (Math.Round(jogador.Posicao.X, 0) != Math.Round(posicaoTarget.X, 0) || Math.Round(jogador.Posicao.Y, 0) != Math.Round(posicaoTarget.Y, 0))
            {

                jogador.Posicao.X = MathHelper.SmoothStep(jogador.Posicao.X, posicaoTarget.X, posicaoStep);
                if (Math.Round(jogador.Posicao.X, 0) == Math.Round(posicaoTarget.X, 0))
                {
                    Console.WriteLine("Animacao X acabou.");
                }
                
                jogador.Posicao.Y = MathHelper.SmoothStep(jogador.Posicao.Y, posicaoTarget.Y, posicaoStep);
                Console.WriteLine("PosicaoY: " + jogador.Posicao.Y + "; targetY: " + posicaoTarget.Y);
                if (Math.Round(jogador.Posicao.Y, 0) == Math.Round(posicaoTarget.Y, 0))
                {
                    Console.WriteLine("Animacao Y acabou.");
                }
                
                
            }
            else
            {
                this.moving = false;
                Console.WriteLine("Animacao de token terminada.");
            }
        }

        

        #endregion

    }
}
