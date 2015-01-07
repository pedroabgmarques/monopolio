/*
 * Author: Pedro Marques
 * Date: 08/11/2014
 * Email: pedroabgmarques@gmail.com /a10855@alunos.ipca.pt
 * Description:
 * 
 * Descreve e gere as propriedades / terrenos que existem no jogo
 * */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        Rua ruaTemp;
        #region Draw
        public void Draw(SpriteBatch spriteBatch, SpriteFont arial12, int indiceCasa)
        {
            if (indiceCasa > 0 && indiceCasa < 10)
            {
                spriteBatch.DrawString(arial12, dono.Nome, new Vector2(CoordsAndSize.X, CoordsAndSize.Y), Color.Blue, MathHelper.ToRadians(0), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            if (indiceCasa > 10 && indiceCasa < 20)
            {
                spriteBatch.DrawString(arial12, dono.Nome, new Vector2(CoordsAndSize.X + CoordsAndSize.Width, CoordsAndSize.Y), Color.Blue, MathHelper.ToRadians(90), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            if (indiceCasa > 20 && indiceCasa < 30)
            {
                spriteBatch.DrawString(arial12, dono.Nome, new Vector2(CoordsAndSize.X + CoordsAndSize.Width, CoordsAndSize.Y + CoordsAndSize.Height), Color.Blue, MathHelper.ToRadians(180), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            if (indiceCasa > 30 && indiceCasa < 40)
            {
                spriteBatch.DrawString(arial12, dono.Nome, new Vector2(CoordsAndSize.X, CoordsAndSize.Y + CoordsAndSize.Height), Color.Blue, MathHelper.ToRadians(270), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            if (this is Rua)
            {
                ruaTemp = (Rua)this;
                ruaTemp.Draw(spriteBatch, arial12, indiceCasa);
            }
        }
        #endregion
    }
}
