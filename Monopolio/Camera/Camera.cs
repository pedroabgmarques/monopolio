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
        public float getZoom()
        {
            return zoom;
        }
        public void setZoom(float zoom)
        {
            this.zoom = zoom;
        }
        
        private Viewport viewport;
        private Vector2 origem;
        private Matrix ViewMatrix;
        public Matrix Transform
        {
            get { return ViewMatrix; }
            set { ViewMatrix = value; }
        }

        private Vector2 posicao;
        public Vector2 Posicao
        {
            get { return posicao; }
            set { 
                posicao = value;
            }
        }

        private Rectangle? limits;
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
        public float getRotacao()
        {
            return this.rotacao;
        }
        public void setRotacao(float rotacao)
        {
            this.rotacao = rotacao;
        }

        bool rotating;

        private Vector2 posicaoCentral;
        public Vector2 getPosicaoCentral()
        {
            return posicaoCentral;
        }
        
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public Camera(GraphicsDevice graphics, Tabuleiro tabuleiro)
        {
            this.viewport = graphics.Viewport;

            posicaoCentral = new Vector2(graphics.Viewport.Width / 2 + 140,
                                    graphics.Viewport.Height / 2 + 420);

            //Valores iniciais
            this.Posicao = posicaoCentral;
            setZoom(Zoom.closeUp);
            this.rotacao = MathHelper.ToRadians(0);
            this.origem = new Vector2(this.viewport.Width / 2.0f,
                                        this.viewport.Height / 2.0f);

            this.rotacaoTarget = rotacao;
            this.rotating = false;
        }
        #endregion

        #region Update

        float rotacaoAtual;
        float rotacaoASomar;

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
                activateRotation(-90);
            }

            if (estadoTeclado.IsKeyDown(Keys.D))
            {
                activateRotation(90);
            }

        }
        #endregion

        #region Helpers

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

        float rotacaoTarget;
        public void rotateTo(float rotacao)
        {
            this.rotacaoTarget += rotacao;
        }

        public Matrix getTransformation(Vector2 parallax)
        {
            ViewMatrix =
                    Matrix.CreateTranslation(new Vector3(-Posicao * parallax, 0.0f)) *
                    // The next line has a catch. See note below.
                    Matrix.CreateTranslation(new Vector3(-origem, 0.0f)) *
                    Matrix.CreateRotationZ(rotacao) *
                    Matrix.CreateScale(getZoom(), getZoom(), 1) *
                    Matrix.CreateTranslation(new Vector3(origem, 0.0f));
                        
            return ViewMatrix;
        }

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
