using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class MegaHeartPotion : HeartPotion {

        public MegaHeartPotion() {
            Name = "Mega Heart Potion";
            HealthIncrease = 5;
        }

        public override Item Clone() {
            return new MegaHeartPotion();
        }
    }
}
