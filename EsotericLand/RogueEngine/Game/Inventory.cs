using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class Inventory {
        public int Gold = 0;
        public readonly List<Item> Items;
        public readonly Menu Menu;

        private readonly Sprite goldSprite = new Sprite("Gold: 0", ConsoleColor.DarkYellow), noItemsSprite = new Sprite("");
        private readonly Menu.Option backOption;

        public Inventory() {
            Items = new List<Item>();
            Menu = new Menu(
                new Sprite[] {
                    new Sprite("\nInventory\n", ConsoleColor.Black, ConsoleColor.Gray),
                    goldSprite,
                    Sprite.LineBreak,
                    noItemsSprite
                },
                new List<Menu.Option>()
            ) {
                Active = false
            };
            backOption = new Menu.Option("Close", ConsoleColor.DarkRed) {
                Action = (source, op) => {
                    Menu.Active = false;
                }
            };
        }

        public void Add(Item item) {
            Items.Add(item);
        }

        public void Remove(Item item) {
            Items.Remove(item);
        }

        public bool ContainsItem(string itemName) {
            return Items.Find(v => v.Name == itemName) != null;
        }

        public void ShowMenu(PlayerUnit playerUnit) {
            Menu.Options.Clear();
            Menu.Options.Add(backOption);
            goldSprite.Display = "Gold: " + Gold;
            if (Items.Count > 0) {
                noItemsSprite.Display = "";
                Dictionary<string, int> itemCount = new Dictionary<string, int>();
                foreach (Item item in Items) {
                    if (itemCount.ContainsKey(item.Name)) {
                        itemCount[item.Name]++;
                    } else {
                        itemCount.Add(item.Name, 1);
                    }
                }
                foreach (KeyValuePair<string, int> pairs in itemCount) {
                    int count = pairs.Value;
                    string display = pairs.Key;
                    if (count > 1)
                        display += $" x{count}";
                    Menu.Options.Add(new Menu.Option(display) {
                        Action = (source, op) => {
                            Item item = Items.Find(v => v.Name == pairs.Key);
                            if (item != null) {
                                if (item.Use(playerUnit) && item is Potion) {
                                    count--;
                                    if (count > 1) {
                                        op.Sprites[0].Display = $"{pairs.Key} x{count}";
                                    } else if (count == 1) {
                                        op.Sprites[0].Display = pairs.Key;
                                    } else {
                                        source.Options.Remove(op);
                                        source.MoveDown();
                                    }
                                }
                            } else {
                                source.Options.Remove(op);
                                source.MoveDown();
                            }
                            playerUnit.BattleMenu.SetBattleCharactersMenu();
                        }
                    });
                }
            } else {
                noItemsSprite.Display = "Empty...";
            }
            Menu.Active = true;
            Menu.CurrentMenu = Menu.ALL.FindIndex(v => v == Menu);
        }
    }
}
