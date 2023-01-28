using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private PlayerSpawner playerSpawner;
    void Start()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlayerSpawner>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            GameManager.Instance.PlayerCount -= 1;
            playerSpawner.Invoke("RePositionAllPlayers", 1f);
        }
    }
}
