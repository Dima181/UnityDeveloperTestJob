using UnityEngine;
using Gameplay.Units.Monster;
using UniRx;

namespace Gameplay.Units.Cannon
{
    [RequireComponent(typeof(SphereCollider))]
    public class OnTriggerCollider : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<MonsterMovement> AddMonster => _addMonster;
        public IReadOnlyReactiveProperty<MonsterMovement> RemoveMonster => _removeMonster;

        private readonly ReactiveProperty<MonsterMovement> _addMonster = new();
        private readonly ReactiveProperty<MonsterMovement> _removeMonster = new();

        private void OnTriggerEnter(Collider other) => 
            _addMonster.Value = other.GetComponent<MonsterMovement>();

        private void OnTriggerExit(Collider other) => 
            _removeMonster.Value = other.GetComponent<MonsterMovement>();
    }
}
