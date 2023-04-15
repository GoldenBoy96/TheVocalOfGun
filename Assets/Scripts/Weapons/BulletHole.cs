using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    private float cooldown = 0;

    void OnEnable()
    {
        cooldown = 300;
    }

    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown -= 1f;
        }
        else
        {
            WeaponEffectManager.Instance.BulletHolePool.Push(gameObject);

        }
    }
}
