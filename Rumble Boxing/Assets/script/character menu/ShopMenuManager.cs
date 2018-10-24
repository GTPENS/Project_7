using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject modelUnit;
    [SerializeField] private Button btn_buy;
    [SerializeField] private Button btn_equip;
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
            modelUnit.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else
        {
            modelUnit.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        }
        modelUnit.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + idModel.ToString(), typeof(RuntimeAnimatorController));
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

    public void unlockMode()
    {
        GameplayDataManager.getInstance().unlockUnit(idModel);
        updateModel();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
