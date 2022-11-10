using System;
using CorD.SparrowInterfaceField;
using Farm.Core.Movement;
using MSFD;
using UnityEngine;


namespace Farm.Core.Hero
{
    public class HeroController : MonoBehaviour, IHero
    {
        [SerializeField] 
        private InterfaceField<IMovable> movableSource;
        private IMovable Movable => movableSource.i;
        private HeroStatus _heroStatus;
        private IHero HeroProvidable => this;
        private IPlantSelectableController SelectableController => ServiceLocator.Get<IPlantSelectableController>();
        private IPlantCollectableController PlantCollectableController => ServiceLocator.Get<IPlantCollectableController>();
        
        private IPlantCuttableController CuttableController => ServiceLocator.Get<IPlantCuttableController>();
        
        public event Action PlantLanded;
        public event Action PlantCutted;
        public event Action PlantCollected;
        private void Awake()
        {
            ServiceLocator.Register(HeroProvidable);
        }
        private void Start()
        {
            SelectableController.PlantSelected += OnPlantSelected;
            PlantCollectableController.PlantCollecting += OnPlantCollectingRequest;
            CuttableController.CutRequest += OnPlantCutRequest;
        }
        private void OnDestroy()
        {
            SelectableController.PlantSelected -= OnPlantSelected;
            PlantCollectableController.PlantCollecting -= OnPlantCollectingRequest;
            CuttableController.CutRequest -= OnPlantCutRequest;
        }
        public HeroStatus GetStatus()
        {
            return _heroStatus;
        }
        
        private void OnPlantSelected(PlantType type, Vector2Int pos)
        {
            SetTargetAndSubscribe(pos, OnPlantLanded);
        }
        
        private void OnPlantLanded()
        {
            PlantLanded?.Invoke();
            ReleaseHeroAndUnsubscribe(OnPlantLanded);
        }

        private void SetTargetAndSubscribe(Vector2Int pos, Action action = null)
        {
            _heroStatus = HeroStatus.Busy;
            
            Movable.SetNextTarget(pos);
            Movable.DestinationReached += action;
        }

        private void ReleaseHeroAndUnsubscribe(Action action = null)
        {
            _heroStatus = HeroStatus.Free;
            Movable.DestinationReached -= action;
        }
        
        private void OnPlantCollectingRequest(Vector2Int pos)
        {
            SetTargetAndSubscribe(pos, OnPlantCollected);
        }

        private void OnPlantCutRequest(Vector2Int pos)
        {
            SetTargetAndSubscribe(pos, OnPlantCutted);
        }

        private void OnPlantCutted()
        {
            PlantCutted?.Invoke();
            ReleaseHeroAndUnsubscribe(OnPlantCutted);
        }

        private void OnPlantCollected()
        {
            PlantCollected?.Invoke();
            ReleaseHeroAndUnsubscribe(OnPlantCollected);
        }
    }
}