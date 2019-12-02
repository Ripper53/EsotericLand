using System;
using System.Collections.Generic;

namespace EsotericLand.RogueEngine.Game {
    public static class Renderer {
        private static readonly List<Sprite> toPrints = new List<Sprite>();
        private static Sprite LatestToPrint => toPrints.Count > 0 ? toPrints[toPrints.Count - 1] : null;
       
        public static void Display() {
            Console.Clear();
            foreach (Sprite toPrint in toPrints) {
                Console.ForegroundColor = toPrint.Foreground;
                Console.BackgroundColor = toPrint.Background;
                Console.Write(toPrint.Display);
            }
            toPrints.Clear();
        }

        public static void Add(Sprite sprite) {
            Add(sprite.Display, sprite.Foreground, sprite.Background);
        }

        private static void Add(string display, ConsoleColor foreground, ConsoleColor background) {
            Sprite latest = LatestToPrint;
            if (latest == null || latest.Foreground != foreground || latest.Background != background) {
                toPrints.Add(new Sprite(display, foreground, background));
            } else {
                latest.Display += display;
            }
        }

        public static void Add(string display) {
            Sprite latest = LatestToPrint;
            if (latest == null) {
                toPrints.Add(new Sprite(display));
            } else {
                latest.Display += display;
            }
        }
    }
}
