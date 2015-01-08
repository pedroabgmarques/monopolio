/*
 * Author: Pedro Marques
 * Date: 09/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere um jogador
 * */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private float dinheiro;
        /// <summary>
        /// Devolve a quantidade de dinheiro que o jogador possui
        /// </summary>
        public float Dinheiro
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

        /// <summary>
        /// Numero de cartas get out of jail que o jogador tem
        /// </summary>
        private int getOutOfJail;
        /// <summary>
        /// Getter / Setter do numero de cartas get out of jail que o jogador possui
        /// </summary>
        public int GetOutOfJail
        {
            get { return getOutOfJail; }
            set { getOutOfJail = value; }
        }
        
        /// <summary>
        /// Número de voltas que o jogador deu ao tabuleiro
        /// </summary>
        private int nVoltas;
        /// <summary>
        /// Getter / Setter do numero de voltas que este jogador deu ao tabuleiro
        /// </summary>
        public int NVoltas
        {
            get { return nVoltas; }
            set { nVoltas = value; }
        }

        /// <summary>
        /// Indica se o jogador está ou não na primeira volta
        /// </summary>
        private bool primeiraVolta;
        /// <summary>
        /// Getter / Setter do indicador de primeira volta ao tabuleiro
        /// </summary>
        public bool PrimeiraVolta
        {
            get { return primeiraVolta; }
            set { primeiraVolta = value; }
        }

        /// <summary>
        /// Valor do último lançamento deste jogador
        /// </summary>
        private int ultimoLancamento;
        /// <summary>
        /// Getter / Setter do valor do ultimo lançamento do jogador
        /// </summary>
        public int UltimoLancamento
        {
            get { return ultimoLancamento; }
            set { ultimoLancamento = value; }
        }

        /// <summary>
        /// Imagem do token deste jogador
        /// </summary>
        private Texture2D token;
        /// <summary>
        /// Getter / Setter da textura do token
        /// </summary>
        public Texture2D Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// "Player 1", "Player 2", etc.
        /// </summary>
        private Texture2D playerSplash;
        /// <summary>
        /// Getter / Setter da textura de splash
        /// </summary>
        public Texture2D PlayerSplash
        {
            get { return playerSplash; }
            set { playerSplash = value; }
        }
        
        /// <summary>
        /// Posição do token do jogador no tabuleiro
        /// </summary>
        public Vector2 Posicao;

        /// <summary>
        /// Offset na posição do jogador, para os tokens não ficarem (demasiado) sobrepostos
        /// </summary>
        private Vector2 offsetPosicao;
        /// <summary>
        /// Getter / Setter do offset de posição deste jogador no tabuleiro
        /// </summary>
        public Vector2 OffsetPosicao
        {
            get { return offsetPosicao; }
            set { offsetPosicao = value; }
        }
        

        private int nJogador;

        /// <summary>
        /// Posição na UI em que aparecem as stats deste jogador
        /// </summary>
        private Vector2 posicaoStats;
        /// <summary>
        /// Getter / Setter da posição em que deverão aparecer as entradas / saidas de dinheiro do jogador
        /// </summary>
        public Vector2 PosicaoStats
        {
            get { return posicaoStats; }
            set { posicaoStats = value; }
        }

        /// <summary>
        /// Lista de animações de entradas ou saídas de dinheiro associadas a este jogador
        /// </summary>
        private List<MoneyAnimation> listaMoneyAnimations;

        /// <summary>
        /// Indica se o jogador está na prisão ou não
        /// </summary>
        private bool jailed;
        /// <summary>
        /// Getter / Setter do estado de prisioneiro do jogador
        /// </summary>
        public bool Jailed
        {
            get { return jailed; }
            set { jailed = value; }
        }

        /// <summary>
        /// Conta o número de doubles seguidos: se três, o jogador vai diretamente para a prisão
        /// </summary>
        private int contadorDoubles;
        /// <summary>
        /// Getter / Setter do contador de doubles do jogador
        /// </summary>
        public int ContadorDoubles
        {
            get { return contadorDoubles; }
            set { contadorDoubles = value; }
        }

        //Verdadeiro se o jogador tirou um double e tem direito a jogar novamente
        private bool jogaOutraVez;
        /// <summary>
        /// Getter / Setter do indicador de direito de jogar novamente
        /// </summary>
        public bool JogaOutraVez
        {
            get { return jogaOutraVez; }
            set { jogaOutraVez = value; }
        }

        //Conta o numero de turnos há que o jogador está na prisão
        private int turnsOnJail;
        /// <summary>
        /// Getter / Setter do numero de turnos há que o jogador está na prisão
        /// </summary>
        public int TurnsOnJail
        {
            get { return turnsOnJail; }
            set { turnsOnJail = value; }
        }

        /// <summary>
        /// Indica se o proximo lançamento é um double para tentar sair da prisão
        /// </summary>
        private bool doubleToEscapeJail;
        /// <summary>
        /// Getter / setter da aposta do jogador em double para sair da prisão
        /// </summary>
        public bool DoubleToEscapeJail
        {
            get { return doubleToEscapeJail; }
            set { doubleToEscapeJail = value; }
        }
        
        
        
        
        
        
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor do Jogador
        /// </summary>
        public Jogador(string nome, int nJogador, ref List<MoneyAnimation> listaMoneyanimations)
        {
            this.nome = nome;
            this.dinheiro = 2 * 500 + 2 * 100 + 2 * 50 + 6 * 20 + 5 * 10 + 5 * 5 + 5 * 1;
            this.casaAtual = 0;
            this.listaPropriedades = new List<Propriedade>();
            this.nVoltas = 0;
            this.primeiraVolta = true;
            this.nJogador = nJogador;
            this.getOutOfJail = 0;
            this.posicaoStats = Vector2.Zero;
            this.listaMoneyAnimations = listaMoneyanimations;
            this.jailed = false;
            this.contadorDoubles = 0;
            this.jogaOutraVez = false;
            this.turnsOnJail = 0;
            this.doubleToEscapeJail = false;
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// Carrega os assets necessários ao jogador
        /// </summary>
        /// <param name="Content">Content Manager</param>
        /// <param name="graphics">Graphics Device</param>
        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            token = Content.Load<Texture2D>("texturas/tabuleiro/tokens/token"+nJogador);
            playerSplash = Content.Load<Texture2D>("texturas/players/player" + nJogador);
            this.Posicao = new Vector2(graphics.Viewport.Width / 2,
                                                    graphics.Viewport.Height / 2);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Faz reset ao estado dos doubles
        /// </summary>
        public void resetDoubles()
        {
            this.contadorDoubles = 0;
            this.jogaOutraVez = false;
        }

        /// <summary>
        /// Retira uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a retirar ao jogador</param>
        public void pagar(float valor)
        {
            this.dinheiro -= valor;
            adicionarMoneyAnimation(new MoneyAnimation(this, -valor));
        }

        /// <summary>
        /// Adiciona uma determinada quantidade de dinheiro ao jogador
        /// </summary>
        /// <param name="valor">Quantidade de dinheiro a adicionar ao jogador</param>
        public void receber(int valor)
        {
            this.dinheiro += valor;
            adicionarMoneyAnimation(new MoneyAnimation(this, valor));
        }

        private void adicionarMoneyAnimation(MoneyAnimation anim)
        {
            //remover as animacoes já existentes deste jogador e inserir a nova
            foreach (MoneyAnimation animation in listaMoneyAnimations)
            {
                if (animation.Jogador == this)
                {
                    animation.Viva = false;
                }
            }
            listaMoneyAnimations.Add(anim);
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
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Utilidade)
                {
                    utilidade = (Utilidade)propriedade;
                    if (utilidade.Tipo == Tipo.Estação)
                    {
                        nEstacoes++;
                    }
                }
            }
            return nEstacoes;
        }

        /// <summary>
        /// Devolve o nº de companhias eletricas que um jogador tem
        /// </summary>
        /// <returns>Número de utilidades que o jogador possui</returns>
        Utilidade utilidade;
        /// <summary>
        /// Devolve o número de utilidades de eletricidade/água que um jogador possui
        /// </summary>
        /// <returns></returns>
        public int nEletricasEAguas()
        {
            int nEletricasEAguas = 0;
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Utilidade)
                {
                    utilidade = (Utilidade)propriedade;
                    if (utilidade.Tipo == Tipo.Eletricidade || utilidade.Tipo == Tipo.Água)
                    {
                        nEletricasEAguas++;
                    }
                }
            }
            return nEletricasEAguas;
        }

        /// <summary>
        /// Devolve o total de dinheiro e investimentos de um jogador
        /// </summary>
        /// <returns>Soma total e dinheiro e investimentos</returns>
        public float totalAssets()
        {
            float soma = 0;
            //Somar o dinheiro vivo
            soma += dinheiro;
            //Somar o preço de compra de todas as propriedades, independentemente de estarem ou não hipotecadas
            foreach (Propriedade propriedade in listaPropriedades)
            {
                soma += propriedade.Custo;
            }
            //Somar o preço de compra de todas as casas
            foreach (Propriedade propriedade in listaPropriedades)
            {
                if (propriedade is Rua)
                {
                    Rua rua = (Rua)propriedade;
                    soma += rua.NCasas * rua.CustoCasa();
                }
            }

            return soma;
        }
        
        /// <summary>
        /// Devolve o numero de casas que o jogador possui
        /// </summary>
        /// <returns>Número de casas</returns>
        public int nCasas()
        {
            int contadorCasas = 0;
            foreach (Propriedade prop in listaPropriedades)
            {
                if (prop is Rua)
                {
                    Rua rua = (Rua)prop;
                    if (rua.NCasas > 0 && rua.NCasas < 5)
                    {
                        //Um hotel são cinco casas
                        contadorCasas += rua.NCasas;
                    }
                }
            }
            return contadorCasas;
        }

        /// <summary>
        /// Devolve o numero de hoteis que um jogador possui
        /// </summary>
        /// <returns>Numero de hoteis</returns>
        public int nHoteis()
        {
            int contadorHoteis = 0;
            foreach (Propriedade prop in listaPropriedades)
            {
                if (prop is Rua)
                {
                    Rua rua = (Rua)prop;
                    if (rua.NCasas > 0 && rua.NCasas == 5)
                    {
                        //Um hotel são cinco casas
                        contadorHoteis++;
                    }
                }
            }
            return contadorHoteis;
        }

        /// <summary>
        /// Devolve a lista de ruas que um jogador possui
        /// </summary>
        /// <returns>Lista de Ruas</returns>
        public List<Rua> listaRuas()
        {
            List<Rua> listaRuas = new List<Rua>();
            foreach (Propriedade prop in listaPropriedades)
            {
                if (prop is Rua)
                {
                    listaRuas.Add((Rua)prop);
                }
            }
            return listaRuas;
        }


        #endregion

        #region Draw
        /// <summary>
        /// Desenha o token do jogador
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Jogador jogadorAtual)
        {
            if (jogadorAtual == this)
            {
                spriteBatch.Draw(token, Posicao + offsetPosicao, null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            else
            {
                spriteBatch.Draw(token, Posicao + offsetPosicao, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            }
            
        }
        #endregion

    }
}
