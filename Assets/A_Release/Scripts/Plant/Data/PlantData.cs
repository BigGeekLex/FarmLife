using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Plant", order = 1)]
public class PlantData : ScriptableObject
{
    public float grownSpeed;
    public PlantType type;
    public GameObject prefab;
}