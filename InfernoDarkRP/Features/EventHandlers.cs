using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;

namespace InfernoDarkRP.Features
{
	public class EventHandlers
	{
		public void WhenSCPDie(DyingEventArgs ev)
		{
			if (ev.Player == null || ev.Attacker == null)
			{
				return;
			}

			RoleTypeId role = ev.Player.Role;
			if (role == RoleTypeId.Scp173)
			{
				ScpContain.Scp173IsDead = true;
				return;
			}

			if (role == RoleTypeId.Scp049)
			{
				ScpContain.Scp049IsDead = true;
				return;
			}

			if (role == RoleTypeId.Scp096)
			{
				ScpContain.Scp096IsDead = true;
				return;
			}

			if (role == RoleTypeId.Scp939)
			{
				ScpContain.Scp939IsDead = true;
				return;
			}

			if (role == RoleTypeId.Scp106)
			{
				ScpContain.Scp106IsDead = true;
				return;
			}
		}

		public void OnRoundStart()
		{
			foreach (Exiled.API.Features.Doors.Door door in Room.Get(RoomType.LczGlassBox).Doors)
			{
				door.KeycardPermissions = KeycardPermissions.ArmoryLevelOne;
			}

			foreach (Window window in Room.Get(RoomType.LczGlassBox).Windows)
			{
				window.DisableScpDamage = true;
				window.Health = 200f;
			}
		}
	}
}