using UnityEngine;
using Gameplay.Units.Monster;
using ScriptableObjects.Simple;
using Zenject;
using Gameplay.Units.Cannon.Abstract;

namespace Gameplay.Units.Simple
{
    public class GuidedProjectile : AbstractProjectile<GuidedProjectileDataConfig>
    {
        public Transform Target;

        private void Update()
        {
            if (Target == null)
            {
                DestroyGameObject();
                return;
            }

            Vector3 direction = Target.transform.position - transform.position;
            transform.Translate(direction * _config.Speed * Time.deltaTime);
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

        public class Factory : PlaceholderFactory<GuidedProjectile> { }
    }
}
