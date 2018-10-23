using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private Image playerBar;
    [SerializeField] private Image enemmyBar;
    [SerializeField] private Text round_txt;
    [SerializeField] private Image timerBar;
    [SerializeField] private List<GameObject> listOfButtonAnimation = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfFilledQuestAnimation = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfFilledQuest = new List<GameObject>();
    [SerializeField] private Text highscore;
    [SerializeField] private Slider sliderBarLoading;

    public void updateRound(int _round)
    {
        round_txt.text = "ROUND " + _round.ToString();
    }

    public void playButtonAnimation(int _idButton)
    {
        listOfButtonAnimation[_idButton - 1].gameObject.GetComponent<Animator>().SetTrigger("isClick");
    }

    public void showFilledQuest(int _idSingleQuest)
    {
        listOfFilledQuestAnimation[_idSingleQuest - 1].gameObject.GetComponent<Animator>().SetTrigger("isFilled");
        listOfFilledQuest[_idSingleQuest - 1].gameObject.SetActive(true);
    }

    public void resetFilledQuest()
    {
        for (int i = 0; i < 5; i++)
        {
            listOfFilledQuest[i].gameObject.SetActive(false);
        }
    }

    public void updateTimerBar(float _fillAmount)
    {
        timerBar.fillAmount = _fillAmount;
    }

    public void updatePlayerBar(float _fillAmount)
    {
        playerBar.fillAmount = _fillAmount;
    }

    public void updateEnemyBar(float _fillAmount)
    {
        enemmyBar.fillAmount = _fillAmount;
    }

    public void updateHighscore(int _highscore)
    {
        highscore.text = _highscore.ToString();
    }

    public void updateSliderLoadingscreen(float _value)
    {
        sliderBarLoading.value = _value;
    }

    // Update is called once per frame
    void Update () {
	}
}
