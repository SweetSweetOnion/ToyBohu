using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int fighter;
    [SerializeField] private GameObject[] hpBar;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; ++i)
        {
            hpBar[i].SetActive(false);
        }
        int hp = GameManager.Instance.GetHP(fighter);
        if (hp > 0)
        {
            hpBar[hp - 1].SetActive(true);
        }
    }
}
