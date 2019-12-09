using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Sprite[] lifeSprite;

    public Image lifeUI;

    public Fighter fighter;


    // Update is called once per frame
    void Update()
    {
        lifeUI.sprite = lifeSprite[fighter.getHp()];

    }
}
