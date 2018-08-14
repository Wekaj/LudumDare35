using LudumDare35.Modules.Types;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System;
using System.Collections;

namespace LudumDare35.Modules
{
    internal sealed class Fortress : Drawable, IEnumerable<IModule>
    {
        private readonly HashSet<Module> modules = new HashSet<Module>();
        private readonly Module[,] structure = new Module[23, 15];
        private readonly Dictionary<Module, Vector2i> modulePositions = new Dictionary<Module, Vector2i>();

        public Fortress(ModuleRegistry registry)
        {
            Source = CreateModule(registry.Source);
            AddModule(Source, Width / 2, Height / 2);
        }

        public IModule this[int x, int y] => structure[x, y];

        public int Width => structure.GetLength(0);
        public int Height => structure.GetLength(1);
        public Vector2f Position { get; set; }
        public Color Palette { get; set; }
        public IModule Source { get; set; }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Translate(Position);
            foreach (KeyValuePair<Module, Vector2i> item in modulePositions)
                item.Key.Draw(target, states, Palette, new Vector2f(item.Value.X, item.Value.Y) * 16f);
        }

        public IModule CreateModule(ModuleType type) => new Module(type);

        public bool AddModule(IModule module, int x, int y)
        {
            Module castedModule = module as Module;

            if (castedModule == null || modules.Contains(castedModule) || !ModuleFits(module, x, y))
                return false;

            modules.Add(castedModule);
            modulePositions.Add(castedModule, new Vector2i(x, y));
            for (int moduleY = 0; moduleY < module.Height; moduleY++)
                for (int moduleX = 0; moduleX < module.Width; moduleX++)
                    if (module[moduleX, moduleY])
                        structure[x + moduleX, y + moduleY] = castedModule;
            castedModule.Connections = GetAdjacentModules(castedModule);

            foreach (Module otherModule in castedModule.Connections)
                otherModule.Connections.Add(castedModule);
            return true;
        }

        public bool RemoveModule(IModule module)
        {
            Module castedModule = module as Module;

            if (castedModule == null || !modules.Contains(castedModule))
                return false;

            Vector2i position = modulePositions[castedModule];
            for (int moduleY = 0; moduleY < module.Height; moduleY++)
                for (int moduleX = 0; moduleX < module.Width; moduleX++)
                    if (module[moduleX, moduleY])
                        structure[position.X + moduleX, position.Y + moduleY] = null;
            modules.Remove(castedModule);
            modulePositions.Remove(castedModule);
            castedModule.Connections.Clear();

            foreach (Module otherModule in modules)
                otherModule.Connections.Remove(castedModule);
            return true;
        }

        public ModuleLink RemoveChain(IModule module)
        {
            Module castedModule = module as Module;

            if (castedModule == null)
                return null;

            Vector2i position = GetPosition(module);
            return RemoveChain(castedModule, new HashSet<IModule>(), position.X, position.Y);
        }

        private ModuleLink RemoveChain(Module module, HashSet<IModule> ignore, int originX, int originY)
        {
            if (!modules.Contains(module))
                return null;
            ignore.Add(module);

            HashSet<Module> connections = new HashSet<Module>(module.Connections);
            Vector2i position = GetPosition(module);
            RemoveModule(module);
            ModuleLink chain = new ModuleLink(module, position.X - originX, position.Y - originY);
            foreach (Module connectedModule in connections)
                if (!ignore.Contains(connectedModule))
                    if (!ConnectedToSource(connectedModule))
                        chain.Links.Add(RemoveChain(connectedModule, ignore, originX, originY));
            return chain;
        }

        public Vector2i GetPosition(IModule module) => modulePositions[(Module)module];

        public bool ModuleFits(IModule module, int x, int y)
        {
            for (int moduleY = 0; moduleY < module.Height; moduleY++)
                for (int moduleX = 0; moduleX < module.Width; moduleX++)
                    if (module[moduleX, moduleY] && structure[x + moduleX, y + moduleY] != null)
                        return false;
            return true;
        }

        public bool ConnectedToSource(IModule module)
        {
            Module castedModule = module as Module;

            if (castedModule == null)
                return false;

            return ConnectedToSource(castedModule, new HashSet<Module>());
        }

        private bool ConnectedToSource(Module module, HashSet<Module> ignore)
        {
            if (module == Source)
                return true;
            ignore.Add(module);
            foreach (Module connectedModule in module.Connections)
            {
                if (ignore.Contains(connectedModule))
                    continue;
                if (connectedModule == Source || ConnectedToSource(connectedModule, ignore))
                    return true;
            }
            return false;
        }

        private HashSet<Module> GetAdjacentModules(Module module)
        {
            HashSet<Module> adjacent = new HashSet<Module>();

            if (module == null || !modules.Contains(module))
                return adjacent;

            Vector2i position = modulePositions[module];
            for (int moduleY = 0; moduleY < module.Height; moduleY++)
                for (int moduleX = 0; moduleX < module.Width; moduleX++)
                    if (module[moduleX, moduleY])
                    {
                        for (int y = moduleY - 1; y < moduleY + 2; y += 2)
                        {
                            Module adjacentModule = structure[position.X + moduleX, position.Y + y];
                            if (adjacentModule != null && !adjacent.Contains(adjacentModule))
                                adjacent.Add(adjacentModule);
                        }
                        for (int x = moduleX - 1; x < moduleX + 2; x += 2)
                        {
                            Module adjacentModule = structure[position.X + x, position.Y + moduleY];
                            if (adjacentModule != null && !adjacent.Contains(adjacentModule))
                                adjacent.Add(adjacentModule);
                        }
                    }
            adjacent.Remove(module);
            return adjacent;
        }

        public IEnumerator<IModule> GetEnumerator() => modules.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => modules.GetEnumerator();

        private sealed class Module : IModule
        {
            private readonly ModuleType type;

            public Module(ModuleType type)
            {
                this.type = type;
            }

            public bool this[int x, int y] => type[x, y];

            public HashSet<Module> Connections { get; set; } = new HashSet<Module>();
            public int Width => type.Width;
            public int Height => type.Height;
            public Texture Texture => type.Texture;
            public Texture Glow => type.Glow;
            public bool Solid => type.Solid;
            public int Income => type.Income;

            public void Draw(RenderTarget target, RenderStates states, Color palette, Vector2f position) 
                => type.Draw(target, states, palette, position);
        }
    }
}
