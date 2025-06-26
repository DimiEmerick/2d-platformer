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

    public Animator animator;

    private float _currentSpeed;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            animator.speed = 2f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            animator.speed = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool(soPlayerSetup.boolRun, true);
            if (myRB.transform.localScale.x != -1)
            {
                myRB.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            myRB.velocity = new Vector2(-_currentSpeed, myRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool(soPlayerSetup.boolRun, true);
            if (myRB.transform.localScale.x != 1)
            {
                myRB.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            myRB.velocity = new Vector2(_currentSpeed, myRB.velocity.y);
        }
        else
        {
            animator.SetBool(soPlayerSetup.boolRun, false);
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