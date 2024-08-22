namespace WoW_Mod
{
    public static class Config
    {
        public const int MAX_LEVEL = 60;
        public const int BASE_XP_PER_KILL = 150;
        public const int BONUS_XP_PER_ROUND = 50;
        public const int XP_MULTIPLIER = 2;

        public static int GetRequiredXpForLevel(int level)
        {
            return 600 * (int)Math.Pow(XP_MULTIPLIER, level - 1);
        }
    }
}
//Traduction:

//Cette classe contient les constantes de configuration pour le mod.
//  Elle définit le niveau maximum, l'XP de base par kill, le bonus XP par round, et le multiplicateur d'XP.

//4.Étapes suivantes
  //  Implémentez la logique pour les sorts passifs et les DoT dans SpellManager.cs
    //Ajoutez la vérification régulière des effets de sort (Proc) dans Plugin.cs
    //Implémentez la logique de combat dans Plugin.cs (OnPlayerDeath, etc.)
//Ajoutez plus de commandes dans CommandManager.cs (par exemple, pour lier les sorts)
//Créez un système de messages réguliers pour informer les joueurs
//Implémentez la progression des statistiques à chaque niveau
//Ajoutez la logique pour débloquer les sorts à des niveaux spécifiques