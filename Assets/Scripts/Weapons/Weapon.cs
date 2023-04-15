
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    #region Variables
    public Transform AttackPoint;

    public Transform Spawn;
    public RaycastHit Hit;

    Camera camera;
    CameraEffects cameraEffects;

    //Gun stats
    public int Damage;
    public float TimeBetweenShooting, Range, ReloadTime, TimeBetweenShots;
    public int MagazineSize, BulletsPerTap;
    public bool AllowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;


    //Spawn
    public float CrosshairRecoil;
    public float MaxCrosshairRecoil;

    //private Transform Spawn;
    #endregion



    [Obsolete]
    void Start()
    {
        AttackPoint = transform.Find("AttackPoint");
        Spawn = transform.parent.parent.parent.Find("SpawnBullet");
        Hit = new RaycastHit();

        bulletsLeft = MagazineSize;
        readyToShoot = true;

        camera = transform.parent.parent.GetComponent<Camera>();

        //Spawn = gameObject.transform.parent.parent;
    }

    [Obsolete]
    void Update()
    {

        if (AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < MagazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = BulletsPerTap;
            Shoot();
        }

        if (!shooting)
        {
            ResetRecoil();
        }

    }


    void Shoot()
    {
        SpawnMuzzleFlash();
        SpawnBulletHole();
        GenerateRecoil();
        CameraEffects.ShakeOnce(0.2f, 3, new Vector3(0.2f, 0.2f, 0.2f), Spawn.GetComponentInChildren<Camera>(), true);
        PlaySound();

        readyToShoot = false;

        Invoke(nameof(ResetShot), TimeBetweenShooting);

        bulletsLeft--;
        bulletsShot--;

        Invoke(nameof(ResetShot), TimeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), TimeBetweenShots);

    }

    void GenerateRecoil()
    {
        
        if (Math.Abs(camera.transform.localRotation.x) < MaxCrosshairRecoil)
        {

            camera.transform.localRotation *= Quaternion.Euler(-CrosshairRecoil, 0, 0);
            Spawn.transform.localRotation *= Quaternion.Euler(-CrosshairRecoil + Random.Range(-0.5f, 0.5f), 0, 0);
            Spawn.transform.localRotation *= Quaternion.Euler(0, Random.Range(-0.5f, 0.5f), 0);
            //Debug.Log(Spawn.transform.localRotation);
        }
        else
        {
            
            if (Math.Abs(Spawn.transform.localRotation.x) < MaxCrosshairRecoil + 0.01f)
            {
                
                Spawn.transform.localRotation *= Quaternion.Euler(-CrosshairRecoil + Random.Range(-0.5f, 0.5f), 0, 0);
                Spawn.transform.localRotation *= Quaternion.Euler(0, Random.Range(-0.5f, 0.5f), 0);
            }
            else
            {
                Debug.Log(-(MaxCrosshairRecoil) * Mathf.Rad2Deg + " | " + MaxCrosshairRecoil);
                Spawn.transform.localRotation = Quaternion.Euler(-(MaxCrosshairRecoil) * 2f * Mathf.Rad2Deg - Random.Range(1f, 3f), 0, 0);
                Spawn.transform.localRotation *= Quaternion.Euler(0, Random.Range(-1f, 1f), 0);
            }

        }
        //Debug.Log(Spawn.transform.localRotation);
        //Spawn.transform.localRotation = Quaternion.Euler(Spawn.transform.localRotation.x, 0, 0);
        //Quaternion.Lerp(Spawn.transform.localRotation, Quaternion.Euler(30, 0, 0), 1);
    }
    void ResetRecoil()
    {

        if (Spawn.localRotation.x < 0)
        {
            //Spawn.transform.localRotation *= Quaternion.Euler(CrosshairSpawn/100, 0, 0);
            Spawn.Rotate(-12 * Time.deltaTime * Vector3.left);
        }
        else
        {
            Spawn.localRotation = Quaternion.Euler(0, Spawn.localRotation.y, Spawn.localRotation.z);
        }
        if (camera.transform.localRotation.x < 0)
        {
            camera.transform.Rotate(-10 * Time.deltaTime * Vector3.left);
        }
        else
        {
            camera.transform.localRotation = Quaternion.Euler(0, Spawn.localRotation.y, Spawn.localRotation.z);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", ReloadTime);
    }

    void SpawnBulletHole()
    {
        if (Physics.Raycast(Spawn.position, Spawn.forward, out Hit, 1000f))
        {
            GameObject bulletHole = WeaponEffectManager.Instance.BulletHolePool.Pop();
            //Debug.Log(bulletHole);
            bulletHole.transform.position = Hit.point + Hit.normal * 0.001f;
            bulletHole.transform.LookAt(Hit.point + Hit.normal);
        }
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzleFlash = WeaponEffectManager.Instance.MuzzleFlashPool.Pop();
        muzzleFlash.transform.parent = AttackPoint;
        muzzleFlash.transform.localPosition = Vector3.zero;
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }

    void PlaySound()
    {
        WeaponSoundManager.Instance.PlaySound("AK74", 0);
    }
}
