/*
 * Author: Pedro Marques
 * Date: 08/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere as ruas
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{

    public enum GrupoRuas
    {
        Brown,
        LightBlue,
        Pink,
        Orange,
        Red,
        Yellow,
        Green,
        Blue
    }


    /// <summary>
    /// Uma rua é uma propriedade que pode ter casas e hotéis
    /// </summary>
    class Rua : Propriedade
    {
        private int nCasas;
        public int NCasas
        {
            get { return nCasas; }
            set { nCasas = value; }
        }

        private int renda, renda1, renda2, renda3, renda4, renda5;
        private int custoCasa;
        private GrupoRuas grupoRuas;
	    public GrupoRuas GrupoRuas
	    {
		    get { return grupoRuas;}
		    set { grupoRuas = value;}
	    }
	

        public Rua(string nome, int custo, int hipoteca, int renda, int renda1, int renda2, int renda3, int renda4, int renda5, int custoCasa, GrupoRuas grupoRuas, Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            base.nome = nome;
            base.custo = custo;
            base.hipoteca = hipoteca;
            base.hipotecada = false;
            base.dono = null;
            this.renda = renda;
            this.renda1 = renda1;
            this.renda2 = renda2;
            this.renda3 = renda3;
            this.renda4 = renda4;
            this.renda5 = renda5;
            this.custoCasa = custoCasa;
            this.grupoRuas = grupoRuas;
        }

        public int Renda()
        {
            switch (nCasas)
            {
                case 0: return renda;
                case 1: return renda1;
                case 2: return renda2;
                case 3: return renda3;
                case 4: return renda4;
                case 5: return renda5;
                default: return renda1;
            }
        }

        /// <summary>
        /// Devolve o custo de construção de uma casa nesta rua
        /// </summary>
        /// <returns>Custo de construção de uma casa / hotel</returns>
        public int CustoCasa()
        {
            return custoCasa;
        }
        
    }
}
