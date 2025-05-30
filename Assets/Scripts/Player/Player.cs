using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRB;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRB.velocity = new Vector2(-_currentSpeed, myRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRB.velocity = new Vector2(_currentSpeed, myRB.velocity.y);
        }

        if (myRB.velocity.x > 0)
        {
            myRB.velocity += friction;
        }
        else if (myRB.velocity.x < 0)
        {
            myRB.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRB.velocity = Vector2.up * forceJump;
            myRB.transform.localScale = Vector2.one;
            DOTween.Kill(myRB.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRB.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRB.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
