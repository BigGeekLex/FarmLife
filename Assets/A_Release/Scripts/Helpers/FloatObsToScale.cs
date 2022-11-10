using System;
using CorD.SparrowInterfaceField;
using UnityEngine;
using UniRx;
public class FloatObsToScale : MonoBehaviour
{
    [SerializeField] 
    private float maxScale = 1.0f;
    [SerializeField] 
    private InterfaceField<IObservable<float>> progressionObsSource;
    private IObservable<float> ProgressionObs => progressionObsSource.i;

    private Vector3 startScale;
    private void Awake()
    {
        startScale = transform.localScale;
        ProgressionObs.Subscribe((x) => OnProgressionChanged(x)).AddTo(this);
    }
    private void OnProgressionChanged(float progression)
    {
        float factor = Mathf.Lerp(0, maxScale, progression);
        Vector3 scale = new Vector3(factor, factor, factor);
        transform.localScale = scale;
    }
    private void OnDisable()
    {
        transform.localScale = startScale;
    }
}
