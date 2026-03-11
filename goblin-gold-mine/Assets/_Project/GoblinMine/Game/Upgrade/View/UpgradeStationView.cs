using System;
using UnityEngine;

namespace _Project.GoblinMine.Game.Upgrade.View
{
    public class UpgradeStationView : MonoBehaviour
    {
        public Action<Collider> OnTriggerEnterAction;
        public Action<Collider> OnTriggerExitAction;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitAction?.Invoke(other);
        }
    }
}
