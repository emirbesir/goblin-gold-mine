using System;
using _Project.GoblinMine.Game.Inventory.Model;
using _Project.GoblinMine.Game.Worker.Configuration;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Worker.View
{
    public class WorkerView : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private GameObject sleepEffects;

        public class Factory : PlaceholderFactory<WorkerView>
        {
        }

        public ResourceType ResourceType => resourceType;
        public Guid Id { get; set; }

        public void SetResourceType(ResourceType type)
        {
            resourceType = type;
        }

        public Action<Collider> OnTriggerStayAction;
        public Action<Collider> OnTriggerExitAction;

        private Tween _sleepBobTween;
        private float _originalY;

        public void Initialize()
        {
            _originalY = transform.position.y;
        }

        public void SetSleeping(bool sleeping, WorkerVisualConfiguration config)
        {
            sleepEffects.SetActive(sleeping);

            if (sleeping)
            {
                _sleepBobTween = transform.DOMoveY(
                        _originalY + config.SleepBobAmplitudeUnits,
                        config.SleepBobDurationSeconds)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                _sleepBobTween?.Kill();
                _sleepBobTween = null;

                var position = transform.position;
                position.y = _originalY;
                transform.position = position;
            }
        }

        public void PlayWakeUpEffect(WorkerVisualConfiguration config)
        {
            transform.DOPunchScale(
                punch: config.WakeUpPunchScale,
                duration: config.WakeUpPunchDurationSeconds,
                vibrato: config.WakeUpPunchVibrato,
                elasticity: config.WakeUpPunchElasticity);
        }

        public void PlayCollectionPulse(WorkerVisualConfiguration config)
        {
            transform.DOPunchScale(
                punch: config.CollectionPunchScale,
                duration: config.CollectionPunchDurationSeconds,
                vibrato: config.CollectionPunchVibrato,
                elasticity: config.CollectionPunchElasticity);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitAction?.Invoke(other);
        }

        private void OnDestroy()
        {
            _sleepBobTween?.Kill();
        }
    }
}
