using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class Character {
        public string Name { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public readonly ResourceData Stamina, Mana, Energy;
        public int GoldDropMin = 1, GoldDropMax = 2, GoldDropThreshold = 50, ItemDropChance = 100;
        public IList<Item> Drops;

        public Character(string name, int health, int stamina = 0, int mana = 0, int energy = 0) {
            Name = name;
            Health = health;
            MaxHealth = health;
            Stamina = new ResourceData(stamina);
            Mana = new ResourceData(mana);
            Energy = new ResourceData(energy);
        }

        public void Regen() {
            Stamina.Regen();
            Mana.Regen();
            Energy.Regen();
        }

        private void Check() {
            if (Health <= 0) {
                Destroy();
            } else if (Health > MaxHealth) {
                Health = MaxHealth;
            }
        }

        public event Action<Character, int> Damaged;
        public void Damage(int damage) {
            Health -= damage;
            Damaged?.Invoke(this, damage);
            Check();
        }

        public event Action<Character, int> Healed;
        public void Heal(int heal) {
            Health += heal;
            Healed?.Invoke(this, heal);
            Check();
        }

        public void IncreaseMaxHealth(int maxHealthAdd) {
            MaxHealth += maxHealthAdd;
            Check();
        }

        public bool UseHealth(int health) {
            if (Health > health) {
                Damage(health);
                return true;
            }
            return false;
        }

        public event Action<Character> Destroyed;
        public void Destroy() {
            Destroyed?.Invoke(this);
        }
    }
}
