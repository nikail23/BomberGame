using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGameProject.Classes
{
    public abstract class GameObject : Transformable, Drawable
    {
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
