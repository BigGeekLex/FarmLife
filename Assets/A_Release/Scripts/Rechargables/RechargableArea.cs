using System;
using CorD.SparrowInterfaceField;
using MPS.Recharhable.Base;
using UnityEngine;
using UniRx;


namespace MPS.Gameplay.Rechargable.Logic
{
    public class RechargableArea : AreaBase, IObservable<RechargableStatus>, IRechargableArea, IObservable<float>
    {
        private IDisposable _currentDisposable;
        
        [SerializeField] 
        private InterfaceField<IRechargerProvidable> recharcherSource;
        private IRechargerProvidable RechargerProvidable => recharcherSource.i;
        
        private ReactiveProperty<RechargableStatus> _statusProperty = new ReactiveProperty<RechargableStatus>();
        private ReactiveProperty<float> _progression = new ReactiveProperty<float>();
        
        private float _speed;

        public event Action OnRechargeFinished;
        private void Awake()
        {
            if (RechargerProvidable != null)
            {
                RechargerProvidable.Finished += FinishProcess;
                RechargerProvidable.Changed += OnChanged;
            }
        }
        private void Start()
        {
            OnStart();
        }
        private void OnEnable()
        {
            _statusProperty.SetValueAndForceNotify(RechargableStatus.None);
            float delay = 0.25f;
            Invoke(nameof(AllowActivation), delay);
        }

        private void AllowActivation()
        {
            Activatable.ChangeActivatableStatus(true);
        }
        private void OnDisable()
        {
            if(RechargerProvidable != null) RechargerProvidable.StopRechargeProcess();
        }
        protected void OnDestroy()
        {
            if(_currentDisposable != null) _currentDisposable.Dispose();

            if (RechargerProvidable != null)
            {
                RechargerProvidable.Finished -= FinishProcess;
                RechargerProvidable.Changed -= OnChanged;
            }
            
            OnDest();
        }
        protected void StopProcess()
        {
            RechargerProvidable.StopRechargeProcess();
            _statusProperty.SetValueAndForceNotify(RechargableStatus.Paused);
        }

        private void OnChanged(float maxValue, float currentValue)
        {
            float minProgression = 0;
            float maxProgression = 1;
            
            float progression = Mathf.Lerp( minProgression, maxProgression, 1 - currentValue / maxValue);
            
            _progression.SetValueAndForceNotify(progression);
        }
        
        private void FinishProcess()
        {
            if (_currentDisposable != null)
            {
                _currentDisposable.Dispose();
            }
            
            RechargerProvidable.StopRechargeProcess();
            
            OnRechargeFinished?.Invoke();
            
            StopProcess();
            
            _statusProperty.SetValueAndForceNotify(RechargableStatus.Finished);
            
            OnDeactivated(Activatable);
        }
        protected void StartProcess(GameObject sender)
        {
            if(_currentDisposable != null) _currentDisposable.Dispose();
            
            if (RechargerProvidable.TryToStartRechargeProcess(_speed))
            {
                _statusProperty.SetValueAndForceNotify(RechargableStatus.Started);
            }
            
            OnRechargeStarted?.Invoke();
        }
        protected override void OnActivated(GameObject sender)
        {
            StartProcess(sender);
        }
        protected override void OnDeactivated(IActivatable activatable)
        {
            Activatable.ChangeActivatableStatus(false);
        }
        public IDisposable Subscribe(IObserver<RechargableStatus> observer)
        {
            observer.OnNext(_statusProperty.Value);
            return _statusProperty.Subscribe(observer);
        }
        public void SetDuration(float speed)
        {
            _speed = speed;
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return _progression.Subscribe(observer);
        }
        
        public float GetMaxBorder()
        {
            return RechargerProvidable.GetMaxBorder();
        }

        public event Action OnRechargeStarted;
    }
}