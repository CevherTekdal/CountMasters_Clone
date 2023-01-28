using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapManager : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    private bool isTriggered = false;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(other.gameObject.transform.parent.gameObject, 0.15f);
            GameManager.Instance.PlayerCount -= 1;

            if (!isTriggered)
            {
                playerSpawner.Invoke("RePositionAllPlayers", 1f);
                isTriggered = true;
            }
        }
    }
}
