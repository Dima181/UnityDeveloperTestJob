using Gameplay.Units.Simple;
using UnityEngine;
using Zenject;

namespace ScriptableObjects.Simple.Installers
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Simple/Installer/" + nameof(SimpleDataInstaller))]
    public class SimpleDataInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GuidedProjectileDataConfig _guidedProjectileDataConfig;
        [SerializeField] private SimpleTowerDataConfig _simpleTowerDataConfig;

        public override void InstallBindings()
        {
            Container.Bind<GuidedProjectileDataConfig>()
                .FromInstance(_guidedProjectileDataConfig)
                .AsSingle();

            Container.Bind<SimpleTowerDataConfig>()
                .FromInstance(_simpleTowerDataConfig)
                .AsSingle();

            Container.Bind<SimpleTower>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindFactory<GuidedProjectile, GuidedProjectile.Factory>()
                .FromComponentInNewPrefab(_simpleTowerDataConfig.ProjectilePrefab);
        }
    }
}
