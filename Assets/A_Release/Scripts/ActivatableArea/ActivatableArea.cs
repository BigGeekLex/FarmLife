using System;
using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using UnityEngine;


public class ActivatableArea : MonoBehaviour, IActivatable
{
    public event Action<GameObject> OnActivated;
    public event Action<IActivatable> OnDeactivated;
    
    private bool _activationStatus;

    private bool _isActivationAllowed;

    private void OnEnable()
    {
        _activationStatus = false;
    }

    public void ChangeActivatableStatus(bool value)
    {
        _isActivationAllowed = value;
    }
    
    public bool TryActivate(GameObject sender)
    {
        if (_isActivationAllowed)
        {
            if (!_activationStatus)
            {
                _activationStatus = true;
            
                OnActivated?.Invoke(sender);
                return true;
            }
        }
        return false;   
    }

    [Button]
    private void ManualActivate()
    {
        TryActivate(null);
    }
    
    public void Deactivate()
    {
        if (_activationStatus)
        {
            _activationStatus = false;
            
            OnDeactivated?.Invoke(this);
        }
    }
}