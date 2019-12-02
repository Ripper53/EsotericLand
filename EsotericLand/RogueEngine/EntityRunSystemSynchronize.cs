using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public abstract class EntityRunSystemSynchronize<T> : EntityRunSystem<T>, IRunSystemSynchronize where T : Entity<T> {
        public bool Finished { get; private set; }

        public override void Run() {
            Finished = true;
            foreach (T entity in Entity<T>.ALL) {
                if (!Finished)
                    return;
                else if (entity.Active)
                    Run(entity);
            }
        }

        public override void Run(T entity) {
            if (!RunSync(entity))
                Finished = false;
        }

        public abstract bool RunSync(T entity);
    }
}
