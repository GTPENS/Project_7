using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject unit;
    [SerializeField] private Animator animationStartGame;
    [SerializeField] private GameObject startGameCanvas;
    [SerializeField] private GameObject questHandler;
    private Animator animatorStartGame;

    private List<int> listOfAnswer = new List<int>();
    private float defaultTimer;
    private float currentBarTimer;
    private float timerSpeed;
    private bool isBattleReady;

	void Start () {
        initDataDefault();
        attachUnitBase();
        questHandler.SetActive(false);
        startGameCanvas.SetActive(false);
        DOVirtual.DelayedCall(3, initStartGameBanner);
    }

    private void initStartGameBanner()
    {
        startGameCanvas.SetActive(true);
        DOVirtual.DelayedCall(3, startGame);
    }

    private void startGame()
    {
        hideAnimationStartGame();
        isBattleReady = true;
        questHandler.SetActive(true);
        generateNewQuest();
    }

    private void initDataDefault()
    {
        isBattleReady = false;
        defaultTimer = 5;
        currentBarTimer = defaultTimer;
        timerSpeed = 0.01f;
    }

    private void attachUnitBase()
    {
        attachUnit(1, false);
        attachUnit(2, true);
    }

    private void generateNewQuest()
    {
        getUIManager().resetFilledQuest();
        questManager.gameObject.GetComponent<QuestManager>().generateQuest();
        listOfAnswer = new List<int>();
        currentBarTimer = defaultTimer;
    }

    public void inputAnswer(int _idAnswer)
    {
        if (!isBattleReady)
        {
            return;
        }
        if (listOfAnswer.Count < 4)
        {
            listOfAnswer.Add(_idAnswer);
            getUIManager().showFilledQuest(listOfAnswer.Count);
            if(listOfAnswer.Count == 4)
            {
                if (questManager.gameObject.GetComponent<QuestManager>().checkAnswer(listOfAnswer))
                {
                    //Debug.Log("answer true");
                    playerPunch();
                }
                else
                {
                    //Debug.Log("answer false");
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


    private List<Unit> listOfUnit = new List<Unit>();
    private void attachUnit(int _id, bool _isEnemy)
    {
        GameObject go = Instantiate(unit);
        go.GetComponent<Unit>().EVENT_UNIT_DEATH += onUnitDeath;
        go.GetComponent<Unit>().EVENT_UNIT_READY += onUnitReady;
        listOfUnit.Add(go.GetComponent<Unit>());
        listOfUnit[listOfUnit.Count - 1].GetComponent<Unit>().init(_id, _isEnemy);
    }

    private void onUnitReady(object sender, EventArgs e)
    {
        initDataDefault();
        getUIManager().updateEnemyBar(((GameObject)sender).GetComponent<Unit>().CurrentHealthPoint/ ((GameObject)sender).GetComponent<Unit>().HealthPoint);
        isBattleReady = true;
        generateNewQuest();
    }

    private void onUnitDeath(object _sender, EventArgs e)
    {
        GameObject sender = ((GameObject)_sender);
        listOfUnit.Remove(sender.GetComponent<Unit>());
        sender.GetComponent<Unit>().EVENT_UNIT_DEATH -= onUnitDeath;
        Destroy(sender);
        isBattleReady = false;
        attachUnit(2, true);
    }

    private Unit getPlayer()
    {
        return (listOfUnit[0]) as Unit;
    }

    private Unit getEnemy()
    {
        return (listOfUnit[getNumberOfUnits() - 1]) as Unit;
    }

    private Unit getUnit(int _index)
    {
        return (listOfUnit[_index]) as Unit;
    }

    private int getNumberOfUnits()
    {
        return listOfUnit.Count;
    }
    

    private UIManager getUIManager()
    {
        return uiManager.gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update () {
        
        if (isBattleReady)
        {
            currentBarTimer -= timerSpeed;
            if (currentBarTimer <= 0)
            {
                enemyPunch();
                generateNewQuest();
            }
            getUIManager().updateTimerBar(currentBarTimer / defaultTimer);
        }
        updateUnit();
        
    }

    private void updateUnit()
    {
        for (int i = 0; i < getNumberOfUnits(); i++)
        {
            getUnit(i).update();
        }
    }

    private void hideAnimationStartGame()
    {
        animationStartGame.GetComponent<Animator>().enabled = false;
        animationStartGame.GetComponent<Animator>().Rebind();
        startGameCanvas.SetActive(false);
    }
}
