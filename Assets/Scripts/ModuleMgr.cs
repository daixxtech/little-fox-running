using System.Collections.Generic;
using Modules;

public sealed class ModuleMgr : IModule {
    private readonly List<IModule> _moduleList;

    public bool NeedUpdate { get; } = false;

    public ModuleMgr() {
        _moduleList = new List<IModule>();
    }

    public void Init() {
        foreach (var module in _moduleList) {
            module.Init();
        }
    }

    public void Dispose() {
        foreach (var module in _moduleList) {
            module.Dispose();
        }
    }

    public void Update() {
        foreach (var module in _moduleList) {
            if (module.NeedUpdate) {
                module.Update();
            }
        }
    }

    public void AddModule(IModule module) {
        _moduleList.Add(module);
    }

    public void RemoveModule(IModule module) {
        _moduleList.Remove(module);
        module.Dispose();
    }
}