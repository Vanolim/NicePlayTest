using UnityEngine;
using Views;
using Zenject;

namespace Core.Installer
{
    /// <summary>
    /// Zenject installer core objects and HUB
    /// </summary>
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField]
        private HUB _hub;
        
        public override void InstallBindings()
        {
            Container.Bind<HUB>().FromComponentInNewPrefab(_hub).AsSingle().NonLazy();
            Container.Bind<CoroutineService>().FromInstance(new CoroutineService(target: this)).AsSingle().Lazy();
            Container.Bind<SaveLoadService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RestartService>().AsSingle();
        }
    }
}