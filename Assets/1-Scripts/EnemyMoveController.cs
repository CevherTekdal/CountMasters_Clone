using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    private float _fwdSpeed = 1.5f;
    private bool _isRunning = false;
    private Animator[] _enemyAnimators;


    void Update()
    {
        if (_isRunning)
        {
            transform.position += transform.forward * _fwdSpeed * Time.deltaTime;
        }
    }


    public void LookTowardsPlayer(Transform target)
    {
        transform.LookAt(target);
    }

    public void RunTowardsPlayer()
    {
        _isRunning = true;
        PlayRunningAnimation();
    }

    private void PlayRunningAnimation()
    {
        var enemySpawner = this.transform.GetChild(0);
        _enemyAnimators = enemySpawner.GetComponentsInChildren<Animator>();
        foreach (Animator animator in _enemyAnimators)
        {
            animator.SetTrigger("Running");
        }
    }

}
