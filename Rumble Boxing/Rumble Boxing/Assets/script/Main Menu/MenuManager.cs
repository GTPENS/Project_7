using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
       
		if(gameObject.tag == "mainmenu")
        {
            transform.DOMoveY(3,1);
            if (Input.GetKeyDown("space"))
            {
                transform.DOShakePosition(5, 15, 1);
            }
        }
        if(gameObject.tag == "obj1")
        {
            transform.DOShakeRotation(2,10,1);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    
}
