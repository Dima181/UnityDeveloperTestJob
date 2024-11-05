using Gameplay.Units.Cannon;
using ScriptableObjects.Abstract;
using UnityEngine;
using Zenject;

namespace ScriptableObjects.Cannon
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Cannon/" + nameof(CannonTowerDataConfig))]
    public class CannonTowerDataConfig : TowerDataConfig
    {
        public float ShootInterval => _shootInterval;
        public float Range => _range;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float LastShotTime => _lastShotTime;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 8f;
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private float _lastShotTime = -0.5f;
        [SerializeField] private float _rotationSpeed = 200;

    }
}
