using UnityEngine;
using Gameplay.Units.Monster;
using System.Collections.Generic;
using ScriptableObjects.Cannon;
using Gameplay.Units.Cannon.Abstract;
using UniRx;

namespace Gameplay.Units.Cannon
{
    public class CannonTower : AbstractTower<CannonProjectile, CannonProjectileDataConfig, CannonTowerDataConfig, CannonProjectile.Factory>
    {        
        private OnTriggerCollider _onTriggerCollider;

        [SerializeField] private Transform _shootPoint;
        private readonly List<MonsterMovement> _monstersInRange = new List<MonsterMovement>();

        private void Start()
        {
            _lastShotTime = _towerConfig.LastShotTime;

            _onTriggerCollider = GetComponent<OnTriggerCollider>();

            var rangeCollider = gameObject.GetComponent<SphereCollider>();
            rangeCollider.radius = _towerConfig.Range;

            _onTriggerCollider.AddMonster
                .Where(monster => monster != null)
                .Subscribe(monster =>
                {
                    AddMonsterToTheList(monster);
                })
                .AddTo(this);

            _onTriggerCollider.RemoveMonster
                .Where(monster => monster != null)
                .Subscribe(monster =>
                {
                    RemoveMonsterFromList(monster);
                })
                .AddTo(this);
        }

        private void Update()
        {
            foreach (var monster in _monstersInRange)
            {
                if (monster == null)
                    continue;

                var leadDirection = CalculatePredictedTargetPosition(
                    _shootPoint.position,
                    monster.transform.position,
                    monster.Direction,
                    monster.Speed,
                    _projectileConfig.Speed);

                Quaternion targetRotation = Quaternion.LookRotation(leadDirection - transform.position);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, 
                    targetRotation, 
                    _towerConfig.RotationSpeed * Time.deltaTime);


                if (_towerConfig.ProjectilePrefab == null || _shootPoint == null 
                    || Time.time < _lastShotTime + _towerConfig.ShootInterval)
                    return;

                Shoot();
                break;
            }
        }

        public Vector3 CalculatePredictedTargetPosition(
            Vector3 shooterPosition,
            Vector3 targetPosition,
            Vector3 targetDirection,
            float targetSpeed,
            float projectileSpeed)
        {
            Vector3 toTarget = targetPosition - shooterPosition;
            float distanceToTarget = toTarget.magnitude;

            float timeToImpact = distanceToTarget / projectileSpeed;

            Vector3 futureTargetPosition = targetPosition + targetDirection.normalized * targetSpeed * timeToImpact;

            futureTargetPosition.y = shooterPosition.y - 1.5f;

            return futureTargetPosition;
        }

        private void Shoot()
        {
            var projectile = GetProjectileFromPool();
            if (projectile == null) return;

            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;

            var rb = projectile.GetComponent<Rigidbody>();
            ResetProjectile(rb);

            projectile.gameObject.SetActive(true);

            _lastShotTime = Time.time;
        }

        private void ResetProjectile(Rigidbody projectileRigidbody)
        {
            projectileRigidbody.velocity = Vector3.zero;
            projectileRigidbody.angularVelocity = Vector3.zero;

            projectileRigidbody.rotation = Quaternion.identity;
        }

        private void AddMonsterToTheList(MonsterMovement monster)
        {
            if (monster != null && !_monstersInRange.Contains(monster))
            {
                _monstersInRange.Add(monster);
            }
        }

        private void RemoveMonsterFromList(MonsterMovement monster)
        {
            if (monster != null)
            {
                _monstersInRange.Remove(monster);
            }
        }
    }
}
