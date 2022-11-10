using System;
using DG.Tweening;
using MSFD;
using UnityEngine;


public class PlantSelectableWindow : MonoBehaviour, ISelectable<PlantType>, IPlantSelectableWindow
{
    [SerializeField]
    private PlantSelectableButton[] views;
    [SerializeField] 
    private float defaultDuration;
    private Vector3 _initialScale;
    private IPlantSelectableWindow PlantSelectableWindowProvider => this;
    private ISelectable<PlantType> VegetationSelectable => this;
    
    public event Action<PlantType> OnSelected;
    private void OnChoosed(PlantType selectedType)
    {
        OnSelected?.Invoke(selectedType);
        UnsubcribeFromSelectables();
    }
    private void Awake()
    {
        ServiceLocator.Register(PlantSelectableWindowProvider);
        ServiceLocator.Register(VegetationSelectable);
        
        _initialScale = gameObject.transform.localScale;
    }
    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
    private void OnDisable()
    {
        UnsubcribeFromSelectables();
    }
    public void Open()
    {
        gameObject.transform.DOScale(_initialScale, defaultDuration).OnComplete(()=>SubscribeToSelectables());
    }
    private void SubscribeToSelectables()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].OnSelected += OnChoosed;
        }
    }
    private void UnsubcribeFromSelectables()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].OnSelected -= OnChoosed;
        } 
    }
    public void Close()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.1f);
    }
}