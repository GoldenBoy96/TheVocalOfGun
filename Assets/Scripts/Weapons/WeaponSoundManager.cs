using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public List<AudioClip> AK74;


    public static WeaponSoundManager Instance { get; set; }

    void Start()
    {
        Instance = GetComponent<WeaponSoundManager>();

        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        AK74 = new List<AudioClip>();
        
        AK74.Add((AudioClip)Resources.Load("Audios/Weapons/AK74_Single"));
       
    }

    private void AddAudio()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        
    }

    public void PlaySound(string weaponName, int sound)
    {
        audioSource.PlayOneShot(AK74[0]);
    }

}
