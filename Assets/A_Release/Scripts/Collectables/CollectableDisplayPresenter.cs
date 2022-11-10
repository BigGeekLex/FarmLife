using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableDisplayPresenter : MonoBehaviour
{
    public TextMeshProUGUI totalNumberText;
    public Image imageSource;

    private PlantType _plantType;
    
    public PlantType GetPlantType()
    {
        return _plantType;
    }
    
    public void Initialize(PlantType type)
    {
        _plantType = type;
    }

    public void OnChanged(PlantType type, int number)
    {
        if (_plantType == type)
        {
            totalNumberText.text = number.ToString();
        }
    }
}