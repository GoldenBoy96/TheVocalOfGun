using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectManager : MonoBehaviour
{
    public GameObject BulletHole;
    public GameObject MuzzleFlash;
    public static WeaponEffectManager Instance { get; private set; }

    public GameObjectPool BulletHolePool { get; set; }

    public GameObjectPool MuzzleFlashPool { get; set; }

    private void Start()
    {
        Instance = GetComponent<WeaponEffectManager>();

        BulletHole = (GameObject)Resources.Load("Prefabs/Weapons/BulletHole");
        MuzzleFlash = (GameObject)Resources.Load("Prefabs/Weapons/MuzzleFlash");

        BulletHolePool = gameObject.AddComponent<GameObjectPool>();
        MuzzleFlashPool = gameObject.AddComponent<GameObjectPool>();

        for (int i = 0; i < 100; i++)
        {
            GameObject bulletHole = Instantiate(BulletHole);
            BulletHolePool.Push(bulletHole);
        }
        for (int i = 0; i < 100; i++)
        {
            GameObject muzzleFlash = Instantiate(MuzzleFlash);
            MuzzleFlashPool.Push(muzzleFlash);
        }
        
    }
}
