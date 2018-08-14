using System.Collections.Generic;

namespace LudumDare35.Input
{
    internal sealed class ButtonManager
    {
        private readonly Dictionary<string, Button> buttons = new Dictionary<string, Button>();

        public Button this[string key] => buttons[key];

        public bool Add(string key, Button button)
        {
            if (buttons.ContainsKey(key))
                return false;

            buttons.Add(key, button);
            return true;
        }

        public bool Remove(string key) => buttons.Remove(key);
        public bool Has(string key) => buttons.ContainsKey(key);

        public void Update()
        {
            foreach (Button button in buttons.Values)
                button.Update();
        }
    }
}
