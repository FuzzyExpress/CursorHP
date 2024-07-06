using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;
using System.Linq; // Import the System.Linq namespace



namespace CursorHP

{
    public class SimpleHealthbar : Mod
    { public SimpleHealthbar() { } }

    public class SimpleHealthbarPlayer : ModPlayer
    {
        public static Vector2 GlobalMouse;
        public float AS;

        public float LineX = GetInstance<Config>().Legacy.LineMultiplier;

        public int LineCount = 0;

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            // if (GetInstance<Config>().Enabled) {

            // Console.WriteLine(GetInstance<Config>().ModeEnum);
            
            if (GetInstance<Config>().ModeEnum == "Legacy") {


                LineCount = 0;

                LineX = GetInstance<Config>().Legacy.LineMultiplier;

                //  var DrawMatrix = new SpriteBatch(Main.graphics.GraphicsDevice);
                //  DrawMatrix.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                var DrawMatrix = Main.spriteBatch;

                var Player = Main.myPlayer;
                base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);

                Vector2 Mouse;
                if (!GetInstance<Config>().Common.RingAroundPlayer)
                {
                    if (!Main.player[Player].dead || !GetInstance<Config>().Common.BreakOnDeath)
                    {
                        Mouse = Main.MouseWorld;
                        GlobalMouse = Main.MouseWorld;
                    }
                    else
                    {
                        Mouse = GlobalMouse;
                    }
                }
                else
                {
                    Mouse.X = (int)(Main.player[Player].position.X + (float)(Main.player[Player].width  / 2));
                    Mouse.Y = (int)(Main.player[Player].position.Y + (float)(Main.player[Player].height / 2));// + Main.player[num25].gfxOffY);
                }
                int health = Main.player[Player].statLife;
                int maxHealth = Main.player[Player].statLifeMax2;


                int scale = GetInstance<Config>().Legacy.RaidusOffest;
                AS = GetInstance<Config>().AlphaSlider;

                if (Main.player[Player].active ) //&& !Main.player[num25].ghost && !Main.player[num25].dead)
                {
                
                    if ( Main.player[Player].statLife != Main.player[Player].statLifeMax2 || GetInstance<Config>().Common.ForceHealth && GetInstance<Config>().Common.ShowHealth )
                    {                    
                        Color fg = HSVToRGB(((float)health / maxHealth) * 120, 1f, 1.0f  );
                        Color bg = HSVToRGB(((float)health / maxHealth) * 120, 1f, 0.666f);
                        Color mg = HSVToRGB(((float)health / maxHealth) * 120, 1f, 0.333f);

                        if (GetInstance<Config>().Legacy.FancyHealth)
                            DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 50 + scale, 5, 0f, 360f, bg);

                        if (GetInstance<Config>().Legacy.FancyHealth)
                            DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 51 + scale, 3, 0f, 360f, mg);

                        DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 51 + scale, 3, 0f, (float)health / maxHealth * 360f, fg);
                    }

                    if (GetInstance<Config>().Common.ShowMana)
                    {
                        if (Main.player[Player].statMana != Main.player[Player].statManaMax2)
                        {
                            float mana = ((float)Main.player[Player].statMana / Main.player[Player].statManaMax2);
                            float fade = Map(mana, 0.75f, 1f, 1f, 0f);

                            if (GetInstance<Config>().Legacy.FancyHealth)
                                DrawCircleRadial( DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 56 + scale, 2, mana * 360, 360, new Color(0, 40,  80,  (int)(fade * 360)) );
                            DrawCircleRadial( DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 56 + scale, 2, 0f, mana *  360, new Color(0, 120, 255, (int)(fade * 360)) );
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
                                
                                    DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 47 + scale, 2, 0f, ( Main.player[Player].buffTime[i] / 10f ), new Color(255, 0, 0));
                                  //  DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 54 + scale, 1, 0f, ( Main.player[num25].buffTime[i] / 10f ), new Color(255, 0, 0));
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
                                    DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 60 + scale, 2, 0f, (Main.player[Player].buffTime[i]), new Color(255, 0, 255));
                                    //  DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 54 + scale, 1, 0f, ( Main.player[num25].buffTime[i] / 10f ), new Color(255, 0, 0));
                                }
                            }
                        }
                    }
                    // Console.WriteLine(Main.player[num25].wingTime + " " + Main.player[num25].wingTimeMax);

                    if ( GetInstance<Config>().Common.ShowFlightTime && Main.player[Player].wingTime < Main.player[Player].wingTimeMax )
                    { // 60 2
                        DrawCircleRadial(DrawMatrix, (int)Mouse.X, (int)Mouse.Y, 60 + scale, 2, 0f, ((float)Main.player[Player].wingTime / Main.player[Player].wingTimeMax) * 360, 
                            new Color(200, 200, 200, Map(Main.player[Player].wingTime / Main.player[Player].wingTimeMax, 0.75f, 1f, 1f, 0f) )); // , "Flight: " float fade = Map(mana, 0.75f, 1f, 1f, 0f);
                    }
                }
            }
        }

        public static float Map(float value, float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            // Ensure that the value is within the source range
            value = MathHelper.Clamp(value, sourceMin, sourceMax);

            // Calculate the normalized value within the source range
            float normalizedValue = (value - sourceMin) / (sourceMax - sourceMin);

            // Map the normalized value to the target range
            float result = targetMin + normalizedValue * (targetMax - targetMin);

            return result;
        }
        Color PremultiplyAlpha(Color colorin)
        {
            Color color = new Color( colorin.R, colorin.G, colorin.B, (int) (colorin.A * AS) );
            float alpha = color.A / 255.0f;
            return new Color(
                (byte)(color.R * alpha),
                (byte)(color.G * alpha),
                (byte)(color.B * alpha),
                color.A
            );
        }
        public static Color HSVToRGB(float hue, float saturation, float value)
        {
          //  Console.WriteLine(" " + hue + " " + saturation + " " + value);
            int hi = (int)(hue / 60) % 6;
            float f = hue / 60 - hi;
            float p = value * (1 - saturation);
            float q = value * (1 - f * saturation);
            float t = value * (1 - (1 - f) * saturation);

            float r, g, b;

            switch (hi)
            {
                case 0: r = value; g = t; b = p; break;
                case 1: r = q; g = value; b = p; break;
                case 2: r = p; g = value; b = t; break;
                case 3: r = p; g = q; b = value; break;
                case 4: r = t; g = p; b = value; break;
                case 5: r = value; g = p; b = q; break;
                default: r = g = b = value; break;
            }

            int red = (int)(r * 255);
            int green = (int)(g * 255);
            int blue = (int)(b * 255);

          //  Console.WriteLine(" " + red + " " + green + " " + blue);
            return new Color(red, green, blue, 255);
        }
        public void DrawCircleRadial(SpriteBatch spriteBatch, int posX, int posY, float radius, float width, float degreeStart, float degreeEnd, Color fill, string label = "")
        {
            // Ensure the angles are within the range [0, 360].
            degreeStart = MathHelper.Clamp(degreeStart, 0f, 360f);
            degreeEnd   = MathHelper.Clamp(degreeEnd, 0f, 360f);

            degreeStart -= 90;
            degreeEnd   -= 90;

          //  if (label != "")
              //  Console.WriteLine(label + posX  +" "+ posY +" "+ radius +" "+ width +" "+ degreeStart +" "+ degreeEnd +" "+ fill);// + degreeStart + " " + degreeEnd + " " + fill);
              //  Console.WriteLine( (int)Math.Ceiling(radius * (endRadians - startRadians)) );


            for (int w = 0; w < width; w++)
            {
                // Calculate the start and end radians.
                float startRadians = MathHelper.ToRadians(degreeStart);
                float endRadians = MathHelper.ToRadians(degreeEnd);

                // Calculate the number of segments based on the circumference.
                int numSegments = (int) ( Math.Ceiling(radius * (endRadians - startRadians)) * LineX );

                if (numSegments < 2)
                    numSegments = 2;

              //  LineCount += numSegments -1;
              //  int numSegments = 10;

                // Draw the radial circle using a series of line segments.
                for (int i = 0; i < numSegments; i++)
                {
                    float progress1 = (float) i / numSegments;
                    float progress2 = (float)(i + 1) / numSegments;

                    float angle1 = MathHelper.Lerp(startRadians, endRadians, progress1);
                    float angle2 = MathHelper.Lerp(startRadians, endRadians, progress2);

                    float x1 = posX + (radius + w) * (float)Math.Cos(angle1);
                    float y1 = posY + (radius + w) * (float)Math.Sin(angle1);

                    float x2 = posX + (radius + w) * (float)Math.Cos(angle2);
                    float y2 = posY + (radius + w) * (float)Math.Sin(angle2);

                    Color DrawFill = PremultiplyAlpha(fill);

                    LineCount++;
                    Terraria.Utils.DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), DrawFill, DrawFill, 1);
                }
                LineCount--;
            }
        }

    }
}