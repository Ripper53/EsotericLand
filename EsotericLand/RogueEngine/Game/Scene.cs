using System;
using System.Collections.Generic;
using EsotericLand.RogueEngine.Game.Units;

namespace EsotericLand.RogueEngine.Game {
    public class Scene : Entity<Scene> {
        private readonly static Random rng = new Random();
        public readonly static Sprite
            GroundSprite = new Sprite(" "),
            WallSprite = new Sprite(" ", background: ConsoleColor.White);

        public enum Tile {
            Ground, Wall
        };

        public readonly Dictionary<Vector2, Unit> Units = new Dictionary<Vector2, Unit>();
        public readonly List<Vector2> FreeSpace = new List<Vector2>();
        public Vector2 Size { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public PlayerUnit PlayerUnit;
        public bool GenerateRequest = false;

        private uint generateCount = 0;


        public Scene(Vector2 size) {
            SetSize(size);
        }

        public void SetSize(Vector2 size) {
            Size = size;
            Tiles = new Tile[size.x, size.y];
        }

        public bool PlaceUnitInRandomFreeSpace(Unit unit) {
            if (GetRandomFreeSpace(out Vector2 position)) {
                unit.Position = position;
                Units.Add(unit.Position, unit);
                return true;
            }
            return false;
        }
        public bool GetRandomFreeSpace(out Vector2 freeSpace) {
            if (FreeSpace.Count > 0) {
                int index = rng.Next(FreeSpace.Count);
                freeSpace = FreeSpace[index];
                FreeSpace.RemoveAt(index);
                return true;
            }
            freeSpace = new Vector2(0, 0);
            return false;
        }

        public void Generate() {
            generateCount++;
            Units.Clear();
            FreeSpace.Clear();
            HashSet<Vector2> openPositions = new HashSet<Vector2>(Size.x * Size.y);
            for (int y = 1; y < Size.y - 1; y++) {
                for (int x = 1; x < Size.x - 1; x++) {
                    openPositions.Add(new Vector2(x, y));
                }
            }

            // Rooms
            List<Vector2> roomEntries = new List<Vector2>(openPositions.Count);
            FreeSpace.Capacity = openPositions.Count;
            int randomMax = rng.Next(10, 200);
            for (int y = 1; y < Size.y - 1; y++) {
                for (int x = 1; x < Size.x - 1; x++) {
                    Vector2 pos = new Vector2(x, y);
                    if (openPositions.Remove(pos)) {
                        switch (rng.Next(randomMax)) {
                            case 0:
                                Vector2 size = new Vector2(rng.Next(2, 11), rng.Next(2, 11));
                                roomEntries.Add(
                                    pos +
                                    new Vector2(
                                        rng.Next(size.x - 1),
                                        rng.Next(size.y - 1)
                                    )
                                );
                                GenerateRoom(openPositions, pos, size);
                                break;
                            default:
                                Tiles[pos.x, pos.y] = Tile.Wall;
                                break;
                        }
                    }
                }
            }

            GenerateBorder();

            // Paths
            // Top to down.
            GeneratePath(
                new Vector2(rng.Next(Size.x - 1), 0),
                new Vector2(rng.Next(Size.x - 1), Size.y - 1)
            );
            // Left to right.
            GeneratePath(
                new Vector2(0, rng.Next(Size.y - 1)),
                new Vector2(Size.x - 1, rng.Next(Size.y - 1))
            );

            for (int i = 1; i < roomEntries.Count; i++) {
                GeneratePath(CheckInBounds(roomEntries[i - 1]), CheckInBounds(roomEntries[i]));
            }
            Vector2 start = CheckInBounds(roomEntries[0]), end = CheckInBounds(roomEntries[roomEntries.Count - 1]);
            GeneratePath(start, end);

            PlayerUnit.PlaceInRandomPosition();

            // Player Stage advantages
            switch (generateCount) {
                case 5:
                    PlayerUnit.Character.Stamina.RegenValue++;
                    break;
                case 10:
                    PlayerUnit.Character.Stamina.RegenValue += 4;
                    break;
                case 11:
                    PlayerUnit.Character.Mana.RegenValue++;
                    PlayerUnit.Character.Mana.IncreaseMax(5);
                    break;
                case 15:
                    PlayerUnit.Character.Mana.RegenValue += 4;
                    PlayerUnit.Character.Energy.IncreaseMax(2);
                    break;
            }

            // Player Gold advantages
            if (PlayerUnit.Inventory.Gold > 500)
                PlayerUnit.Character.Heal(20);
            else if (PlayerUnit.Inventory.Gold > 100)
                PlayerUnit.Character.Heal(5);
            else if (PlayerUnit.Inventory.Gold > 50)
                PlayerUnit.Character.Heal(1);

            PutUnits();
        }

        private void PutUnits() {
            if (!BoppinUnit.BoppinDefeated && PlayerUnit.Inventory.Gold > 1000) {
                RunCountRandom(0, 2, () => new BoppinUnit(this));
            }
            if (PlayerUnit.Inventory.Gold > 300 && rng.Next(0, 4) == 0) {
                RunCountRandom(1, 3, () => new DragonUnit(this));
            }
            if (generateCount > 20) {
                RunCountRandom(0, 4, () => new CyborgUnit(this));
                RunCountRandom(0, 10, () => new GoblinUnit(this));
                RunCountRandom(0, 5, () => new OrcUnit(this));
                RunCountRandom(0, 4, () => new VampireUnit(this));
                RunCountRandom(0, 4, () => new WendigoUnit(this));
                RunCountRandom(0, 4, () => new TrollUnit(this));
                RunCountRandom(0, 2, () => new ChestUnit(this));
                RunCountRandom(0, 2, () => new LivingChestUnit(this));
            } else if (generateCount > 14) {
                RunCountRandom(0, 3, () => new VampireUnit(this));
                RunCountRandom(1, 4, () => new WendigoUnit(this));
            } else if (generateCount > 13) {
                RunCount(2, () => new ChestUnit(this));
                RunCount(5, () => new LivingChestUnit(this));
            } else if (generateCount > 12) {
                RunCountRandom(1, 4, () => new CyborgUnit(this));
                new GoblinUnit(this);
            } else if (generateCount > 11) {
                RunCountRandom(5, 7, () => {
                    new VampireUnit(this);
                    RunCountRandom(1, 4, () => new WendigoUnit(this));
                });
            } else if (generateCount > 10) {
                RunCountRandom(9, 15, () => {
                    new GoblinUnit(this);
                    switch (rng.Next(0, 5)) {
                        case 0:
                            new ChestUnit(this);
                            break;
                        case 1:
                            new LivingChestUnit(this);
                            break;
                    }
                });
            } else if (generateCount > 8) {
                RunCountRandom(1, 4, () => {
                    new VampireUnit(this);
                    new GoblinUnit(this);
                });
            } else if (generateCount > 5) {
                RunCountRandom(1, 3, () => {
                    new ChestUnit(this);
                    new LivingChestUnit(this);
                    new GoblinUnit(this);
                });
                RunCountRandom(2, 5, () => new OrcUnit(this));
            } else if (generateCount > 2) {
                RunCount(1, () => new ChestUnit(this));
                RunCountRandom(1, 3, () => new OrcUnit(this));
                RunCountRandom(1, 3, () => new BanditUnit(this));
            } else {
                RunCountRandom(2, 4, () => new BanditUnit(this));
            }
        }
        private static void RunCountRandom(int min, int max, Action action) {
            RunCount(rng.Next(min, max), action);
        }
        private static void RunCount(int count, Action action) {
            for (int i = 0; i < count; i++)
                action();
        }

        private void GenerateBorder() {
            for (int x = 0; x < Size.x; x++) {
                Tiles[x, 0] = Tile.Wall;
                FreeSpace.Remove(new Vector2(x, 0));
            }
            for (int x = 0; x < Size.x; x++) {
                Tiles[x, Size.y - 1] = Tile.Wall;
                FreeSpace.Remove(new Vector2(x, Size.y - 1));
            }
            for (int y = 0; y < Size.y; y++) {
                Tiles[0, y] = Tile.Wall;
                FreeSpace.Remove(new Vector2(0, y));
            }
            for (int y = 0; y < Size.y; y++) {
                Tiles[Size.x - 1, y] = Tile.Wall;
                FreeSpace.Remove(new Vector2(Size.x - 1, y));
            }
        }

        private Vector2 CheckInBounds(Vector2 point) {
            if (point.x >= Size.x)
                point.x = Size.x - 1;
            if (point.y >= Size.y)
                point.y = Size.y - 1;
            return point;
        }

        private void GenerateRoom(HashSet<Vector2> openPositions, Vector2 position, Vector2 size, Tile tile = Tile.Ground) {
            for (int y = position.y, yCount = 0; yCount < size.y && y < Size.y; y++, yCount++) {
                for (int x = position.x, xCount = 0; xCount < size.x && x < Size.x; x++, xCount++) {
                    Vector2 pos = new Vector2(x, y);
                    openPositions.Remove(pos);
                    Tiles[x, y] = tile;
                    FreeSpace.Add(pos);
                }
            }
        }

        private Vector2 GetEnds(Vector2 point, Tile tile = Tile.Ground) {
            if (point.x == 0) {
                Tiles[point.x, point.y] = tile;
                point.x++;
            } else if (point.x == Size.x - 1) {
                Tiles[point.x, point.y] = tile;
                point.x--;
            }
            if (point.y == 0) {
                Tiles[point.x, point.y] = tile;
                point.y++;
            } else if (point.y == Size.y - 1) {
                Tiles[point.x, point.y] = tile;
                point.y--;
            }
            return point;
        }

        private void GeneratePath(Vector2 start, Vector2 end) {
            start = GetEnds(start);
            end = GetEnds(end);
            while (start != end) {
                if (start.x == 0) {
                    if (start.y == 0) {
                        // (min, min)
                        // Choose random path to go either right or down.
                        switch (rng.Next(0, 2)) {
                            case 0:
                                start = GenerateHorizontalPath(start, start.x + 2);
                                break;
                            default:
                                start = GenerateVerticalPath(start, start.y + 2);
                                break;
                        }
                    } else {
                        // (min, y)
                        // Go right.
                        start = GenerateHorizontalPath(start, end.x);
                    }
                } else if (start.y == 0) {
                    // (x, min)
                    // Go down.
                    start = GenerateVerticalPath(start, end.y);
                } else
                if (start.x == Size.x - 1) {
                    if (start.y == Size.y - 1) {
                        // (max, max)
                        // Choose random path to go left or up.
                        switch (rng.Next(0, 2)) {
                            case 0:
                                start = GenerateHorizontalPath(start, start.x - 2);
                                break;
                            default:
                                start = GenerateVerticalPath(start, start.y - 2);
                                break;
                        }
                    } else {
                        // (max, y)
                        // Go left.
                        start = GenerateHorizontalPath(start, end.x);
                    }
                } else if (start.y == Size.y - 1) {
                    // (x, max)
                    // Go up.
                    start = GenerateVerticalPath(start, end.y);
                } else {
                    // (x, y)
                    // Go in any direction.
                    start = GenerateRandomPathDirection(start, end);
                }
            }
        }

        private Vector2 GenerateRandomPathDirection(Vector2 start, Vector2 end) {
            switch (rng.Next(2)) {
                case 0:
                    start = GenerateHorizontalPath(start, end.x);
                    start = GenerateVerticalPath(start, end.y);
                    break;
                default:
                    start = GenerateVerticalPath(start, end.y);
                    start = GenerateHorizontalPath(start, end.x);
                    break;
            }
            return start;
        }

        private Vector2 GenerateHorizontalPath(Vector2 pos, int horTarget, Tile tile = Tile.Ground) {
            if (pos.x < horTarget) {
                for (; pos.x < horTarget; pos.x++) {
                    Tiles[pos.x, pos.y] = tile;
                    FreeSpace.Add(pos);
                }
            } else if (pos.x > horTarget) {
                for (; pos.x >= horTarget; pos.x--) {
                    Tiles[pos.x, pos.y] = tile;
                    FreeSpace.Add(pos);
                }
            }
            return pos;
        }

        private Vector2 GenerateVerticalPath(Vector2 pos, int verTarget, Tile tile = Tile.Ground) {
            if (pos.y < verTarget) {
                for (; pos.y < verTarget; pos.y++) {
                    Tiles[pos.x, pos.y] = tile;
                    FreeSpace.Add(pos);
                }
            } else if (pos.y > verTarget) {
                for (; pos.y >= verTarget; pos.y--) {
                    Tiles[pos.x, pos.y] = tile;
                    FreeSpace.Add(pos);
                }
            }
            return pos;
        }

    }
}
