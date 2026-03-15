using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using InfernoDarkRP.CustomRoles;
using HarmonyLib;
using InfernoDarkRP.Features;

namespace InfernoDarkRP.Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "InfernoDarkRP";
        public override string Author => "Vip3r";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 13, 1);

        private Exiled.CustomRoles.API.Features.CustomRole _directeur;
        private Exiled.CustomRoles.API.Features.CustomRole _scientifiquesup;
        private Exiled.CustomRoles.API.Features.CustomRole _chefgarde;
        
        private EventHandlers EventHandler;
        private ScpContain ScpContain;
        
        public override void OnEnabled()
        {
            EventHandler = new EventHandlers();
            ScpContain = new ScpContain();
            
            _directeur = new DirecteurDuSite();
            _directeur.Register();
            _scientifiquesup = new ScientifiqueSuperviseur();
            _scientifiquesup.Register();
            _chefgarde = new ChefGarde();
            _chefgarde.Register();
            
            Exiled.Events.Handlers.Player.Dying += EventHandler.WhenSCPDie;
            Exiled.Events.Handlers.Server.RoundStarted += EventHandler.OnRoundStart;
            
            Exiled.Events.Handlers.Server.RoundStarted += ScpContain.OnRoundStart;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpContain.SCP049DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpContain.SCP096DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpContain.SCP173DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpContain.SCP939DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpContain.SCP106DoorOpen;
            Exiled.Events.Handlers.Player.Spawned += ScpContain.SCPSpawned;
            
            Features.ProximityChat.RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _directeur.Unregister();
            _scientifiquesup.Unregister();
            _chefgarde.Unregister();
            
            Exiled.Events.Handlers.Player.Dying -= EventHandler.WhenSCPDie;
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandler.OnRoundStart;
            
            Exiled.Events.Handlers.Server.RoundStarted -= ScpContain.OnRoundStart;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpContain.SCP049DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpContain.SCP096DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpContain.SCP173DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpContain.SCP939DoorOpen;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpContain.SCP106DoorOpen;
            Exiled.Events.Handlers.Player.Spawned -= ScpContain.SCPSpawned;
            
            Features.ProximityChat.UnregisterEvents();

            EventHandler = null;
            ScpContain = null;
            base.OnDisabled();
        }
    }
}