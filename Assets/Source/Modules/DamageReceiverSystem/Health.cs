﻿using System;
using UnityEngine;

namespace Modules.DamageReceiverSystem
{
    internal class Health
    {
        private readonly float _minHealth;
        private readonly float _maxHealth;

        private float _currentHealth;
        

        internal Health(float maxHealth, float minHealth = 0f)
        {
            _minHealth = minHealth;
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            IsDead = false;
        }

        internal bool IsDead { get; private set; }

        internal void TakeDamage(float damage, Action onDieCallback)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            if (IsDead)
                return;

            _currentHealth = Mathf.Clamp(_currentHealth -= damage, _minHealth, _maxHealth);

            if (_currentHealth <= _minHealth)
            {
                IsDead = true;
                onDieCallback?.Invoke();
            }
        }

        internal void Execute(Action OnDieCallback)
        {
            TakeDamage(_currentHealth, OnDieCallback);
        }
    }
}