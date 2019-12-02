using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public abstract class BattleBrain {

        public abstract void Action(Character source, Character enemy);
        public abstract IEnumerable<Sprite> GetActionDescription();
    }
}
