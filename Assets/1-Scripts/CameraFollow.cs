using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _followSpeed = 1;
    private Vector3 _offset;
    private Vector3 _targetPos, _smoothFollow;


    private void Start()
    {
        _offset = transform.position - _followTarget.position;
    }


    private void LateUpdate()
    {
        SmoothCamFollow();
    }


    private void SmoothCamFollow()
    {
        _targetPos = _followTarget.position + _offset;
        _smoothFollow = Vector3.Lerp(transform.position, _targetPos, _followSpeed);
        transform.position = _smoothFollow;
        transform.LookAt(_followTarget);
    }
}
