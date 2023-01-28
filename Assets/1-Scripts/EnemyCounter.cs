using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;


    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = (_enemySpawner.totalEnemyAmount).ToString();
    }
}
