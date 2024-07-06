// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria;
// using Terraria.GameContent.UI.Elements;
// using Terraria.UI;

// namespace CursorHP
// {
//     public class SimpleHealthbarUI : UIState
//     {
//         private UIImage healthImage;
//         private UIImage manaImage;

//         public override void OnInitialize()
//         {
//             // Load textures
//             Texture2D healthTexture = ModContent.Request<Texture2D>("CursorHP/Textures/HealthIcon").Value;
//             Texture2D manaTexture = ModContent.Request<Texture2D>("CursorHP/Textures/ManaIcon").Value;

//             // Create UI elements
//             healthImage = new UIImage(healthTexture);
//             healthImage.Left.Set(100, 0f); // Position from the left
//             healthImage.Top.Set(100, 0f);  // Position from the top
//             Append(healthImage);

//             manaImage = new UIImage(manaTexture);
//             manaImage.Left.Set(150, 0f); // Position from the left
//             manaImage.Top.Set(100, 0f);  // Position from the top
//             Append(manaImage);
//         }
//     }
// }



