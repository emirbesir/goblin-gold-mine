using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.GoblinMine.Game.Inventory.View
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private TextMeshProUGUI textComponent;

        public Guid Id { get; set; }

        public void SetSprite(Sprite sprite)
        {
            imageComponent.sprite = sprite;
        }

        public void SetText(string text)
        {
            textComponent.text = text;
        }

        public class Factory : PlaceholderFactory<ResourceView>
        {
        }
    }
}