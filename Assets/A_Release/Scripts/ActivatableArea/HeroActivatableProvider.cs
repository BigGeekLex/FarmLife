using System.Collections;
using MSFD;
using UnityEngine;
public class HeroActivatableProvider : MonoBehaviour
{ 
    [SerializeField] 
    private float defaultDelay = 1;
    [SerializeField] 
    private DetectInfo info;
    private void OnTriggerEnter(Collider other)
    {
       StartCoroutine(Activate(other));
    }
    private IEnumerator Activate(Collider other)
    {
       if (info.IsTargetCorrect(other))
       {
           IActivatable activatable;
           if (other.TryGetComponent(out activatable))
           {
               yield return new WaitForSeconds(defaultDelay);
               activatable.TryActivate(gameObject);
           }
       }
    }
}
