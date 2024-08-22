using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;


namespace WoW_Mod
{
    public static class CommandManager
    {  
        private static WoWModPlugin plugin;
        
        public static void RegisterCommands(WoWModPlugin pluginInstance)
        {
            plugin = pluginInstance;

            Server.ExecuteCommand("sm_addcommand wowstats \"Affiche vos statistiques WoW\"");
            Server.ExecuteCommand("sm_addcommand wowclass \"Change votre classe WoW\"");
            Server.ExecuteCommand("sm_addcommand wowspells \"Affiche vos sorts disponibles\"");
            Server.ExecuteCommand("sm_addcommand wowcast \"Lance un sort\"");
        }

        [ConsoleCommand("wowstats", "Affiche vos statistiques WoW")]
        public static void CommandStats(CCSPlayerController player, CommandInfo command)
        {
            var playerData = plugin.GetPlayerData(player.SteamID.ToString());
            if (playerData != null)
            {
                player.PrintToChat($"Niveau: {playerData.Level}, XP: {playerData.Experience}/{Config.GetRequiredXpForLevel(playerData.Level)}");
                player.PrintToChat($"Classe: {playerData.Class}, Santé: {playerData.Health}/{playerData.MaxHealth}, Mana: {playerData.Mana}/{playerData.MaxMana}");
                player.PrintToChat($"Force: {playerData.Strength}, Agilité: {playerData.Agility}, Intelligence: {playerData.Intelligence}, Endurance: {playerData.Stamina}");
            }
        }

        [ConsoleCommand("wowclass", "Change votre classe WoW")]
        public static void CommandChangeClass(CCSPlayerController player, CommandInfo command)
        {
            if (command.ArgCount < 2)
            {
                player.PrintToChat("Usage: !wowclass <Classe>");
                return;
            }

            var className = command.ArgByIndex(1);
            if (Enum.TryParse(className, true, out PlayerData.PlayerClass newClass))
            {
                var playerData = plugin.GetPlayerData(player.SteamID.ToString());
                if (playerData != null)
                {
                    playerData.Class = newClass;
                    playerData.ResetStats();
                    player.PrintToChat($"Votre classe a été changée en {newClass}");
                }
            }
            else
            {
                player.PrintToChat("Classe non valide. Essayez: Warrior, Paladin, Mage, Priest, Rogue, Druid, Shaman, Warlock, DeathKnight, DemonHunter");
            }
        }

        [ConsoleCommand("wowspells", "Affiche vos sorts disponibles")]
        public static void CommandSpells(CCSPlayerController player, CommandInfo command)
        {
            var playerData = plugin.GetPlayerData(player.SteamID.ToString());
            if (playerData != null)
            {
                var availableSpells = SpellManager.GetAvailableSpells(playerData.Class, playerData.Level);
                player.PrintToChat("Sorts disponibles :");
                foreach (var spell in availableSpells)
                {
                    player.PrintToChat($"{spell.Name} (Niveau {spell.UnlockLevel})");
                }
            }
        }

        [ConsoleCommand("wowcast", "Lance un sort")]
        public static void CommandCast(CCSPlayerController player, CommandInfo command)
        {
            if (command.ArgCount < 2)
            {
                player.PrintToChat("Usage: !wowcast <NomDuSort>");
                return;
            }

            var spellName = command.ArgByIndex(1);
            var playerData = plugin.GetPlayerData(player.SteamID.ToString());
            if (playerData != null)
            {
                var availableSpells = SpellManager.GetAvailableSpells(playerData.Class, playerData.Level); // Assurez-vous que cela est bien une méthode.
                var spell = availableSpells.FirstOrDefault(s => s.Name.Equals(spellName, StringComparison.OrdinalIgnoreCase));

                if (spell != null)
                {
                    if (playerData.CanCastSpell(spell))
                    {
                        playerData.CastSpell(spell);
                        player.PrintToChat($"Vous avez lancé {spell.Name}");
                    }
                    else
                    {
                        player.PrintToChat("Vous ne pouvez pas lancer ce sort pour le moment (cooldown ou mana insuffisant)");
                    }
                }
                else
                {
                    player.PrintToChat("Sort non trouvé ou non disponible");
                }
            }
        }
    }

  
    
       
    










}