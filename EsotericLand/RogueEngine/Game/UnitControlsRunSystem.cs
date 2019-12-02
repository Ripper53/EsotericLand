using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitControlsRunSystem : EntityRunSystemSynchronize<UnitControls> {

        public override bool RunSync(UnitControls entity) {
            return entity.Controls();
        }
    }
}
