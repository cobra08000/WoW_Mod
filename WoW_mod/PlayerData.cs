namespace WoW_Mod
{
    public class PlayerData
    {

    public enum PlayerClass
        {
            Warrior,
            Paladin,
            Mage,
            Priest,
            Rogue,
            Druid,
            Shaman,
            Warlock,
            DeathKnight,
            DemonHunter
        }


        public string SteamId { get; set; }
        public PlayerClass Class { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Stamina { get; set; }
        public Dictionary<string, DateTime> SpellCooldowns { get; set; }
        public List<ActiveEffect> ActiveEffects { get; set; }  // Assurez-vous que c'est bien une liste de `ActiveEffect`




        public PlayerData(string steamId)
        {
            SteamId = steamId;
            Class = PlayerClass.Warrior; // Classe par défaut
            Level = 1;
            Experience = 0;
            SpellCooldowns = new Dictionary<string, DateTime>();
            ActiveEffects = new List<ActiveEffect>();  // Liste de `ActiveEffect`
            ResetStats();
        }

        public void ResetStats()
        {
            
            Strength = 10 + (Level - 1) * 2;
            Agility = 10 + (Level - 1) * 2;
            Intelligence = 10 + (Level - 1) * 2;
            Stamina = 10 + (Level - 1) * 2;

            MaxHealth = 100 + (Stamina * 10);
            MaxMana = 100 + (Intelligence * 10);
            Health = MaxHealth;
            Mana = MaxMana;

            // Ajustements spécifiques à la classe
            switch (Class)
            {
                case PlayerClass.Warrior:
                    Strength += Level * 3;
                    Stamina += Level * 2;
                    break;
                case PlayerClass.Rogue:
                case PlayerClass.DemonHunter:
                    Agility += Level * 3;
                    Stamina += Level;
                    break;
                case PlayerClass.Mage:
                case PlayerClass.Warlock:
                case PlayerClass.Priest:
                    Intelligence += Level * 3;
                    Stamina += Level;
                    break;
                    // Ajoutez d'autres ajustements spécifiques aux classes ici
            }
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            while (Experience >= Config.GetRequiredXpForLevel(Level))
            {
                Experience -= Config.GetRequiredXpForLevel(Level);
                Level++;
                ResetStats(); // Exemple : Réinitialiser les stats lors de la montée de niveau
            }
        }

        private void CheckLevelUp()
        {
            while (Experience >= Config.GetRequiredXpForLevel(Level) && Level < Config.MAX_LEVEL)
            {
                Experience -= Config.GetRequiredXpForLevel(Level);
                Level++;
                ResetStats();
                Utils.BroadcastMessage($"{SteamId} est monté au niveau {Level} !");
            }
        }

        public bool CanCastSpell(Spell spell)
        {
            if (spell.ManaCost > Mana)
            {
                return false;
            }

            if (SpellCooldowns.TryGetValue(spell.Name, out DateTime cooldownEnd))
            {
                if (DateTime.Now < cooldownEnd)
                {
                    return false;
                }
            }

            return true;
        }

        public void CastSpell(Spell spell, PlayerData target = null)
        {
            if (!CanCastSpell(spell))
            {
                return;
            }

            Mana -= spell.ManaCost;
            SpellCooldowns[spell.Name] = DateTime.Now.AddSeconds(spell.Cooldown);

            switch (spell.Type)
            {
                case SpellType.Damage:
                    if (target != null)
                    {
                        int damage = Utils.CalculateDamage(spell.BasePower, Strength);
                        target.TakeDamage(damage);
                    }
                    break;
                case SpellType.Heal:
                    int healing = Utils.CalculateHeal(spell.BasePower, Intelligence);
                    Health = Math.Min(MaxHealth, Health + healing);
                    break;
                case SpellType.Buff:
                    ActiveEffects.Add(new ActiveEffect(spell.Name, spell.Duration, EffectType.Buff));
                    break;
                case SpellType.Debuff:
                    if (target != null)
                    {
                        target.ActiveEffects.Add(new ActiveEffect(spell.Name, spell.Duration, EffectType.Debuff));
                    }
                    break;
                case SpellType.Dot:
                    if (target != null)
                    {
                        target.ActiveEffects.Add(new ActiveEffect(spell.Name, spell.Duration, EffectType.Dot, spell.BasePower));
                    }
                    break;
            }
        }

        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public void UpdateEffects()
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                var effect = ActiveEffects[i];

                if (effect != null)
                {
                    effect.RemainingDuration--;

                    if (effect.RemainingDuration <= 0)
                    {
                        ActiveEffects.RemoveAt(i);
                    }
                    else if (effect.EffectType == EffectType.Dot)
                    {
                        TakeDamage(effect.Power);
                    }
                }
            }
        }


public void AddEffect(ActiveEffect effect)
    {
        if (effect != null)
        {
            ActiveEffects.Add(effect);
        }
    }

    public void RemoveEffect(ActiveEffect effect)
    {
        if (effect != null)
        {
            ActiveEffects.Remove(effect);
        }
    }
    public class ActiveEffect
    {
        public string Name { get; set; }
        public int RemainingDuration { get; set; }
        public EffectType EffectType { get; set; }
        public int Power { get; set; }  // Notez que Power est optionnel (par défaut 0)

        public ActiveEffect(string name, int duration, EffectType effectType, int power = 0)
        {
            Name = name;
            RemainingDuration = duration;
            EffectType = effectType;
            Power = power;
        }
    }

    public enum EffectType
    {
        Buff,
        Debuff,
        Dot
    }

















    }
    
}
//Traduction:

//Cette mise à jour de PlayerData ajoute la gestion des cooldowns des sorts et des effets actifs (buffs, debuffs, DoTs).
//La méthode ResetStats ajuste maintenant les statistiques en fonction de la classe du joueur.
//Les méthodes CastSpell et UpdateEffects gèrent respectivement le lancement des sorts et la mise à jour des effets actifs.