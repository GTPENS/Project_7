using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject modelUnit;
    [SerializeField] private GameObject lockerModel;
    private int idModel;
	void Start () {
        GameplayDataManager.getInstance().reset();
        idModel = 1;
        updateModel();
	}

    private void updateModel()
    {
        if (GameplayDataManager.getInstance().isUnitUnlocked(idModel))
        {
            lockerModel.gameObject.SetActive(false);
        }
        else
        {
            lockerModel.gameObject.SetActive(true);
        }
        modelUnit.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + idModel.ToString(), typeof(RuntimeAnimatorController));
    }

    public void prevModel()
    {
        idModel--;
        if(idModel < 1)
        {
            idModel = 2;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void nextModel()
    {
        idModel++;
        if (idModel > 2)
        {
            idModel = 1;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void unlockMode()
    {
        GameplayDataManager.getInstance().unlockUnit(idModel);
        updateModel();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
