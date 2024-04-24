using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{

    private const string IsIdleKey = "IsIdle";

    private Animator _animator;

    public void Initialize() => _animator = GetComponent<Animator>();

    public void StartIdle() => _animator.SetBool(IsIdleKey, true);
    public void StopIdle() => _animator.SetBool(IsIdleKey, false);

    public void DisableAnimator() => _animator.enabled = false;
    public void EnableAnimator() => _animator.enabled = false;

}
