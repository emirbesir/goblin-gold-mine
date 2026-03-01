using UnityEngine;
using Zenject;

namespace _Project._GameName.Project.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Bind project-level dependencies here
        }
        
        public override void Start()
        {
            base.Start();
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}