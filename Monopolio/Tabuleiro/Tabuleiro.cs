/*
 * Author: Pedro Marques
 * Date: 07/11/204
 * Email: pedroabgmarques@gmail.com / a10855@alunos.ipca.pt
 * Description:
 * 
 * Lida com tudo o que tenha a ver com o tabuleiro do jogo
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monopolio
{
    /// <summary>
    /// Estado, Construtor e Métodos do tabuleiro
    /// </summary>
    public class Tabuleiro
    {

        #region Estado

        private Vector2 posicao;

        public Vector2 Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }
        

        private Texture2D textura;
        public Texture2D Textura
        {
            get { return textura; }
        }
        
        /// <summary>
        /// Lista de casas que compõem o tabuleiro
        /// </summary>
        private List<Casa> listaCasas;
        public List<Casa> ListaCasas()
        {
            return listaCasas;
        }

        #endregion

        #region LoadContent
        /// <summary>
        /// Loading de assets
        /// </summary>
        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            textura = Content.Load<Texture2D>("texturas/tabuleiro/tabuleiro_grande");
            this.posicao = new Vector2(graphics.Viewport.Width / 2,
                                                    graphics.Viewport.Height / 2);

            listaCasas = new List<Casa>();

            listaCasas.Add(new Partida(
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 1334,
                    203, 201)));
            listaCasas.Add(new Rua("Old Kent Road", 60, 30, 2, 10, 30, 90, 160, 250,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4,
                    (int)Posicao.Y + 1334,
                    121, 201)));
            listaCasas.Add(new CommunityChest(
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117,
                    (int)Posicao.Y + 1334,
                    113, 201)));
            listaCasas.Add(new Rua("Whitechapel Road", 60, 30, 4, 20, 60, 180, 320, 450,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4,
                    (int)Posicao.Y + 1334,
                    116, 201)));
            listaCasas.Add(new Imposto("IRS", 10, 200,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6,
                    (int)Posicao.Y + 1334,
                    118, 201)));
            listaCasas.Add(new Utilidade(Tipo.Estação, "Kings Cross Station", 200, 100, 25, 50, 100, 200,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6 - 118 - 5,
                    (int)Posicao.Y + 1334,
                    118, 201)));
            listaCasas.Add(new Rua("The Angel Islington", 100, 50, 6, 30, 90, 270, 400, 550,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6 - 118 - 5 - 118 - 7,
                    (int)Posicao.Y + 1334,
                    120, 201)));
            listaCasas.Add(new Sorte(
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6 - 118 - 5 - 118 - 7 - 120 - 4,
                    (int)Posicao.Y + 1334,
                    120, 201)));
            listaCasas.Add(new Rua("Euston Road", 100, 50, 6, 30, 90, 270, 400, 550,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6 - 118 - 5 - 118 - 7 - 120 - 4 - 120 - 2,
                    (int)Posicao.Y + 1334,
                    118, 201)));
            listaCasas.Add(new Rua("Pentonville Road", 120, 60, 8, 40, 100, 300, 450, 600,
                new Rectangle(
                    (int)Posicao.X + 1333 - 122 - 4 - 117 - 4 - 113 - 4 - 116 - 6 - 118 - 5 - 118 - 7 - 120 - 4 - 120 - 2 - 118 - 6,
                    (int)Posicao.Y + 1334,
                    120, 201)));

            listaCasas.Add(new Prisao(
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334,
                    199, 201)));

            listaCasas.Add(new Rua("Pall Mall", 140, 70, 10, 50, 150, 450, 625, 750,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4,
                    199, 118)));
            listaCasas.Add(new Utilidade(Tipo.Eletricidade, "EDP", 150, 75, 4, 10,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2,
                    199, 116)));
            listaCasas.Add(new Rua("Whitehall", 140, 70, 10, 50, 150, 450, 625, 750,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8,
                    199, 119)));
            listaCasas.Add(new Rua("Northumberland Avenue", 160, 80, 12, 60, 180, 500, 700, 900,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4,
                    199, 119)));
            listaCasas.Add(new Utilidade(Tipo.Estação, "Marylebone Station", 200, 100, 25, 50, 100, 200,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4 - 119 - 4,
                    199, 119)));
            listaCasas.Add(new Rua("Bow Street", 180, 90, 14, 70, 200, 550, 750, 950,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4 - 119 - 4 - 119 - 5,
                    199, 120)));
            listaCasas.Add(new CommunityChest(
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4 - 119 - 4 - 119 - 5 - 120 - 4,
                    199, 118)));
            listaCasas.Add(new Rua("Marlborough Street", 180, 90, 14, 70, 200, 550, 750, 950,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4 - 119 - 4 - 119 - 5 - 120 - 4 - 118 - 4,
                    199, 118)));
            listaCasas.Add(new Rua("Vine Street", 200, 100, 16, 80, 220, 600, 800, 1000,
                new Rectangle(
                    (int)Posicao.X + 24,
                    (int)Posicao.Y + 1334 - 118 - 4 - 118 - 2 - 116 - 8 - 119 - 4 - 119 - 4 - 119 - 5 - 120 - 4 - 118 - 4 - 118 - 7,
                    199, 120)));

            listaCasas.Add(new Descanso(new Rectangle(
                    (int)Posicao.X + 24, 
                    (int)Posicao.Y + 24, 
                    200, 198)));

            listaCasas.Add(new Rua("The Strand", 220, 110, 18, 90, 250, 700, 875, 1050, new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5, 
                    (int)Posicao.Y + 24, 
                    122, 198)));
            listaCasas.Add(new Sorte(new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3,
                    (int)Posicao.Y + 24, 
                    116, 198)));
            listaCasas.Add(new Rua("Fleet Street", 220, 110, 18, 90, 250, 700, 875, 1050, new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4,
                    (int)Posicao.Y + 24, 
                    120, 198)));
            listaCasas.Add(new Rua("Trafalgar Square", 240, 120, 20, 100, 300, 750, 925, 1100, new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4,
                    (int)Posicao.Y + 24,
                    119, 198)));
            listaCasas.Add(new Utilidade(Tipo.Estação, "Fenchurch St. Station", 200, 100, 25, 50, 100, 200,
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4,
                    (int)Posicao.Y + 24,
                    119, 198)));
            listaCasas.Add(new Rua("Leicester Square", 260, 130, 22, 110, 330, 800, 975, 1150,
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4 + 119 + 4,
                    (int)Posicao.Y + 24,
                    120, 198)));
            listaCasas.Add(new Rua("Coventry Street", 260, 130, 22, 110, 330, 800, 975, 1150,
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4 + 119 + 4 + 120 + 4,
                    (int)Posicao.Y + 24,
                    120, 198)));
            listaCasas.Add(new Utilidade(Tipo.Água, "Aguas de Portugal", 150, 75, 4, 10,
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4 + 119 + 4 + 120 + 4 + 120 + 4,
                    (int)Posicao.Y + 24,
                    119, 198)));
            listaCasas.Add(new Rua("Piccadilly", 280, 140, 24, 120, 360, 850, 1025, 1200,
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4 + 119 + 4 + 120 + 4 + 120 + 4 + 119 + 4,
                    (int)Posicao.Y + 24,
                    115, 198)));

            listaCasas.Add(new GoTo(
                new Rectangle(
                    (int)Posicao.X + 24 + 199 + 5 + 122 + 3 + 116 + 4 + 120 + 4 + 119 + 4 + 119 + 4 + 120 + 4 + 120 + 4 + 119 + 4 + 115 + 4,
                    (int)Posicao.Y + 24,
                    203, 198)));
         
            listaCasas.Add(new Rua("Regent Street", 300, 150, 26, 130, 390, 900, 1100, 1275,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5,
                    203, 120)));
            listaCasas.Add(new Rua("Oxford Street", 300, 150, 26, 130, 390, 900, 1100, 1275,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5,
                    203, 118)));
            listaCasas.Add(new CommunityChest(
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5,
                    203, 118)));
            listaCasas.Add(new Rua("Bond Street", 320, 160, 28, 150, 450, 1000, 1200, 1400,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5,
                    203, 119)));
            listaCasas.Add(new Utilidade(Tipo.Estação, "Liverpool Street Station", 200, 100, 25, 50, 100, 200,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5 + 119 + 5,
                    203, 118)));
            listaCasas.Add(new Sorte(
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5 + 119 + 5 + 119 + 4,
                    203, 118)));
            listaCasas.Add(new Rua("Park Lane", 350, 175, 35, 175, 500, 1100, 1300, 1500,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5 + 119 + 5 + 119 + 5 + 118 + 4,
                    203, 120)));
            listaCasas.Add(new Imposto("IMI", 0, 75,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5 + 119 + 5 + 119 + 5 + 118 + 4 + 120 + 4,
                    203, 115)));
            listaCasas.Add(new Rua("Mayfair", 400, 200, 50, 200, 600, 1400, 1700, 2000,
                new Rectangle(
                    (int)Posicao.X + 1333,
                    (int)Posicao.Y + 24 + 198 + 5 + 120 + 5 + 118 + 5 + 118 + 5 + 119 + 5 + 119 + 5 + 118 + 4 + 120 + 4 + 115 + 4,
                    203, 119)));
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public Tabuleiro()
        {
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Devolve a casa de um determinado indice
        /// </summary>
        /// <param name="indice">Indice da casa a devolver</param>
        /// <returns>A casa que se encontra no indice indicado</returns>
        public Casa Casa(int indice)
        {
            return listaCasas[indice];
        }

        /// <summary>
        /// Devolve o índice de uma casa x casas à frente ou atrás
        /// </summary>
        /// <param name="indiceAtual">Índice da casa em que estamos atualmente</param>
        /// <param name="casasAAvancar">Nº de casas a avançar ou recuar</param>
        /// <returns>Índice da casa que se encontra à distância pretendida</returns>
        public int IndiceCasaAFrente(int indiceAtual, int casasAAvancar)
        {
            if (indiceAtual + casasAAvancar >= listaCasas.Count)
            {
                return casasAAvancar - (listaCasas.Count - indiceAtual);
            }
            else if (indiceAtual + casasAAvancar < 0)
            {
                return listaCasas.Count + (casasAAvancar + indiceAtual);
            }
            {
                return indiceAtual + casasAAvancar;
            }
        }

        /// <summary>
        /// Devolve o número de casas de diferença que temos que mover para chegar a uma determinada casa
        /// </summary>
        /// <param name="indiceAtual">Indice em que estamos</param>
        /// <param name="indiceDesejado">Indice para onde vamos</param>
        /// <returns></returns>
        public int nCasasDiferenca(int indiceAtual, int indiceDesejado)
        {
            if (indiceAtual < indiceDesejado)
            {
                return indiceDesejado - indiceAtual;
            }
            else
            {
                return -(indiceAtual - indiceDesejado);
            }
        }

        public void ResetCasasPiscaram()
        {
            foreach (Casa casa in listaCasas)
            {
                casa.Piscou = false;
            }
        }

        /// <summary>
        /// Executa as acções necessárias caso passemos na casa de partida
        /// </summary>
        /// <param name="jogador">Uma instância do jogador que se está a mover</param>
        /// <param name="indice">O índice da casa atual</param>
        private void processarCasaPartida(Jogador jogador, int indice)
        {
            if (indice == 0)
            {
                //Casa de partida, atualizar voltas e pagar se for o caso
                if (jogador.PrimeiraVolta)
                {
                    jogador.PrimeiraVolta = false;
                }
                else
                {
                    jogador.NVoltas++;
                }
                if (jogador.NVoltas > 0)
                {
                    jogador.receber(200);
                }
            }
        }

        float rotacaoAEfetuar = 0f;
        int contadorCasaEspecialEncontrada;
        /// <summary>
        /// Verifica e executa uma rotação caso seja necessário
        /// É necessária uma rotação sempre que o movimento nos faz passar por uma casa de canto
        /// </summary>
        /// <param name="camera">Uma instância da camera</param>
        /// <param name="indiceAtual">Índice da casa em que estamos atualmente</param>
        /// <param name="casasAMover">Nº de casas a mover (negativo se vamos andar para trás)</param>
        public float verificarRotacaoEPartida(Camera camera, int indiceAtual, int casasAMover, Jogador jogador)
        {

            int indiceFinal = 0;

            contadorCasaEspecialEncontrada = 0;
            if (casasAMover > 0)
            {
                //Vamos andar para a frente
                if (indiceAtual + casasAMover < listaCasas.Count)
                {
                    //Não chegamos ao "fim" do tabuleiro
                    indiceFinal = indiceAtual + casasAMover;
                    for (int i = indiceAtual; i <= indiceAtual + casasAMover; i++)
                    {

                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {
                            contadorCasaEspecialEncontrada++;
                            
                        }
                        processarCasaPartida(jogador, i);
                    }

                }
                else
                {
                    //Este movimento passa pelo fim do tabuleiro, temos que scanar todas as casas até ao fim
                    // e depois todas as casas no inicio do tabuleiro até à casa em que vamos ficar
                    indiceFinal = listaCasas.Count - indiceAtual + casasAMover;

                    for (int i = indiceAtual; i < listaCasas.Count; i++)
                    {
                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {
                            contadorCasaEspecialEncontrada++;
                        }
                        processarCasaPartida(jogador, i);
                    }
                    for (int i = 0; i <= casasAMover - (listaCasas.Count - indiceAtual); i++)
                    {
                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {
                            contadorCasaEspecialEncontrada++;
                        }
                        processarCasaPartida(jogador, i);
                    }
                }

                //Se começamos numa casa de canto, temos q descontar uma rotação
                if ((indiceAtual == 0 || indiceAtual == 10 || indiceAtual == 20 || indiceAtual == 30)
                    //|| ((indiceAtual != 0 && indiceAtual != 10 && indiceAtual != 20 && indiceAtual != 30) &&
                    //(indiceFinal == 0 || indiceFinal == 10 || indiceFinal == 20 || indiceFinal == 30)))
                    )
                {
                    contadorCasaEspecialEncontrada -= 1;
                }

                if (contadorCasaEspecialEncontrada > 0)
                {
                    rotacaoAEfetuar = camera.getRotacao() + MathHelper.ToRadians(-90 * contadorCasaEspecialEncontrada);
                }

            }
            else if(casasAMover < 0)
            {
                //Vamos andar para trás no tabuleiro
                if (indiceAtual + casasAMover >= 0)
                {
                    indiceFinal = indiceAtual + casasAMover;
                    //Não chegamos ao pricipio do tabuleiro
                    for (int i = indiceAtual; i >= indiceAtual + casasAMover; i--)
                    {
                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {

                            contadorCasaEspecialEncontrada++;

                        }
                    }
                }
                else
                {
                    //Este movimento anda para trás até ao inicio do tabuleiro e continua a andar para trás pelo final do tabuleiro
                    //Temos que testar da casa atual até ao 0 e depois do final do tabuleiro até à casa onde vamos
                    indiceFinal = listaCasas.Count + (indiceAtual + casasAMover);
                    if(indiceFinal == 40) indiceFinal = 0;
                    for (int i = indiceAtual; i >= 0; i--)
                    {
                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {

                            contadorCasaEspecialEncontrada++;

                        }
                    }
                    for (int i = listaCasas.Count - 1; i >= listaCasas.Count + (indiceAtual + casasAMover); i--)
                    {
                        if (listaCasas[i].Nome == "Partida" || listaCasas[i].Nome == "Prisao"
                            || listaCasas[i].Nome == "Descanso" || listaCasas[i].Nome == "GoTo")
                        {

                            contadorCasaEspecialEncontrada++;

                        }
                    }
                }

                //Se começamos numa casa de canto, temos q descontar uma rotação
                if (

                    ((indiceAtual == 0 || indiceAtual == 10 || indiceAtual == 20 || indiceAtual == 30) &&
                    (indiceFinal == 0 || indiceFinal == 10 || indiceFinal == 20 || indiceFinal == 30))

                    ||

                    ((indiceAtual != 0 && indiceAtual != 10 && indiceAtual != 20 && indiceAtual != 30) && 
                    (indiceFinal == 0 || indiceFinal == 10 || indiceFinal == 20 || indiceFinal == 30))

                    || 
                    
                    ((indiceAtual == 0 && indiceAtual == 10 && indiceAtual == 20 && indiceAtual == 30) &&
                    (indiceFinal == 0 || indiceFinal == 10 || indiceFinal == 20 || indiceFinal == 30))
                    )
                    
                {
                    contadorCasaEspecialEncontrada -= 1;
                }

                if (contadorCasaEspecialEncontrada > 0)
                {
                    rotacaoAEfetuar = camera.getRotacao() + MathHelper.ToRadians(90 * contadorCasaEspecialEncontrada);
                }
            }

            return rotacaoAEfetuar;
            
        }

        #endregion

        #region Draw
        /// <summary>
        /// Desenha o tabuleiro
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Camera camera, GraphicsDevice graphics)
        {
            spriteBatch.Draw(textura, posicao, Color.White);
        }
        #endregion

    }
}
