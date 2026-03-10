using System;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using InfernoDarkRP.CustomRoles;

namespace InfernoDarkRP.Plugin
{
    public class Plugin : Plugin<Config.Config>
    {
        public override string Name => "InfernoDarkRP";
        public override string Author => "Vip3r";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 13, 1);

        private Exiled.CustomRoles.API.Features.CustomRole _directeur;
        private Exiled.CustomRoles.API.Features.CustomRole _scientifiquesup;
        private Exiled.CustomRoles.API.Features.CustomRole _chefgarde;
        
        public override void OnEnabled()
        {
            _directeur = new DirecteurDuSite();
            _directeur.Register();
            _scientifiquesup = new ScientifiqueSuperviseur();
            _scientifiquesup.Register();
            _chefgarde = new ChefGarde();
            _chefgarde.Register();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _directeur.Unregister();
            _scientifiquesup.Unregister();
            _chefgarde.Unregister();
            
            base.OnEnabled();
        }
    }
}