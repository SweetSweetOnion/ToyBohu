using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    public Sprite[] RoundSprite;

    public Image RoundUI;


    // Update is called once per frame
    void Update()
    {
        RoundUI.sprite = RoundSprite[GameManager.Instance.GetVictory(0)];
    }
}
