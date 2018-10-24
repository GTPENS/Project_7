using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject modelUnit;
    [SerializeField] private Button btn_buy;
    [SerializeField] private Button btn_equip;
    [SerializeField] private Text txt_characterName;
    [SerializeField] private GameObject canvasLoadingScreen;
    [SerializeField] private Slider sliderBarLoading;
    [SerializeField] private List<RuntimeAnimatorController> listOfAnimationControllerCharater = new List<RuntimeAnimatorController>();

    private int idModel;
	void Start () {
        GameplayDataManager.getInstance().reset();
        idModel = 1;
        updateModel();
	}

    private void updateModel()
    {
        txt_characterName.text = DatabaseCharacter.getInstance().getName(idModel);
        if (GameplayDataManager.getInstance().isUnitUnlocked(idModel))
        {
            modelUnit.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else
        {
            modelUnit.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        }
        //modelUnit.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + idModel.ToString(), typeof(RuntimeAnimatorController));
        modelUnit.GetComponent<Animator>().runtimeAnimatorController = listOfAnimationControllerCharater[idModel - 1];
    }

    public void prevModel()
    {
        idModel--;
        if(idModel < 1)
        {
            idModel = GameplayDataManager.getInstance().TotalUnits;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void nextModel()
    {
        idModel++;
        if (idModel > GameplayDataManager.getInstance().TotalUnits)
        {
            idModel = 1;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void unlockModel()
    {
        GameplayDataManager.getInstance().unlockUnit(idModel);
        updateModel();
    }

    public void equipModel()
    {
        GameplayDataManager.getInstance().IdEquipedUnit = idModel;
    }

    public void StartGame()
    {
        canvasLoadingScreen.SetActive(true);
        StartCoroutine(loadAsync());
    }

    IEnumerator loadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            Debug.Log("loading" + operation.progress);
            sliderBarLoading.value = (float)Mathf.Clamp01(operation.progress / .9f);
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
