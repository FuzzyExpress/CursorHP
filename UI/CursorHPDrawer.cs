using System;
// using System.Collections.Generic;
// using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
// using Terraria.GameContent;
// using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;


// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using ReLogic.Graphics;
// using System;
// using System.Collections.Generic;
// using System.Collections.Specialized;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.GameContent;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;



namespace CursorHP.UI
{
    public class CursorHPDrawer : UIElement
    {
        private bool EnableNew;
        private static Vector2 GlobalMouse;
        private static int reloadFrame = -5;
        public float AS;

        private static readonly int RingFrameMax = 200;

        public override void Update(GameTime gameTime)
        { 
            if (GetInstance<Config>().ModeEnum == "Images")
            {
                EnableNew = true;
            }
            else
            {
                EnableNew = false;
                reloadFrame = -5;
            }
        }
   
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            // spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            
            

            // Draw nothing if cursor is disabled
            if (!EnableNew)
            {
                return;
            }

            var Player = Main.myPlayer;
            Vector2 Mouse;
            float alpha = GetInstance<Config>().AlphaSlider;
            int health = Main.player[Player].statLife;
            int maxHealth = Main.player[Player].statLifeMax2;

            if (!GetInstance<Config>().Common.RingAroundPlayer)
            {
                if (!Main.player[Player].dead || !GetInstance<Config>().Common.BreakOnDeath)
                {
                    Mouse = Main.MouseScreen;
                    GlobalMouse = Main.MouseScreen;
                }
                else
                {
                    Mouse = GlobalMouse;
                }
            }
            else
            {
                // Mouse.X = (int)(Main.player[num25].position.X + (float)(Main.player[num25].width  / 2));
                // Mouse.Y = (int)(Main.player[num25].position.Y + (float)(Main.player[num25].height / 2));// + Main.player[num25].gfxOffY);
                Mouse = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
            }



            /// Image Loader Scriptclip
            // /*
            if (reloadFrame == -1) { print("[CursorHP]: Running Image Loader"); }
            
            else if (reloadFrame >= 0 && reloadFrame <= 500)
            {
                Draw(  spriteBatch, GetCursorTexture("health", reloadFrame), new Vector2(Main.screenWidth, Main.screenHeight) / 2f, 0.5f );
                if (reloadFrame <= RingFrameMax)
                {
                    foreach (string str in new string[] {"healthSickness", "mana", "manaSickness", "wingTime", "chaosState"})
                    { Draw( spriteBatch, GetCursorTexture(str, reloadFrame), new Vector2(Main.screenWidth, Main.screenHeight) / 2f, 0.5f ); } 
                }
            }
            
            else if (reloadFrame == 501) { print("[CursorHP]: Image Loader Done"); }
            if (Main.PlayerLoaded) { reloadFrame += 1; } // */
 

            // Vector2 Test = Mouse;
            // Test.X -= 200;
            // Draw(spriteBatch, GetCursorTexture(), Test, alpha);


            /// Draw Rings
            if (Main.player[Player].active)
            {
                if (Main.player[Player].statLife != Main.player[Player].statLifeMax2 || GetInstance<Config>().Common.ForceHealth && GetInstance<Config>().Common.ShowHealth)
                {
                    int frame = (int)Math.Round( Map( health, 0, maxHealth, 500, 0) );
                    var image = GetCursorTexture("health", frame);
                    Draw(spriteBatch, image, Mouse, alpha);
                }
            }
            if (GetInstance<Config>().Common.ShowPotionSickness)
            {
                if (Main.player[Player].statLife != Main.player[Player].statLifeMax2 || GetInstance<Config>().Common.ShowPotionSicknessAtFullHealth)
                {
                    for (int i = 0; i < Main.player[Player].buffType.Length; i++)
                    {
                        if (Main.player[Player].buffType[i] == BuffID.PotionSickness)
                        {
                            int frame = (int)Math.Round( Map( Main.player[Player].buffTime[i], 0, 60*60, RingFrameMax, 0) );
                            var image = GetCursorTexture("healthSickness", frame);
                            Draw(spriteBatch, image, Mouse, alpha);
                        }
                    }
                }
            }


            if (GetInstance<Config>().Common.ShowMana)
            {
                int mana    = Main.player[Player].statMana;
                int manaMax = Main.player[Player].statManaMax2;
                if (mana != manaMax)
                {
                    int frame = (int)Math.Round( Map( mana, 0, manaMax, RingFrameMax, 0) );
                    var image = GetCursorTexture("mana", frame);
                    Draw(spriteBatch, image, Mouse, alpha);
                }
            }
            if (GetInstance<Config>().Common.ShowManaSickness)
            {
                if (Main.player[Player].statMana != Main.player[Player].statManaMax2 || GetInstance<Config>().Common.ShowManaSicknessAtFullMana)
                {
                    for (int i = 0; i < Main.player[Player].buffType.Length; i++)
                    {
                        if (Main.player[Player].buffType[i] == BuffID.ManaSickness)
                        {
                            int frame = (int)Math.Round( Map( Main.player[Player].buffTime[i], 0, 10*60, RingFrameMax, 0) );
                            var image = GetCursorTexture("manaSickness", frame);
                            Draw(spriteBatch, image, Mouse, alpha);
                        }
                    }
                }
            }


            if (GetInstance<Config>().Common.RodOfDiscord)
            {
                if (true)
                {
                    for (int i = 0; i < Main.player[Player].buffType.Length; i++)
                    {
                        if (Main.player[Player].buffType[i] == BuffID.ChaosState)
                        {
                            Console.WriteLine(Main.player[Player].buffTime[i]);
                            int frame = (int)Math.Round( Map( Main.player[Player].buffTime[i], 0, 6*60, RingFrameMax, 0) );
                            var image = GetCursorTexture("chaosState", frame);
                            Draw(spriteBatch, image, Mouse, alpha);
                        }
                    }
                }
            }

            if ( GetInstance<Config>().Common.ShowFlightTime && Main.player[Player].wingTime < Main.player[Player].wingTimeMax )
            { // 60 2
                int frame = (int)Math.Round( Map( Main.player[Player].wingTime, 0, Main.player[Player].wingTimeMax, RingFrameMax, 0) );
                var image = GetCursorTexture("wingTime", frame);
                Draw(spriteBatch, image, Mouse, alpha);
            }



        }
        

        private static void Draw(SpriteBatch spriteBatch, Texture2D image, Vector2 vector, float alpha)
        {
            spriteBatch.Draw(
                image,
                vector, 
                null,
                Color.White * alpha,
                0f,
                image.Size() / 2f,
                0.9f,
                SpriteEffects.None,
                1);
        }

        
        private static Texture2D GetCursorTexture(string intake, int frame)
        {   
            return ModContent.Request<Texture2D>( "CursorHP/sprites/" + intake + "/" + frame.ToString() + "" ).Value;
        }
        private static Texture2D GetCursorTexture()
        {   
            return ModContent.Request<Texture2D>( "CursorHP/sprites/health/300Test" ).Value;
        }

        public static void print(string str)
        {
            Console.WriteLine(str);
            Main.NewText(str);
        }
        private static float Clamp(float value, float min, float max)
        {
            if (value > max)
            {
                return max;
            }
            if (value < min)
            {
                return min;
            }
            return value;
        }
        public static float Map(float value, float FromMin, float FromMax, float ToMin, float ToMax)
        {
            // Ensure that the value is within the source range
            value = MathHelper.Clamp(value, FromMin, FromMax);

            // Calculate the normalized value within the source range
            float normalizedValue = (value - FromMin) / (FromMax - FromMin);

            // Map the normalized value to the target range
            float result = ToMin + normalizedValue * (ToMax - ToMin);

            return result;
        }
    }
}