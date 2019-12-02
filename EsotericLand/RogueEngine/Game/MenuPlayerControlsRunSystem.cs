using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class MenuPlayerControlsRunSystem : RunSystemSynchronize {

        public override bool RunSync() {
            if (!Menu.Selected || Menu.ALL.Count == 0) return true;
            Menu menu = Menu.ALL[Menu.CurrentMenu];
            IList<Menu.Option> options = menu.Options;
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    menu.MoveUp();
                    return true;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    menu.MoveDown();
                    return true;
                case ConsoleKey.Enter:
                    options[menu.SelectedOption].Action(menu, options[menu.SelectedOption]);
                    return true;
                case ConsoleKey.Tab:
                    Menu.Next();
                    return true;
                default:
                    return false;
            }
        }
    }
}
