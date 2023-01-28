using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayerStackPositioner(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }
}
