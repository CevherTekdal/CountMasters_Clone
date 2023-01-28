using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerKill : MonoBehaviour
{
    private EnemySpawner _enemySpawner;


    void Start()
    {
        _enemySpawner = this.transform.parent.parent.GetComponent<EnemySpawner>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //destroyingPlayer = other.gameObject.transform.parent.gameObject;
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
            if (GameManager.Instance.PlayerCount > 0)
            {
                GameManager.Instance.PlayerCount -= 1;
            }
            if (_enemySpawner.totalEnemyAmount > 0)
            {
                _enemySpawner.totalEnemyAmount -= 1;
            }
        }
    }
}
