using System;
using System.Collections.Generic;

namespace LudumDare35.Content
{
    internal abstract class ContentLoader<T>
        where T : IDisposable
    {
        private readonly Dictionary<string, T> content = new Dictionary<string, T>();

        public T Load(string filename)
        {
            if (!content.ContainsKey(filename))
                content.Add(filename, FromFile(filename));

            return content[filename];
        }

        public bool Unload(string fileName)
        {
            if (!content.ContainsKey(fileName))
                return false;

            content[fileName].Dispose();
            content.Remove(fileName);
            return false;
        }

        public void UnloadAll()
        {
            foreach (T asset in content.Values)
                asset.Dispose();

            content.Clear();
        }

        protected abstract T FromFile(string filename);
    }
}
