using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project.GoblinMine.Game.Upgrade.Model;

namespace _Project.GoblinMine.Game.Upgrade.View
{
    public class UpgradeOptionView : MonoBehaviour
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Button buyButton;

        public UpgradeType UpgradeType => upgradeType;
        public Action<UpgradeType> OnBuyClicked;

        private void Awake()
        {
            if (buyButton == null)
                buyButton = GetComponentInChildren<Button>();

            if (nameText == null || costText == null || levelText == null)
            {
                var texts = GetComponentsInChildren<TextMeshProUGUI>();
                if (texts.Length >= 3)
                {
                    nameText = texts[0];
                    costText = texts[1];
                    levelText = texts[2];
                }
            }

            if (buyButton != null)
                buyButton.onClick.AddListener(() => OnBuyClicked?.Invoke(upgradeType));
        }

        public void SetName(string name)
        {
            if (nameText != null)
                nameText.text = name;
        }

        public void SetCost(int cost)
        {
            if (costText != null)
                costText.text = cost.ToString();
        }

        public void SetLevel(int level)
        {
            if (levelText != null)
                levelText.text = $"Lv.{level}";
        }

        public void SetInteractable(bool interactable)
        {
            if (buyButton != null)
                buyButton.interactable = interactable;
        }

        private void OnDestroy()
        {
            if (buyButton != null)
                buyButton.onClick.RemoveAllListeners();
        }
    }
}
