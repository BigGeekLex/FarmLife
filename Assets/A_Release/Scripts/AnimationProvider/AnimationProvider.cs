using System;
using System.Collections;
using CorD.SparrowInterfaceField;
using Farm.Core.Hero;
using Farm.Core.Movement;
using UniRx;
using UnityEngine;


public class AnimationProvider : MonoBehaviour
{
    [SerializeField] 
    private InterfaceField<IMovable> movable;

    [SerializeField] 
    private InterfaceField<IHero> hero;
    [SerializeField] 
    private string speedAnimationParamName = "Speed_f";

    private Animator _controller;

    private void Awake()
    {
        _controller = GetComponent<Animator>();

        hero.i.PlantLanded += PlayLandedAnimation;
        hero.i.PlantCollected += PlayCollectingAnimation;
        movable.i.GetSpeedObs().Subscribe((x) => ChangeSpeed(x)).AddTo(this);
    }

    private void OnDestroy()
    {
        hero.i.PlantLanded -= PlayLandedAnimation;
        hero.i.PlantCollected -= PlayCollectingAnimation;
    }

    private void ChangeSpeed(float speed)
    {
        _controller.SetFloat(speedAnimationParamName, speed);
    }

    private void PlayLandedAnimation()
    {
        StartCoroutine(ChangeBoolParamByName("Jump_b"));
    }

    private void PlayCollectingAnimation()
    {
        StartCoroutine(ChangeBoolParamByName("Crouch_b"));
    }
    private IEnumerator ChangeBoolParamByName(string name)
    {
        float defaultDelay = 1.0f;
        _controller.SetBool(name, true);
        
        yield return new WaitForSeconds(defaultDelay);
        
        _controller.SetBool(name, false);
    }
}
