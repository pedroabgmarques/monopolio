using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    /// <summary>
    /// Gere as animações da camera, de forma a permitir automatizar séries de movimentos
    /// </summary>
    public class CameraAnimationManager
    {

        #region Estado
        /// <summary>
        /// Fila de animações
        /// </summary>
        private Queue animations;
        /// <summary>
        /// Objeto reutilizado para criar animações e inseri-las na fila
        /// </summary>
        private Animation tempAnimation;
        /// <summary>
        /// Animação que está a ser executada num determinado momento
        /// </summary>
        private Animation activeAnimation;

        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public CameraAnimationManager()
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
        /// <param name="zoomTarget">Zoom desejado</param>
        /// <param name="rotacaoTarget">Rotação desejada</param>
        public void newAnimation(Vector2 posicaoTarget, float zoomTarget, float rotacaoTarget, Action<string> accao = null)
        {
            tempAnimation = new Animation(posicaoTarget, zoomTarget, rotacaoTarget, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }

        /// <summary>
        /// Insere na fila uma nova animação de posição
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        public void newAnimation(Vector2 posicaoTarget, Action<string> accao = null)
        {
            tempAnimation = new Animation(posicaoTarget, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }

        /// <summary>
        /// Insere na fila uma nova animação de posição e zoom
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// /// <param name="zoom">Zoom desejada</param>
        public void newAnimation(Vector2 posicaoTarget, float zoom, Action<string> accao = null)
        {
            tempAnimation = new Animation(posicaoTarget, zoom, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }

        /// <summary>
        /// Insere uma nova animação de posicao e rotação
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// <param name="rotacaoTarget">Rotação desejada</param>
        /// <param name="v">Hack foleiro para distinguir contrutores</param>
        /// <param name="accao">Código a executar no final da animação</param>
        public void newAnimation(Vector2 posicaoTarget, float rotacaoTarget, bool v, Action<string> accao = null)
        {
            tempAnimation = new Animation(posicaoTarget, rotacaoTarget, true, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }

        /// <summary>
        /// Insere na fila uma nova animação de zoom
        /// </summary>
        /// <param name="zoomTarget">Zoom desejado</param>
        public void newAnimation(float zoomTarget, Action<string> accao = null)
        {
            tempAnimation = new Animation(zoomTarget, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }

        /// <summary>
        /// Insere uma nova animaçao de rotação
        /// </summary>
        /// <param name="rotacaoTarget">Rotação desejada</param>
        /// <param name="v">Hack para distinguir construtores</param>
        /// <param name="accao">Código a executar no final da animação</param>
        public void newAnimation(float rotacaoTarget, bool v, Action<string> accao = null)
        {
            tempAnimation = new Animation(rotacaoTarget, v, accao);
            animations.Enqueue(tempAnimation);
            Console.WriteLine("Animação adicionada!");
        }
        #endregion

        #region Update
        /// <summary>
        /// Atualiza a animação atual
        /// </summary>
        /// <param name="camera">Uma instância da camera</param>
        public void Update(Camera camera)
        {

            if (activeAnimation != null)
            {
                //Temos uma animação ativa, executá-la
                activeAnimation.Update(camera);
                //Verificar se ainda está ativa
                if (!activeAnimation.Moving && !activeAnimation.Zooming && !activeAnimation.Rotating)
                {
                    //A animação acabou
                    //Se tem accao, executá-la
                    if (activeAnimation.getAccao() != null)
                    {
                        activeAnimation.getAccao().Invoke(activeAnimation.getAccao().Method.ToString());
                    }
                    //Passar para a próxima animação
                    getNextAnimation(camera);
                }
                
            }
            else
            {
                //Não temos uma animação ativa
                if (animations.Count > 0)
                {
                    getNextAnimation(camera);
                }
                else
                {
                    //Não há animações pendentes
                    activeAnimation = null;
                }
            }
        }

        /// <summary>
        /// Devolve a próxima animação a executar e inicia os seus valores com valores atuais da camera
        /// </summary>
        /// <param name="camera">Uma instância da camera</param>
        private void getNextAnimation(Camera camera)
        {
            if (animations.Count > 0)
            {
                activeAnimation = (Animation)animations.Dequeue();
                if (!activeAnimation.Moving)
                {
                    activeAnimation.setPosicaoTarget(camera.Posicao);
                }
                if (!activeAnimation.Zooming)
                {
                    activeAnimation.setZoomTarget(camera.getZoom());
                }
                if (!activeAnimation.Rotating)
                {
                    activeAnimation.setRotacaoTarget(camera.getRotacao());
                }
            }
            else
            {
                activeAnimation = null;
            }
        }
        #endregion

    }
}
