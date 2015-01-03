using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class CommunityChest : Casa
    {


        #region Construtor
        public CommunityChest(Rectangle coordsAndSize)
        {
            base.nome = "Community Chest";
            base.CoordsAndSize = coordsAndSize;
        }
        #endregion
        
    }
}
