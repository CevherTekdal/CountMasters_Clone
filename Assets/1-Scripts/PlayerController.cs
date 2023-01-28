using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fwdSpeed, horizontalSpeedFactor, inputSensitivity;
    [SerializeField] private GameObject floorObject;
    private Touch touch;
    private Vector2 touchPos, previousTouchPos;
    private float normalizedDeltaPosition, targetPos;
    private bool warMoveSystem = false, isRunning = false;
    [SerializeField] private PlayerSpawner playerSpawner;


    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            if (!isRunning) StartRunningFirstPlayer();
            TouchInput();
            MovewithSlide();
        }
    }


    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPos = touch.position;
                touchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.position;
            }

            normalizedDeltaPosition = ((touchPos.x - previousTouchPos.x) / Screen.width) * inputSensitivity;
        }
        targetPos = targetPos + normalizedDeltaPosition;
        targetPos = Mathf.Clamp(targetPos, floorObject.GetComponent<BoxCollider>().bounds.min.x + 0.5f, floorObject.GetComponent<BoxCollider>().bounds.max.x - 0.5f);

        previousTouchPos = touchPos;
    }



    private void MovewithSlide()
    {
        if (warMoveSystem)
        {
            transform.position += transform.forward * fwdSpeed * Time.deltaTime;
        }
        else
        {
            float horizontalSpeed = Time.deltaTime * horizontalSpeedFactor;
            float newPositionTarget = Mathf.Lerp(transform.position.x, targetPos, horizontalSpeed);
            float newPositionDifference = newPositionTarget - transform.position.x;

            newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
            transform.position = new Vector3(transform.position.x + newPositionDifference, transform.position.y, transform.position.z + fwdSpeed * Time.deltaTime);
        }
    }

    public bool WarFormationActivator()
    {
        fwdSpeed = 1.5f;
        return warMoveSystem = true;
    }
    public bool WarFormationInActivator()
    {
        fwdSpeed = 4;
        targetPos = transform.position.x;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        playerSpawner.RePositionAllPlayers();
        return warMoveSystem = false;
    }

    public void LookTowardsEnemy(Transform target)
    {
        transform.LookAt(target);
    }

    private void StartRunningFirstPlayer()
    {
        playerSpawner.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("Running");
        isRunning = true;
    }
}
