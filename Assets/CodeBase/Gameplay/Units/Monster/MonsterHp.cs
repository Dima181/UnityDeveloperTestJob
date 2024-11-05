using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Units.Monster
{
    public class MonsterHp : MonoBehaviour
    {
        [SerializeField][Utils.ReadOnlyField.ReadOnly] private int _currentHp;

        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>(false);

        [Inject]
        public void Construct(int maxHp) =>
            _currentHp = maxHp;

        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _isDead.Value = true;
            Destroy(gameObject);
        }
    }
}