using System;
using System.Linq;
using UnityEngine;
using Modules.Weapons.WeaponItemSystem;
using Modules.Weapons.WeaponTypeSystem;
using Modules.InputSystem.Interfaces;

namespace Modules.PlayerWeaponsHandler
{
    public class WeaponHandler : IWeaponHandlerInfo
    {
        protected IAttackInput _attackInput;
        protected IPickInput _pickInput;
        private WeaponItem _currentWeaponItem;
        private WeaponItem _defaultWeapon;
        private Transform _container;
        private Transform _pickPoint;
        private float _pickRadius;

        public event Action<IWeaponInfo> WeaponPicked;
        public event Action<WeaponType> Attacked;

        public bool CurrentWeaponItemIsEmpty => _currentWeaponItem == null || _currentWeaponItem == _defaultWeapon;
        public WeaponType CurrentWeaponType => _currentWeaponItem.WeaponType;
        
        public WeaponHandler(WeaponHandlerData weaponHandlerData, IAttackInput attackInput, IPickInput pickInput)
        {
            _pickRadius = weaponHandlerData.PickRadius;
            _pickPoint = weaponHandlerData.PickPoint;
            _container = weaponHandlerData.Container;
            _defaultWeapon = weaponHandlerData.DefaultWeapon;
            _attackInput = attackInput;
            _pickInput = pickInput;
            _pickInput.PickReceived += OnPickInputReceived;
            EquipWeaponItem(_defaultWeapon);
        }

        public void UnequipWeaponItem()
        {
            if (_currentWeaponItem == null || _currentWeaponItem == _defaultWeapon)
                return;
            
            _currentWeaponItem.Unequip();
            _currentWeaponItem.Attacked -= OnAttack;
            _currentWeaponItem = null;
            _attackInput.AttackReceived -= OnAttackInputReceived;

            if (_defaultWeapon != null)
                _defaultWeapon.Attacked += OnAttack;
        }

        protected void OnPickInputReceived()
        {
            bool HasPickableWeapon = TryGetWeapon(out WeaponItem weaponItem);

            if (CurrentWeaponItemIsEmpty == false && _currentWeaponItem.IsEquipped)
            {
                _currentWeaponItem.Throw();
                _currentWeaponItem.Attacked -= OnAttack;
                _currentWeaponItem = _defaultWeapon;
                _attackInput.AttackReceived -= OnAttackInputReceived;
            }

            if (HasPickableWeapon)
            {
                EquipWeaponItem(weaponItem);
            }
        }

        private void EquipWeaponItem(WeaponItem weaponItem)
        {
            if(weaponItem == null)
                return;
            
            _currentWeaponItem = weaponItem;
            _currentWeaponItem.Attacked += OnAttack;
            _currentWeaponItem.Equip(_container);
            WeaponPicked?.Invoke(_currentWeaponItem);
            _attackInput.AttackReceived += OnAttackInputReceived;
        }

        private void OnAttackInputReceived()
        {
            _currentWeaponItem.Attack();
        }

        private bool TryGetWeapon(out WeaponItem weaponItem)
        {
            weaponItem = Physics.OverlapSphere(_pickPoint.position, _pickRadius)
                .Where(collider => IsColliderAvailableWeapon(collider))
                .OrderBy(collider => (collider.transform.position - _pickPoint.position).magnitude)
                .FirstOrDefault()
                ?.GetComponent<WeaponItem>();

            return weaponItem != null;
        }

        private bool IsColliderAvailableWeapon(Collider collider)
        {
            WeaponItem weaponItem = collider.GetComponent<WeaponItem>();

            if (weaponItem == null || weaponItem == _currentWeaponItem || weaponItem.IsEquipped == true)
                return false;

            return true;
        }

        private void OnAttack(WeaponType weaponType)
        {
            Attacked?.Invoke(weaponType);
        }
    }
}