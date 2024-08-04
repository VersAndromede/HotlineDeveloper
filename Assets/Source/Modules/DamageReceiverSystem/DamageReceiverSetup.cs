using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Modules.DamageReceiverSystem
{
    [RequireComponent(typeof(DamageReceiverView))]
    public class DamageReceiverSetup : MonoBehaviour
    {
        private DamageReceiverPresenter _damageReceiverPresenter;
        private DamageReceiverView _damageReceiverView;
        
        public void Initialize(DamageableConfig damageableConfig)
        {
            DamageReceiver damageReceiver = new(damageableConfig, this.GetCancellationTokenOnDestroy());
            _damageReceiverView = GetComponent<DamageReceiverView>();
            _damageReceiverPresenter = new DamageReceiverPresenter(damageReceiver, _damageReceiverView);
        }

        private void OnDestroy()
        {
            _damageReceiverPresenter?.Dispose();
        }
    }
}