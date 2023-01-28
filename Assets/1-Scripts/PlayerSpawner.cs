using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float distance = 0.3f, randomizeDistance = 0.2f, distanceIncrement = 0.25f;
    private GameObject spawnedObj;
    private float angle, angleIncrement;
    private int circleNumber = 1, circlePopulation = 7;
    private Animator _animator;
    private Transform centerPoint;


    void Start()
    {
        angleIncrement = Mathf.Floor(360 / circlePopulation);       //çembere yerleşebilecek insan sayısına göre açısal boşluğu ayarla
        centerPoint = this.transform.parent.GetChild(2);
    }

    public void SpawnPlayer(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            spawnedObj = Instantiate(prefab);
            spawnedObj.transform.position = GetPosition();
            spawnedObj.transform.rotation = transform.parent.rotation;
            spawnedObj.transform.parent = transform;

            _animator = spawnedObj.transform.GetChild(0).GetComponent<Animator>();
            _animator.SetFloat("cycleOffset", Random.Range(0f, 1f));    //her bir instance'ın farklı yürüme animasyon başlangıç noktaları
            _animator.SetTrigger("Running");
        }
        GameManager.Instance.PlayerCount += amount;
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

    public void RePositionAllPlayers()
    {
        //Eksik player varsa onları bir doğuralım.
        if (this.transform.childCount < GameManager.Instance.PlayerCount)
        {
            var amount = GameManager.Instance.PlayerCount - this.transform.childCount;
            SpawnPlayer(amount);
            GameManager.Instance.PlayerCount -= amount;     //eksikleri bir daha eklemesin diye çıkardık
        }

        //gerekli değişkenleri default haline getirelim.
        distance = 0.3f;
        circleNumber = 1;
        circlePopulation = 7;
        angle = 0;
        angleIncrement = Mathf.Floor(360 / circlePopulation);
        //tüm childların pozisyonlarını ayarlayalım.
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).position = GetPosition();
            this.transform.GetChild(i).rotation = transform.parent.rotation;
            this.transform.GetChild(i).parent = transform;
        }
    }

}
