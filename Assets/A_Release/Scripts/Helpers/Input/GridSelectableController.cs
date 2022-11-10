using System;
using Farm.Core.Hero;
using MSFD;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;


namespace Farm.Core.Hero
{
    public enum HeroStatus
    {
        Free,
        Busy
    }
}
namespace Farm.Core.Input
{
    public class GridSelectableController : MonoBehaviour, IObservable<IGrid>
    {
        [Required][SerializeField]
        private CorD.SparrowInterfaceField.InterfaceField<IObservable<Vector2Int>> inputObsSource;

        private ReactiveProperty<IGrid> _selectedGrid = new ReactiveProperty<IGrid>();
        private IObservable<Vector2Int> InputObs => inputObsSource.i;

        private Vector2Int _prevPos = new Vector2Int(Int32.MaxValue, Int32.MinValue);
        
        private IHero HeroProvider => ServiceLocator.Get<IHero>();

        private IWorldGridProvider WorldGridProvider => ServiceLocator.Get<IWorldGridProvider>();
        private void Awake()
        {
            InputObs.Subscribe((x) => OnNextInputPosition(x)).AddTo(this);
        }
        private void OnNextInputPosition(Vector2Int pos)
        {
            float minDelta = 0.5f;
            
            if (Vector2Int.Distance(_prevPos, pos) >= minDelta)
            {
                _prevPos = pos;
                TrySelectGrid(pos);
            }
        }
        private bool TrySelectGrid(Vector2Int pos)
        {
            IGrid selected = null;
            
            if (!WorldGridProvider.TrySelectGridByPosition(pos, out selected) || HeroProvider.GetStatus() == HeroStatus.Busy)
            {
                return false;
            }
            
            selected?.OnSelected();
            _selectedGrid.SetValueAndForceNotify(selected);
            
            return true;
        }

        public IDisposable Subscribe(IObserver<IGrid> observer)
        {
            observer.OnNext(_selectedGrid.Value);
            return _selectedGrid.Subscribe(observer);
        }
    }
}
