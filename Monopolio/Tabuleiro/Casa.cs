/*
 * Author: Pedro Marques
 * Date: 08/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Uma casa do tabuleiro
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
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
        public Rectangle CoordsAndSize
        {
            get { return coordsAndSize; }
            set { coordsAndSize = value; }
        }

        #endregion


    }
}
