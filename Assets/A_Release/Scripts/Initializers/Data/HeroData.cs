using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Hero", order = 1)]
public class HeroData : ScriptableObject
{
    [AssetsOnly] 
    public GameObject prefab;

    public MeshRenderer heroModel;
}