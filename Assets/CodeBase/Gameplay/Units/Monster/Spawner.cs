using UnityEngine;
using System.Collections;
using System;
using UniRx;
using Zenject;
using System.Collections.Generic;
using Gameplay.Units.Simple;

namespace Gameplay.Units.Monster
{
    public class Spawner : MonoBehaviour
    {
        [Inject] private IFactory<MonsterMovement> _monsterFactory;

        [SerializeField] private Transform _moveTarget;
        [SerializeField] private float _spawnInterval = 3f;

        [SerializeField] private List<MonsterMovement> _allMonsters = new List<MonsterMovement>();
        private IDisposable _spawnSubscription;

        [Inject]
        public void Construct(SimpleTower simpleTower)
        {
            simpleTower.Construct(_allMonsters);
        }

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            _spawnSubscription = Observable
                .Interval(TimeSpan.FromSeconds(_spawnInterval))
                .Subscribe(_ => SpawnMonster())
                .AddTo(this);
        }

        private void SpawnMonster()
        {
            var monster = _monsterFactory.Create();
            monster.transform.position = transform.position;
            monster.SetMoveTarget(_moveTarget);

            _allMonsters.Add(monster);

            var monsterHp = monster.GetComponent<MonsterHp>();
            monsterHp.IsDead
                .Where(isDead => isDead)
                .Take(1)
                .Subscribe(_ =>
                {
                    _allMonsters.Remove(monster);
                })
                .AddTo(monster);
        }

        private void OnDestroy()
        {
            _spawnSubscription?.Dispose();
        }
    }
}
