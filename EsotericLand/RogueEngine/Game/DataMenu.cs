using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class DataMenu : Menu {
        public readonly PlayerUnit Unit;

        public DataMenu(PlayerUnit unit) : base(new Sprite[] { }, new Option[] { }) {
            Unit = unit;
            Sprites = new List<Sprite>();
        }

        public override void Print() {
            Refresh();
            base.Print();
        }

        public void Refresh() {
            Character c = Unit.Character;
            Sprites.Clear();
            Sprites.Add(Sprite.Health);
            Sprites.Add(new Sprite($": {c.Health}/{c.MaxHealth}"));
            GetResourceDataDescription(Sprite.Stamina, c.Stamina);
            GetResourceDataDescription(Sprite.Mana, c.Mana);
            GetResourceDataDescription(Sprite.Energy, c.Energy);
        }

        private void GetResourceDataDescription(Sprite sprite, ResourceData resourceData) {
            if (resourceData.Value > 0) {
                Sprites.Add(new Sprite("    "));
                Sprites.Add(sprite);
                Sprites.Add(new Sprite($": {resourceData.Value}/{resourceData.Max}"));
            }
        }
    }
}
