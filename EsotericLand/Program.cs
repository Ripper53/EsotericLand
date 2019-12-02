using System;
using System.Threading;
using EsotericLand.RogueEngine;
using EsotericLand.RogueEngine.Game;

namespace EsotericLand {
    class Program {
        public static readonly Random RNG = new Random();
        public const int SizeX = 70, SizeY = 35;

        private static readonly GameSystemManager gameSystemManager = new GameSystemManager();
        private static bool quitRequest = false;
        private static void QuitGame() {
            quitRequest = true;
            ClearGame();
        }
        private static void ClearGame() {
            Scene.ALL.Clear();
            Menu.ALL.Clear();
            Menu.CurrentMenu = 0;
            Menu.Selected = false;
            Unit.ALL.Clear();
        }

        static void Main(string[] args) {
            Console.Title = "Esoteric Land";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = SizeY + 2;
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            while (!quitRequest) {
                Intro();
                PlayGame();
                ClearGame();
            }
        }

        private static void HelpMenu() {
            bool backToMainMenu = false;
            Menu menu = null;
            menu = new Menu(
                new Sprite[] {
                    new Sprite("Help", ConsoleColor.Black, ConsoleColor.Gray),
                    new Sprite("\nYou are represented by: "),
                    PlayerUnit.PlayerSprite,
                    new Sprite("\nUse WASD or Arrow Keys to move around.\nIf your "),
                    Sprite.Health,
                    new Sprite(" drops to 0, you die and the run is over.\nThere are 3 resource types:\n"),
                    Sprite.Stamina,
                    new Sprite(" (physical)\n"),
                    Sprite.Mana,
                    new Sprite("    (magical)\n"),
                    Sprite.Energy,
                    new Sprite ("  (electrical)\nThese resources can be used when in battle.\nColliding with an enemy will commence the battle.\nUse TAB to switch through menus (if there is more than one).\nAs you progress through dungeons and acquire gold, you will be given perks automatically.\n")
                },
                new Menu.Option[] {
                    new Menu.Option("Back") {
                        Action = (source, op) => {
                            backToMainMenu = true;
                        }
                    }
                }
            );
            Menu.CurrentMenu = Menu.ALL.Count - 1;

            gameSystemManager.Reset();
            while (!backToMainMenu) {
                gameSystemManager.Update();
            }
            menu.Destroy();
            Menu.Selected = true;
        }

        private static void Credits() {
            bool backToMainMenu = false;
            Menu menu = null;
            menu = new Menu(
                new Sprite[] {
                    new Sprite("Credits", ConsoleColor.Black, ConsoleColor.Gray),
                    new Sprite("\nCreated by "),
                    new Sprite("Albar", ConsoleColor.Green),
                    new Sprite(" for the first "),
                    new Sprite("Lost Cartridge Jam", ConsoleColor.Blue),
                    new Sprite("!\n")
                },
                new Menu.Option[] {
                    new Menu.Option("Back") {
                        Action = (source, op) => {
                            backToMainMenu = true;
                        }
                    }
                }
            );
            Menu.CurrentMenu = Menu.ALL.Count - 1;

            gameSystemManager.Reset();
            while (!backToMainMenu) {
                gameSystemManager.Update();
            }
            menu.Destroy();
            Menu.Selected = true;
        }

        private static void Intro() {
            // Used this site for ascii art: http://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20
            Menu menu = null;
            menu = new Menu(
                new Sprite[] {
                    new Sprite(
@"                                 (                      
               )                 )\ )             (     
 (          ( /(  (  (  (       (()/(    )        )\ )  
 )\  (   (  )\())))\ )( )\  (    /(_))( /(  (    (()/(  
((_) )\  )\(_))//((_|()((_) )\  (_))  )(_)) )\ )  ((_)) 
| __((_)((_) |_(_))  ((_|_)((_) | |  ((_)_ _(_/(  _| |  
| _|(_-< _ \  _/ -_)| '_| / _|  | |__/ _` | ' \)) _` |  
|___/__|___/\__\___||_| |_\__|  |____\__,_|_||_|\__,_|
", ConsoleColor.Red)
                },
                new Menu.Option[] {
                    new Menu.Option("Start", ConsoleColor.Green) {
                        Action = (source, op) => {
                            Menu.Selected = false;
                        }
                    },
                    new Menu.Option("Help", ConsoleColor.Yellow) {
                        Action = (source, op) => {
                            menu.Active = false;
                            HelpMenu();
                            menu.Active = true;
                        }
                    },
                    new Menu.Option("Credits", ConsoleColor.Blue) {
                        Action = (source, op) => {
                            menu.Active = false;
                            Credits();
                            menu.Active = true;
                        }
                    },
                    new Menu.Option("Quit", ConsoleColor.DarkRed) {
                        Action = (source, op) => {
                            QuitGame();
                        }
                    }
                }
            );

            Menu.Selected = true;
            while (Menu.Selected) {
                gameSystemManager.Update();
            }
            
            menu.Destroy();
        }

        private static void PlayGame() {
            PlayerUnit playerUnit = CreateGame();

            while (!quitRequest) {
                gameSystemManager.Update();
                Character c = playerUnit.Character;
                if (c.Health <= 0) {
                    Character enemy = playerUnit.BattleMenu.CharacterEnemy;
                    Renderer.Add(new Sprite(enemy.Name + "\n"));
                    Renderer.Add(Sprite.Health);
                    Renderer.Add(new Sprite($": {enemy.Health}/{enemy.MaxHealth}\n\n"));
                    Renderer.Add(new Sprite(c.Name + "\n"));
                    Renderer.Add(Sprite.Health);
                    Renderer.Add(new Sprite($": {c.Health}/{c.MaxHealth}\n\nYou Died!\nPress enter to exit...\n"));
                    Renderer.Display();
                    Console.ReadLine();
                    QuitGame();
                }
            }
        }

        private static PlayerUnit CreateGame() {
            // Scene
            Scene scene = new Scene(new Vector2(SizeX, SizeY));
            // Player
            PlayerUnit playerUnit = new PlayerUnit("You", scene);
            playerUnit.Inventory.Add(new RogueEngine.Game.Items.Weapons.WoodenHammer());

            scene.GenerateRequest = true;
            return playerUnit;
        }

    }
}
