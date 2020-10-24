using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Get rigidbody
    private Rigidbody2D rb;

    [SerializeField]
    private float _speed = 3f;

    public float jumpForce = 5.0f;

    [SerializeField] private LayerMask groundLayer;
    private bool resetJumpNeeded = false;
    private bool _grounded = false;

    //Handle to player animation
    private PlayerAnimation _playerAnim;

    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    void Start()
    {
        // assign the rigidbody
        rb = GetComponent<Rigidbody2D>();

        //Assign handle to playeranimation
        _playerAnim = GetComponent<PlayerAnimation>();

        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        //Geting the sprite from second component

    }


    void Update()
    {
        Movement();

        //Attack
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    public void Movement()
    {
        // Horizental input for left and right
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else
        if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            Jump();
        }

        //current velocity=new velocity(horizental input,current velocity.y)
        rb.velocity = new Vector2(move * _speed, rb.velocity.y);

        _playerAnim.Move(move);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        StartCoroutine(ResetJumpNeededRoutine());
        _playerAnim.Jump(true);
    }

    public bool IsGrounded()
    {
        //2D raycast to the ground
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1F, groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 1F, Color.green);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                _playerAnim.Jump(false);
                return true;
            }


        }
        return false;
    }

    private void Flip(bool facingRight)
    {
        if (facingRight == true)
        {
            _playerSprite.flipX = false;

            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (facingRight == false)
        {
            _playerSprite.flipX = true;

            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }


    IEnumerator ResetJumpNeededRoutine()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
