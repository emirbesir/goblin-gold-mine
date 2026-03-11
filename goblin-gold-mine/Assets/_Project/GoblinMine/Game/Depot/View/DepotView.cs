using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.GoblinMine.Game.Depot.View
{
    public class DepotView : MonoBehaviour
    {
        [SerializeField] private float depositPunchScale = 0.3f;
        [SerializeField] private float depositPunchDuration = 0.4f;

        public Action<Collider> OnTriggerStayAction;
        public Action<Collider> OnTriggerExitAction;

        public void PlayDepositEffect()
        {
            transform.DOPunchScale(
                Vector3.one * depositPunchScale,
                depositPunchDuration,
                10,
                1f);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitAction?.Invoke(other);
        }
    }
}
