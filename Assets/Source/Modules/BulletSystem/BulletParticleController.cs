using System.Collections;
using Modules.BulletSystem;
using UnityEngine;

namespace Source.Modules.EffectsSystem
{
    public class BulletParticleController : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        private Coroutine _coroutine;
        private ParticleSystem.MinMaxCurve _trailWidth;

        public void DeactivateParticle()
        {
            _trailRenderer.Clear();
            _trailRenderer.enabled = false;
        }

        public void ActivateParticle()
        {
            _trailRenderer.Clear();
            _trailRenderer.enabled = true;
        }
    }
}