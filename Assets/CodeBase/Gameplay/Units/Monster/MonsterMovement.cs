using UnityEngine;
using System.Collections;
using Zenject;
using System;
using UniRx;

namespace Gameplay.Units.Monster
{
    [RequireComponent(typeof(Rigidbody))]
    public class MonsterMovement : MonoBehaviour
    {
        public float Speed => _speed;
        public Vector3 Direction => _direction;

        private const float ReachDistance = 1f;
        private Transform _moveTarget;
        private float _speed;

        private Rigidbody _rigidbody;

        private CompositeDisposable _disposables = new CompositeDisposable();

        private Vector3 _direction;

        private void Start() => 
            _rigidbody = GetComponent<Rigidbody>();

        [Inject]
        public void Construct(float speed) =>
            _speed = speed;

        public void SetMoveTarget(Transform moveTarget)
        {
            _moveTarget = moveTarget;

            ObserveDistanceToTarget()
                .Where (distance => distance > ReachDistance)
                .Subscribe(_ => MoveTowardsTarget())
                .AddTo(this);

            ObserveDistanceToTarget()
                .Where(distance => distance <= ReachDistance)
                .Take(1)
                .Subscribe(_ => MonsterDie())
                .AddTo(this);

        }

        private IObservable<float> ObserveDistanceToTarget()
        {
            return Observable
                .EveryUpdate()
                .Where(_ => _moveTarget != null)
                .Select(_ => Vector3.Distance(transform.position, _moveTarget.position));
        }

        private void MoveTowardsTarget()
        {
            _direction = (_moveTarget.position - transform.position);

            var translation = _direction.normalized * Speed;
            transform.Translate(translation * Time.deltaTime);
        }

        private void MonsterDie()
        {
            var monsterHp = GetComponent<MonsterHp>();
            if(monsterHp == null) return;
            monsterHp.Die();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public float GetSpeed() =>
            Speed;

        public Vector3 GetVelocity() =>
            _rigidbody != null ? _rigidbody.velocity : Vector3.zero;

        public class Factory : PlaceholderFactory<MonsterMovement> { }
    }
}