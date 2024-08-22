using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;


namespace WoW_Mod
{
    public class WoWModPlugin : BasePlugin
    {
        public override string ModuleName => "WoW Mod";
        public override string ModuleVersion => "1.0";
        private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();

        public override void Load(bool hotReload)
        {
            base.Load(hotReload);

            DatabaseManager.Initialize();
            SpellManager.Initialize();
            CommandManager.RegisterCommands(this);

            RegisterEventHandlers();
            Server.NextFrame(() => StartEffectUpdateTimer());
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnect);
            RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
            RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
            RegisterEventHandler<EventRoundStart>(OnRoundStart);
            RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        }

        private HookResult OnPlayerConnect(EventPlayerConnectFull @event, GameEventInfo info)
        {
            var player = @event.Userid;
            var steamId = player.SteamID.ToString();

            if (!players.ContainsKey(steamId))
            {
                var playerData = DatabaseManager.LoadPlayer(steamId);
                players[steamId] = playerData;
            }

            return HookResult.Continue;
        }

        private HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
        {
            var player = @event.Userid;
            var steamId = player.SteamID.ToString();

            if (players.ContainsKey(steamId))
            {
                DatabaseManager.SavePlayer(players[steamId], player.IpAddress);
                players.Remove(steamId);
            }

            return HookResult.Continue;
        }

        private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
        {
            var victim = @event.Userid;
            var attacker = @event.Attacker;

            if (victim != null && attacker != null && victim != attacker)
            {
                var victimSteamId = victim.SteamID.ToString();
                var attackerSteamId = attacker.SteamID.ToString();

                if (players.TryGetValue(attackerSteamId, out var attackerData))
                {
                    attackerData.AddExperience(Config.BASE_XP_PER_KILL);
                    Utils.BroadcastMessage($"{attacker.PlayerName} a gagné {Config.BASE_XP_PER_KILL} XP en tuant {victim.PlayerName}");
                }
            }

            return HookResult.Continue;
        }

        private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
        {
            foreach (var playerData in players.Values)
            {
                playerData.ResetStats();
            }

            return HookResult.Continue;
        }

        private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
        {
            foreach (var playerData in players.Values)
            {
                playerData.AddExperience(Config.BONUS_XP_PER_ROUND);
            }

            return HookResult.Continue;
        }

        private void StartEffectUpdateTimer()
        {
            Server.NextFrame(() =>
            {
                foreach (var playerData in players.Values)
                {
                    playerData.UpdateEffects();
                }
                StartEffectUpdateTimer();
            });
        }

        public PlayerData GetPlayerData(string steamId)
        {
            if (players.TryGetValue(steamId, out var playerData))
            {
                return playerData;
            }
            return null;
        }
    }
}


//Cette mise à jour du Plugin principal gère les événements du jeu (connexion, déconnexion, mort, début et fin de round).
//Il maintient une liste des joueurs actifs et leurs données.
//Il gère l'attribution d'XP pour les kills et les rounds.
//Un timer est mis en place pour mettre à jour régulièrement les effets actifs sur les joueurs.