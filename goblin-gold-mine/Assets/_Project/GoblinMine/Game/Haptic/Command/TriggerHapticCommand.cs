using UnityEngine;

namespace _Project.GoblinMine.Game.Haptic.Command
{
    public class TriggerHapticCommand
    {
        public void Execute()
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}
