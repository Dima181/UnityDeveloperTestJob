using ScriptableObjects.Abstract;
using UnityEngine;

namespace ScriptableObjects.Simple
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Simple/" + nameof(GuidedProjectileDataConfig))]
    public class GuidedProjectileDataConfig : ProjectileDataConfig
    {
        public float Speed => _speed;
        public int Damage => _damage;

        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private int _damage = 10;

    }
}
