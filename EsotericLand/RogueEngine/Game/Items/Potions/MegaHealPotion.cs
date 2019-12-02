using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class MegaHealPotion : HealPotion {

        public MegaHealPotion() {
            Name = "Mega Heal Potion";

            Heal = 5;
        }

        public override Item Clone() {
            return new MegaHealPotion();
        }
    }
}
