using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class MenuRunSystem : RunSystem {

        public override void Run() {
            foreach (Menu menu in Menu.ALL) {
                if (menu.Active)
                    menu.Print();
            }
        }

    }
}
