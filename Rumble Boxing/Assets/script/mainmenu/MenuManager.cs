﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject tittle;
    [SerializeField] private GameObject charRight;
    [SerializeField] private GameObject charLeft;
    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnOptions;
    [SerializeField] private GameObject btnExit;

    private Vector3 tittleDefaultPos;
    private Vector3 charRightDefaultPos;
    private Vector3 charLeftDefaultPos;
    private Vector3 btnPlayDefaultPos;
    private Vector3 btnOptionsDefaultPos;
    private Vector3 btnExitDefaultPos;

    // Use this for initialization
    void Start () {
        Debug.Log(btnPlay.transform.position);
        Debug.Log(btnOptions.transform.position);
        Debug.Log(btnExit.transform.position);
        setDefaultPosition();
        setSpawnPosition();
        tittleMove();
    }

    private void setDefaultPosition()
    {
        tittleDefaultPos = tittle.transform.position;
        charRightDefaultPos = charRight.transform.position;
        charLeftDefaultPos = charLeft.transform.position;
        btnPlayDefaultPos = btnPlay.transform.position;
        btnOptionsDefaultPos = btnOptions.transform.position;
        btnExitDefaultPos = btnExit.transform.position;
    }

    private void setSpawnPosition()
    {
        tittle.transform.position       = new Vector3(0.0f,   6.5f, 90.0f);
        charRight.transform.position    = new Vector3(5.3f,  -1.7f, 90.0f);
        charLeft.transform.position     = new Vector3(-5.3f, -1.7f, 90.0f);
        btnPlay.transform.position      = new Vector3(0.0f, -5.8f, 90f);
        btnOptions.transform.position   = new Vector3(0.0f, -5.8f, 90f);
        btnExit.transform.position      = new Vector3(0.0f, -5.8f, 90f);
    }

    private void tittleMove()
    {
        tittle.transform.DOMove(tittleDefaultPos, 0.5f).SetEase(Ease.InCirc).OnComplete(charMove);
    }

    private void charMove()
    {
        charRight.transform.DOMove(charRightDefaultPos, 0.5f).SetEase(Ease.InCirc);
        charLeft.transform.DOMove(charLeftDefaultPos, 0.5f).SetEase(Ease.InCirc).OnComplete(buttonMove);
    }

    private void buttonMove()
    {
        btnPlay.transform.DOMove(btnPlayDefaultPos, 0.5f).SetEase(Ease.InCirc);
        btnOptions.transform.DOMove(btnOptionsDefaultPos, 0.5f).SetDelay(0.25f).SetEase(Ease.InCirc);
        btnExit.transform.DOMove(btnExitDefaultPos, 0.5f).SetDelay(0.5f).SetEase(Ease.InCirc);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void EqitGame()
    {
        Debug.Log("berhasil");
        Application.Quit();
    }
}
