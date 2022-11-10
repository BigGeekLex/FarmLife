using DG.Tweening;
using MSFD;
using UnityEngine;

public class CameraZoomToTarget : MonoBehaviour
{
    [SerializeField] 
    private float defaultDuration = 1.25f;
    [SerializeField]
    private float zoomFactor = 2f;
    
    private float _initialOrtoSize;
    private IPlantSpawner Spawner => ServiceLocator.Get<IPlantSpawner>();

    private Quaternion _initial;
    private Camera Camera => Camera.main;
    
    private IPlant _currentServicedPlant;
    private void Start()
    {
        _initial = Camera.transform.rotation;
        _initialOrtoSize = Camera.orthographicSize;
       
        Spawner.Spawned += OnNextPlantSpawn;
    }
    
    private void OnNextPlantSpawn(Vector3 target, IPlant spawnedPlant)
    {
        _currentServicedPlant = spawnedPlant;

        _currentServicedPlant.GrownStarted += ZoomOut;
       
        ZoomIn(target);
    }

    private void ZoomIn(Vector3 target)
    {
        float targetZoom = Camera.orthographicSize / zoomFactor;

        Camera.transform.DORotateQuaternion(Quaternion.LookRotation(target - Camera.transform.position, Vector3.up), defaultDuration);
        Camera.DOOrthoSize(targetZoom, defaultDuration);
    }

    private void ZoomOut()
    {
        _currentServicedPlant.GrownStarted -= ZoomOut;
        Camera.transform.DORotateQuaternion(_initial, defaultDuration);
        Camera.DOOrthoSize(_initialOrtoSize, defaultDuration);
    }
}
