using System;
using System.Collections.Generic;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private readonly double baseHealth;
        private double health;
        private readonly double baseArmor;
        private double armor;
        private readonly double abilityPoints;

        protected Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            this.Name = name;
            this.baseHealth = health;
            this.Health = health;
            this.baseArmor = armor;
            this.Armor = armor;
            this.abilityPoints = abilityPoints;
            this.Bag = bag;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }

                name = value;
            }
        }

        public double BaseHealth
        {
            get
            {
                return baseHealth;
            }
        }

        public double Health
        {
            get => health;
            set
            {
                if (value < 0)
                {
                    health = 0;
                }

                else if (value > BaseHealth)
                {
                    health = BaseHealth;
                }

                else
                {
                    health = value;
                }
            }
        }

        public double BaseArmor
        {
            get
            {
                return baseArmor;
            }
        }

        public double Armor
        {
            get => armor;
            private set
            {
                if (value < 0)
                {
                    armor = 0;
                }

                else
                {
                    armor = value;
                }
            }
        }

        public double AbilityPoints
        {
            get
            {
                return abilityPoints;
            }
        }
       

        public IBag Bag { get; private set; }

        public bool IsAlive { get; set; } = true;

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();

            var damage = hitPoints - Armor;
            Armor -= hitPoints;

            if (damage > 0)
            {
                Health -= damage;
            }

            if (Health <= 0)
            {
                IsAlive = false;
            }
        }

        public void UseItem(Item item)
        {
            EnsureAlive();

            item.AffectCharacter(this);
        }

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}