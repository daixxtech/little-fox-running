namespace Modules {
    public interface IModule {
        bool NeedUpdate { get; }

        void Awake();

        void Dispose();

        void Update();
    }
}
