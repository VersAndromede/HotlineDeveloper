using System;
using System.Collections;
using Source.Modules.EffectsSystem;
using UnityEngine;

namespace Modules.BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _lifetime;
        [SerializeField] private BulletParticleController _particleController;
        [field: SerializeField] public Collider Collider { get; private set; }

        private WaitForSeconds _waitlifetime;
        private Coroutine _coroutine;
        
        public event Action<Bullet> LifespanStarted;
        public event Action<Bullet> LifespanEnded;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _particleController.DeactivateParticle();
            
            LifespanEnded?.Invoke(this);
        }

        public void Init(Vector3 direction, float speed)
        {
            _waitlifetime = new WaitForSeconds(_lifetime);
            _coroutine = StartCoroutine(StartLifetime());
            _rigidbody.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            _rigidbody.velocity = direction * speed;
        }

        public void SetPosition(Vector3 position)
        {
            _rigidbody.position = position;
            _particleController.ActivateParticle();
            LifespanStarted?.Invoke(this);
        }

        public void SetInterpolation(RigidbodyInterpolation interpolation)
        {
            _rigidbody.interpolation = interpolation;
        }

        private IEnumerator StartLifetime()
        {
            yield return _waitlifetime;
            LifespanEnded?.Invoke(this);
        }
    }
}