using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f;
    float timeLeft;
    public bool isTimeToStop = false;
    public GameController gameSet;

    // Use this for initialization
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update () {
        
            
	}

    IEnumerator CountDown()
    {
        float counter = maxTime;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1f);
            counter--;
            timerBar.fillAmount = counter / maxTime;
            if(gameSet.isPlayerAnswer == true)
            {
                gameSet.RightEffect();
                //Time.timeScale = 0;
                //timerBar.fillAmount = 1;
            }
            if (counter <= 0)
            {
                isTimeToStop = true;
                gameSet.RandomAnimal();
                gameSet.WrongEffect();
                
            }

        }

    }

    public void TimeRunning()
    {
        StartCoroutine(CountDown());
    }
}
