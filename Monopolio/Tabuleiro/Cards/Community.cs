using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Descreve uma carta de comunidade / sorte
    /// </summary>
    public class CommunityAndChance
    {
        #region Estado

        /// <summary>
        /// Texto da carta a apresentar ao utilizador
        /// </summary>
        private string texto;
        /// <summary>
        /// Getter / Setter do texto da carta de comunidade / sorte
        /// </summary>
        public string Texto
        {
            get { return texto; }
        }

        /// <summary>
        /// Acção a ser executada pela carta
        /// </summary>
        private Action<string> accao;
        /// <summary>
        /// Getter da acção a executar pela carta de comunidade / sorte
        /// </summary>
        public Action<string> Accao
        {
            get { return accao; }
        }

        /// <summary>
        /// Se a carta é boa ou má
        /// </summary>
        private TipoOpcao tipoOpcao;
        /// <summary>
        /// Getter do tipo de opção da carta (resultado bom ou mau para o utilizador)
        /// </summary>
        public TipoOpcao TipoOpcao
        {
            get { return tipoOpcao; }
            set { tipoOpcao = value; }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="tipoOpcao">Bom / Mau</param>
        /// <param name="texto"></param>
        /// <param name="accao"></param>
        public CommunityAndChance(TipoOpcao tipoOpcao, string texto, Action<string> accao)
        {
            this.texto = texto;
            this.accao = accao;
            this.tipoOpcao = tipoOpcao;
        }
        #endregion
    }
}
