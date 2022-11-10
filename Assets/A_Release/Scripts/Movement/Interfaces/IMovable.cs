using System;
using UnityEngine;

namespace Farm.Core.Movement
{
    public interface IMovable
    {
        public event Action Moved;
        
        public event Action DestinationReached;
        public void SetNextTarget(Vector2Int targetPos);

        public IObservable<float> GetSpeedObs();
    }
}