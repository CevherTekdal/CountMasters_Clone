using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateManager : MonoBehaviour
{
    private PlayerSpawner _spawner;
    private TextMeshProUGUI bonusText;
    public bool isMultiplier;
    private bool isTriggered;
    public int gateAmount;
    void Start()
    {
        _spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlayerSpawner>();
        bonusText = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //isMultiplier = Random.value < 0.5;          //Eğer randomizasyon yapılmak isteniyorsa bu şekilde ilerlenebilir


        if (isMultiplier)
        {
            //gateAmount = Random.Range(2, 4);        //Eğer randomizasyon yapılmak isteniyorsa bu şekilde ilerlenebilir
            bonusText.text = "x" + gateAmount;
        }
        else
        {
            //gateAmount = Random.Range(1, 100);      //Eğer randomizasyon yapılmak isteniyorsa bu şekilde ilerlenebilir
            bonusText.text = "+" + gateAmount;
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider" && !isTriggered)
        {
            isTriggered = true;
            if (isMultiplier)
            {
                _spawner.SpawnPlayer(GameManager.Instance.PlayerCount * gateAmount - GameManager.Instance.PlayerCount);
            }
            else
            {
                _spawner.SpawnPlayer(gateAmount);
            }
            Destroy(this.transform.parent.gameObject);
        }
    }
}
