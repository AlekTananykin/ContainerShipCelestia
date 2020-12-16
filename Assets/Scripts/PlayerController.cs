using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _helth;

    private List<IWeapon> _weaponStorage;
    private int _selectedWeapon;

    void Awake()
    {
        _helth = 100;
        _selectedWeapon = 0;
        _weaponStorage = new List<IWeapon>();
        _weaponStorage.Add(new ArmHit());
    }

    void Update()
    {
    }

    public void ReactToHit(int hitAccount)
    {
        _helth -= hitAccount;
        Debug.Log("Player hit. " + _helth.ToString());
    }

    private void AddWeapon(IWeapon weapon)
    {
        IWeapon storageWeapon = 
            _weaponStorage.Find((IWeapon item) => weapon.Name == item.Name);

        if (null != storageWeapon)
            storageWeapon.AddCharge(weapon.Charge);
        else
            _weaponStorage.Add(weapon);
    }

    private IWeapon GetNextWeapon()
    {
        _selectedWeapon = (_selectedWeapon + 1) % _weaponStorage.Count;
        return _weaponStorage[_selectedWeapon];
    }

    private void AddHealth(int count)
    {
        _helth = Mathf.Max(100, _helth + count);
        Debug.Log("Player helth. " + _helth.ToString());
    }
}
