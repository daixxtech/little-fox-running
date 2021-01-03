using System;
using Config;
using Facade;
using UnityEngine;

namespace Modules.Scenes {
    public class LevelScene : MonoBehaviour {
        [SerializeField] private Player _player;
        [SerializeField] private Garbage[] _garbageArray;
        [SerializeField] private AnimationOnce _rightBinEffect;
        [SerializeField] private AnimationOnce _wrongBinEffect;

        private void Awake() {
            _player = transform.Find("Player/Character").gameObject.AddComponent<Player>();

            Transform garbageRootTrans = transform.Find("Garbage");
            int count = garbageRootTrans.childCount;
            _garbageArray = new Garbage[count];
            ConfGarbage[] confList = ConfGarbage.Array;
            int confLength = confList.Length;
            if (confLength == 0) {
                return;
            }
            for (int i = 0; i < count; i++) {
                Garbage garbage = garbageRootTrans.GetChild(i).gameObject.AddComponent<Garbage>();
                int randomIndex = UnityEngine.Random.Range(0, confLength);
                garbage.Conf = confList[randomIndex];
                _garbageArray[i] = garbage;
            }

            _rightBinEffect = transform.Find("Effects/RightBin").GetComponent<AnimationOnce>();
            _wrongBinEffect = transform.Find("Effects/WrongBin").GetComponent<AnimationOnce>();

            SceneFacade.ShowGarbageTriggerEffect += ShowGarbageTriggerEffect;
        }

        private void OnDestroy() {
            SceneFacade.ShowGarbageTriggerEffect -= ShowGarbageTriggerEffect;
        }

        private void ShowGarbageTriggerEffect(bool value, Vector3 position, Action animOverCallback) {
            AnimationOnce effect = value ? _rightBinEffect : _wrongBinEffect;
            effect.gameObject.SetActive(true);
            effect.transform.position = position;
            if (animOverCallback != null) {
                effect.animationOver += animOverCallback;
            }
        }
    }
}