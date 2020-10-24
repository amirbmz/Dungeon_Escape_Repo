using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Handle to animator 
    private Animator _anim;
    private Animator _swordAnimation;


    void Start()
    {
        //Assign to  animator
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
   public void Move(float move)
    {
        //anim set float
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");

        _swordAnimation.SetTrigger("SwordAnimation");
    }
}
