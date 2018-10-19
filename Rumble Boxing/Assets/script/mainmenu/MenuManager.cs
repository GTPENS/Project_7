using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject tittle;
    [SerializeField] private GameObject char1;
    [SerializeField] private GameObject char2;
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject OptionButton;
    [SerializeField] private GameObject QuitMenu;


    // Use this for initialization
    void Start () {
        
        tittle.transform.DOMoveY(3, 4).OnComplete(charMove);
        menuMove();
        
    }

    private void charMove()
    {
        char1.transform.DOMoveX(1.5f, 4);
        char2.transform.DOMoveX(-1.5f, 4);
        char1.transform.DOShakeRotation(2, 10, 1);
        char2.transform.DOShakeRotation(2, 10, 1);
    }

    private void menuMove()
    {
        PlayButton.transform.DOMoveX(79, 2);
        OptionButton.transform.DOMoveX(79, 2);
        QuitMenu.transform.DOMoveX(79, 2);
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
