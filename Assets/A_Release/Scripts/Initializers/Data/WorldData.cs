using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/World", order = 1)]
public class WorldData : ScriptableObject
{
    public int widht;
    public int lenght;
    public int yOffset;
    
    [AssetsOnly] 
    public GameObject spawnedBlock;
}