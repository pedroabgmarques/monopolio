using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class GoTo : Casa
    {

        public GoTo(Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = "GoTo";
        }

        //TODO: lógica do vá para a prisão

    }
}
