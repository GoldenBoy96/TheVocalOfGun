using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<WeaponEffectManager>();
        Player = (GameObject)Resources.Load("Prefabs/Player/Player");
        Instantiate(Player, transform.Find("Map1").Find("SpawnPoint"));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
