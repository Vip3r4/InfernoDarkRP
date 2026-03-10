using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace InfernoDarkRP.CustomRoles
{
    [CustomRole(RoleTypeId.Scientist)]
    public class DirecteurDuSite : CustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "<color=red>Directeur du Site</color>";
        public override string Description { get; set; } = "<b>Diriges le site et veilles à son bon fonctionnement.</b>";
        public override string CustomInfo { get; set; } = "Directeur du Site";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardFacilityManager}",
            $"{ItemType.ArmorCombat}",
            $"{ItemType.Medkit}",
            $"{ItemType.Adrenaline}",
            $"{ItemType.Radio}",
            $"{ItemType.GunCOM18}"
        };

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new RoleSpawnPoint()
                {
                    Role = RoleTypeId.Scientist,
                    Chance = 100
                }
            }
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato9, 85 },
        };
    }
}