using System.ComponentModel;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;


namespace CursorHP.Configs
{
    class LegacyConfig
    {

        [DefaultValue(true)]
		public bool FancyHealth;

        

        [Range(0.025f, 1f)]
        [DrawTicks]
        [DefaultValue(0.05f)]
        [Increment(0.025f)]
        [Slider]
        public float LineMultiplier { get; set; }


        [Range(-40, 100)]
        [DrawTicks]
        [DefaultValue(0)]
        [Increment(10)]
        [Slider]
        public int RaidusOffest { get; set; }

        public void onLoad()
        {
            //AlphaSlider = MathHelper.Clamp(AlphaSlider, 0f, 1f);
            LineMultiplier = MathHelper.Clamp(LineMultiplier, 0f, 1f);
            RaidusOffest = (int) MathHelper.Clamp(RaidusOffest, -40, 100);
        }

    }
    
} /* public override void OnLoaded() {
            // Ensure the value stays within the defined range
            AlphaSlider = MathHelper.Clamp(AlphaSlider, 0f, 1f);
            LineMultiplier = MathHelper.Clamp(LineMultiplier, 0f, 1f);
            ScaleSlider = (int) MathHelper.Clamp(ScaleSlider, -40, 100);
        }*/
