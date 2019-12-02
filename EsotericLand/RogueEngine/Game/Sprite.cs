using System;

namespace EsotericLand.RogueEngine.Game {
    public class Sprite {
        public const ConsoleColor
            HealthColor = ConsoleColor.Red,
            StaminaColor = ConsoleColor.Magenta,
            ManaColor = ConsoleColor.Blue,
            EnergyColor = ConsoleColor.Yellow;

        public static readonly Sprite
            LineBreak = new Sprite("\n"),
            Health = new Sprite("Health", HealthColor),
            Stamina = new Sprite("Stamina", StaminaColor),
            Mana = new Sprite("Mana", ManaColor),
            Energy = new Sprite("Energy", EnergyColor);

        public string Display;
        public ConsoleColor Foreground, Background;

        public Sprite(string display, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) {
            Display = display;
            Foreground = foreground;
            Background = background;
        }

        public static Sprite Clone(Sprite sprite) {
            return new Sprite(sprite.Display, sprite.Foreground, sprite.Background);
        }

        public static void Write(Sprite sprite) {
            Console.ForegroundColor = sprite.Foreground;
            Console.BackgroundColor = sprite.Background;
            Console.Write(sprite.Display);
        }
    }
}
