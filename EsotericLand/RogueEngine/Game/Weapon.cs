using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public abstract class Weapon : Item {
        public IList<Attack> Attacks { get; protected set; }
        
        public abstract class Attack {

            public abstract bool Use(Weapon source, Character friendly, Character target);
            public abstract IList<Sprite> GetDescription(Weapon source, Character friendly, Character target);
        }

        public override bool Use(PlayerUnit playerUnit) {
            playerUnit.EquipWeapon(this);
            return true;
        }

        public List<Sprite> GetDescription(Character friendly, Character target) {
            List<Sprite> sprites = new List<Sprite>(Attacks.Count);
            foreach (Attack atk in Attacks) {
                sprites.AddRange(atk.GetDescription(this, friendly, target));
            }
            return sprites;
        }
    }
}
