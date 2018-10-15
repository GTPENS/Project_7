using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    // Use this for initialization
    private int id;
    private int damage;
    private int healthPoint;
    private int currentHealthPoint;
    private bool isEnemy;
    private bool isDead;
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

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public void init(int _id, bool _isEnemy)
    {
        DOTween.Init();
        Id = _id;
        IsEnemy = _isEnemy;
        HealthPoint = 100;
        CurrentHealthPoint = HealthPoint;
        Damage = 20;
        IsDead = false;
        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + Id.ToString(), typeof(RuntimeAnimatorController));
        setWalk();
    }

    private void setWalk()
    {
        animator.SetTrigger("walk");
        Debug.Log(this.transform.position);
        if (!IsEnemy)
        {
            this.transform.position = new Vector3(-4, 0.8f, 90);
            this.transform.DOMove(new Vector3(-1.1f, 0.8f, 90), 2).SetEase(Ease.Linear).OnComplete(setIdle);
        }
        else
        {
            this.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            this.transform.position = new Vector3(4, 0.8f, 90);
            this.transform.DOMove(new Vector3(0.8f, 0.8f, 90), 2).SetEase(Ease.Linear).OnComplete(setIdle);
        }
    }

    private void setIdle()
    {
        animator.SetTrigger("idle");
    }

    public void getHit(int _damage)
    {
        CurrentHealthPoint -= _damage;
        animator.SetTrigger("hit");
        if (CurrentHealthPoint <= 0)
        {
            IsDead = true;
            setIsDead();
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
        animator.SetTrigger("ko");
    }

    public void attack()
    {
        //GetComponent<Animator>().SetTrigger("punch");
        animator.SetTrigger("punch");
    }
    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator.GetCurrentAnimatorStateInfo(0).length - 0.1f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("character ko") &&
            IsDead)
        {
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + " - " + animator.GetCurrentAnimatorStateInfo(0).length);
            IsDead = false;
            animator.enabled = false;
            if (isEnemy)
            {
                pullOutKoEnemy();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void pullOutKoEnemy()
    {
        transform.DOMove(new Vector3(5, 0.8f, 90), 1.5f).SetEase(Ease.Linear).OnComplete(generateNewEnemyModel);
    }
}
