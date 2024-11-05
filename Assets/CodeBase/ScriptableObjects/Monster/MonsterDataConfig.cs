using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScriptableObjects.Monster
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Monster/" + nameof(MonsterDataConfig))]
    public class MonsterDataConfig : ScriptableObject
    {
        public float MonsterSpeed => _monsterSpeed;
        public int MonsterMaxHP => _monsterMaxHP;

        [SerializeField] private float _monsterSpeed;
        [SerializeField] private int _monsterMaxHP;

    }
}
