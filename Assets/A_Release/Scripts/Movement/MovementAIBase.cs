using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Farm.Core.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementAIBase : MonoBehaviour, IMovable
    {
        [SerializeField] 
        private UnityEvent onMove;
        
        private NavMeshAgent _agent;
        private Vector3 _target;

        private ReactiveProperty<float> _currentSpeed = new ReactiveProperty<float>();
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public event Action Moved;
        public event Action DestinationReached;
        public void SetNextTarget(Vector2Int targetPos)
        {
            _target = new Vector3(targetPos.x, 0.5f, targetPos.y);
            _agent.SetDestination(_target);
            
            IDisposable disposable =null;
            disposable = Observable.EveryEndOfFrame().Subscribe((x) => CheckDestinationReached(disposable));
        }

        public IObservable<float> GetSpeedObs()
        {
            return _currentSpeed;
        }

        private bool CheckDestinationReached(IDisposable disposable)
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        DestinationReached?.Invoke();
                        
                        _currentSpeed.SetValueAndForceNotify(_agent.velocity.sqrMagnitude);
                        disposable.Dispose();
                        return true;
                    }
                }
                
                _currentSpeed.SetValueAndForceNotify(_agent.velocity.sqrMagnitude);
                Moved?.Invoke();
                onMove?.Invoke();
            }
            return false;
        }
    }
}