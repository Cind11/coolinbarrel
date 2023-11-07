using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace coolinbarrel
{
    public class coolinbarrelModSystem : ModSystem
    {
        public static string[] QuenchBonusMats = null;
        public static bool QuenchEnabled = false;

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockEntityBehaviorClass("CoolInBarrel", typeof(CoolInBarrel));

            var usefullStuffconfig = api.LoadModConfig("UsefulStuffConfig.json");
            if (usefullStuffconfig != null)
            {
                QuenchBonusMats = usefullStuffconfig["QuenchBonusMats"]?.AsArray<string>();
                QuenchEnabled = usefullStuffconfig["QuenchEnabled"]?.AsBool() ?? false;
            }
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            foreach (var block in api.World.Blocks)
            {
                if (block is BlockBarrel)
                {
                    var behavior = new BlockEntityBehaviorType()
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
