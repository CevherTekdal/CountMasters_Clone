using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Slider levelProgressBar;
    [SerializeField] private GameObject finishLine;
    private float maxDistancetoFinish;
    [SerializeField] private PlayerController playerController;
    private void Start()
    {
        maxDistancetoFinish = finishLine.transform.position.z - playerController.transform.position.z;
    }

    void Update()
    {
        float distance = finishLine.transform.position.z - playerController.transform.position.z;
        levelProgressBar.value = 1 - (distance / maxDistancetoFinish);
    }

}
