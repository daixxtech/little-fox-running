using Config;
using Facade;
using System;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Modules.Scenes {
    public class LevelScene : MonoBehaviour {
        [SerializeField] private Player _player;
        [SerializeField] private Garbage[] _garbageArray;
        [SerializeField] private AnimationOnce _rightBinEffect;
        [SerializeField] private AnimationOnce _wrongBinEffect;
        [SerializeField] private int _destroyedGarbageCount;

        private async void Awake() {
            transform.Find("AirBarrier/Tilemap").GetComponent<Tilemap>().color = Color.clear;

            _player = transform.Find("Player/Character").gameObject.AddComponent<Player>();

            Transform garbageRootTrans = transform.Find("Garbage");
            int count = garbageRootTrans.childCount;
            _garbageArray = new Garbage[count];
            ConfGarbage[] confList = await ConfGarbage.GetArray();
            int confLength = confList.Length;
            if (confLength != 0) {
                for (int i = 0; i < count; i++) {
                    Garbage garbage = garbageRootTrans.GetChild(i).gameObject.AddComponent<Garbage>();
                    int randomIndex = UnityEngine.Random.Range(0, confLength);
                    garbage.SetData(confList[randomIndex]);
                    _garbageArray[i] = garbage;
                }
            }

            _rightBinEffect = transform.Find("Effects/RightBin").GetComponent<AnimationOnce>();
            _wrongBinEffect = transform.Find("Effects/WrongBin").GetComponent<AnimationOnce>();

            if (Camera.main != null) {
                Camera.main.gameObject.AddComponent<CameraController>();
            }

            UIFacade.HideUIAll?.Invoke();
            UIFacade.ShowUI?.Invoke(UIDef.LEVEL_MAIN);

            SceneFacade.IsAllGarbageCollected += IsAllGarbageCollected;
            SceneFacade.OnGarbageDestroy += OnGarbageDestroy;
        }

        private void OnDestroy() {
            SceneFacade.IsAllGarbageCollected -= IsAllGarbageCollected;
            SceneFacade.OnGarbageDestroy -= OnGarbageDestroy;

            if (Camera.main != null) {
                Destroy(Camera.main.gameObject.GetComponent<CameraController>());
            }
        }

        private void ShowGarbageTriggerEffect(bool value, Vector3 position, Action animOverCallback) {
            AnimationOnce effect = value ? _rightBinEffect : _wrongBinEffect;
            effect.gameObject.SetActive(true);
            effect.transform.position = position;
            if (animOverCallback != null) {
                effect.animationOver += animOverCallback;
            }
        }

        private bool IsAllGarbageCollected() {
            return _destroyedGarbageCount >= _garbageArray.Length;
        }

        private void OnGarbageDestroy(Garbage garbage, bool value) {
            ++_destroyedGarbageCount;
            if (value) {
                if (IsAllGarbageCollected()) {
                    ShowGarbageTriggerEffect(true, garbage.transform.position, () => UIFacade.ShowUI?.Invoke(UIDef.ACCOMPLISHED));
                } else {
                    ShowGarbageTriggerEffect(true, garbage.transform.position, null);
                }
            } else {
                Time.timeScale = 0;
                ShowGarbageTriggerEffect(false, garbage.transform.position, () => { UIFacade.ShowUIByParam?.Invoke(UIDef.TIPS, garbage.Conf); });
            }
        }
    }
}
