﻿using System;
using UnityEngine;

namespace Facade {
    public static class SceneFacade {
        public static Action<string, Action> AddLoadedCallback;
        public static Action<string, Action> RemoveLoadedCallback;
        public static Action<string, Action> AddUnloadedCallback;
        public static Action<string, Action> RemoveUnloadedCallback;
        public static Action<string> LoadScene;
        public static Action<string> LoadSceneAsync;
        public static Action<bool, Vector3, Action> ShowGarbageTriggerEffect;
    }
}