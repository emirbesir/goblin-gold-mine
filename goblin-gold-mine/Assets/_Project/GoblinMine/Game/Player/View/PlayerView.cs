using UnityEngine;

namespace _Project.GoblinMine.Game.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}