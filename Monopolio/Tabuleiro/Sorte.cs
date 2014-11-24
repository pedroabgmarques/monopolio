using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class Sorte : Casa
    {
        public Sorte(Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = "Sorte";
        }

        //TODO: devolver uma carta de sorte aleatoriamente
    }
}
