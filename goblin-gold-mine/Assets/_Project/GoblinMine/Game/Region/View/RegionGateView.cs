using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.GoblinMine.Game.Region.View
{
    public class RegionGateView : MonoBehaviour
    {
        [SerializeField] private int regionIndex;

        public int RegionIndex => regionIndex;

        public Action<Collider> OnTriggerEnterAction;

        private void Awake()
        {
            var collider = GetComponent<BoxCollider>();
            if (collider != null)
                collider.isTrigger = false;
        }

        public void SetLocked(bool locked)
        {
            gameObject.SetActive(locked);
        }

        public void PlayUnlockEffect()
        {
            transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterAction?.Invoke(other);
        }
    }
}
