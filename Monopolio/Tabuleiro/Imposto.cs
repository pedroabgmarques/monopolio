using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{
    class Imposto : Casa
    {

        private int percentagem;
        public int Percentagem
        {
            get { return percentagem; }
            set { percentagem = value; }
        }

        private int custoFixo;
        public int CustoFixo
        {
            get { return custoFixo; }
            set { custoFixo = value; }
        }
        
        public Imposto(string nome, int percentagem, int custoFixo, Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = nome;
            this.percentagem = percentagem;
            this.custoFixo = custoFixo;
        }

    }
}
