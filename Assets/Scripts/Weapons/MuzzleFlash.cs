using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private float cooldown = 0;
    
    void OnEnable()
    {
        cooldown = this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length * 100;
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= 1.5f;
        } 
        else
        {
            WeaponEffectManager.Instance.MuzzleFlashPool.Push(gameObject);
            
        }
    }


}
