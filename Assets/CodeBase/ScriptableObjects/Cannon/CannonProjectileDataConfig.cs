using ScriptableObjects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScriptableObjects.Cannon
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Cannon/" + nameof(CannonProjectileDataConfig))]
    public class CannonProjectileDataConfig : ProjectileDataConfig
    {
        public float Speed => _speed;
        public int Damage => _damage;
        public int TimeToDestroy => _timeToDestroy;

        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private int _damage = 10;
        [SerializeField] private int _timeToDestroy = 2;

    }
}
