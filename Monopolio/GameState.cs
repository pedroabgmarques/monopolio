using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{

    public enum Estado
    {
        Inicial,
        Lançamento,
        Compra,
        Leilão
    }
    /// <summary>
    /// Esta classe estática guarda o estado em que o jogo se encontra.
    /// Está implementada como uma árvore binária constituída por listas ligadas.
    /// </summary>
    public static class GameState
    {
        /// <summary>
        /// 
        /// </summary>
        static private Estado estado;
        static public Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }

    }
}
