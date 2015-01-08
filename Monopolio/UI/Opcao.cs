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
        /// <summary>
        /// Botão verde
        /// </summary>
        Bom,
        /// <summary>
        /// Não implementado
        /// </summary>
        Neutro,
        /// <summary>
        /// Botão vermelho
        /// </summary>
        Mau,
        /// <summary>
        /// Não implementado
        /// </summary>
        Close
    }

    /// <summary>
    /// Descreve uma opção a incluir numa UI
    /// </summary>
    public class Opcao
    {

        /// <summary>
        /// Texto da Opção
        /// </summary>
        private string textoOpcao;
        /// <summary>
        /// Getter do texto de uma opção a incluir numa UI
        /// </summary>
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
        /// <summary>
        /// Getter do tipo de opção do botão
        /// </summary>
        public TipoOpcao TipoOpcao
        {
            get { return tipoOpcao; }
        }

        /// <summary>
        /// Se não está ativa, o clique não faz nada
        /// </summary>
        private bool activa;
        /// <summary>
        /// Getter / Setter do indicador de UI ativa
        /// </summary>
        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }
        

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="textoOpcao">Texto da Opção</param>
        /// <param name="tipoOpcao">Tipo de Opção</param>
        /// <param name="accao">Código a ser executado</param>
        /// <param name="closeOnClick">Se a UI deve ser fechada quando o utilizador clica na opção</param>
        public Opcao(string textoOpcao, TipoOpcao tipoOpcao, bool closeOnClick, Action<string> accao)
        {
            this.textoOpcao = textoOpcao;
            this.accao = accao;
            this.tipoOpcao = tipoOpcao;
            this.closeOnClick = closeOnClick;
            this.activa = true;
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
