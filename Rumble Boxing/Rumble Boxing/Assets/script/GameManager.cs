using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    private List<int> listOfAnswer = new List<int>();
    private float defaultTimer;
    private float currentBarTimer;
    private float timerSpeed;

	void Start () {
        initDataDefault();
        generateNewQuest();
    }

    private void initDataDefault()
    {
        defaultTimer = 2;
        currentBarTimer = defaultTimer;
        timerSpeed = 0.01f;
        initDataPlayer();
    }

    private void initDataPlayer()
    {
        getPlayer().init(1, false);
        getEnemy().init(2, true);
    }

    private void generateNewQuest()
    {
        questManager.gameObject.GetComponent<QuestManager>().generateQuest();
        listOfAnswer = new List<int>();
        currentBarTimer = defaultTimer;
    }
    

    public void inputAnswer(int _idAnswer)
    {
        Debug.Log(_idAnswer);
        if (listOfAnswer.Count < 4)
        {
            listOfAnswer.Add(_idAnswer);
            if(listOfAnswer.Count == 4)
            {
                if (questManager.gameObject.GetComponent<QuestManager>().checkAnswer(listOfAnswer))
                {
                    Debug.Log("answer true");
                    playerPunch();
                }
                else
                {
                    Debug.Log("answer false");
                    enemyPunch();
                }
                generateNewQuest();

            }
        }
    }

    private void enemyPunch()
    {
        int damage = getEnemy().Damage;
        getEnemy().attack();
        getPlayer().getHit(damage);
        getUIManager().updatePlayerBar((float)getPlayer().CurrentHealthPoint / getPlayer().HealthPoint);
    }

    private void playerPunch()
    {
        getPlayer().attack();
        int damage = getPlayer().Damage;
        getEnemy().getHit(damage);
        getUIManager().updateEnemyBar((float)getEnemy().CurrentHealthPoint / getEnemy().HealthPoint);
    }

    private Unit getPlayer()
    {
        return player.gameObject.GetComponent<Unit>();
    }

    private Unit getEnemy()
    {
        return enemy.gameObject.GetComponent<Unit>();
    }

    private UIManager getUIManager()
    {
        return uiManager.gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update () {
        currentBarTimer -= timerSpeed;
        if(currentBarTimer <= 0)
        {
            enemyPunch();
            generateNewQuest();
        }
        getUIManager().updateTimerBar(currentBarTimer / defaultTimer);
    }
}
