using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{

    /// <summary>
    /// Possíveis estados do Jogo
    /// </summary>
    public enum Estado
    {
        /// <summary>
        /// Estado inicial do jogo, antes de haver jogadores
        /// </summary>
        Inicial,
        /// <summary>
        /// Um jogador tem a vez de jogar, está a lançar dados
        /// </summary>
        Lançamento,
        /// <summary>
        /// O jogador lançou os dados e agora estamos a processar a casa
        /// </summary>
        Casa
    }
    /// <summary>
    /// Esta classe estática guarda o estado em que o jogo se encontra.
    /// </summary>
    public static class GameState
    {
        /// <summary>
        /// 
        /// </summary>
        static private Estado estado;
        /// <summary>
        /// Getter / Setter do estado do jogo
        /// </summary>
        static public Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }

    }
}
