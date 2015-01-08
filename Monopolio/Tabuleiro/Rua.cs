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
using Microsoft.Xna.Framework.Graphics;

namespace Monopolio
{
    /// <summary>
    /// Grupos de ruas / monopólios
    /// </summary>
    public enum GrupoRuas
    {
        /// <summary>
        /// Castanho
        /// </summary>
        Brown,
        /// <summary>
        /// Azul clarinho
        /// </summary>
        LightBlue,
        /// <summary>
        /// Cor-de-rosa
        /// </summary>
        Pink,
        /// <summary>
        /// Cor de laranja
        /// </summary>
        Orange,
        /// <summary>
        /// Vermelho
        /// </summary>
        Red,
        /// <summary>
        /// Amarelo
        /// </summary>
        Yellow,
        /// <summary>
        /// Verde
        /// </summary>
        Green,
        /// <summary>
        /// Azul
        /// </summary>
        Blue
    }


    /// <summary>
    /// Uma rua é uma propriedade que pode ter casas e hotéis
    /// </summary>
    public class Rua : Propriedade
    {
        private int nCasas;
        /// <summary>
        /// Getter / Setter do numero de casas desta rua
        /// </summary>
        public int NCasas
        {
            get { return nCasas; }
            set { nCasas = value; }
        }

        private int renda, renda1, renda2, renda3, renda4, renda5;
        private int custoCasa;
        private GrupoRuas grupoRuas;
        /// <summary>
        /// Getter / Setter do grupo de ruas a que esta rua pertence
        /// </summary>
	    public GrupoRuas GrupoRuas
	    {
		    get { return grupoRuas;}
		    set { grupoRuas = value;}
	    }
	
        /// <summary>
        /// Construtor de uma Rua
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="custo">Custo</param>
        /// <param name="hipoteca">Valor da hipoteca</param>
        /// <param name="renda">renda sem casas</param>
        /// <param name="renda1">renda com uma casa</param>
        /// <param name="renda2">renda com duas casa</param>
        /// <param name="renda3">renda com três casa</param>
        /// <param name="renda4">renda com quatro casa</param>
        /// <param name="renda5">renda com cinco casa</param>
        /// <param name="custoCasa">custo de casa casa construída</param>
        /// <param name="grupoRuas">grupo de ruas a que a rua pertence</param>
        /// <param name="coordsAndSize">coordenadas e dimensões da rua no tabuleiro</param>
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

        /// <summary>
        /// Devolve a renda a pagar
        /// </summary>
        /// <returns>Renda a pagar, de acordo com o numero de casas construídas</returns>
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

        private string textoTemp;
        /// <summary>
        /// Desenha informação relativa a uma rua - dono e nº de casas / hotéis
        /// </summary>
        /// <param name="spriteBatch">Instância de spritebatch</param>
        /// <param name="arial12">Fonte a utilizar</param>
        /// <param name="indiceCasa">Indice da casa</param>
        new public void Draw(SpriteBatch spriteBatch, SpriteFont arial12, int indiceCasa)
        {
            if (nCasas > 0)
            {
                if (nCasas == 1)
                {
                    textoTemp = nCasas + "house";
                }
                else if (nCasas < 5)
                {
                    textoTemp = nCasas + "houses";
                }
                else
                {
                    textoTemp = "1 hotel";
                }
                if (indiceCasa > 0 && indiceCasa < 10)
                {
                    spriteBatch.DrawString(arial12, textoTemp, new Vector2(CoordsAndSize.X + 73, CoordsAndSize.Y), Color.Blue, MathHelper.ToRadians(0), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                }
                if (indiceCasa > 10 && indiceCasa < 20)
                {
                    spriteBatch.DrawString(arial12, textoTemp, new Vector2(CoordsAndSize.X + CoordsAndSize.Width, CoordsAndSize.Y + 73), Color.Blue, MathHelper.ToRadians(90), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                }
                if (indiceCasa > 20 && indiceCasa < 30)
                {
                    spriteBatch.DrawString(arial12, textoTemp, new Vector2(CoordsAndSize.X - 73 + CoordsAndSize.Width, CoordsAndSize.Y + CoordsAndSize.Height), Color.Blue, MathHelper.ToRadians(180), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                }
                if (indiceCasa > 30 && indiceCasa < 40)
                {
                    spriteBatch.DrawString(arial12, textoTemp, new Vector2(CoordsAndSize.X + 73, CoordsAndSize.Y + CoordsAndSize.Height), Color.Blue, MathHelper.ToRadians(270), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                }
            }
            
        }
        
    }
}
