using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class SceneRunSystem : EntityRunSystem<Scene> {

        public override void Run(Scene entity) {
            if (entity.GenerateRequest) {
                entity.GenerateRequest = false;
                entity.Generate();
            }

            for (int y = 0; y < entity.Size.y; y++) {
                for (int x = 0; x < entity.Size.x; x++) {
                    Vector2 pos = new Vector2(x, y);
                    if (entity.Units.ContainsKey(pos)) {
                        Renderer.Add(entity.Units[pos].Sprite);
                    } else {
                        switch (entity.Tiles[x, y]) {
                            case Scene.Tile.Ground:
                                Renderer.Add(Scene.GroundSprite);
                                break;
                            default:
                                Renderer.Add(Scene.WallSprite);
                                break;
                        }
                    }
                    if (x == entity.Size.x - 1)
                        Renderer.Add("\n");
                }
            }
        }

    }
}
