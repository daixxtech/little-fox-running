using System;
using Config;
using UnityEngine;

namespace Facade {
    public static class PlayerFacade {
        public static Func<Vector3> GetPosition;
        public static Func<EGarbage> GetGarbageBin;
        public static Action<EGarbage> OnGarbageBinChanged;
    }
}