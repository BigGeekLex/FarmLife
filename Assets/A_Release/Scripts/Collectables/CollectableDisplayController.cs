using System;
using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;
using UniRx;
public class CollectableDisplayController : MonoBehaviour
{
    [SerializeField] 
    private PlantCollectableDisplayContainer displayData;

    [SerializeField] 
    private Transform parent;
    private IPlantCollectableController CollectableController => ServiceLocator.Get<IPlantCollectableController>();

    private List<CollectableDisplayPresenter> _presenters = new List<CollectableDisplayPresenter>();
    private void Start()
    {
        Initialize();
        CollectableController.GetCollectableDataObs().Subscribe((x) => RefreshDisplayInfo(x)).AddTo(this);
    }

    private void Initialize()
    {
        foreach (var data in displayData.Datas)
        {
            GameObject nextGo = PC.Spawn(displayData.prefab, Vector3.zero, Quaternion.identity, true, parent);
            CollectableDisplayPresenter presenter = nextGo.GetComponent<CollectableDisplayPresenter>();

            presenter.imageSource.sprite = data.icon;
            presenter.totalNumberText.text = 0.ToString();
            presenter.Initialize(data.plantType);
            
            _presenters.Add(presenter);
        }
    }
    private void RefreshDisplayInfo(PlantCollectableData data)
    {
        foreach (var d in data.Collectables)
        {
            foreach (var presenter in _presenters)
            {
                presenter.OnChanged(d.Key, d.Value);
            }      
        }
    }
}

[Serializable]
public struct PlantCollectableDisplayData
{
    public PlantType plantType;
    public Sprite icon;
}