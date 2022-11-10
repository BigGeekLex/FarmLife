using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace Farm.Core.Input
{
    public class InputPositionHandler : MonoBehaviour, IObservable<Vector2Int>
    {
        private ReactiveProperty<Vector2Int> _currentInputPosition = new ReactiveProperty<Vector2Int>();
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1) || UnityEngine.Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    Vector2Int inputPos = new Vector2Int(Mathf.RoundToInt(hit.point.x),Mathf.RoundToInt(hit.point.z));
                    _currentInputPosition.SetValueAndForceNotify(inputPos);    
                }
            }
        }
    
        public IDisposable Subscribe(IObserver<Vector2Int> observer)
        {
            return _currentInputPosition.Subscribe(observer);
        }
    }
}

