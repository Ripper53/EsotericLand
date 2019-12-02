using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public abstract class Item {
        public string Name { get; protected set; }

        public abstract bool Use(PlayerUnit playerUnit);
        public abstract Item Clone();
    }
}
