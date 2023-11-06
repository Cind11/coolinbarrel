using Vintagestory.API.Common;

namespace coolinbarrel
{
    public class coolinbarrelModSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockEntityBehaviorClass("CoolInBarrel", typeof(CoolInBarrel));
        }
    }
}
