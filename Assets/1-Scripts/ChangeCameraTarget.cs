using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _speed = 0.4f;
    private Quaternion _newRot;
    private Vector3 _offset;

    public bool isSwitching = false;

    void Update()
    {
        if (isSwitching)
        {
            _offset = _target.position - transform.position;
            _newRot = Quaternion.LookRotation(_offset);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _newRot, Time.time * _speed);
        }
    }
}
