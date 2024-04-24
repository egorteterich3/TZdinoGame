using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private EnemyView _view;
    [SerializeField] private RagdollHandler _ragdollHandler;

    private void Awake()
    {
        _view.Initialize();
        _ragdollHandler.Initialize();
    }

    public void Kill()
    {
        _view.DisableAnimator();
        _ragdollHandler.Enable();
        Invoke("DestroyEnemy", 3.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
