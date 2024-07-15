using DG.Tweening;
using UnityEngine;

namespace Source.Modules.GUISystem
{
    public class UILoopScaler : GUITransformAnimator
    {
        [SerializeField] private float _scale = 1f;
        [SerializeField] private float _duration = 9f;
        
        private Vector3 _rotation; 
    
        protected override void Animate()
        {
            Transform.DOScale(_scale,_duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}