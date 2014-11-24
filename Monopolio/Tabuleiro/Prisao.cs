using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class Prisao : Casa
    {

        public Prisao(Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = "Prisao";
        }

        //TODO: logica da prisao

    }
}
