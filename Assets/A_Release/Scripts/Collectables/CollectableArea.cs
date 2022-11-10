using System;
using CorD.SparrowInterfaceField;
using Farm.Core.Hero;
using MSFD;
using UnityEngine;

public class CollectableArea : AreaBase, IPlantCollectable
{
    [SerializeField] 
    private InterfaceField<IActionButtonProvidable> buttonSource;
    private IPlantCollectableController PlantCollectableController => ServiceLocator.Get<IPlantCollectableController>();
    private IHero Hero => ServiceLocator.Get<IHero>();
    
    private IPlant _plant;
    private void Start()
    {
        base.OnStart();
    }

    private void OnDestroy()
    {
        OnDest();
    }

    public void Initialize(IPlant plant)
    {
        _plant = plant;
    }

    public void ShowCollectView()
    {
        buttonSource.i.Activate();
        buttonSource.i.GetButton().onClick.AddListener(OnCollectedButtonClicked);
    }
    
    public event Action<PlantType, IPlantCollectable> Collected;

    private void OnCollectedButtonClicked()
    {
        if (Hero.GetStatus() == HeroStatus.Free)
        {
            Activatable.ChangeActivatableStatus(true);
            PlantCollectableController?.OnCollectingRequest(_plant.GetPlacedGrid().GetPosition(), this);
            
            buttonSource.i.GetButton().onClick.RemoveListener(OnCollectedButtonClicked);
            buttonSource.i.Deactivate();
        }
    }
    protected override void OnActivated(GameObject sender)
    {
        Collected?.Invoke(_plant.GetPlantType(), this);
        
        OnDeactivated(Activatable);
    }
    
    protected override void OnDeactivated(IActivatable activatable)
    {
        Activatable.ChangeActivatableStatus(false);
    }
}