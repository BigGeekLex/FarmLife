using MSFD;
using UnityEngine;

[RequireComponent(typeof(PlantControllerBase))]
public class PlantScoreProvider : MonoBehaviour
{
    private IPlant _plant;
    private IScoreProvidable ScoreProvidable => ServiceLocator.Get<IScoreProvidable>();
    
    private void Start()
    {
        _plant = GetComponent<IPlant>();
        _plant.GrownFinished += OnGrownFinishedScoreRequest;
    }
    private void OnDestroy()
    {
        _plant.GrownFinished -= OnGrownFinishedScoreRequest;
    }
    private void OnGrownFinishedScoreRequest()
    {
        float maxBorder = 0f;
        float grownSpeed = _plant.GetGrownSpeed(out maxBorder);
        
        ScoreProvidable?.OnScoreCalculationRequest(maxBorder,grownSpeed);
    }
}