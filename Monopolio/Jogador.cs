/*
 * Author: Pedro Marques
 * Date: 09/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere um jogador
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// Estado, Construtor e Métodos do jogador
    /// </summary>
    public class Jogador
    {

        #region Estado
        /// <summary>
        /// Nome do jogador
        /// </summary>
        private string nome;
        /// <summary>
        /// Devolve o nome do jogador
        /// </summary>
        public string Nome
        {
            get { return nome; }
        }
        
        /// <summary>
        /// Quantidade de dinheiro que o jogador possui
        /// </summary>
        private int dinheiro;
        /// <summary>
        /// Devolve a quantidade de dinheiro que o jogador possui
        /// </summary>
        public int Dinheiro
        {
            get { return dinheiro; }
        }
        /// <summary>
        /// Indice da casa em que o jogador se encontra
        /// </summary>
        private int casaAtual;
        /// <summary>
        /// Devolve o indice da casa em que o jogador se encontra
        /// </summary>
        public int CasaAtual
        {
            get { return casaAtual; }
            set { this.casaAtual = value; }
        }

        /// <summary>
        /// Lista de propriedades que o jogador possui
        /// </summary>
        private List<Propriedade> listaPropriedades;
        /// <summary>
        /// Devolve a lista de propriedades que o jogador possui
        /// </summary>
        public List<Propriedade> ListaPropriedades
        {
            get { return listaPropriedades; }
        }
        
        

        #endregion

        #region Construtor
        /// <summary>
        /// Construtor do Jogador
        /// </summary>
        public Jogador(string nome)
        {
            this.nome = nome;
            this.dinheiro = 2 * 500 + 2 * 100 + 2 * 50 + 6 * 20 + 5 * 10 + 5 * 5 + 5 * 1;
            this.casaAtual = 0;
            this.listaPropriedades = new List<Propriedade>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Retira uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a retirar ao jogador</param>
        public void pagar(int valor)
        {
            this.dinheiro -= valor;
        }

        /// <summary>
        /// Adiciona uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a adicionar ao jogador</param>
        public void receber(int valor)
        {
            this.dinheiro += valor;
        }

        /// <summary>
        /// Move o jogador para uma determinada casa
        /// </summary>
        /// <param name="casa">Indice da casa para a qual se pretende mover o jogador</param>
        public void goTo(int casa)
        {
            this.casaAtual = casa;
        }

        /// <summary>
        /// Adiciona uma propriedade à lista de propriedades do jogador
        /// </summary>
        /// <param name="propriedade">Propriedade a adicionar à lista de propriedades do jogador</param>
        public void adicionarPropriedade(Propriedade propriedade)
        {
            propriedade.Dono = this;
            listaPropriedades.Add(propriedade);
            pagar(propriedade.Custo);
        }

        /// <summary>
        /// Retira uma propriedade à lista de propriedades do jogador
        /// </summary>
        /// <param name="propriedade">Propriedade a retirar à lista de propriedades do jogador</param>
        public void retirarPropriedade(Propriedade propriedade)
        {
            propriedade.Dono = null;
            listaPropriedades.Remove(propriedade);
        }

        /// <summary>
        /// Devolve o nº de estaçoes que um jogador possui
        /// </summary>
        /// <returns></returns>
        public int nEstacoes()
        {
            int nEstacoes = 0;
            foreach (Utilidade prop in listaPropriedades)
            {
                if (prop.Tipo == Tipo.Estação)
                {
                    nEstacoes++;
                }
            }
            return nEstacoes;
        }

        /// <summary>
        /// Devolve o nº de companhias eletricas que um jogador tem
        /// </summary>
        /// <returns></returns>
        public int nEletricas()
        {
            int nEletricas = 0;
            foreach (Utilidade prop in listaPropriedades)
            {
                if (prop.Tipo == Tipo.Eletricidade)
                {
                    nEletricas++;
                }
            }
            return nEletricas;
        }

        /// <summary>
        /// Devolve o nº de companhias da agua que um jogador possui
        /// </summary>
        /// <returns></returns>
        public int nAguas()
        {
            int nAguas = 0;
            foreach (Utilidade prop in listaPropriedades)
            {
                if (prop.Tipo == Tipo.Água)
                {
                    nAguas++;
                }
            }
            return nAguas;
        }

        #endregion

    }
}
