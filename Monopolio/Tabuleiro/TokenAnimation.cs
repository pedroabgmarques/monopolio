using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    public class TokenAnimation
    {

        #region Estado

        /// <summary>
        /// Posição para onde será movido o token
        /// </summary>
        private Vector2 posicaoTarget;
        public Vector2 PosicaoTarget
        {
            get { return posicaoTarget; }
            set { posicaoTarget = value; }
        }

        private Jogador jogador;

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

        public TokenAnimation(Vector2 posicaoTarget, Jogador jogador)
        {
            PosicaoTarget = posicaoTarget;
            Jogador = jogador;
            this.moving = true;
        }

        #endregion

        #region Update

        public void Update(Tabuleiro tabuleiro)
        {
            if (Math.Round(jogador.Posicao.X, 0) != Math.Round(posicaoTarget.X, 0))
            {
                jogador.Posicao.X = MathHelper.SmoothStep(jogador.Posicao.X, posicaoTarget.X, posicaoStep);
            }
            else if (Math.Round(jogador.Posicao.Y, 0) != Math.Round(posicaoTarget.Y, 0))
            {
                jogador.Posicao.Y = MathHelper.SmoothStep(jogador.Posicao.Y, posicaoTarget.Y, posicaoStep);
            }
            else
            {
                this.moving = false;
            }
        }

        #endregion

    }
}
