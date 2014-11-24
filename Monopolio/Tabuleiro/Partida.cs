using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class Partida : Casa
    {

        public Partida(Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = "Partida";
        }

        //TODO: receber o numero de voltas do jogador e, se > 0, pagar 200

    }
}
