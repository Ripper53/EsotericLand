using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitDestroyRunSystem : RunSystem {
        public static readonly HashSet<Unit> ToDestroy = new HashSet<Unit>();

        public override void Run() {
            foreach (Unit unit in ToDestroy)
                unit.Destroy();
            ToDestroy.Clear();
        }
    }
}
