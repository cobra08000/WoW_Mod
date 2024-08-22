


namespace WoW_Mod
{
    
    public enum SpellType { Damage, Heal, Buff, Debuff, Dot }

    public class Spell
    {

        public string Name { get; set; }
        public SpellType Type { get; set; }
        public int BasePower { get; set; }
        public int ManaCost { get; set; }
        public int Cooldown { get; set; }
        public int UnlockLevel { get; set; }
        public bool IsPassive { get; set; }
        public int Duration { get; set; } // Pour les DoTs et les buffs

        public Spell(string name, SpellType type, int basePower, int manaCost, int cooldown, int unlockLevel, bool isPassive = false, int duration = 0)
        {
            Name = name;
            Type = type;
            BasePower = basePower;
            ManaCost = manaCost;
            Cooldown = cooldown;
            UnlockLevel = unlockLevel;
            IsPassive = isPassive;
            Duration = duration;
        }
    }

    public static class SpellManager
    {
        private static Dictionary<PlayerData.PlayerClass, List<Spell>> classSpells = new Dictionary<PlayerData.PlayerClass, List<Spell>>();
       

        public static void Initialize()
        {
            InitializeWarriorSpells();
            InitializePaladinSpells();
            InitializeMageSpells();
            InitializePriestSpells();
            InitializeRogueSpells();
            InitializeDruidSpells();
            InitializeShamanSpells();
            InitializeWarlockSpells();
            InitializeDeathKnightSpells();
            InitializeDemonHunterSpells();
        }
      
        private static void InitializeWarriorSpells()
        {
            classSpells[PlayerData.PlayerClass.Warrior] = new List<Spell>
            {
                new Spell("Frappe héroïque", SpellType.Damage, 20, 10, 5, 1),
                new Spell("Cri de guerre", SpellType.Buff, 0, 20, 30, 5),
                new Spell("Tourbillon", SpellType.Damage, 30, 25, 8, 10),
                new Spell("Coup de bouclier", SpellType.Damage, 25, 15, 6, 20),
                new Spell("Dernier rempart", SpellType.Buff, 0, 30, 60, 25),
                new Spell("Exécution", SpellType.Damage, 50, 30, 12, 30),
                new Spell("Rage indomptable", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Maîtrise des armes", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializePaladinSpells()
        {
            classSpells[PlayerData.PlayerClass.Paladin] = new List<Spell>
            {
                new Spell("Jugement", SpellType.Damage, 15, 10, 6, 1),
                new Spell("Lumière sacrée", SpellType.Heal, 20, 15, 8, 5),
                new Spell("Marteau de courroux", SpellType.Damage, 25, 20, 10, 10),
                new Spell("Bénédiction de protection", SpellType.Buff, 0, 30, 300, 20),
                new Spell("Consécration", SpellType.Damage, 10, 25, 12, 25),
                new Spell("Bouclier divin", SpellType.Buff, 0, 35, 300, 30),
                new Spell("Dévotion", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Sens de la justice", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeMageSpells()
        {
            classSpells[PlayerData.PlayerClass.Mage] = new List<Spell>
            {
                new Spell("Éclair de givre", SpellType.Damage, 15, 10, 3, 1),
                new Spell("Projectiles des arcanes", SpellType.Damage, 20, 15, 5, 5),
                new Spell("Explosion de feu", SpellType.Damage, 30, 25, 8, 10),
                new Spell("Métamorphose", SpellType.Debuff, 0, 30, 30, 20),
                new Spell("Blizzard", SpellType.Damage, 40, 35, 12, 25),
                new Spell("Pyroblast", SpellType.Damage, 50, 40, 15, 30),
                new Spell("Évocation", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Clairvoyance", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializePriestSpells()
        {
            classSpells[PlayerData.PlayerClass.Priest] = new List<Spell>
            {
                new Spell("Mot de l'ombre : Douleur", SpellType.Dot, 10, 10, 0, 1, false, 18),
                new Spell("Soins rapides", SpellType.Heal, 20, 15, 3, 5),
                new Spell("Prière de soins", SpellType.Heal, 30, 25, 10, 10),
                new Spell("Châtiment", SpellType.Damage, 25, 20, 8, 20),
                new Spell("Nova sacrée", SpellType.Damage, 35, 30, 15, 25),
                new Spell("Gardien de l'esprit", SpellType.Buff, 0, 40, 180, 30),
                new Spell("Esprit divin", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Méditation", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeRogueSpells()
        {
            classSpells[PlayerData.PlayerClass.Rogue] = new List<Spell>
            {
                new Spell("Attaque sournoise", SpellType.Damage, 20, 35, 0, 1),
                new Spell("Éviscération", SpellType.Damage, 30, 35, 5, 5),
                new Spell("Rupture", SpellType.Dot, 40, 25, 8, 10, false, 24),
                new Spell("Coup de pied", SpellType.Debuff, 0, 25, 10, 20),
                new Spell("Poison mortel", SpellType.Dot, 50, 30, 10, 25, false, 12),
                new Spell("Disparition", SpellType.Buff, 0, 35, 180, 30),
                new Spell("Maître assassin", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Furtivité améliorée", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeDruidSpells()
        {
            classSpells[PlayerData.PlayerClass.Druid] = new List<Spell>
            {
                new Spell("Colère", SpellType.Damage, 15, 10, 1, 1),
                new Spell("Récupération", SpellType.Heal, 20, 15, 10, 5),
                new Spell("Griffure", SpellType.Dot, 25, 20, 6, 10, false, 12),
                new Spell("Sarments", SpellType.Debuff, 0, 25, 15, 20),
                new Spell("Eclat lunaire", SpellType.Damage, 35, 30, 8, 25),
                new Spell("Tranquillité", SpellType.Heal, 50, 40, 480, 30),
                new Spell("Affinité naturelle", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Vigueur du fauve", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeShamanSpells()
        {
            classSpells[PlayerData.PlayerClass.Shaman] = new List<Spell>
            {
                new Spell("Eclair", SpellType.Damage, 20, 15, 2, 1),
                new Spell("Vague de soins", SpellType.Heal, 25, 20, 8, 5),
                new Spell("Choc de flammes", SpellType.Damage, 30, 25, 6, 10),
                new Spell("Horion de terre", SpellType.Damage, 35, 30, 5, 20),
                new Spell("Chaîne d'éclairs", SpellType.Damage, 40, 35, 10, 25),
                new Spell("Arme Furie-des-vents", SpellType.Buff, 0, 40, 30, 30),
                new Spell("Bouclier de foudre", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Maîtrise des éléments", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeWarlockSpells()
        {
            classSpells[PlayerData.PlayerClass.Warlock] = new List<Spell>
            {
                new Spell("Trait de l'ombre", SpellType.Damage, 15, 10, 2, 1),
                new Spell("Corruption", SpellType.Dot, 20, 15, 0, 5, false, 18),
                new Spell("Immolation", SpellType.Dot, 25, 20, 0, 10, false, 15),
                new Spell("Drain de vie", SpellType.Damage, 30, 25, 0, 20),
                new Spell("Malédiction d'agonie", SpellType.Dot, 35, 30, 0, 25, false, 24),
                new Spell("Inferno", SpellType.Damage, 50, 40, 600, 30),
                new Spell("Pacte démoniaque", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Affliction améliorée", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeDeathKnightSpells()
        {
            classSpells[PlayerData.PlayerClass.DeathKnight] = new List<Spell>
            {
                new Spell("Frappe de mort", SpellType.Damage, 20, 15, 4, 1),
                new Spell("Peste", SpellType.Dot, 25, 20, 10, 5, false, 21),
                new Spell("Étreinte glaciale", SpellType.Damage, 30, 25, 8, 10),
                new Spell("Frappe du fléau", SpellType.Damage, 35, 30, 6, 20),
                new Spell("Décomposition", SpellType.Dot, 40, 35, 10, 25, false, 15),
                new Spell("Armée des morts", SpellType.Buff, 0, 50, 600, 30),
                new Spell("Présence impie", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Sang runique", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        private static void InitializeDemonHunterSpells()
        {
            classSpells[PlayerData.PlayerClass.DemonHunter] = new List<Spell>
            {
                new Spell("Frappe du chaos", SpellType.Damage, 20, 15, 4, 1),
                new Spell("Lancer de glaive", SpellType.Damage, 25, 20, 10, 5),
                new Spell("Morsure démoniaque", SpellType.Damage, 30, 25, 8, 10),
                new Spell("Oeil de Leotheras", SpellType.Buff, 0, 30, 60, 20),
                new Spell("Décret sigillaire", SpellType.Damage, 40, 35, 10, 25),
                new Spell("Métamorphose", SpellType.Buff, 0, 50, 240, 30),
                new Spell("Agilité démoniaque", SpellType.Buff, 0, 0, 0, 1, true),
                new Spell("Âme de feu", SpellType.Buff, 0, 0, 0, 10, true)
            };
        }

        public static List<Spell> GetSpellsForClass(PlayerData.PlayerClass playerClass)
        {
            if (classSpells.TryGetValue(playerClass, out var spells))
            {
                return spells;
            }
            return new List<Spell>();  // Spécifiez le type générique ici
        }


    }
}


//Ce script définit les sorts pour toutes les classes, y compris les sorts passifs.
//Chaque classe a 6 sorts actifs et 2 sorts passifs.
//Les sorts sont débloqués à différents niveaux.
//La méthode GetAvailableSpells permet de récupérer les sorts disponibles pour un joueur en fonction de sa classe et de son niveau.