/*
 * Author: Pedro Marques
 * Date: 08/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere as propriedades / terrenos que existem no jogo
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{

    //Estado, Construtor e Metodos das propriedades
    public class Propriedade : Casa
    {

        #region Estado
        
        /// <summary>
        /// Custo da compra da propriedade
        /// </summary>
        protected int custo;
        /// <summary>
        /// Devolve o custo de compra da propriedade
        /// </summary>
        public int Custo
        {
            get { return custo; }
        }

        /// <summary>
        /// Valor pago pela hipoteca da propriedade
        /// </summary>
        protected int hipoteca;
        /// <summary>
        /// Devolve o valor pago pela hipoteca da propriedade
        /// </summary>
        public int Hipoteca
        {
            get { return hipoteca; }
        }

        /// <summary>
        /// Indica se a propriedade está hipotecada ou não
        /// </summary>
        protected bool hipotecada;
        /// <summary>
        /// Get/Set da hipoteca da propriedade
        /// </summary>
        public bool Hipotecada
        {
            get { return hipotecada; }
            set { hipotecada = value; }
        }

        /// <summary>
        /// O jogador que é dono deste terreno  
        /// </summary>
        protected Jogador dono;
        /// <summary>
        /// Devolve o jogador que é dono deste terreno
        /// </summary>
        public Jogador Dono
        {
            get { return dono; }
            set { dono = value; }
        }
        
        
        #endregion
    }
}
