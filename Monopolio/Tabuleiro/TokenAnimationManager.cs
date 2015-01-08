using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Manager de animações de tokens
    /// </summary>
    public class TokenAnimationManager
    {

        #region Estado
        /// <summary>
        /// Fila de animações
        /// </summary>
        private Queue animations;
        /// <summary>
        /// Objeto reutilizado para criar animações e inseri-las na fila
        /// </summary>
        private TokenAnimation tempAnimation;
        /// <summary>
        /// Animação que está a ser executada num determinado momento
        /// </summary>
        private TokenAnimation activeAnimation;

        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public TokenAnimationManager()
        {
            animations = new Queue();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Devolve todas as animações em lista
        /// </summary>
        /// <returns>Animações em lista</returns>
        public int getQueuedAnimations()
        {
            return animations.Count;
        }

        /// <summary>
        /// Insere na fila uma nova animação de posição, zoom e rotação
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// <param name="jogador">Instância de jogador</param>
        public void newAnimation(Vector2 posicaoTarget, Jogador jogador)
        {
            tempAnimation = new TokenAnimation(posicaoTarget, jogador);
            animations.Enqueue(tempAnimation);
        }
        #endregion

        #region Update
        /// <summary>
        /// Atualiza a animação atual
        /// </summary>
        /// <param name="tabuleiro">Uma instância do tabuleiro</param>
        public void Update(Tabuleiro tabuleiro)
        {

            if (activeAnimation != null)
            {
                //Temos uma animação ativa, executá-la
                activeAnimation.Update(tabuleiro);
                //Verificar se ainda está ativa
                if (!activeAnimation.Moving)
                {
                    //Passar para a próxima animação
                    getNextAnimation();
                }
            }
            else
            {
                //Não temos uma animação ativa
                if (animations.Count > 0)
                {
                    getNextAnimation();
                }
                else
                {
                    //Não há animações pendentes
                    activeAnimation = null;
                }
            }
        }

        
        private void getNextAnimation()
        {
            if (animations.Count > 0)
            {
                activeAnimation = (TokenAnimation)animations.Dequeue();
            }
            else
            {
                activeAnimation = null;
            }
        }
        #endregion

    }
}
