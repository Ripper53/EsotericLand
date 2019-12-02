using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class Unit : Entity<Unit> {
        public Sprite Sprite;
        public Character Character;
        public BattleBrain BattleBrain;
        public Vector2 Position;
        public readonly Scene CurrentScene;

        public enum MoveDirection {
            None, Up, Right, Down, Left
        };
        public MoveDirection Direction;

        public Unit(Sprite sprite, Character character, Scene scene) {
            Sprite = sprite;
            CurrentScene = scene;
            Character = character;
        }

        public bool PlaceInRandomPosition() {
            return CurrentScene.PlaceUnitInRandomFreeSpace(this);
        }

        public override void Destroy() {
            base.Destroy();
            CurrentScene.Units.Remove(Position);
        }

        protected virtual bool Move(Vector2 position) {
            switch (CurrentScene.Tiles[position.x, position.y]) {
                case Scene.Tile.Wall:
                    return false;
                default:
                    if (CurrentScene.Units.ContainsKey(position)) {
                        if (!TakenPosition(position, CurrentScene.Units[position]))
                            return false;
                        else
                            CurrentScene.Units.Remove(position);
                    }
                    CurrentScene.Units.Remove(Position);
                    Position = position;
                    CurrentScene.Units.Add(Position, this);
                    return true;
            }
        }
        protected virtual bool TakenPosition(Vector2 position, Unit unit) {
            return false;
        }

        protected virtual bool CanMove(Vector2 position) {
            return
                position.x >= 0 &&
                position.y >= 0 &&
                position.x < CurrentScene.Size.x &&
                position.y < CurrentScene.Size.y &&
                CurrentScene.Tiles[position.x, position.y] != Scene.Tile.Wall &&
                !CurrentScene.Units.ContainsKey(position);
        }

        public bool CanMoveUp() {
            return CanMove(new Vector2(Position.x, Position.y - 1));
        }
        public bool MoveUp() {
            Vector2 pos = new Vector2(Position.x, Position.y - 1);
            if (pos.y < 0) return OutOfBounds(MoveDirection.Up);
            return Move(pos);
        }
        protected virtual bool OutOfBounds(MoveDirection direction) => false;

        public bool CanMoveRight() {
            return CanMove(new Vector2(Position.x + 1, Position.y));
        }
        public bool MoveRight() {
            Vector2 pos = new Vector2(Position.x + 1, Position.y);
            if (pos.x == CurrentScene.Size.x) return OutOfBounds(MoveDirection.Right);
            return Move(pos);
        }

        public bool CanMoveDown() {
            return CanMove(new Vector2(Position.x, Position.y + 1));
        }
        public bool MoveDown() {
            Vector2 pos = new Vector2(Position.x, Position.y + 1);
            if (pos.y == CurrentScene.Size.y) return OutOfBounds(MoveDirection.Down);
            return Move(pos);
        }

        public bool CanMoveLeft() {
            return CanMove(new Vector2(Position.x - 1, Position.y));
        }
        public bool MoveLeft() {
            Vector2 pos = new Vector2(Position.x - 1, Position.y);
            if (pos.x < 0) return OutOfBounds(MoveDirection.Left);
            return Move(pos);
        }
    }
}
