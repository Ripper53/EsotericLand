using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitRandomControls : UnitControls {
        public int FollowDistance = 5;
        public Vector2 DirectionMultiplier = new Vector2(1, 1);

        private bool MoveHorizontal(int x) {
            if (x > 0) {
                MoveRight();
                return true;
            } else if (x < 0) {
                MoveLeft();
                return true;
            }
            return false;
        }

        private bool MoveVertical(int y) {
            if (y > 0) {
                MoveDown();
                return true;
            } else if (y < 0) {
                MoveUp();
                return true;
            }
            return false;
        }

        public override bool Controls() {
            Vector2 dir = (Unit.CurrentScene.PlayerUnit.Position - Unit.Position) * DirectionMultiplier;
            int randomNum = Program.RNG.Next(4);
            if (Math.Abs(dir.x) < FollowDistance && Math.Abs(dir.y) < FollowDistance) {
                switch (randomNum) {
                    case 0:
                    case 1:
                        if (MoveHorizontal(dir.x) || MoveVertical(dir.y))
                            return true;
                        break;
                    default:
                        if (MoveVertical(dir.y) || MoveHorizontal(dir.x))
                            return true;
                        break;
                }

            }
            switch (randomNum) {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveRight();
                    break;
                case 2:
                    MoveDown();
                    break;
                default:
                    MoveLeft();
                    break;
            }
            return true;
        }

        private void MoveUp() {
            if (Unit.CanMoveUp()) {
                Unit.Direction = Unit.MoveDirection.Up;
            } else {
                CheckAllPositions();
            }
        }

        private void MoveRight() {
            if (Unit.CanMoveRight()) {
                Unit.Direction = Unit.MoveDirection.Right;
            } else {
                CheckAllPositions();
            }
        }

        private void MoveDown() {
            if (Unit.CanMoveDown()) {
                Unit.Direction = Unit.MoveDirection.Down;
            } else {
                CheckAllPositions();
            }
        }

        private void MoveLeft() {
            if (Unit.CanMoveLeft()) {
                Unit.Direction = Unit.MoveDirection.Left;
            } else {
                CheckAllPositions();
            }
        }

        private void CheckAllPositions() {
            if (Unit.CanMoveUp())
                Unit.Direction = Unit.MoveDirection.Up;
            else if (Unit.CanMoveRight())
                Unit.Direction = Unit.MoveDirection.Right;
            else if (Unit.CanMoveDown())
                Unit.Direction = Unit.MoveDirection.Down;
            else if (Unit.CanMoveLeft())
                Unit.Direction = Unit.MoveDirection.Left;
            else
                Unit.Direction = Unit.MoveDirection.None;
        }
    }
}
