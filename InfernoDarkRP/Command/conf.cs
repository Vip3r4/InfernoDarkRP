using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using InventorySystem.Items.ThrowableProjectiles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Exiled.API.Features;
using System.Text.RegularExpressions;
using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using Exiled.Events.Handlers;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace InfernoDarkRP.Command
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ScpRecontainCommand : ICommand
    {
        public string Command => "conf";

        public string[] Aliases => new string[] { "conf" };

        public string Description => "Command that allow SCP to recontain themself in a room";

        private static List<RoomType> Rooms = new List<RoomType>()
        {
            RoomType.LczArmory,
            RoomType.HczArmory,
            RoomType.EzIntercom,
            RoomType.Lcz330,
            RoomType.Hcz079,
            RoomType.Hcz939,
            RoomType.Hcz049
        };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!player.IsScp || player.Role.Type == RoleTypeId.Scp079)
            {
                response = "Seulement les SCP peuvent faire cela";
                return false;
            }
            
            string text = player.Role.Type.ToString();
            string text2 = string.Join(" ", Regex.Replace(player.Role.ToString(), "[^0-9]+", "").ToCharArray());

            if (Rooms.Contains(player.CurrentRoom.Type))
            {
                /*switch (player.CurrentRoom.Type)
                {
                    case RoomType.LczArmory:
                        
                        if (Door.Get(DoorType.LczArmory).IsOpen)
                        {
                            response = "Vous ne pouvez pas faire cela si la porte est ouverte.";
                            return false;
                        }

                        if (player.Role != RoleTypeId.Scp0492)
                        {
                            Door.Get(DoorType.LczArmory).Lock(Single.MaxValue, DoorLockType.Lockdown2176);
                            Exiled.API.Features.Cassie.MessageTranslated("scp " + text2 + " contained successfully in Light containment zone Armory", text + " <b>à été reconfiné avec succès dans <color=yellow><b><i>l'Armurie de la LCZ.</i></color></b>");
                        }

                        Ragdoll.CreateAndSpawn(player.Role, player.Nickname, "Successfully recontained", player.Position, default(Quaternion), player);
                        player.ReferenceHub.roleManager.ServerSetRole(RoleTypeId.Overwatch, RoleChangeReason.Died);
                        response = "Done.";
                        return true;
                }*/
                if (!Rooms.Contains(player.CurrentRoom.Type))
                {
                    response = "Vous ne pouvez pas vous reconfiner dans cette salle.";
                    return false;
                }

                Door door = player.CurrentRoom.Doors.FirstOrDefault();

                if (door != null && door.IsOpen)
                {
                    response = "Vous ne pouvez pas faire cela si la porte est ouverte.";
                    return false;
                }

                door?.Lock(float.MaxValue, DoorLockType.Lockdown2176);

                string scp = player.Role.Type.ToString().Replace("Scp", "");

                Exiled.API.Features.Cassie.MessageTranslated(
                    $"scp {scp} contained successfully",
                    $"SCP {scp} <b>a été reconfiné.</b>"
                );

                Ragdoll.CreateAndSpawn(player.Role, player.Nickname, "Successfully recontained", player.Position, Quaternion.identity, player);

                player.Role.Set(RoleTypeId.Overwatch);

                response = "Reconfinement réussi.";
                return true;
            }
            else
            {
                response = "Vous ne pouvez pas vous reconfiner dans cette salle";
                return false;
            }
        }
    }
}