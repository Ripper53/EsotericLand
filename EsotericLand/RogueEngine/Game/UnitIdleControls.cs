using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitIdleControls : UnitControls {

        public override bool Controls() {
            Unit.Direction = Unit.MoveDirection.None;
            return true;
        }
    }
}
