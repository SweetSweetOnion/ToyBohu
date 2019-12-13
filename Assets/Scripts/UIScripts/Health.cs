using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int fighter;
    [SerializeField] private GameObject[] hpBar;

    private int previousHP = 3;

    // Update is called once per frame
    void Update()
    {
        int hp = GameManager.Instance.GetHP(fighter);
        if (previousHP == hp)
        {
            return;
        }
        if (previousHP > hp)
        {
            GetComponentInParent<UIShake>().Shake();
            GetComponentInParent<UIShake>().Flash();
        }
        previousHP = hp;
        for (int i = 0; i < 3; ++i)
        {
            hpBar[i].SetActive(false);
        }
        if (hp > 0)
        {
            hpBar[hp - 1].SetActive(true);
        }
    }
}
