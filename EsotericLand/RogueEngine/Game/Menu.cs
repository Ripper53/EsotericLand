using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class Menu : Entity<Menu> {
        public static bool Selected = false;
        public static int CurrentMenu = 0;

        public override bool Active {
            set {
                base.Active = value;
                if (!value && ALL[CurrentMenu] == this)
                    Next();
            }
        }

        public class Option {
            public readonly IList<Sprite> Sprites;
            public Action<Menu, Option> Action;

            public Option(string display, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) {
                Sprites = new Sprite[] { new Sprite(display, foreground, background) };
                Action = None;
            }

            public Option(IList<Sprite> sprites) {
                Sprites = sprites;
                Action = None;
            }

            public static void None(Menu menu, Option source) { }
        }

        public IList<Sprite> Sprites;
        public IList<Option> Options;
        public int SelectedOption = 0;

        public Menu(IList<Sprite> sprites, IList<Option> options) {
            Sprites = sprites;
            Options = options;
        }

        public void MoveUp() {
            SelectedOption--;
            if (SelectedOption < 0)
                SelectedOption = Options.Count - 1;
        }
        public void MoveDown() {
            SelectedOption++;
            if (SelectedOption >= Options.Count)
                SelectedOption = 0;
        }

        public override void Destroy() {
            base.Destroy();
            if (CurrentMenu >= ALL.Count) {
                CurrentMenu = 0;
                Selected = false;
            }
        }

        public static void Next() {
            int oldCurrentMenuValue = CurrentMenu;
            do {
                CurrentMenu++;
                if (CurrentMenu == ALL.Count) {
                    CurrentMenu = 0;
                }
                if (CurrentMenu == oldCurrentMenuValue)
                    break;
            } while (!ALL[CurrentMenu].Active);
        }

        public virtual void Print() {
            foreach (Sprite sprite in Sprites) {
                Renderer.Add(sprite);
            }
            Renderer.Add("\n");
            for (int i = 0; i < Options.Count; i++) {
                if (ALL[CurrentMenu] == this && SelectedOption == i)
                    Renderer.Add(new Sprite(">"));
                foreach (Sprite sprite in Options[i].Sprites) {
                    Renderer.Add(sprite);
                }
                Renderer.Add("\n");
            }
        }

    }
}
