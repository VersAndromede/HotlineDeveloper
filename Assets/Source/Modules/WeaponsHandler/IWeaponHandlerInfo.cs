using System;
using Modules.Weapons.WeaponItemSystem;
using Modules.Weapons.WeaponTypeSystem;

namespace Modules.PlayerWeaponsHandler
{
    public interface IWeaponHandlerInfo
    {
        public bool CurrentWeaponItemIsEmpty { get; }
        public WeaponType CurrentWeaponType { get; }
    }
}