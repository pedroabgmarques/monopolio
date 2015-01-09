/*
 * Author: Pedro Marques
 * Date: 07/11/204
 * Email: pedroabgmarques@gmail.com / a10855@alunos.ipca.pt
 * Description:
 * 
 * Implementa uma camera 2D com capacidades de movimento, zoom e rotação
 * Com elementos do artigo "2D Camera with parallax scrolling in XNA" de David Gouveia
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Monopolio
{

    /// <summary>
    /// Estado, Construtor e Metodos da Camara
    /// </summary>
    public class Camera
    {

        #region Estado

        private float zoom;

        private const float maxZoom = 0.34f;
        /// <summary>
        /// Devolve o valor atual de zoom da camara
        /// </summary>
        /// <returns>Valor de zoom</returns>
        public float getZoom()
        {
            return zoom;
        }
        /// <summary>
        /// Setter do valor de zoom da camara
        /// </summary>
        /// <param name="zoom">Valor de zoom</param>
        public void setZoom(float zoom)
        {
            this.zoom = zoom;
        }
        
        private Viewport viewport;
        private Vector2 origem;
        private Matrix ViewMatrix;
        /// <summary>
        /// Getter / Setter da matrix de transformação da camara
        /// </summary>
        public Matrix Transform
        {
            get { return ViewMatrix; }
            set { ViewMatrix = value; }
        }

        private Vector2 posicao;
        /// <summary>
        /// Getter / Setter da posição da camara
        /// </summary>
        public Vector2 Posicao
        {
            get { return posicao; }
            set { 
                posicao = value;
            }
        }

        private Rectangle? limits;
        /// <summary>
        /// Getter / Setter dos limites da camara
        /// </summary>
        public Rectangle? Limits
        {
            get
            {
                return limits;
            }
            set
            {
                limits = value;
            }
        }

        private float rotacao;
        /// <summary>
        /// getter da rotação da camara
        /// </summary>
        /// <returns>Rotação da camara</returns>
        public float getRotacao()
        {
            return this.rotacao;
        }
        /// <summary>
        /// Setter da rotação da camara
        /// </summary>
        /// <param name="rotacao">Rotação da camara</param>
        public void setRotacao(float rotacao)
        {
            this.rotacao = rotacao;
        }

        bool rotating;

        private Vector2 posicaoCentral;
        /// <summary>
        /// Devolve a posição central da camara
        /// </summary>
        /// <returns>Posição central da camara</returns>
        public Vector2 getPosicaoCentral()
        {
            return posicaoCentral;
        }
        
        #endregion

        #region Construtor
        /// <summary>
        /// Constrói uma nova camara
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        /// <param name="tabuleiro">Tabuleiro do jogo</param>
        public Camera(GraphicsDevice graphics, Tabuleiro tabuleiro)
        {
            this.viewport = graphics.Viewport;

            posicaoCentral = new Vector2(graphics.Viewport.Width / 2 + 140,
                                    graphics.Viewport.Height / 2 + 420);

            //Valores iniciais
            this.Posicao = posicaoCentral;
            setZoom(Zoom.closeUp);
            this.rotacao = MathHelper.ToRadians(0);
            //origem  = centro do ecrã
            this.origem = new Vector2(this.viewport.Width / 2.0f,
                                        this.viewport.Height / 2.0f);

            this.rotacaoTarget = rotacao;
            this.rotating = false;
        }
        #endregion

        #region Update

        /// <summary>
        /// Guarda a rotação atual da camara
        /// </summary>
        float rotacaoAtual;
        /// <summary>
        /// Guarda a rotação de deverá ser somada à camara
        /// </summary>
        float rotacaoASomar;

        /// <summary>
        /// Atualiza a posição, zoom e rotação da camara
        /// </summary>
        /// <param name="estadoTeclado">Estado atual do teclado</param>
        /// <param name="graphics">Graphics device></param>
        /// <param name="tabuleiro">Tabuleiro do jogo</param>
        /// <param name="cameraAnimationManager">Uma instância de cameraAnimationManager</param>
        public void Update(KeyboardState estadoTeclado, GraphicsDevice graphics, Tabuleiro tabuleiro, CameraAnimationManager cameraAnimationManager)
        {
            ////Posicao
            //if (estadoTeclado.IsKeyDown(Keys.Left))
            //{
            //    //Seta para a esquerda
            //        Move(new Vector2(-5f / getZoom(), 0f));
            //}
            //if (estadoTeclado.IsKeyDown(Keys.Right))
            //{
            //    //Seta para a direita
            //        Move(new Vector2(+5f / getZoom(), 0f));
            //}
            //if (estadoTeclado.IsKeyDown(Keys.Up))
            //{
                
            //        Move(new Vector2(0f, -5f / getZoom()));
            //}
            //if (estadoTeclado.IsKeyDown(Keys.Down))
            //{
                
            //        Move(new Vector2(0f, +5f / getZoom()));
            //}

            //Escala
            if (estadoTeclado.IsKeyDown(Keys.Z))
            {
                //Todo o tabuleiro
                cameraAnimationManager.newAnimation(new Vector2(graphics.Viewport.Width / 2 + 140,
                                        graphics.Viewport.Height / 2 + 420),
                            Zoom.longe);
            }
            if (estadoTeclado.IsKeyDown(Keys.X))
            {
                //Médio
                cameraAnimationManager.newAnimation(Zoom.medio);
            }
            if (estadoTeclado.IsKeyDown(Keys.C))
            {
                //Perto
                cameraAnimationManager.newAnimation(Zoom.perto);
            }
            if (estadoTeclado.IsKeyDown(Keys.V))
            {
                //Close-Up
                cameraAnimationManager.newAnimation(Zoom.closeUp);
            }

            //Rotação
            if (estadoTeclado.IsKeyDown(Keys.A))
            {
                if(cameraAnimationManager.getQueuedAnimations() == 0)
                    cameraAnimationManager.newAnimation(rotacao + MathHelper.ToRadians(-90), true);
            }

            if (estadoTeclado.IsKeyDown(Keys.D))
            {
                if (cameraAnimationManager.getQueuedAnimations() == 0)
                    cameraAnimationManager.newAnimation(rotacao + MathHelper.ToRadians(90), true);
            }

        }
        #endregion

        #region Helpers

        /// <summary>
        /// Ativa a rotação da camara
        /// </summary>
        /// <param name="degrees">Número de graus que a camara deve rodar</param>
        public void activateRotation(int degrees)
        {
            if (!rotating)
            {
                rotacaoAtual = rotacao;
                rotacaoASomar = MathHelper.ToRadians(degrees);
                rotateTo(rotacaoAtual + rotacaoASomar);
                rotating = true;
            }
        }

        /// <summary>
        /// Rotação desejada para a camara
        /// </summary>
        float rotacaoTarget;
        /// <summary>
        /// Efetua uma rotação "artesanal" da camara
        /// </summary>
        /// <param name="rotacao"></param>
        public void rotateTo(float rotacao)
        {
            this.rotacaoTarget += rotacao;
        }

        /// <summary>
        /// Devolve a view matrix
        /// </summary>
        /// <param name="parallax">Parallax a aplicar a camara</param>
        /// <returns>View Matrix usada para desenhar no ecrã</returns>
        public Matrix getTransformation(Vector2 parallax)
        {
            ViewMatrix =
                    Matrix.CreateTranslation(new Vector3(-Posicao * parallax, 0.0f)) *
                    Matrix.CreateTranslation(new Vector3(-origem, 0.0f)) *
                    Matrix.CreateRotationZ(rotacao) *
                    Matrix.CreateScale(getZoom(), getZoom(), 1) *
                    Matrix.CreateTranslation(new Vector3(origem, 0.0f));
                        
            return ViewMatrix;
        }

        /// <summary>
        /// Devolve a view matrix sem rotação
        /// </summary>
        /// <param name="parallax">Parallax a aplicar à camara</param>
        /// <returns>View Matrix usada para desenhar no ecrã</returns>
        public Matrix getTransformationNoRotation(Vector2 parallax)
        {
            ViewMatrix =
                    Matrix.CreateTranslation(new Vector3(-Posicao * parallax, 0.0f)) *
                    // The next line has a catch. See note below.
                    Matrix.CreateTranslation(new Vector3(-origem, 0.0f)) *
                    Matrix.CreateScale(getZoom(), getZoom(), 1) *
                    Matrix.CreateTranslation(new Vector3(origem, 0.0f));

            return ViewMatrix;
        }

        #endregion

        //public void Move(Vector2 displacement)
        //{
        //    displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-rotacao));
        //    posicaoTarget += displacement;
        //}
    }
}
