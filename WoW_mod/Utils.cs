
using CounterStrikeSharp.API;


namespace WoW_Mod
{
    public static class Utils
    {
        private static Random random = new Random();

        public static void BroadcastMessage(string message)
        {
            Server.PrintToChatAll($"[WoW Mod] {message}");
        }

        public static void ScheduleTask(Action action, float delay)
        {
            Server.NextFrame(() =>
            {
                Server.NextFrame(() =>
                {
                    action();
                });
            });
        }

        public static int CalculateDamage(int baseDamage, int strength)
        {
            return baseDamage + (strength * 3);
        }

        public static bool TryDodge(int agility)
        {
            return random.Next(100) < agility;
        }

        public static int CalculateHeal(int baseHeal, int intelligence)
        {
            return baseHeal + (intelligence * 3);
        }
    }
}