using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.GoblinMine.Game.Player.View
{
    public class CarryCapacityView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI capacityText;
        [SerializeField] private Image fillBar;

        private void Awake()
        {
            if (capacityText == null)
                capacityText = GetComponentInChildren<TextMeshProUGUI>();

            if (fillBar == null)
            {
                var images = GetComponentsInChildren<Image>();
                foreach (var img in images)
                {
                    if (img.gameObject != gameObject && img.type == Image.Type.Filled)
                    {
                        fillBar = img;
                        break;
                    }
                }
            }
        }

        public void UpdateCapacity(int current, int max)
        {
            if (capacityText != null)
                capacityText.text = $"{current}/{max}";

            if (fillBar != null)
                fillBar.fillAmount = max > 0 ? (float)current / max : 0f;
        }
    }
}
