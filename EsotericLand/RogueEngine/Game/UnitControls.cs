using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public abstract class UnitControls : Entity<UnitControls> {
        public Unit Unit;

        public UnitControls() { }
        public UnitControls(Unit unit) {
            Unit = unit;
        }

        public abstract bool Controls();
    }
}
