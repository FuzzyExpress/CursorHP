using CursorHP.Configs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Config;

namespace CursorHP
{
	class Config : ModConfig
	{
		// You MUST specify a ConfigScope.
		public override ConfigScope Mode => ConfigScope.ClientSide;


        [DrawTicks]
		[OptionStrings(new string[] { "Off", "Images", "Legacy" })]
		[DefaultValue("Images")]
		public string ModeEnum;

        [Range(0f, 1f)]
        [DrawTicks]
        [DefaultValue(0.8f)]
        [Increment(0.1f)]
        [Slider]
        public float AlphaSlider { get; set; }


        [Expand(false)]
        public CommonConfig Common = new();
        public ImagesConfig Images = new();

        [Expand(false)]
        public LegacyConfig Legacy = new();


        public override void OnLoaded()
        {
            // Ensure the value stays within the defined range
            AlphaSlider = MathHelper.Clamp(AlphaSlider, 0f, 1f);
            Legacy.onLoad();
            Images.onLoad();
        }

    }
}
