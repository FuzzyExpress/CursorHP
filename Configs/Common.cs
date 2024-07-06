using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CursorHP.Configs
{
    class CommonConfig
    {
        [Header("HeathHeader")]

        [DefaultValue(true)]
        public bool ShowHealth;

        [DefaultValue(false)]
        public bool ForceHealth;

        [DefaultValue(true)]
		public bool ShowPotionSickness;

		[DefaultValue(true)]
		public bool ShowPotionSicknessAtFullHealth;

        // mana
        [Header("ManaHeader")]


		[DefaultValue(true)]
		public bool ShowMana;

        [DefaultValue(true)]
		public bool ShowManaSickness;

		[DefaultValue(false)]
		public bool ShowManaSicknessAtFullMana;

        // other
        [Header("OtherHeader")]


        [DefaultValue(true)]
        public bool RodOfDiscord;

        [DefaultValue(true)]
        public bool ShowFlightTime;

        [DefaultValue(false)]
        public bool RingAroundPlayer;

        [DefaultValue(true)]
        public bool BreakOnDeath;
    }
}
