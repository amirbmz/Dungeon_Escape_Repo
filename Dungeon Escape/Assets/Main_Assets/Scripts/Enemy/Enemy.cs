using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;


    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {

        //If Idle anim
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
            //Do noting
        }
        Movement();
    }

    public virtual void Movement()
    {
        //Flip
        if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }
        else
       if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }

        //Move
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else
          if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

}
