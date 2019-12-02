using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitAI : Unit {
        public readonly UnitControls Controls;

        public UnitAI(Sprite sprite, Character character, UnitControls unitControls, Scene scene) : base(sprite, character, scene) {
            Controls = unitControls;
            Controls.Unit = this;
            if (!PlaceInRandomPosition()) {
                Controls.Destroy();
                ALL.Remove(this);
            }
        }

        protected static bool SetDropIfPlayerDoesNotHave(UnitAI unit, Item item) {
            if (!unit.CurrentScene.PlayerUnit.Inventory.ContainsItem(item.Name)) {
                unit.Character.Drops = new Item[] {
                    item
                };
                return true;
            }
            return false;
        }

        protected override bool Move(Vector2 position) {
            base.Move(position);
            return true;
        }

        protected override bool CanMove(Vector2 position) {
            return
                position.x >= 0 &&
                position.y >= 0 &&
                position.x < CurrentScene.Size.x &&
                position.y < CurrentScene.Size.y &&
                CurrentScene.Tiles[position.x, position.y] != Scene.Tile.Wall &&
                (
                    (!CurrentScene.Units.ContainsKey(position)) ||
                    (CurrentScene.Units[position] == CurrentScene.PlayerUnit)
                );
        }

        protected override bool TakenPosition(Vector2 position, Unit unit) {
            if (unit == CurrentScene.PlayerUnit)
                CurrentScene.PlayerUnit.Battle(this);
            return false;
        }

        public override void Destroy() {
            base.Destroy();
            Controls.Destroy();
        }

    }
}
