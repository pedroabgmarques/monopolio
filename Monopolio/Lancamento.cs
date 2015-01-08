using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Estrutura de um lançamento
    /// </summary>
    public struct Lancamento
    {
        /// <summary>
        /// Valor do primeiro dado
        /// </summary>
        public int dado1;
        /// <summary>
        /// Valor do segundo dado
        /// </summary>
        public int dado2;
        /// <summary>
        /// Valor da soma dos dados
        /// </summary>
        public int somaDados
        {
            get { return dado1 + dado2; }
        }
    }
}
