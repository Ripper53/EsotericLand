namespace EsotericLand.RogueEngine {
    public abstract class EntityRunSystem<T> : RunSystem where T : Entity<T> {

        public override void Run() {
            foreach (T entity in Entity<T>.ALL) {
                if (entity.Active)
                    Run(entity);
            }
        }

        public abstract void Run(T entity);
    }
}
