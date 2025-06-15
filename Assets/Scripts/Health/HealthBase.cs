using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public float startLife = 10;
    public float delayToKill = 1f;
    public bool destroyOnKill = false;

    private float _currentLife;
    private bool _isDead = false;
    [SerializeField] private FlashColor _flashColor;

    private void Awake()
    {
        Init();  
        if(_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if (destroyOnKill) Destroy(gameObject, delayToKill);
        OnKill.Invoke();
    }
}
