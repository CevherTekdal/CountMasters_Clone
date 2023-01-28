using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public float distance = 0.3f, randomizeDistance = 0.1f, distanceIncrement = 0.25f;
    private GameObject spawnedObj;
    private float angle, angleIncrement;
    private int circleNumber = 1, circlePopulation = 7;
    public int totalEnemyAmount;
    private Transform centerPoint;
    public int spawnAmount;


    void Start()
    {
        angleIncrement = Mathf.Floor(360 / circlePopulation);       //çembere yerleşebilecek insan sayısına göre açısal boşluğu ayarla
        centerPoint = this.transform.parent.GetChild(2);
        SpawnEnemy(spawnAmount);
    }

    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            spawnedObj = Instantiate(prefab);
            spawnedObj.transform.position = GetPosition();
            spawnedObj.transform.rotation = transform.parent.rotation;
            spawnedObj.transform.parent = transform;
        }
        totalEnemyAmount = amount + 1;
    }


    private Vector3 GetPosition()
    {
        if (angle + angleIncrement > 360)       //Create new circle and place on it
        {
            distance += distanceIncrement;
            circleNumber++;
            circlePopulation = 7 * circleNumber;
            angleIncrement = Mathf.Floor(360 / circlePopulation);
            angle = 0;
        }


        var x = Random.Range(distance - randomizeDistance, distance + randomizeDistance) *
                Mathf.Cos(Mathf.Deg2Rad * angle);
        var z = Random.Range(distance - randomizeDistance, distance + randomizeDistance) *
                Mathf.Sin(Mathf.Deg2Rad * angle);
        angle = angle + angleIncrement;

        return new Vector3(centerPoint.position.x + x, centerPoint.position.y, centerPoint.position.z + z);
    }

}
