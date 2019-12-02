using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class ResourceData {
        public const int Min = 0;
        public int RegenValue = 0;
        public int Value { get; private set; }
        public int Max { get; private set; }

        public ResourceData(int value) {
            Value = value;
            Max = value;
        }

        public void Regen() {
            Add(RegenValue);
        }

        private void Check() {
            if (Value > Max)
                Value = Max;
            else if (Value < Min)
                Value = Min;
        }

        public void Add(int value) {
            Value += value;
            Check();
        }
        public void Remove(int value) {
            Value -= value;
            Check();
        }

        public bool ContainsEnough(int value) {
            return value <= Value;
        }

        public bool Use(int value) {
            if (ContainsEnough(value)) {
                Remove(value);
                return true;
            }
            return false;
        }

        public void IncreaseMax(int addMax) {
            Max += addMax;
            Check();
        }
    }
}
