using UnityEngine;

namespace _Project.GoblinMine.Game.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        public CharacterController CharacterController => characterController;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}