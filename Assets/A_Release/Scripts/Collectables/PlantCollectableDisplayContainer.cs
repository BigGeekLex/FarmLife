using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantCollectableContainer", menuName = "Data/PlantCollectableContainer", order = 1)]
public class PlantCollectableDisplayContainer : ScriptableObject
{
    public GameObject prefab;
    public List<PlantCollectableDisplayData> Datas;
}