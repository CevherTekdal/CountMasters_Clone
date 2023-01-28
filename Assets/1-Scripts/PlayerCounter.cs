using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCounter : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.PlayerCount.ToString();
    }
}
