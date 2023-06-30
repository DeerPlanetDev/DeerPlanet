using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHP = 100;
    private int maxHP;
    public static PlayerHealth instance;



    void Awake()
    {
        maxHP = playerHP;
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
