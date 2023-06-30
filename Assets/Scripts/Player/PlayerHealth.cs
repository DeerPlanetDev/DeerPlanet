using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHP = 100;
    public int maxHP = 100;
    public static PlayerHealth instance;



    void Awake()
    {
        instance = this;
    }

    public void ModifyHP(int hp)
    {
        playerHP = Mathf.Clamp(playerHP + hp, 0, maxHP);
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        if (playerHP <= 0)
            LevelManager.instance.EndLevel(3);
    }

}
