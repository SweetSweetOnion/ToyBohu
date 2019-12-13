using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    [SerializeField] private GameObject[] roundBar;

    [SerializeField] private int indexPlayer;

    private int previousScore = 0;


    // Update is called once per frame
    void Update()
    {
        int score = GameManager.Instance.GetVictory(indexPlayer);

        if(previousScore == score)
        {
            return;
        }

        previousScore = score;

        for(int i = 0; i < 3; i++)
        {
            roundBar[i].SetActive(false);
        }

        if (score <= 2)
        {
            roundBar[score].SetActive(true);
        }

    }
}
