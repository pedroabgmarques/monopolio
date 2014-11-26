using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monopolio
{
    /// <summary>
    /// Tipo de opção, muda a textura desenhada para esta opção
    /// </summary>
    public enum TipoOpcao
    {
        Bom,
        Neutro,
        Mau,
        Close
    }

    public class Opcao
    {

        /// <summary>
        /// Texto da Opção
        /// </summary>
        private string textoOpcao;
        public string TextoOpcao
        {
            get { return textoOpcao; }
        }

        /// <summary>
        /// True se o rato está por cima desta opção
        /// </summary>
        private bool hover;
        /// <summary>
        /// Devolve true se o rato está por cima desta opção
        /// </summary>
        public bool Hover
        {
            get { return hover; }
            set { hover = value; }
        }

        /// <summary>
        /// True se a opção está a ser clicada
        /// </summary>
        private bool clique;
        /// <summary>
        /// Devolve true se a opção está a ser clicada
        /// </summary>
        public bool Clique
        {
            get { return clique; }
            set { clique = value; }
        }

        /// <summary>
        /// True se o clique nesta opção fecha a UI
        /// </summary>
        private bool closeOnClick;
        /// <summary>
        /// Devolve true se o clique nesta opção fecha a UI
        /// </summary>
        public bool CloseOnClick
        {
            get { return closeOnClick; }
            set { closeOnClick = value; }
        }
        
        /// <summary>
        /// Rectângulo que envolve a opção, utilizado para detectar cliques na opção
        /// </summary>
        public Rectangle rectangulo;

        /// <summary>
        /// Código a ser executado quando existe um clique na opcao
        /// </summary>
        Action<string> accao;

        /// <summary>
        /// Tipo de opcao (bom, mau, neutro)
        /// </summary>
        private TipoOpcao tipoOpcao;
        public TipoOpcao TipoOpcao
        {
            get { return tipoOpcao; }
        }        

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="textoOpcao">Texto da Opção</param>
        /// <param name="tipoOpcao">Tipo de Opção</param>
        /// <param name="accao">Código a ser executado</param>
        public Opcao(string textoOpcao, TipoOpcao tipoOpcao, bool closeOnClick, Action<string> accao)
        {
            this.textoOpcao = textoOpcao;
            this.accao = accao;
            this.tipoOpcao = tipoOpcao;
            this.closeOnClick = closeOnClick;
        }

        /// <summary>
        /// Exucuta o código da opção
        /// </summary>
        public void ExecutarAccao()
        {
            accao.Invoke(accao.Method.ToString());
        }
    }
}
