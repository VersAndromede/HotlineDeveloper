﻿using Modules.BulletPoolSystem;
using Modules.BulletSystem;
using UnityEngine;

namespace Modules.Items.Weapons
{
    internal abstract class ShotStrategy : MonoBehaviour
    {
        [SerializeField] private BulletSpawnPoint _bulletSpawnPoint;
        
        private WeaponConfig _config;
        private BulletPool _bulletPool;

        protected float BulletSpeed => _config.BulletSpeed;

        private void OnValidate()
        {
            if (_bulletSpawnPoint == null)
                _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>(true);
        }

        internal abstract void Shot();

        internal void Init(WeaponConfig config, BulletPool bulletPool)
        {
            _config = config;
            _bulletPool = bulletPool;
        }

        protected Bullet InstantiateBullet()
        {
            Bullet bullet = _bulletPool.Get();
            bullet.SetPosition(_bulletSpawnPoint.transform.position);
            return bullet;
        }
    }
}