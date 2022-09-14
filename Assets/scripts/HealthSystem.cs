using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IDamagable 
{
    public int _health;
    public int _maxHealth;
    [SerializeField] private Slider _slider;
    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            _slider.value = CalculatePercent();
            Debug.Log(_health);
        }
    }

    private float CalculatePercent()
    {
        float res =(float) _health/_maxHealth ;
        return res*100;
    }

}
