using System;
using Modules.Weapons.WeaponTypeSystem;
using UnityEngine;

namespace Modules.DamageSystem
{
    [Serializable]
    public struct DamageData
    {
        public WeaponType WeaponType;
        [Range(0,1)]
        public float Value;
        public bool IsKnockout;
        public bool IsLethal;
        
        private static readonly float _zeroDamageValue = 0f;
        private static readonly float _normalDamageValue = 1f;
        private DamageData(float value, WeaponType weaponType, bool isKnockout = false, bool isLethal = false)
        {
            Value = value;
            IsKnockout = isKnockout;
            IsLethal = isLethal;
            WeaponType = weaponType;
        }
        
        public static DamageData MeleeDamage => new DamageData(_normalDamageValue, WeaponType.Melee, false, false);
        public static DamageData RangeDamage => new DamageData(_normalDamageValue, WeaponType.Range, false, false);
        public static DamageData KnockoutDamage => new DamageData(_zeroDamageValue, WeaponType.Melee, true, false);
        public static DamageData ZeroDamage => new DamageData(_zeroDamageValue, WeaponType.Melee, false, false);
        public static DamageData ExecutionDamage => new DamageData(_zeroDamageValue, WeaponType.Melee, false, true);
    }
}