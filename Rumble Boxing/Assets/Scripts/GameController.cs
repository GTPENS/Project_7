using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int[] quest = new int[] { 1, 2, 3, 4 };
    int correctAnswer = 0;
    int questNumber = 0;

    public Transform[] placeHolder;
    public GameObject[] animal;
    public TimerScript timer;
    public bool isPlayerAnswer = false;
   
    // Use this for initialization
    void Start()
    {
        RandomAnimal();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputButton(int tipe)
    {
        if (tipe == 1)
            print("A");
        if (tipe == 2)
            print("B");
        if (tipe == 3)
            print("C");
        if (tipe == 4)
            print("D");
        Answer(tipe);
    }

    public void Answer(int answer)
    {
        if (quest[questNumber] == answer)
        {
            correctAnswer++;
        }
        else
        {
            print("Kamu payah");
        }
        if (questNumber >= 3)
        {
            questNumber = 0;

            if (correctAnswer == 4)
            {
                print("Benar");
            }
            else
            {
                print("Pukul");
            }
            isPlayerAnswer = true;
            correctAnswer = 0;
            RandomizeArray();
            RandomAnimal();
        }
        else
        {
            questNumber++;
        }

        print("Quest Number = " + questNumber);
    }

    void RandomizeArray()
    {
        for (int i = quest.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            int tmp = quest[i];
            quest[i] = quest[r];
            quest[r] = tmp;
        }
    }

    public void RandomAnimal()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (quest[i] == j + 1)
                {
                    animal[j].transform.position = placeHolder[i].position;
                }
            }
        }
    }

    public void WrongEffect()
    {
        print("Musuh Memukul");
        StartCoroutine(StopforAWhile());
        
    }

    public void RightEffect()
    {
        print("Player Memukul");
        StartCoroutine(StopforAWhile());
        
    }
    IEnumerator StopforAWhile()
    {
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 1;
        timer.TimeRunning();
        isPlayerAnswer = false;
    }


}
