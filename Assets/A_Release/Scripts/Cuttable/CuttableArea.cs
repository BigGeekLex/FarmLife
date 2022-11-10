using System;
using CorD.SparrowInterfaceField;
using Farm.Core.Hero;
using MSFD;
using UnityEngine;


public class CuttableArea : AreaBase, IPlantCuttable
{
    [SerializeField] 
    private InterfaceField<IActionButtonProvidable> buttonProvidable;
    private IHero Hero => ServiceLocator.Get<IHero>();
    private IPlantCuttableController CuttableController => ServiceLocator.Get<IPlantCuttableController>();
    
    private IPlant _plant;
    private void Start()
    {
        base.OnStart();
    }

    private void OnDestroy()
    {
        OnDest();
    }
    
    private void OnCuttedButtonClicked()
    {
        if (Hero.GetStatus() == HeroStatus.Free)
        {
            Activatable.ChangeActivatableStatus(true);
            
            CuttableController?.SendCutRequest(_plant.GetPlacedGrid().GetPosition());
            
            buttonProvidable.i.GetButton().onClick.RemoveListener(OnCuttedButtonClicked);
            buttonProvidable.i.Deactivate();
        }
    }
    protected override void OnActivated(GameObject sender)
    {
        Cutted?.Invoke();
        OnDeactivated(Activatable);
    }
    protected override void OnDeactivated(IActivatable activatable)
    {
        Activatable.ChangeActivatableStatus(false);
    }

    public void Initialize(IPlant plant)
    {
        _plant = plant;
    }

    public void ShowCuttableView()
    {
        buttonProvidable.i.Activate();
        buttonProvidable.i.GetButton().onClick.AddListener(OnCuttedButtonClicked);
    }

    public event Action Cutted;
}