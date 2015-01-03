using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    public class Community
    {
        #region Estado

        /// <summary>
        /// Texto da carta a apresentar ao utilizador
        /// </summary>
        private string texto;
        public string Texto
        {
            get { return texto; }
        }

        /// <summary>
        /// Acção a ser executada pela carta
        /// </summary>
        private Action<string> accao;
        public Action<string> Accao
        {
            get { return accao; }
        }

        /// <summary>
        /// Se a carta é boa ou má
        /// </summary>
        private TipoOpcao tipoOpcao;
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
        /// <param name="texto"></param>
        /// <param name="accao"></param>
        public Community(TipoOpcao tipoOpcao, string texto, Action<string> accao)
        {
            this.texto = texto;
            this.accao = accao;
            this.tipoOpcao = tipoOpcao;
        }
        #endregion
    }
}
