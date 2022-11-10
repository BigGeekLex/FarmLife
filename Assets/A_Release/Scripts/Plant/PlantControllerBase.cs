using System;
using MPS.Gameplay.Rechargable.Logic;
using UnityEngine;
using UnityEngine.Events;


public class PlantControllerBase : MonoBehaviour, IPlant
{
    [SerializeField]
    private CorD.SparrowInterfaceField.InterfaceField<IRechargableArea> grownRechargableSource;

    [SerializeField] 
    private UnityEvent GrownStartedEvent;
    [SerializeField] 
    private UnityEvent GrownFinishedEvent;
    private IRechargableArea GrowRechargableArea => grownRechargableSource.i;

    private PlantType _plantType;
    private float _grownSpeed;
    private IGrid _placedGrid;
    
    public event Action GrownFinished;
    public event Action GrownStarted;

    private void Awake()
    {
        GrowRechargableArea.OnRechargeFinished += OnGrownFinished;
        GrowRechargableArea.OnRechargeStarted += OnGrownStarted;
    }
    private void OnDestroy()
    {
        GrowRechargableArea.OnRechargeFinished -= OnGrownFinished;
        GrowRechargableArea.OnRechargeStarted -= OnGrownStarted;
    }
    private void OnGrownFinished()
    {
        GrownFinished?.Invoke();
        GrownFinishedEvent.Invoke();
    }

    private void OnGrownStarted()
    {
        GrownStarted?.Invoke();
        GrownStartedEvent.Invoke();
    }
    public void Initialize(PlantType type, float grownSpeed, IGrid placedGrid)
    {
        _plantType = type;
        _grownSpeed = grownSpeed;
        _placedGrid = placedGrid;
        
        GrowRechargableArea.SetDuration(_grownSpeed);
    }

    public void ReleasePlant()
    {
        _placedGrid.ReleaseGrid();
        PC.Despawn(this.gameObject);
    }

    public PlantType GetPlantType()
    {
        return _plantType;
    }

    public IGrid GetPlacedGrid()
    {
        return _placedGrid;
    }
    public float GetGrownSpeed(out float maxBorder)
    {
        maxBorder = GrowRechargableArea.GetMaxBorder();
        return _grownSpeed;
    }
}