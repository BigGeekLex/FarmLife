using System;

namespace MPS.Gameplay.Rechargable.Logic
{
    public interface IRechargableArea
    {
        float GetMaxBorder();

        event Action OnRechargeStarted;
        
        event Action OnRechargeFinished;
        void SetDuration(float speed);
    }
}