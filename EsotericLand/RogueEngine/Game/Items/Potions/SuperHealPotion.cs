using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class SuperHealPotion : HealPotion {

        public SuperHealPotion() {
            Name = "Super Heal Potion";
            Heal = 20;
        }

        public override Item Clone() {
            return new SuperHealPotion();
        }
    }
}
