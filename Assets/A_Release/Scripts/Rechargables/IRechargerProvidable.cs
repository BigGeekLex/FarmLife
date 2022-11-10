using System;

namespace MPS.Recharhable.Base
{
    public interface IRechargerProvidable
    {
        public float GetMaxBorder();
        public event Action Finished;
        public event Action Started;
        public event Action<float,float> Changed;
        public bool TryToStartRechargeProcess(float speed);
        public void StopRechargeProcess();
    }
}