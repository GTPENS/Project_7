using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    // Use this for initialization
    private int id;
    private int damage;
    private int healthPoint;
    private int currentHealthPoint;
    private bool isEnemy;
    private Animator animator;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int HealthPoint
    {
        get
        {
            return healthPoint;
        }

        set
        {
            healthPoint = value;
        }
    }

    public int CurrentHealthPoint
    {
        get
        {
            return currentHealthPoint;
        }

        set
        {
            currentHealthPoint = value;
        }
    }

    public bool IsEnemy
    {
        get
        {
            return isEnemy;
        }

        set
        {
            isEnemy = value;
        }
    }

    public void init(int _id, bool _isEnemy)
    {
        Id = _id;
        IsEnemy = _isEnemy;
        HealthPoint = 100;
        CurrentHealthPoint = HealthPoint;
        Damage = 20;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character"+Id.ToString(), typeof(RuntimeAnimatorController));
    }

    public void getHit(int _damage)
    {
        CurrentHealthPoint -= _damage;
        animator.SetTrigger("hit");
        if(CurrentHealthPoint <= 0)
        {
            setIsDead();
            if (isEnemy)
            {
                generateNewEnemyModel();
            }
        }
    }

    private void generateNewEnemyModel()
    {
        //int idEnemy = UnityEngine.Random.Range(11, 15);
        init(2, true);
    }

    private void setIsDead()
    {
        //animation death here
    }

    public void attack()
    {
        //GetComponent<Animator>().SetTrigger("punch");
        animator.SetTrigger("punch");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
