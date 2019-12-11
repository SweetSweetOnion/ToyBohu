using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum GameState { GameStart,Tuto, Fight, RoundStart, RoundEnd, GameEnd };

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private int[] victory = new int[2];
	[SerializeField]private AudioManager audioManager;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Fighter[] fighters;
    private Vector3[] startingPositions = new Vector3[2];
    private PlayerInputManager playerInputManager;
    private GameState state = GameState.GameStart;



    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        InitGame();
    }

    private void InitGame()
    {
        //Temporary, we should generate the fighters instead of reading them
        for(int i = 0; i < 2; ++i)
        {
            startingPositions[i] = fighters[i].transform.position;
        }

		InitRound();
    }

    private void Update()
    {
        switch (state) {
            case GameState.GameStart:
            {
                fighters[1].SetOpponent(fighters[0]);
                fighters[0].SetOpponent(fighters[1]);
                state = GameState.Fight;
                break;
            }
        }
    }

    private IEnumerator DelayBeforeNewRound()
    {
        state = GameState.RoundEnd;
        yield return new WaitForSeconds(2);
        InitRound();
    }

    private IEnumerator DelayBeforeFight()
    {
        state = GameState.RoundStart;
        yield return new WaitForSeconds(1);
        state = GameState.Fight;
    }

    private void InitRound()
    {
        state = GameState.RoundStart;
        for (int i = 0; i < 2; ++i)
        {
            fighters[i].GetComponent<Physics>().DirectMoveAt(startingPositions[i]);
            fighters[i].Initialize();
            fighters[i].SetOpponent(fighters[(i + 1) % 2]);
        }
		UpdateAudio();
		StartCoroutine("DelayBeforeFight");
    }

    //Public methods;

    public bool PlayerCanInteract()
    {
        return state == GameState.Fight || state == GameState.RoundEnd;
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TryWin()
    {
        for(int i = 0; i < 2; ++i)
        {
            if (fighters[i].IsDead())
            {
                ++victory[(i + 1) % 2];
                if (victory[(i + 1) % 2] >= 2)
                {
                    state = GameState.GameEnd;
                    StartCoroutine("Restart");
                }
                else
                {
                    StartCoroutine("DelayBeforeNewRound");
                }
                return;
            }
        }
    }

	private void UpdateAudio()
	{
		switch (GetRoundId())
		{
			case 0:
				audioManager.Round1Audio();
				break;
			case 1:
				audioManager.Round2Audio();
				break;
			case 2:
				audioManager.Round3Audio();
				break;
		}
	}

    public int GetVictory(int index)
    {
        return victory[index];
    }

	public int GetRoundId()
	{
		return victory[0] + victory[1];
	}

}
