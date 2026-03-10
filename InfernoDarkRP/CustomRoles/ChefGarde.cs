using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace InfernoDarkRP.CustomRoles
{
    [CustomRole(RoleTypeId.FacilityGuard)]
    public class ChefGarde : CustomRole
    {
        public override uint Id { get; set; } = 3;
        public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "<color=grey>Chef Garde</color>";
        public override string Description { get; set; } = "<b>Ton rôle est de diriger le Département de la Sécurité interne du site.</b>";
        public override string CustomInfo { get; set; } = "Chef Garde";
        public override bool IgnoreSpawnSystem { get; set; } = true;

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardMTFPrivate}",
            $"{ItemType.Medkit}",
            $"{ItemType.Painkillers}",
            $"{ItemType.Radio}",
            $"{ItemType.ArmorCombat}",
            $"{ItemType.Jailbird}",
            $"{ItemType.GunCrossvec}",
        };
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new RoleSpawnPoint()
                {
                    Role = RoleTypeId.FacilityGuard,
                    Chance = 100
                }
            }
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato9, 220 },
        };
    }
}