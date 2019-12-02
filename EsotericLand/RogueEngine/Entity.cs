using System.Collections.Generic;

namespace EsotericLand.RogueEngine {
    public abstract class Entity {
        public virtual bool Active { get; set; } = true;
    }

    public abstract class Entity<T> : Entity where T : Entity<T> {
        public static List<T> ALL = new List<T>();

        public Entity() {
            ALL.Add((T)this);
        }

        public virtual void Destroy() {
            ALL.Remove((T)this);
        }
        
    }
}
