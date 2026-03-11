using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Upgrade.Model;
using UnityEngine;

namespace _Project.GoblinMine.Game.Upgrade.View
{
    public class UpgradePanelView : MonoBehaviour
    {
        [SerializeField] private List<UpgradeOptionView> upgradeOptionViews;

        public List<UpgradeOptionView> UpgradeOptionViews => upgradeOptionViews;

        private void Awake()
        {
            if (upgradeOptionViews == null || upgradeOptionViews.Count == 0)
                upgradeOptionViews = GetComponentsInChildren<UpgradeOptionView>(true).ToList();
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public UpgradeOptionView GetOptionByType(UpgradeType type)
        {
            foreach (var option in upgradeOptionViews)
            {
                if (option.UpgradeType == type)
                    return option;
            }

            return null;
        }
    }
}
