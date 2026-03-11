using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Roles;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Interactables.Interobjects;
using MEC;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp3114;
using UnityEngine;

namespace InfernoDarkRP.Features
{
    public class ScpContain
    {
        public static bool Scp049IsDead;
        public static bool Scp096IsDead;
        public static bool Scp173IsDead;
        public static bool Scp939IsDead;
        public static bool Scp106IsDead;

        private Player GetAvailableScpPlayer()
        {
            Player player = Player.Get(RoleTypeId.Overwatch).FirstOrDefault();

            if (player == null)
                player = Player.Get(RoleTypeId.Spectator).FirstOrDefault();

            return player;
        }

        #region ROUND START
        public void OnRoundStart()
        {
            foreach (Player player in Player.List.Where(o => o.IsScp))
            {
                player.Role.Set(RoleTypeId.Overwatch);
                player.Broadcast(10, "<b>Tu es <color=red><i>SCP.</i></color>\nAttend qu'un <i><color=blue>confinement</color></i> s'ouvre pour apparaître</b>");
            }

            foreach (Exiled.API.Features.Doors.Door door in Room.Get(RoomType.Hcz096).Doors)
            {
                door.KeycardPermissions = KeycardPermissions.ContainmentLevelTwo;
            }
        }
        #endregion

        #region SPAWN SCP
        public void SCPSpawned(SpawnedEventArgs ev)
        {
            if (ev.Reason is SpawnReason.RoundStart)
                return;
        }
        #endregion

        #region DOORS OPEN
        private void OpenDoorMessage(Player player, string messageEng, string messageFr)
        {
            Exiled.API.Features.Cassie.MessageTranslated(messageEng, messageFr);
        }

        public void SCP049DoorOpen(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed || Player.Get(RoleTypeId.Scp049).Any() || Scp049IsDead) return;
            Player player = GetAvailableScpPlayer();
            if (player == null) return;
            if (ev.Door.Type is DoorType.Scp049Gate)
            {
                player.Role.Set(RoleTypeId.Scp049, RoleSpawnFlags.UseSpawnpoint);
                OpenDoorMessage(player,
                    "The containment chamber of SCP 0 4 9 has been opened",
                    "<b>La chambre de confinement de<color=red><i> SCP-049</i></color> a été ouverte.</b>");
            }
        }

        public void SCP096DoorOpen(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed || Player.Get(RoleTypeId.Scp096).Any() || Scp096IsDead) return;
            Player player = GetAvailableScpPlayer();
            if (player == null) return;
            if (ev.Door.Type is DoorType.Scp096)
            {
                player.Role.Set(RoleTypeId.Scp096, RoleSpawnFlags.UseSpawnpoint);
                OpenDoorMessage(player,
                    "The containment chamber of SCP 0 9 6 has been opened",
                    "<b>La chambre de confinement de<color=red><i> SCP-096</i></color> a été ouverte, veuillez baisser les yeux.</b>");
            }
        }

        public void SCP173DoorOpen(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed || Player.Get(RoleTypeId.Scp173).Any() || Scp173IsDead) return;
            Player player = GetAvailableScpPlayer();
            if (player == null) return;
            if (ev.Door.Type is DoorType.Scp173NewGate)
            {
                player.Role.Set(RoleTypeId.Scp173, RoleSpawnFlags.UseSpawnpoint);
                OpenDoorMessage(player,
                    "The containment chamber of SCP 1 7 3 has been opened",
                    "<b>La chambre de confinement de<color=red><i> SCP-173</i></color> a été ouverte.</b>");
            }
        }

        public void SCP939DoorOpen(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed || Player.Get(RoleTypeId.Scp939).Any() || Scp939IsDead) return;
            Player player = GetAvailableScpPlayer();
            if (player == null) return;
            if (!(ev.Player.CurrentRoom.Type is RoomType.Hcz939)) return;
            if (ev.Door.Type is DoorType.LightContainmentDoor)
            {
                player.Role.Set(RoleTypeId.Scp939, RoleSpawnFlags.UseSpawnpoint);
                OpenDoorMessage(player,
                    "The containment chamber of SCP 9 3 9 has been opened",
                    "<b>La chambre de confinement de<color=red><i> SCP-939</i></color> a été ouverte.</b>");
            }
        }
        
        public void SCP106DoorOpen(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed || Player.Get(RoleTypeId.Scp106).Any() || Scp106IsDead) return;
            Player player = GetAvailableScpPlayer();
            if (player == null) return;
            if (!(ev.Player.CurrentRoom.Type is RoomType.Hcz106)) return;
            if (ev.Door.Type is DoorType.Scp106Primary || ev.Door.Type is DoorType.Scp106Secondary)
            {
                player.Role.Set(RoleTypeId.Scp106, RoleSpawnFlags.UseSpawnpoint);
                OpenDoorMessage(player,
                    "The containment chamber of SCP 1 0 6 has been opened",
                    "<b>La chambre de confinement de<color=red><i> SCP-939</i></color> a été ouverte.</b>");
            }
        }
        #endregion
    }
}