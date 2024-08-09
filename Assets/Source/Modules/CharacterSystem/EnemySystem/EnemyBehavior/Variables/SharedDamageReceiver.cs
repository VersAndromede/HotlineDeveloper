using BehaviorDesigner.Runtime;

namespace Modules.DamagerSystem.Enemies.EnemyBehavior.Variables
{
    public class SharedDamageReceiver : SharedVariable<DamageReceiverView>
    {
        public static implicit operator SharedDamageReceiver(DamageReceiverView value)
        {
            return new SharedDamageReceiver { Value = value };
        }
    }
}