using Infrastructure;
using ScriptableObjects.Monster;
using UnityEngine;
using Zenject;

namespace Gameplay.Units.Monster.Installers
{
    public class MonsterInstaller : MonoInstaller
    {
        [SerializeField] private MonsterMovement _monsterPrefab;
        [SerializeField] private MonsterDataConfig _monsterDataConfig;

        public override void InstallBindings()
        {
            Container.BindIFactory<MonsterMovement>()
                .To<MonsterMovement>()
                .FromComponentInNewPrefab(_monsterPrefab);

            Container.BindInstance(_monsterDataConfig.MonsterSpeed)
                .WhenInjectedInto<MonsterMovement>();

            Container.BindInstance(_monsterDataConfig.MonsterMaxHP)
                .WhenInjectedInto<MonsterHp>();
        }
    }
}
