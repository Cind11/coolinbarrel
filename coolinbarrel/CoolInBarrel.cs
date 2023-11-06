using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace coolinbarrel
{
    public class CoolInBarrel : BlockEntityBehavior
    {
        BlockEntity be;
        ICoreAPI api;

        long lastPlayedSizzlesTotalMs;

        public CoolInBarrel(BlockEntity blockentity) : base(blockentity)
        {
            be = blockentity;
        }

        public override void Initialize(ICoreAPI api, JsonObject properties)
        {
            base.Initialize(api, properties);
            this.api = api;
            be.RegisterGameTickListener(OnGameTick, 100);
        }

        public void OnGameTick(float dt)
        {
            BlockEntityBarrel bebarrel = be as BlockEntityBarrel;
            if (bebarrel == null) return;

            if ((bebarrel.Inventory[0]?.Empty ?? true) || (bebarrel.Inventory[1]?.Empty ?? true)) return;

            ItemStack stack = bebarrel.Inventory[0]?.Itemstack;
            if (stack == null) return;

            float temp = stack.Collectible?.GetTemperature(api.World, bebarrel.Inventory[0]?.Itemstack) ?? 0;

            if (temp > 20)
            {
                stack?.Collectible?.SetTemperature(api.World, stack, Math.Max(20, temp - 15));

                if (temp > 90)
                {
                    Entity.SplashParticleProps.BasePos.Set(Pos.X+0.5f, Pos.Y, Pos.Z+0.5f);
                    Entity.SplashParticleProps.AddVelocity.Set(0, 0, 0);
                    Entity.SplashParticleProps.QuantityMul = 0.3f;
                    api.World.SpawnParticles(Entity.SplashParticleProps);
                }

                if (temp > 200 && api.World.ElapsedMilliseconds - lastPlayedSizzlesTotalMs > 10000)
                {
                
                    api.World.PlaySoundAt(new AssetLocation("sounds/sizzle"), Pos.X, Pos.Y, Pos.Z);
                    lastPlayedSizzlesTotalMs = api.World.ElapsedMilliseconds;
                }
            }
        }
    }
}
