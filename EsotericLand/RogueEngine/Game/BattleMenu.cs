using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class BattleMenu : Menu {
        public Character CharacterEnemy { get; private set; }
        public readonly PlayerUnit PlayerUnit;
        public readonly Character CharacterPlayer;
        public BattleBrain BattleBrainEnemy;
        public bool PlayerAttacked = false;

        public void SetEnemy(Unit unit) {
            CharacterEnemy = unit.Character;
            BattleBrainEnemy = unit.BattleBrain;
            UnitDestroyRunSystem.ToDestroy.Add(unit);
        }

        #region Menu
        public void SetBattleCharactersMenu() {
            List<Sprite>
                character1Sprites = GetCharacterDetails(CharacterEnemy),
                character2Sprites = GetCharacterDetails(CharacterPlayer);
            character2Sprites.Add(new Sprite($"Equipped: {PlayerUnit.EquippedWeapon.Name}\n"));
            List<Sprite> sprites = new List<Sprite>(character1Sprites.Count + character2Sprites.Count);
            sprites.AddRange(character1Sprites);
            sprites.AddRange(character2Sprites);
            Sprites = sprites;
        }

        private List<Sprite> GetCharacterDetails(Character character) {
            List<Sprite> sprites = new List<Sprite>(5) {
                new Sprite(character.Name + "\n"),
                Sprite.Health,
                new Sprite($": {character.Health}/{character.MaxHealth}\n")
            };
            AddResourceDetails(sprites, Sprite.Stamina, character.Stamina);
            AddResourceDetails(sprites, Sprite.Mana, character.Mana);
            AddResourceDetails(sprites, Sprite.Energy, character.Energy);
            sprites[sprites.Count - 1].Display += "\n";
            return sprites;
        }
        private static void AddResourceDetails(List<Sprite> sprites, Sprite resourceSprite, ResourceData resourceData) {
            if (resourceData.Max > 0) {
                sprites.Add(resourceSprite);
                sprites.Add(new Sprite($": {resourceData.Value}/{resourceData.Max}\n"));
            }
        }
        #endregion

        public BattleMenu(PlayerUnit playerUnit) :
            base(
                new Sprite[] {

                },
                new Option[] {

                }) {
            PlayerUnit = playerUnit;
            CharacterPlayer = playerUnit.Character;
        }

        public void Battle() {
            if (!PlayerAttacked) return;
            PlayerAttacked = false;
            if (CharacterEnemy.Health > 0) {
                BattleBrainEnemy.Action(CharacterEnemy, CharacterPlayer);
                CharacterPlayer.Regen();
                CharacterEnemy.Regen();
                SetBattleCharactersMenu();

                Renderer.Add(new Sprite("Enemy Actions:\n"));
                foreach (Sprite sprite in BattleBrainEnemy.GetActionDescription())
                    Renderer.Add(sprite);
                Renderer.Add("\n\n");
            }
        }
    }
}
