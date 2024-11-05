using ScriptableObjects.Abstract;
using UnityEngine;
using Zenject;

namespace Gameplay.Units.Cannon.Abstract
{
    public abstract class AbstractProjectile<T> : MonoBehaviour
        where T : ProjectileDataConfig
    {
        protected T _config;

        [Inject]
        public virtual void Construct(T config) => 
            _config = config;
    }
}