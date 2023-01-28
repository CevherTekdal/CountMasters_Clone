using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerWar : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyMoveController enemyMoveController;
    private bool _isTriggered = false;
    private Transform _playerParent, _enemyParent, _playerSpawner, _enemySpawner;
    private int _enemyCount, _playerCount;
    private bool _isWarStarted = false, _winWar = false, _loseWar = false, _isWarEnded = false;


    void Start()
    {
        _playerParent = playerController.gameObject.transform;
        _playerSpawner = _playerParent.GetChild(0);
        _enemyParent = enemyMoveController.gameObject.transform;
        _enemySpawner = _enemyParent.GetChild(0);
    }

    void Update()
    {
        if (_isWarStarted & !_isWarEnded)
        {
            if (_winWar)
            {
                if (_enemySpawner.childCount == 0)       //hepsi öldüğü an savaş kazanıldı demektir
                {
                    WinWar();
                }
            }
            else if (_loseWar)
            {
                if (_playerSpawner.childCount == 0)      //hepsi öldüğü an savaş kaybedildi demektir
                {
                    _isWarEnded = true;
                    LoseWar();
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && !_isTriggered)
        {
            var playerCenterPoint = _playerParent.GetChild(2);
            var enemyCenterPoint = _enemyParent.GetChild(2);
            MovetoEachOther(playerCenterPoint, enemyCenterPoint);
            StartWar();

            _isTriggered = true;
        }
    }


    private void MovetoEachOther(Transform playerTarget, Transform enemyTarget)
    {
        enemyMoveController.LookTowardsPlayer(playerTarget);
        enemyMoveController.RunTowardsPlayer();

        playerController.LookTowardsEnemy(enemyTarget);
        playerController.WarFormationActivator();
    }


    private void StartWar()
    {
        _isWarStarted = true;

        //Savaş başlangıcında kimin galip geleceğini anlıyoruz.
        _enemyCount = _enemySpawner.GetComponent<EnemySpawner>().totalEnemyAmount;
        _playerCount = GameManager.Instance.PlayerCount;


        if (_playerCount > _enemyCount) _winWar = true;
        else _loseWar = true;
    }


    private void WinWar()
    {
        GameManager.Instance.PlayerCount = _playerCount - _enemyCount;
        _enemySpawner.GetComponent<EnemySpawner>().totalEnemyAmount = 0;
        playerController.WarFormationInActivator();
        Destroy(_enemyParent.gameObject);
    }

    private void LoseWar()
    {
        GameManager.Instance.PlayerCount = 0;
        _enemySpawner.GetComponent<EnemySpawner>().totalEnemyAmount = _enemyCount - _playerCount;
        GameManager.Instance.GameOver();
    }

}
