using System;
using MSFD;
using UniRx;
public class ScoreController : IScoreProvidable
{
    private float _defaultScoreFactor = 2.0f;
    
    private ReactiveProperty<int> _scoreProperty = new ReactiveProperty<int>();
    
    public void OnScoreCalculationRequest(float maxBorder, float vegetableGrownSpeed)
    {
        int calculatedScore = CalculateScore(maxBorder, vegetableGrownSpeed);
        
        _scoreProperty.SetValueAndForceNotify(_scoreProperty.Value + calculatedScore);
    }
    public IObservable<int> GetScoreObs()
    {
        return _scoreProperty;
    }
    private int CalculateScore(float maxBorder, float vegetableGrownSpeed)
    {
        float calculatedGrownTime = maxBorder/(-vegetableGrownSpeed);

        return (int) _defaultScoreFactor * (int) calculatedGrownTime;
    }
}