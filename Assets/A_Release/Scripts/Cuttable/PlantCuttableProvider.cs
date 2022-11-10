using CorD.SparrowInterfaceField;
using UnityEngine;

[RequireComponent(typeof(PlantControllerBase))]
public class PlantCuttableProvider : MonoBehaviour
{
    [SerializeField] 
    private InterfaceField<IPlantCuttable> plantCuttableSource;
    private IPlantCuttable Cuttable => plantCuttableSource.i;
    
    private IPlant _plant;
    private void Start()
    {
        if(Cuttable == null) return;
        
        _plant = GetComponent<IPlant>();
        
        Cuttable.Initialize(_plant);
        Cuttable.Cutted += OnCutted;
        _plant.GrownFinished += SendCutRequest;
    }
    private void SendCutRequest()
    {
        Cuttable.ShowCuttableView();
    }
    private void OnCutted()
    {
        _plant.ReleasePlant();
    }
    private void OnDestroy()
    {
        Cuttable.Cutted -= OnCutted;
        _plant.GrownFinished -= SendCutRequest;
    }
}