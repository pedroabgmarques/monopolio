/*
 * Author: Pedro Marques
 * Date: 08/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere as utilidades
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monopolio
{

    /// <summary>
    /// Enumerador dos tipos de utilidades que existem
    /// </summary>
    enum Tipo
    {
        Estação,
        Eletricidade,
        Água
    }

    /// <summary>
    /// As utilidades são as estações, companhias de eletricidade e água
    /// Herdam das propriedades
    /// </summary>
    class Utilidade : Propriedade
    {

        private Tipo tipo;
        public Tipo Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private int renda1, renda2, renda3, renda4;
        private int multiplicador1, multiplicador2;

        public Utilidade(Tipo tipo, string nome, int custo, int hipoteca, int renda1, int renda2, int renda3, int renda4, Rectangle coordAndSize)
        {
            base.CoordsAndSize = coordAndSize;
            this.Tipo = tipo;
            base.nome = nome;
            base.custo = custo;
            base.hipoteca = hipoteca;
            base.dono = null;
            this.renda1 = renda1;
            this.renda2 = renda2;
            this.renda3 = renda3;
            this.renda4 = renda4;
        }

        public Utilidade(Tipo tipo, string nome, int custo, int hipoteca, int multiplicador1, int multiplicador2, Rectangle coordsAndSize)
        {
            base.CoordsAndSize = coordsAndSize;
            this.Tipo = tipo;
            base.nome = nome;
            base.custo = custo;
            base.hipoteca = hipoteca;
            base.hipotecada = false;
            this.multiplicador1 = multiplicador1;
            this.multiplicador2 = multiplicador2;
        }
        
        /// <summary>
        /// Devolve o valor a pagar de renda, consoante o numero de outras utilidades do mesmo tipo que o jogador tenha
        /// </summary>
        /// <param name="nUtilidades">Numero de outras utilidades do mesmo tipo possuidas pelo dono desta utilidade</param>
        /// <returns></returns>
        public int renda(int nUtilidades)
        {
            switch (nUtilidades)
            {
                case 1: return renda1;
                case 2: return renda2;
                case 3: return renda3;
                case 4: return renda4;
                default: return 0;
            }
        }

        /// <summary>
        /// Devolve a renda a pagar no caso especial das companhias de eletricidade
        /// </summary>
        /// <param name="nEletricas">Numero de companhias de electricidade que o jogador possui</param>
        /// <param name="dados">Numero que saiu nos dados no ultimo lançamento</param>
        /// <returns></returns>
        public int rendaEletricas(int nEletricas, int dados)
        {
            switch (nEletricas)
            {
                case 1: return this.multiplicador1 * dados;
                case 2: return this.multiplicador2 * dados;
                default: return 0;
            }
        }

    }
}
