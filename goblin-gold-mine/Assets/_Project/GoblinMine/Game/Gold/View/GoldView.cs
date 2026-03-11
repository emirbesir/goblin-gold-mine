using TMPro;
using UnityEngine;

namespace _Project.GoblinMine.Game.Gold.View
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        private void Awake()
        {
            if (goldText == null)
                goldText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetGoldAmount(int amount)
        {
            goldText.text = amount.ToString();
        }
    }
}
