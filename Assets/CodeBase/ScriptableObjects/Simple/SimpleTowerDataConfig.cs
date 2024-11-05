using ScriptableObjects.Abstract;
using UnityEngine;

namespace ScriptableObjects.Simple
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Simple/" + nameof(SimpleTowerDataConfig))]
    public class SimpleTowerDataConfig : TowerDataConfig
    {
        public float ShootInterval => _shootInterval;
        public float Range => _range;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float LastShotTime => _lastShotTime;

        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private float _lastShotTime = -0.5f;

    }
}
