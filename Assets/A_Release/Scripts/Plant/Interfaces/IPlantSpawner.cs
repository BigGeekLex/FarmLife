using System;
using UnityEngine;

public interface IPlantSpawner
{
    event Action<Vector3, IPlant> Spawned;
}