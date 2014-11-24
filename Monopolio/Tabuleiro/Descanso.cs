using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class Descanso : Casa
    {

        public Descanso(Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = "Descanso";
        }

        //TODO: logica do descanso (não faz nada?!?)

    }

    
}
