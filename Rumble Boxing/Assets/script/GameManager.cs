﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject unit;
    [SerializeField] private GameObject startGameCanvas;
    [SerializeField] private GameObject questHandler;
    [SerializeField] private GameObject popUpGameOver;
    [SerializeField] private GameObject canvasLoadingScreen;
    [SerializeField] private GameObject vfxPunchText;
    [SerializeField] private Animator animationStartGame;
    private Animator animatorStartGame;

    private List<int> listOfAnswer = new List<int>();
    private float defaultTimer;
    private float currentBarTimer;
    private float timerSpeed;
    private bool isBattleReady;
    private int unitReadyCounter;
    private int highscore;

    private const float timerDecreaser = 0.2f;
    private const float timerDefault = 5;
	void Start () {
        unitReadyCounter = 0;
        highscore = 0;
        initDataDefault();
        attachUnitBase();
        questHandler.SetActive(false);
        startGameCanvas.SetActive(false);
        popUpGameOver.SetActive(false);
        canvasLoadingScreen.SetActive(false);
    }

    private void initStartGameBanner()
    {
        startGameCanvas.SetActive(true);
        DOVirtual.DelayedCall(3, startGame);
    }

    private void startGame()
    {
        hideAnimationStartGame();
        questHandler.SetActive(true);
        isBattleReady = true;
        generateNewQuest();
    }

    private void initDataDefault()
    {
        isBattleReady = false;
        defaultTimer = timerDefault;
        if(defaultTimer <= 1.5f)
        {
            defaultTimer = 1.5f;
        }
        else
        {
            defaultTimer = timerDefault - (timerDecreaser * highscore);
        }
        Debug.Log("timer " + defaultTimer);
        currentBarTimer = defaultTimer;
        timerSpeed = 0.01f;
    }

    private void attachUnitBase()
    {
        int idPlayer = GameplayDataManager.getInstance().IdEquipedUnit;
        if(idPlayer == null)
        {
            idPlayer = 1;
        }
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
        if (listOfAnswer.Count < questManager.GetComponent<QuestManager>().TotalQuest)
        {
            listOfAnswer.Add(_idAnswer);
            getUIManager().showFilledQuest(listOfAnswer.Count);
            if(listOfAnswer.Count == questManager.GetComponent<QuestManager>().TotalQuest)
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
        attachVfxPunchText(getPlayer().transform.position.x);
        if (getPlayer().IsDead)
        {
            Debug.Log("game over");
            gameOver();
        }
    }

    private void playerPunch()
    {
        getPlayer().attack();
        int damage = getPlayer().Damage;
        getEnemy().getHit(damage);
        getUIManager().updateEnemyBar((float)getEnemy().CurrentHealthPoint / getEnemy().HealthPoint);
        attachVfxPunchText(getEnemy().transform.position.x);
        if (getEnemy().IsDead)
        {
            highscore++;
            getUIManager().updateRound(highscore + 1);
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
        getUIManager().updateEnemyBar((float)getEnemy().CurrentHealthPoint / getEnemy().HealthPoint);
    }

    private void onUnitReady(object sender, EventArgs e)
    {
        initDataDefault();
        unitReadyCounter++;
        Debug.Log("ready " + unitReadyCounter);
        if(unitReadyCounter == 1)
        {
            initStartGameBanner();
        }
        else if(unitReadyCounter == 2)
        {
            isBattleReady = true;
            generateNewQuest();
        }else if(unitReadyCounter >= 3)
        {
            unitReadyCounter = 2;
            questHandler.SetActive(true);
            isBattleReady = true;
            generateNewQuest();
            Debug.Log("timer " + timerSpeed);
        }
    }

    private void onUnitDeath(object _sender, EventArgs e)
    {
        GameObject sender = ((GameObject)_sender);
        listOfUnit.Remove(sender.GetComponent<Unit>());
        sender.GetComponent<Unit>().EVENT_UNIT_DEATH -= onUnitDeath;
        Destroy(sender);
        isBattleReady = false;
        questHandler.SetActive(false);
        if (sender.GetComponent<Unit>().IsEnemy)
        {
            attachUnit(2, true);
        }
        //unitReadyCounter--;
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

    private void gameOver()
    {
        popUpGameOver.SetActive(true);
        getUIManager().updateHighscore(highscore);
    }

    public void quitToMenu()
    {
        canvasLoadingScreen.SetActive(true);
        StartCoroutine(loadAsync());
    }


    IEnumerator loadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            Debug.Log("loading" + operation.progress);
            getUIManager().updateSliderLoadingscreen((float)Mathf.Clamp01(operation.progress / .9f));
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    private List<VfxPunchText> listOfVfx = new List<VfxPunchText>();
    private void attachVfxPunchText(float _targetPosX)
    {
        GameObject go = Instantiate(vfxPunchText);
        go.GetComponent<VfxPunchText>().EVENT_REMOVE += onRemoveVfxPunchText;
        listOfVfx.Add(go.GetComponent<VfxPunchText>());
        listOfVfx[getNumberOfVfxPunchText() - 1].init(_targetPosX);
    }

    private void onRemoveVfxPunchText(object _sender, EventArgs e)
    {
        GameObject sender = ((GameObject)_sender);
        listOfVfx.Remove(sender.GetComponent<VfxPunchText>());
        sender.GetComponent<VfxPunchText>().EVENT_REMOVE-= onRemoveVfxPunchText;
        Destroy(sender);
    }

    private int getNumberOfVfxPunchText()
    {
        return listOfVfx.Count;
    }
}
