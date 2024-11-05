using UnityEngine;
using System.Collections;
using Gameplay.Units.Monster;
using ScriptableObjects.Cannon;
using Zenject;
using Gameplay.Units.Cannon.Abstract;

namespace Gameplay.Units.Cannon
{
    public class CannonProjectile : AbstractProjectile<CannonProjectileDataConfig>
    {
        private void Update()
        {
            transform.Translate(transform.forward * _config.Speed * Time.deltaTime, Space.World);
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_config.TimeToDestroy);

            DestroyGameObject();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MonsterHp monster))
            {
                HandleCollisionWithMonster(monster);
                DestroyGameObject();
            }
        }

        private void DestroyGameObject() =>
            ReturnToPool();

        private void HandleCollisionWithMonster(MonsterHp monster) =>
            monster.TakeDamage(_config.Damage);

        private void ReturnToPool() => 
            gameObject.SetActive(false);

        public class Factory : PlaceholderFactory<CannonProjectile> { }
    }
}
