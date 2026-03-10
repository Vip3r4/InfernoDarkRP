using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace InfernoDarkRP.CustomRoles
{
    [CustomRole(RoleTypeId.Scientist)]
    public class ScientifiqueSuperviseur : CustomRole
    {
        public override uint Id { get; set; } = 2;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "<color=yellow>Scientifique Superviseur</color>";
        public override string Description { get; set; } = "<b>Ton rôle est de réaliser des expériences sur les anomalies <color=red>SCP</color>.</b>";
        public override string CustomInfo { get; set; } = "Scientifique Superviseur<";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardResearchCoordinator}",
            $"{ItemType.Medkit}",
            $"{ItemType.Painkillers}",
            $"{ItemType.Radio}",
            $"{ItemType.ArmorLight}",
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
    }
}