using System;

public interface IScoreProvidable
{ 
    void OnScoreCalculationRequest(float maxBorder, float vegetableGrownSpeed);
    IObservable<int> GetScoreObs();
}