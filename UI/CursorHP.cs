using Terraria;
using Terraria.UI;

namespace CursorHP.UI
{
    public class CursorHPImager : UIState
    {
        public CursorHPDrawer Drawer;

        public override void OnInitialize()
        {
            Drawer = new CursorHPDrawer();

            Append(Drawer);
        }
    }
}