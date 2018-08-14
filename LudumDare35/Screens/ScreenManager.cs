using System;
using System.Collections.Generic;

namespace LudumDare35.Screens
{
    internal sealed class ScreenManager
    {
        private readonly Dictionary<Type, IScreen> screens = new Dictionary<Type, IScreen>();
        private readonly HashSet<Type> addingTypes = new HashSet<Type>(), 
            removingTypes = new HashSet<Type>();
        private readonly HashSet<IScreen> addingScreens = new HashSet<IScreen>();

        public bool Add<T>(T screen)
            where T : IScreen
        {
            Type type = typeof(T);

            if (addingTypes.Contains(type) 
                || (screens.ContainsKey(type) && !removingTypes.Contains(type)))
                return false;

            addingTypes.Add(type);
            addingScreens.Add(screen);
            return true;
        }

        public bool Remove<T>()
            where T : IScreen
        {
            Type type = typeof(T);

            if (!screens.ContainsKey(type) || removingTypes.Contains(type))
                return false;

            removingTypes.Add(type);
            return true;
        }

        public bool Has<T>() where T : IScreen => screens.ContainsKey(typeof(T));
        public T Get<T>() where T : IScreen => (T)screens[typeof(T)];

        public void Update(float delta)
        {
            foreach (Type type in removingTypes)
                screens.Remove(type);
            removingTypes.Clear();

            foreach (IScreen screen in addingScreens)
                screens.Add(screen.GetType(), screen);
            addingTypes.Clear();
            addingScreens.Clear();

            foreach (IScreen screen in screens.Values)
                screen.Update(delta);
        }

        public void Draw()
        {
            foreach (Type type in removingTypes)
                screens.Remove(type);
            removingTypes.Clear();

            foreach (IScreen screen in addingScreens)
                screens.Add(screen.GetType(), screen);
            addingTypes.Clear();
            addingScreens.Clear();

            foreach (IScreen screen in screens.Values)
                screen.Draw();
        }
    }
}
