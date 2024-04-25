using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.InputSystem;
using UnityEngine;

namespace Modules.Characters.Enemies.EnemyBehavior.Actions
{
    [TaskCategory("CustomTask")]
    [TaskName("Shoot")]
    public class Shoot : Action
    {
        private AiInput _input;
        
        public override TaskStatus OnUpdate()
        {
            _input.ReceiveAttack();
            return TaskStatus.Running;
        }
    }
}