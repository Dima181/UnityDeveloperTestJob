using Gameplay.Units.Cannon;
using UnityEngine;
using Zenject;

namespace ScriptableObjects.Cannon.Installers
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Cannon/Installer/" + nameof(CannonDataInstaller))]
    public class CannonDataInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CannonProjectileDataConfig _cannonProjectileDataConfig;
        [SerializeField] private CannonTowerDataConfig _cannonTowerDataConfig;

        public override void InstallBindings()
        {
            Container.Bind<CannonProjectileDataConfig>()
                .FromInstance(_cannonProjectileDataConfig)
                .AsSingle();

            Container.Bind<CannonTowerDataConfig>()
                .FromInstance(_cannonTowerDataConfig)
                .AsSingle();

            Container.BindFactory<CannonProjectile, CannonProjectile.Factory>()
                .FromComponentInNewPrefab(_cannonTowerDataConfig.ProjectilePrefab);
        }
    }
}
