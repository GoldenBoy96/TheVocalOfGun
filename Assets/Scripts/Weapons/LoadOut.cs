using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    #region Variables
    //this store weapons that player equip, can switch by number key
    public GameObject[] LoadOuts;
    public Transform WeaponParent;
    public GameObject CurrentWeapon;

    #endregion



    [Obsolete]
    void Start()
    {
        InitWeapon();
        Debug.Log(LoadOuts[0]);
        Equip(1);
    }

    [Obsolete]
    void Update()
    {
        CheckInputEquipWeapon();

    }

    [Obsolete]
    void CheckInputEquipWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Equip(4);
        }
    }

    [Obsolete]
    void InitWeapon()
    {
        for (int i = 0; i < LoadOuts.Length; i++)
        {
            GameObject newEquipment = Instantiate(LoadOuts[i], WeaponParent);
            newEquipment.transform.localPosition = Vector3.zero;
            //newEquipment.transform.localEulerAngles = Vector3.zero;
            newEquipment.active = false;
            LoadOuts[i] = newEquipment;
        }

    }

    [Obsolete]
    void Equip(int index)
    {
        if (index > 0 && index <= LoadOuts.Length)
        {
            foreach (GameObject weapon in LoadOuts)
            {
                if (weapon.active == true)
                {
                    weapon.active = false;
                }
            }
            LoadOuts[index - 1].active = true;
            CurrentWeapon = LoadOuts[index - 1];
            //Debug.Log(Equals(LoadOut[index - 1].ToString()));
        }
    }
}
