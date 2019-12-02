using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitRunSystem : EntityRunSystemSynchronize<Unit> {

        public override bool RunSync(Unit entity) {
            switch (entity.Direction) {
                case Unit.MoveDirection.Up:
                    return entity.MoveUp();
                case Unit.MoveDirection.Right:
                    return entity.MoveRight();
                case Unit.MoveDirection.Down:
                    return entity.MoveDown();
                case Unit.MoveDirection.Left:
                    return entity.MoveLeft();
                default:
                    return true;
            }
        }

    }
}
