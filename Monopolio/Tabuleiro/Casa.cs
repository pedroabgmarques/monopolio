/*
 * Uma casa do tabuleiro
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    /// <summary>
    /// Descreve uma casa do tabuleiro
    /// </summary>
    public class Casa
    {
        #region Estado
        /// <summary>
        /// Nome da Casa
        /// </summary>
        protected string nome;
        /// <summary>
        /// Get/Set do nome da Casa
        /// </summary>
        public string Nome
        {
            get { return nome; }
        }

        private Rectangle coordsAndSize;
        /// <summary>
        /// Getter / Setter do rectangulo de coordenadas e tamanho da casa no tabuleiro
        /// </summary>
        public Rectangle CoordsAndSize
        {
            get { return coordsAndSize; }
            set { coordsAndSize = value; }
        }

        #endregion


    }
}
