using BepInEx;
using R2API.Utils;
using RoR2;

namespace InfusedTurrets
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    public class InfusedTurrets : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Chinchi";
        public const string PluginName = "InfusedTurrets";
        public const string PluginVersion = "1.0.0";

        private void OnEnable()
        {
            On.RoR2.CharacterMaster.AddDeployable += InheritInfusion;
        }

        private void OnDisable()
        {
            On.RoR2.CharacterMaster.AddDeployable -= InheritInfusion;
        }

        private void InheritInfusion(On.RoR2.CharacterMaster.orig_AddDeployable orig, CharacterMaster self, Deployable deployable, DeployableSlot slot)
        {
            orig(self, deployable, slot);
            if (slot == DeployableSlot.EngiTurret)
            {
                var minionMaster = deployable.GetComponent<CharacterMaster>();
                if (minionMaster && minionMaster.inventory && self.inventory)
                {
                    minionMaster.inventory.AddInfusionBonus(self.inventory.infusionBonus);
                }
            }
        }
    }
}