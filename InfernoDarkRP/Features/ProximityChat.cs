using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.Spectating;
using System.Collections.Generic;
using UnityEngine;
using VoiceChat;
using VoiceChat.Networking;
using IVoiceRole = PlayerRoles.Voice.IVoiceRole;
using Player = Exiled.API.Features.Player;
using SpectatorRole = PlayerRoles.Spectating.SpectatorRole;

namespace InfernoDarkRP.Features
{
    internal class ProximityChat
    {
        public ProximityChat()
        {
            Instance = this;
        }
        public static ProximityChat Instance;
        public static void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.VoiceChatting += OnPlayerUsingVoiceChat;
            Exiled.Events.Handlers.Server.RestartingRound += OnRoundRestarted;

            Exiled.Events.Handlers.Player.TogglingNoClip += OnPlayerTogglingNoClip;

            Exiled.Events.Handlers.Player.ChangingRole += OnChangeRole;
        }

        public static void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.VoiceChatting -= OnPlayerUsingVoiceChat;
            Exiled.Events.Handlers.Server.RestartingRound -= OnRoundRestarted;

            Exiled.Events.Handlers.Player.TogglingNoClip -= OnPlayerTogglingNoClip;

            Exiled.Events.Handlers.Player.ChangingRole -= OnChangeRole;
        }

        public static List<Player> ToggledPlayers { get; internal set; } = new List<Player>();

        private static void OnRoundRestarted()
        {
            ToggledPlayers.Clear();
        }

        private static void OnChangeRole(ChangingRoleEventArgs args)
        {
            if (args.NewRole == RoleTypeId.Scp049)
            {
                string hudText = $"<b> Tu peux activer le chat de proximité en utilisant la touche assignée du Noclip dans tes paramètres.</b>";
                var dynamicHint = new DynamicHint
                {
                    Id = "pchatnoclip",
                    FontSize = 25,
                    LineHeight = 1f,
                    Text = hudText,
                    TargetX = 0,
                    TargetY = 750,
                    Priority = HintPriority.Medium,
                };
                PlayerDisplay.Get(args.Player).RemoveHint("pchatnoclip");
                PlayerDisplay.Get(args.Player).AddHint(dynamicHint);
                Timing.CallDelayed(10f, () => {
                    PlayerDisplay.Get(args.Player).RemoveHint("pchatnoclip");
                });
            }
        }

        public static void OnPlayerTogglingNoClip(TogglingNoClipEventArgs args)
        {
            /*if (FpcNoclip.IsPermitted(args.Player.ReferenceHub))
                return;*/

            if (args.Player.Role.Type == RoleTypeId.Scp049)
            {
                if (ToggledPlayers.Contains(args.Player))
                {
                    ToggledPlayers.Remove(args.Player);
                    string hudText = $"<b>Le chat de proximité est maintenant <color=red>désactivé</color>.</b>";
                    var dynamicHint = new DynamicHint
                    {
                        Id = "pchatd",
                        FontSize = 30,
                        LineHeight = 1f,
                        Text = hudText,
                        TargetX = 0,
                        TargetY = 750,
                        Priority = HintPriority.High,
                    };
                    PlayerDisplay.Get(args.Player).RemoveHint("pchata");
                    PlayerDisplay.Get(args.Player).AddHint(dynamicHint);
                    Timing.CallDelayed(5f, () => {
                        PlayerDisplay.Get(args.Player).RemoveHint("pchatd");
                    });
                    args.IsAllowed = false;
                    return;
                }
                else
                {
                    ToggledPlayers.Add(args.Player);
                    string hudText = $"<b>Le chat de proximité est maintenant <color=green>activé</color>.</b>";
                    var dynamicHint = new DynamicHint
                    {
                        Id = "pchata",
                        FontSize = 30,
                        LineHeight = 1f,
                        Text = hudText,
                        TargetX = 0,
                        TargetY = 750,
                        Priority = HintPriority.High,
                    };
                    PlayerDisplay.Get(args.Player).RemoveHint("pchatd");
                    PlayerDisplay.Get(args.Player).AddHint(dynamicHint);
                    Timing.CallDelayed(5f, () => {
                        PlayerDisplay.Get(args.Player).RemoveHint("pchata");
                    });
                    args.IsAllowed = false;
                }
            }
        }

        public static void OnPlayerUsingVoiceChat(VoiceChattingEventArgs args)
        {
            if (args.VoiceMessage.Channel != VoiceChatChannel.ScpChat)
                return;

            if (args.Player.Role.Type != RoleTypeId.Scp049)
            {
                return;
            }

            if (!ToggledPlayers.Contains(args.Player))
            {
                return;
            }

            SendProximityMessage(args.VoiceMessage);

            args.IsAllowed = false;
        }

        private static void SendProximityMessage(VoiceMessage msg)
        {
            foreach (ReferenceHub referenceHub in ReferenceHub.AllHubs)
            {
                if (referenceHub.roleManager.CurrentRole is SpectatorRole && !msg.Speaker.IsSpectatedBy(referenceHub))
                    continue;

                if (!(referenceHub.roleManager.CurrentRole is IVoiceRole voiceRole2))
                    continue;

                if (Vector3.Distance(msg.Speaker.transform.position, referenceHub.transform.position) >= 15)
                    continue;

                if (voiceRole2.VoiceModule.ValidateReceive(msg.Speaker, VoiceChatChannel.Proximity) is VoiceChatChannel.None)
                    continue;

                msg.Channel = VoiceChatChannel.Proximity;
                referenceHub.connectionToClient.Send(msg);
            }
        }
    }
}