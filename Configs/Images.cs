using System.ComponentModel;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;


namespace CursorHP.Configs
{
    class ImagesConfig
    {
        [Range(20, 200)]
        [DrawTicks]
        [DefaultValue(90)]
        [Increment(10)]
        [Slider]
        public int Scale { get; set; }

        public void onLoad()
        {
            Scale = (int) MathHelper.Clamp(Scale, 20, 200);
        }
    }
    
}