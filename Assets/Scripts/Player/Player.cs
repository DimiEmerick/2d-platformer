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
    public AudioRandomPlayAudioClips audioSteps;

    private float _currentSpeed;
    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float disToGround;
    public float spaceToGround = .1f;
    public LayerMask groundLayer;
    public ParticleSystem jumpVFX;

    private Vector3 _rayStartPosition;

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

    private bool IsGrounded()
    {
        Debug.DrawRay(_rayStartPosition, Vector2.down, Color.magenta, disToGround + spaceToGround);
        return Physics2D.Raycast(_rayStartPosition, Vector2.down, disToGround + spaceToGround, groundLayer);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        _rayStartPosition = transform.position;
        _rayStartPosition.y -= 2.5f;
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
            if (IsGrounded())
            {
                audioSteps.PlayRandom();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRB.transform.localScale.x != 1)
            {
                myRB.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            myRB.velocity = new Vector2(_currentSpeed, myRB.velocity.y);
            if (IsGrounded())
            {
                audioSteps.PlayRandom();
            }
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            myRB.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRB.transform.localScale = Vector2.one;
            DOTween.Kill(myRB.transform);
            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        // if (jumpVFX != null) jumpVFX.Play();
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