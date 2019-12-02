using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitPlayerControls : UnitControls {
        public UnitPlayerControls(Unit unit) : base(unit) { }

        public override bool Controls() {
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    Unit.Direction = Unit.MoveDirection.Up;
                    return true;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    Unit.Direction = Unit.MoveDirection.Right;
                    return true;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    Unit.Direction = Unit.MoveDirection.Down;
                    return true;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    Unit.Direction = Unit.MoveDirection.Left;
                    return true;
                default:
                    return false;
            }
        }
    }
}
