using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Modules.Characters.Enemies.EnemyBehavior.Variables;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Characters.Enemies.EnemyBehavior.Actions
{
    [TaskCategory("CustomTask")]
    [TaskName("EnemyPatrol")]
    public class EnemyPatrol : Action
    {
        public SharedVector3Array _patrolPoints;

        private NavMeshAgent _navMeshAgent;
        private Queue<Vector3> _route;

        public override void OnAwake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _route = new Queue<Vector3>(_patrolPoints.Value);
        }

        public override void OnStart()
        {
            SetDestination();
        }

        public override TaskStatus OnUpdate()
        {
            if (_navMeshAgent == null)
                return TaskStatus.Failure;
            
            if (_navMeshAgent.remainingDistance <= 0)
                SetDestination();

            return TaskStatus.Running;
        }

        public override void OnConditionalAbort()
        {
            _navMeshAgent.isStopped = true;
        }

        private void SetDestination()
        {
            Vector3 nextPoint = _route.Dequeue();
            _route.Enqueue(nextPoint);
            _navMeshAgent.SetDestination(nextPoint);
            _navMeshAgent.isStopped = false;
        }
    }
}