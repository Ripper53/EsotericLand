using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitRunSystemSynchronizer : RunSystemSynchronizer {

        public UnitRunSystemSynchronizer() {
            RunSystems = new RunSystem[] {
                new SceneRunSystem(),
                new MenuRunSystem(),
                new RendererRunSystem(),

                new MenuPlayerControlsRunSystem(),
                new UnitRunSystemGroupSynchronizer(),
                new UnitDestroyRunSystem()
            };
        }
    }
}
