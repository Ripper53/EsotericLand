using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    /// <summary>
    /// Only one object of this type should exist.
    /// </summary>
    public class PlayerUnit : Unit {
        public static readonly Sprite PlayerSprite = new Sprite("@", ConsoleColor.Cyan);
        private static readonly Random rng = new Random();
        public readonly BattleSystemManager BattleSystemManager;

        public readonly DataMenu DataMenu;
        public readonly BattleMenu BattleMenu;
        public readonly UnitPlayerControls PlayerControls;
        public readonly List<Menu.Option> BattleOptions;
        public readonly Inventory Inventory;
        public Weapon EquippedWeapon { get; private set; }
        private readonly Menu.Option inventoryOption;

        public PlayerUnit(string name, Scene scene) : base(PlayerSprite, new Character(name, 10, 2), scene) {
            scene.PlayerUnit = this;
            Character.Stamina.RegenValue = 1;

            DataMenu = new DataMenu(this);
            BattleOptions = new List<Menu.Option>();
            BattleMenu = new BattleMenu(this) {
                Active = false,
                Options = BattleOptions
            };
            BattleSystemManager = new BattleSystemManager(BattleMenu);
            PlayerControls = new UnitPlayerControls(this);

            Inventory = new Inventory();
            Inventory.Add(new Items.Weapons.BareHands());
            inventoryOption = new Menu.Option("Inventory") {
                Action = (source, op) => {
                    Inventory.ShowMenu(this);
                }
            };

            EquipWeapon(null);
        }

        public void EquipWeapon(Weapon weapon) {
            if (weapon == null)
                weapon = new Items.Weapons.BareHands();
            EquippedWeapon = weapon;
            BattleOptions.Clear();
            BattleOptions.Capacity = EquippedWeapon.Attacks.Count + 1;
            BattleOptions.Add(inventoryOption);
            foreach (Weapon.Attack attack in EquippedWeapon.Attacks) {
                BattleOptions.Add(new Menu.Option(attack.GetDescription(EquippedWeapon, Character, BattleMenu.CharacterEnemy)) {
                    Action = (source, op) => {
                        if (attack.Use(EquippedWeapon, Character, BattleMenu.CharacterEnemy))
                            BattleMenu.PlayerAttacked = true;
                    }
                });
            }
        }

        protected override bool Move(Vector2 position) {
            if (base.Move(position)) {
                Character.Regen();
                return true;
            }
            return false;
        }

        public void Battle(Unit toBattleUnit) {
            DataMenu.Active = false;
            BattleMenu.Active = true;
            BattleMenu.SetEnemy(toBattleUnit);
            BattleMenu.SetBattleCharactersMenu();

            Menu.CurrentMenu = Menu.ALL.FindIndex(v => v == BattleMenu);
            Menu.Selected = true;
            BattleSystemManager.InBattle = true;
            while (BattleSystemManager.InBattle) {
                BattleSystemManager.Update();
                if (BattleMenu.CharacterEnemy.Health <= 0) {
                    BattleSystemManager.InBattle = false;
                    Drop(BattleMenu.CharacterEnemy);
                } else if (BattleMenu.CharacterPlayer.Health <= 0) {
                    BattleSystemManager.InBattle = false;
                }
            }
            Inventory.Menu.Active = false;
            BattleMenu.Active = false;
            DataMenu.Active = true;
            Menu.Selected = false;
        }

        protected override bool TakenPosition(Vector2 position, Unit unit) {
            Battle(unit);
            return true;
        }

        private void Drop(Character character) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"{character.Name} defeated!\n");
            if (character.Drops == null || character.Drops.Count == 0 || rng.Next(character.ItemDropChance) < character.GoldDropThreshold) {
                // Gold
                int gold = rng.Next(character.GoldDropMin, character.GoldDropMax + 1);
                Inventory.Gold += gold;
                Console.WriteLine($"Dropped {gold} gold.");
            } else {
                // Item
                Item item = character.Drops[rng.Next(character.Drops.Count)].Clone();
                Inventory.Add(item);
                Console.WriteLine($"Dropped {item.Name}.");
            }
            Console.ReadKey(true);
        }

        protected override bool OutOfBounds(MoveDirection direction) {
            foreach (Unit unit in CurrentScene.Units.Values) {
                if (unit != this)
                    UnitDestroyRunSystem.ToDestroy.Add(unit);
            }
            CurrentScene.GenerateRequest = true;
            return true;
        }
    }
}
