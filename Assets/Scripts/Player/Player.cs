using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRB;
    public HealthBase healthBase;

    [Header("Player Setup")]
    public SOPlayerSetup soPlayerSetup;

    private float _currentSpeed;
    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float disToGround;
    public float spaceToGround = .1f;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if (collider2D != null)
        {
            disToGround = collider2D.bounds.extents.y;
        }
    }

    private void IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, disToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRB.transform.localScale.x != -1)
            {
                myRB.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            myRB.velocity = new Vector2(-_currentSpeed, myRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRB.transform.localScale.x != 1)
            {
                myRB.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            myRB.velocity = new Vector2(_currentSpeed, myRB.velocity.y);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if (myRB.velocity.x > 0)
        {
            myRB.velocity += soPlayerSetup.friction;
        }
        else if (myRB.velocity.x < 0)
        {
            myRB.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            myRB.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRB.transform.localScale = Vector2.one;
            DOTween.Kill(myRB.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRB.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRB.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}