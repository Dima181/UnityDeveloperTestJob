using UnityEngine;
using Gameplay.Units.Monster;
using System.Collections.Generic;
using Zenject;
using ScriptableObjects.Simple;
using Gameplay.Units.Cannon.Abstract;

namespace Gameplay.Units.Simple
{
    public class SimpleTower : AbstractTower<GuidedProjectile, GuidedProjectileDataConfig, SimpleTowerDataConfig, GuidedProjectile.Factory>
    {
        [SerializeField] private List<MonsterMovement> _monstersInRange = new List<MonsterMovement>();

        [Inject]
        public void Construct(List<MonsterMovement> allMonsters) => 
            _monstersInRange = allMonsters;

        private void Start() => 
            _lastShotTime = _towerConfig.LastShotTime;

        private void Update()
        {
            if (_towerConfig.ProjectilePrefab == null || _monstersInRange.Count == 0)
                return;

            foreach (var monster in _monstersInRange)
            {
                if (monster == null || Vector3.Distance(transform.position, monster.transform.position) > _towerConfig.Range)
                    continue;

                if (_lastShotTime + _towerConfig.ShootInterval > Time.time)
                    continue;

                Shoot(monster);
                _lastShotTime = Time.time;
                break;
            }
        }

        private void Shoot(MonsterMovement target)
        {
            var projectileInstance = GetProjectileFromPool();
            if (projectileInstance == null) return;

            projectileInstance.transform.position = gameObject.transform.position + Vector3.up * 1.5f;
            projectileInstance.transform.rotation = gameObject.transform.rotation;

            projectileInstance.gameObject.SetActive(true);

            if (projectileInstance.TryGetComponent(out GuidedProjectile projectile))
            {
                projectile.Target = target.transform;
            }
        }
    }
}
