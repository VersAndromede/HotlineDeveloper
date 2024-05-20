using System;
using System.Threading;
using Modules.DamageSystem.DamageStrategy;
using Modules.DamageSystem;

namespace Modules.DamageSystem
{
    public class DamageReceiver
    {
        private Health _health;
        private Consciousness _consciousness;
        private IDamageReceiveStrategy _damageReceiveStrategy;

        public event Action<float> HealthChanged;
        public event Action Died;
        public event Action Knocked;
        public event Action Recovered; 

        public DamageReceiver(DamageableConfig damageableConfig, CancellationToken cancellationToken)
        {
            _health = new Health(damageableConfig.MaxValue);
            _consciousness = new Consciousness(damageableConfig.RecoverTime, cancellationToken);
            _damageReceiveStrategy = damageableConfig.DamageReceiveStrategy;
        }

        public void Receive(DamageData damage)
        {
            DamageData modifiedDamage = _damageReceiveStrategy.GetDamage(damage);
            
            if (modifiedDamage.IsLethal && _consciousness.IsKnocked)
                _health.Execute(HealthChanged,Died);
            
            if(modifiedDamage.IsKnockout && _consciousness.IsKnocked == false)
                _consciousness.Knockout(Knocked, Recovered);
            
            _health.TakeDamage(modifiedDamage.Value, HealthChanged,Died);
        }
    }
}