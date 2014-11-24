using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    public struct Lancamento
    {
        public int dado1;
        public int dado2;
        public int somaDados
        {
            get { return dado1 + dado2; }
        }
    }
}
