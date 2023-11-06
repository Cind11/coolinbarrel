using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace coolinbarrel
{
    public class coolinbarrelModSystem : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide.IsServer();
        }

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockEntityBehaviorClass("CoolInBarrel", typeof(CoolInBarrel));
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            foreach (Block block in api.World.Blocks)
            {
                if (block is BlockBarrel)
                {
                    BlockEntityBehaviorType behavior = new BlockEntityBehaviorType()
                    {
                        Name = "CoolInBarrel",
                        properties = null
                    };

                    block.BlockEntityBehaviors = block.BlockEntityBehaviors.Append(behavior);
                }
            }
        }
    }
}
