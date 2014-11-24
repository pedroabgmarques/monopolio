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
        Vector2 posicaoTarget;
        public void setPosicaoTarget(Vector2 posicaoTarget)
        {
            this.posicaoTarget = posicaoTarget;
        }
        float zoomTarget;
        public void setZoomTarget(float zoom)
        {
            this.zoomTarget = zoom;
        }
        float rotacaoTarget;
        public void setRotacaoTarget(float rotacao)
        {
            this.rotacaoTarget = rotacao;
        }

        float posicaoStep = 0.07f;
        float zoomStep = 0.07f;
        float rotacaoStep = 0.07f;

        /// <summary>
        /// Flag de movimento
        /// </summary>
        private bool moving;
        public bool Moving
        {
            get { return moving; }
        }

        /// <summary>
        /// Flag de zoom
        /// </summary>
        private bool zooming;
        public bool Zooming
        {
            get { return zooming; }
        }

        /// <summary>
        /// Flag de rotação
        /// </summary>
        private bool rotating;
        public bool Rotating
        {
            get { return rotating; }
        }

        /// <summary>
        /// Código a ser executado no final da animação
        /// </summary>
        private Action<string> accao;
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
                camera.Posicao = Vector2.Lerp(camera.Posicao, posicaoTarget, posicaoStep);
            }
            else
            {
                moving = false;
            }
            if (Math.Round(camera.getZoom(), 2) != Math.Round(zoomTarget, 2))
            {
                if (zoomTarget == Zoom.longe && posicaoTarget != camera.getPosicaoCentral())
                {
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
                        camera.setRotacao(0);
                        this.setRotacaoTarget(0);
                    }

                    rotating = false;
                }
            }
        }
        #endregion

    }
}
