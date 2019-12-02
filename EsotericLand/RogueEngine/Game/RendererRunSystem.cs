using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class RendererRunSystem : RunSystem {

        public override void Run() {
            Renderer.Display();
        }
    }
}
