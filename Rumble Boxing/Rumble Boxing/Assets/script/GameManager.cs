using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject unit;
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
        attachUnit(1, false);
        attachUnit(2, true);
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
        if (getPlayer().IsDead)
        {
            Debug.Log("game over");
        }
    }

    private void playerPunch()
    {
        getPlayer().attack();
        int damage = getPlayer().Damage;
        getEnemy().getHit(damage);
        getUIManager().updateEnemyBar((float)getEnemy().CurrentHealthPoint / getEnemy().HealthPoint);
        if (getEnemy().IsDead)
        {
            Debug.Log("enemy dead, generate new enemy");
        }
    }


    private List<GameObject> listOfUnit = new List<GameObject>();
    private void attachUnit(int _id, bool _isEnemy)
    {
        GameObject go = Instantiate(unit);
        listOfUnit.Add(go);
        listOfUnit[listOfUnit.Count - 1].GetComponent<Unit>().init(_id, _isEnemy);
    }

    private Unit getPlayer()
    {
        return listOfUnit[0].GetComponent<Unit>();
    }

    private Unit getEnemy()
    {
        return listOfUnit[listOfUnit.Count - 1].GetComponent<Unit>();
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
