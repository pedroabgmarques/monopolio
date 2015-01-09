    /*
 * Date: 08/11/2014
 * Author: David Gouveia
 * 
 * Gere as camadas de parallax e desenha as sprites
 * http://www.david-gouveia.com/portfolio/2d-camera-with-parallax-scrolling-in-xna/
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
    /// Descreve uma camada de parallax
    /// </summary>
    public class ParallaxLayer
    {
        /// <summary>
        /// Construtor de uma camada de parallax
        /// </summary>
        /// <param name="camera">Instância da camara</param>
        public ParallaxLayer(Camera camera)
        {
            this.camera = camera;
            parallax = Vector2.One;
            sprites = new List<Sprite>();
        }

        /// <summary>
        /// Getter / Setter da camada de parallax
        /// </summary>
        public Vector2 parallax { get; set; }
        /// <summary>
        /// Getter da lista de sprites da camada de parallax
        /// </summary>
        public List<Sprite> sprites { get; private set; }

        /// <summary>
        /// Desenha uma camada de parallax (todas as suas sprites) no ecrã
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getTransformation(parallax));
            foreach (Sprite sprite in sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        private readonly Camera camera;
    }
}
