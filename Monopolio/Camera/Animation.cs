using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    /// <summary>
    /// Implementa uma animação de camera
    /// </summary>
    class Animation
    {

        #region Estado
        /// <summary>
        /// Posição para onde a camera será movida
        /// </summary>
        Vector2 posicaoTarget;
        /// <summary>
        /// Setter de posição da camera
        /// </summary>
        /// <param name="posicaoTarget"></param>
        public void setPosicaoTarget(Vector2 posicaoTarget)
        {
            this.posicaoTarget = posicaoTarget;
        }
        /// <summary>
        /// Zoom para o qual se deseja mover a camera
        /// </summary>
        float zoomTarget;
        /// <summary>
        /// Setter de zoom desejado
        /// </summary>
        /// <param name="zoom"></param>
        public void setZoomTarget(float zoom)
        {
            this.zoomTarget = zoom;
        }
        /// <summary>
        /// Rotação que se deseja para a camera
        /// </summary>
        float rotacaoTarget;
        /// <summary>
        /// Setter de rotação desejada
        /// </summary>
        /// <param name="rotacao">Rotação desejada</param>
        public void setRotacaoTarget(float rotacao)
        {
            this.rotacaoTarget = rotacao;
        }

        /// <summary>
        /// Velocidade da mudança de posição
        /// </summary>
        float posicaoStep = 0.07f;
        /// <summary>
        /// Velocidade da mudança de zoom
        /// </summary>
        float zoomStep = 0.07f;
        /// <summary>
        /// Velocidade da rotação
        /// </summary>
        float rotacaoStep = 0.07f;

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

        /// <summary>
        /// Flag de zoom
        /// </summary>
        private bool zooming;
        /// <summary>
        /// Getter da flag de zoom
        /// </summary>
        public bool Zooming
        {
            get { return zooming; }
        }

        /// <summary>
        /// Flag de rotação
        /// </summary>
        private bool rotating;
        /// <summary>
        /// Getter da flag de rotacao
        /// </summary>
        public bool Rotating
        {
            get { return rotating; }
        }

        /// <summary>
        /// Código a ser executado no final da animação
        /// </summary>
        private Action<string> accao;
        /// <summary>
        /// Getter do código a ser executado no final da animação
        /// </summary>
        /// <returns></returns>
        public Action<string> getAccao()
        {
            return accao;
        }
        #endregion

        #region Construtor

        /// <summary>
        /// Espécie de construtor privado para inicializar flags
        /// </summary>
        private void activateAnimation(bool moving, bool zooming, bool rotating)
        {
            this.moving = moving;
            this.zooming = zooming;
            this.rotating = rotating;
        }

        /// <summary>
        /// Cria uma animação de posicao, zoom e rotacao
        /// </summary>
        /// <param name="posicaoTarget">Posicao para a qual se deseja mover a camera</param>
        /// <param name="zoomTarget">Zoom desejado</param>
        /// <param name="rotacaoTarget">Rotacao desejada</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(Vector2 posicaoTarget, float zoomTarget, float rotacaoTarget, Action<string> accao = null)
        {
            this.posicaoTarget = posicaoTarget;
            this.zoomTarget = zoomTarget;
            this.rotacaoTarget = rotacaoTarget;
            this.accao = accao;
            activateAnimation(true, true, true);
        }

        /// <summary>
        /// Cria uma animação de posição
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(Vector2 posicaoTarget, Action<string> accao = null)
        {
            this.posicaoTarget = posicaoTarget;
            this.accao = accao;
            activateAnimation(true, false, false);
        }

        /// <summary>
        /// Cria uma animação de zoom
        /// </summary>
        /// <param name="zoomTarget">Zoom desejado</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(float zoomTarget, Action<string> accao = null)
        {
            this.zoomTarget = zoomTarget;
            this.accao = accao;
            activateAnimation(false, true, false);
        }

        /// <summary>
        /// Cria uma animação de posicao e zoom
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// <param name="zoomTarget">Zoom desejado</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(Vector2 posicaoTarget, float zoomTarget, Action<string> accao = null)
        {
            this.posicaoTarget = posicaoTarget;
            this.zoomTarget = zoomTarget;
            this.accao = accao;
            activateAnimation(true, true, false);
        }

        /// <summary>
        /// Cria uma animação de posicao e rotacao
        /// </summary>
        /// <param name="posicaoTarget">Posição desejada</param>
        /// <param name="rotacaoTarget">Rotação desejada</param>
        /// /// <param name="v">Hack estúpido para distinguir os contrutores</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(Vector2 posicaoTarget, float rotacaoTarget, bool v, Action<string> accao = null)
        {
            this.posicaoTarget = posicaoTarget;
            this.rotacaoTarget = rotacaoTarget;
            this.accao = accao;
            activateAnimation(true, false, true);
        }

        /// <summary>
        /// Cria uma animação de rotação
        /// </summary>
        /// <param name="rotacaoTarget">Rotação desejada</param>
        /// <param name="v">Hack foleiro para distinguir os contrutores</param>
        /// <param name="accao">Acção a executar no final da animação</param>
        public Animation(float rotacaoTarget, bool v, Action<string> accao = null)
        {
            this.rotacaoTarget = rotacaoTarget;
            this.accao = accao;
            activateAnimation(false, false, true);
        }
        #endregion

        #region Update
        /// <summary>
        /// Atualiza o estado da animação
        /// </summary>
        public void Update(Camera camera)
        {
            if (Math.Round(camera.Posicao.X, 0) != Math.Round(posicaoTarget.X, 0)
                || Math.Round(camera.Posicao.Y, 0) != Math.Round(posicaoTarget.Y, 0))
            {
                //Ainda não chegámos à posição desejada, continuar a mover
                camera.Posicao = Vector2.Lerp(camera.Posicao, posicaoTarget, posicaoStep);
            }
            else
            {
                //Chegámos à posição desejada, desligar a flag de movimento
                moving = false;
            }
            if (Math.Round(camera.getZoom(), 2) != Math.Round(zoomTarget, 2))
            {
                if (zoomTarget == Zoom.longe && posicaoTarget != camera.getPosicaoCentral())
                {
                    //Se o zoom vai para longe, centrar o tabuleiro para vermos o tabuleiro todo
                    posicaoTarget = camera.getPosicaoCentral();
                }
                camera.setZoom(MathHelper.Lerp(camera.getZoom(), zoomTarget, zoomStep));
            }
            else
            {
                zooming = false;
            }
            if (Math.Abs(camera.getRotacao() - rotacaoTarget) > 0.0001 
                //Hack foleiro para resolver um problema de rotação de +0 para -0?
                //Limita as rotações a um máximo de 270º
                && Math.Abs(camera.getRotacao() - rotacaoTarget) <= MathHelper.ToRadians(270))
            {
                camera.setRotacao(MathHelper.Lerp(camera.getRotacao(), rotacaoTarget, rotacaoStep));
            }
            else
            {
                if (rotating)
                {

                    if ((Math.Abs((float)Math.Round((decimal)camera.getRotacao(), 0)) % (float)Math.Round(MathHelper.Pi, 0) == 0) && Math.Abs((float)Math.Round(camera.getRotacao(), 2)) > 3.14f)
                    {
                        //Demos uma volta completa, rotação 2pi = 0.
                        //Impede a acumulação de erro na rotação
                        camera.setRotacao(0);
                        this.setRotacaoTarget(camera.getRotacao());
                    }

                    rotating = false;
                }
            }
        }
        #endregion

    }
}
