using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    public Animator animator;
    public HealthBase healthBase;
    public Collider2D enemyCollider;
    public string triggerAttack = "Attack";
    public string triggerKill = "Death";
    public float timeToDestroy = 1f;
    public bool isDead;

    private void Awake()
    {
        isDead = false;
        if (healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        isDead = true;
        healthBase.OnKill -= OnEnemyKill;
        PlayKillAnimation();
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();
        if ((health != null) && (isDead == false))
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayKillAnimation()
    {
        animator.SetTrigger(triggerKill);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
