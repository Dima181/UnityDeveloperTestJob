using ScriptableObjects.Abstract;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.Units.Cannon.Abstract
{
    public abstract class AbstractTower<TProjectile, TProjectileConfig, TTowerConfig, TFactory> : MonoBehaviour
        where TProjectile : AbstractProjectile<TProjectileConfig>
        where TProjectileConfig : ProjectileDataConfig
        where TTowerConfig : TowerDataConfig
        where TFactory : IFactory<TProjectile>
    {
        protected List<TProjectile> _projectilePool = new List<TProjectile>();
        protected const int PoolSize = 2;

        protected float _lastShotTime;

        protected TProjectileConfig _projectileConfig;
        protected TTowerConfig _towerConfig;

        private TFactory _factory;

        [Inject]
        public virtual void Construct(TProjectileConfig projectileConfig, TTowerConfig towerConfig, TFactory factory)
        {
            _projectileConfig = projectileConfig;
            _towerConfig = towerConfig;
            _factory = factory;
            AddProjectilePool();
        }

        private void AddProjectilePool()
        {
            for (int i = 0; i < PoolSize; i++)
            {
                var projectile = _factory.Create();
                projectile.gameObject.SetActive(false);
                _projectilePool.Add(projectile);
            }
        }

        protected TProjectile GetProjectileFromPool()
        {
            foreach (var projectile in _projectilePool)
            {
                if (!projectile.gameObject.activeInHierarchy)
                {
                    return projectile;
                }
            }

            var newProjectile = _factory.Create();
            _projectilePool.Add(newProjectile);
            return newProjectile;
        }
    }
}
