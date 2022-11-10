using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenScaler : MonoBehaviour
{
    [SerializeField] 
    private float scaleFactor = 1.35f;
    [SerializeField]
    private float duration = 0.25f;
    private Vector3 _initialScale;
    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void PlayScaleEffect()
    {
        Vector3 nextScale = new Vector3(_initialScale.x * scaleFactor, _initialScale.y * scaleFactor,
            _initialScale.z * scaleFactor);
        transform.DOScale(nextScale, duration).OnComplete(()=> transform.DOScale(_initialScale, duration));
    }
}
