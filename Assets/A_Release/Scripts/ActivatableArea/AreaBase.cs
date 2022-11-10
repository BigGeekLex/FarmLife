using UnityEngine;

[RequireComponent(typeof(ActivatableArea))]
public abstract class AreaBase : MonoBehaviour
{ 
    protected IActivatable Activatable;
    protected virtual void OnStart()
    {
        Activatable = GetComponent<IActivatable>();
        
        Activatable.OnActivated += OnActivated;
        Activatable.OnDeactivated += OnDeactivated;
    }
    protected virtual void OnDest()
    {
        Activatable.OnActivated -= OnActivated;
        Activatable.OnDeactivated -= OnDeactivated;
    }
    protected abstract void OnActivated(GameObject sender);
    protected abstract void OnDeactivated(IActivatable activatable);
}