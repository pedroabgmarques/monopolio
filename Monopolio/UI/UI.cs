/*
 * Author: Pedro Marques
 * Date: 09/11/204
 * Email: pedroabgmarques@gmail.com / a10855@alunos.ipca.pt
 * Description:
 * 
 * Faz a gestão de toda a UI do jogo
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Monopolio
{
    /// <summary>
    /// Estado, Construtor e Métodos da UI
    /// </summary>
    public class UI
    {

        #region Estado
        protected Vector2 posicao;
        protected string nomeTextura;
        protected Texture2D textura;

        protected List<Opcao> listaOpcoes;
        public List<Opcao> getListaOpcoes()
        {
            return listaOpcoes;
        }

        protected Texture2D botaoBom, botaoNeutro, botaoMau, botaoBom_hover, botaoNeutro_hover, botaoMau_hover,
            botaoBom_click, botaoNeutro_click, botaoMau_click, botaoClose, botaoClose_hover, botaoClose_click;

        public Texture2D getBotao(TipoOpcao tipoOpcao, bool hover, bool click)
        {
            if (click)
            {
                switch (tipoOpcao)
                {
                    case TipoOpcao.Bom:
                        return botaoBom_click;
                    case TipoOpcao.Neutro:
                        return botaoNeutro_click;
                    case TipoOpcao.Mau:
                        return botaoMau_click;
                    case TipoOpcao.Close:
                        return botaoClose_click;
                    default:
                        return botaoBom_click;
                }
            }

            else if (hover)
            {
                switch (tipoOpcao)
                {
                    case TipoOpcao.Bom:
                        return botaoBom_hover;
                    case TipoOpcao.Neutro:
                        return botaoNeutro_hover;
                    case TipoOpcao.Mau:
                        return botaoMau_hover;
                    case TipoOpcao.Close:
                        return botaoClose_hover;
                    default:
                        return botaoBom_hover;
                }
            }
            else
            {
                switch (tipoOpcao)
                {
                    case TipoOpcao.Bom:
                        return botaoBom;
                    case TipoOpcao.Neutro:
                        return botaoNeutro;
                    case TipoOpcao.Mau:
                        return botaoMau;
                    case TipoOpcao.Close:
                        return botaoClose;
                    default:
                        return botaoBom;
                }
            }

        }
       
        protected bool ativa;        
        public bool Ativa
        {
            get { return ativa; }
        }
        
        protected bool modal;
        #endregion

        #region Construtor

        /// <summary>
        /// Construtor vazio para 
        /// </summary>
        public UI()
        {
        }

        #endregion

        #region Métodos
        public void ativarUI(ref UI UIModalAtiva)
        {
            this.ativa = true;
            if (this.modal)
            {
                UIModalAtiva = this;
            }
        }

        public void desativarUI(ref UI UIModalAtiva)
        {
            this.ativa = false;
            if (this.modal)
            {
                UIModalAtiva = null;
            }
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// Carrega a textura do componente de UI
        /// </summary>
        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            textura = Content.Load<Texture2D>("texturas/UI/"+ this.nomeTextura);
            if (this.posicao == Vector2.Zero)
            {
                this.posicao.X = (graphics.Viewport.Width / 2) - (textura.Width / 2);
                this.posicao.Y = (graphics.Viewport.Height - textura.Height) / 2;
            }
            botaoBom = Content.Load<Texture2D>("texturas/UI/botao_bom");
            botaoNeutro = Content.Load<Texture2D>("texturas/UI/botao_neutro");
            botaoMau = Content.Load<Texture2D>("texturas/UI/botao_mau");
            botaoBom_hover = Content.Load<Texture2D>("texturas/UI/botao_bom_hover");
            botaoNeutro_hover = Content.Load<Texture2D>("texturas/UI/botao_neutro_hover");
            botaoMau_hover = Content.Load<Texture2D>("texturas/UI/botao_mau_hover");
            botaoBom_click = Content.Load<Texture2D>("texturas/UI/botao_bom_click");
            botaoNeutro_click = Content.Load<Texture2D>("texturas/UI/botao_neutro_click");
            botaoMau_click = Content.Load<Texture2D>("texturas/UI/botao_mau_click");
            botaoClose = Content.Load<Texture2D>("texturas/UI/botao_close");
            botaoClose_hover = Content.Load<Texture2D>("texturas/UI/botao_close_hover");
            botaoClose_click = Content.Load<Texture2D>("texturas/UI/botao_close_click");
        }
        #endregion

        #region Draw
        /// <summary>
        /// Desenha a textura deste componente de UI
        /// </summary>
        /// <param name="spriteBatch">Spritebatch em que estamos a desenhar</param>
        /// <param name="camera">Camera ativa</param>
        virtual public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(textura, posicao, Color.White);
        }

        virtual public void Update()
        {
        }

        virtual public Lancamento Update(Random geradorRandom)
        {
            return new Lancamento();
        }

        /// <summary>
        /// Overridden by UI_Jogadores
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        /// <param name="arial12"></param>
        /// <param name="listaJogadores"></param>
        /// <param name="tabuleiro"></param>
        virtual public void Draw(SpriteBatch spriteBatch, Camera camera, SpriteFont arial12, List<Jogador> listaJogadores, Tabuleiro tabuleiro)
        {
            spriteBatch.Draw(textura, posicao, Color.White);
        }

        #endregion

    }
}