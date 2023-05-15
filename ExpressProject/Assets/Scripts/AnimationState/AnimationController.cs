using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public bool isMoveing;
    public bool isLocked;

    public  AnimationState charState;
    private Coroutine coroutineLock = null;
    private bool allowedInput = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetInteger("Action", (int)(AnimationTriggers)System.Enum.Parse(typeof(AnimationTriggers), trigger));
        animator.SetTrigger("Trigger");
    }

    public void ChangeCharacterState(float waitTime, AnimationState state)
    {
        StartCoroutine(_ChangeCharacterState(waitTime, state));
    }

    private IEnumerator _ChangeCharacterState(float waitTime, AnimationState state)
    {
        yield return new WaitForSeconds(waitTime);
        charState = state;
    }

    public void LockMovement(float lockTime)
    {
        if(coroutineLock != null)
        {
            StopCoroutine(coroutineLock);
        }
        coroutineLock = StartCoroutine(_LockMovement(lockTime));
    }

    private IEnumerator _LockMovement(float lockTime)
    {
        allowedInput = false;
        isLocked = true;
        animator.applyRootMotion = true;
        if(lockTime != -1f)
        {
            yield return new WaitForSeconds(lockTime);
            isLocked = false;
            animator.applyRootMotion = false;
            allowedInput = true;
        }
    }
}
