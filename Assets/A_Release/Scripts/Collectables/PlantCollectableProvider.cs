using CorD.SparrowInterfaceField;
using UnityEngine;

[RequireComponent(typeof(PlantControllerBase))]
public class PlantCollectableProvider : MonoBehaviour
{
    [SerializeField] 
    private InterfaceField<IPlantCollectable> plantCollectableSource;
    private IPlantCollectable Collectable => plantCollectableSource.i;
    
    private IPlant _plant;
    private void Start()
    {
        if(Collectable == null) return;
        
        _plant = GetComponent<IPlant>();
        
        Collectable.Initialize(_plant);

        Collectable.Collected += OnCollected;
        _plant.GrownFinished += SendCollectRequest;
    }
    private void SendCollectRequest()
    {
        Collectable.ShowCollectView();
    }
    private void OnCollected(PlantType plantType, IPlantCollectable collectable)
    {
        _plant.ReleasePlant();
    }
    private void OnDestroy()
    {
        _plant.GrownFinished -= SendCollectRequest;
        Collectable.Collected -= OnCollected;
    }
}