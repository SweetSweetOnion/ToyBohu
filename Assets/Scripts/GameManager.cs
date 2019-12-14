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

    [Header("Parameters")]
    [SerializeField] private float beginRoundDelay = 3f;
    [SerializeField] private bool beginRoundIsLit = true;
    [SerializeField] private float endRoundDelay = 2f;
    [SerializeField] private float tutoDelay = 20f;
    [SerializeField] private float delayBeforeBeingAbleToRestart = 7f;
    [Header("Links")]
	[SerializeField] private AudioManager audioManager;
    [SerializeField] private Fighter[] fighters;
    [SerializeField] private GameObject roomLight;
    [SerializeField] private GameObject[] roundTexts;
    [SerializeField] private GameObject[] victoryTexts;
    [SerializeField] private GameObject restartIndication;
    [SerializeField] private GameObject tuto;
    [SerializeField] private SceneField menu;

    private Vector3[] startingPositions = new Vector3[2];
    private PlayerInputManager playerInputManager;
    private GameState state = GameState.GameStart;

    private int[] victory = new int[2];
    private int currentRound;
    private int winner = -1;


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
        currentRound = -1;
        state = GameState.Tuto;
        StartCoroutine("DelayDuringTuto");
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
        LastHPOfTheGame();
    }

    private IEnumerator DelayDuringTuto()
    {
        roomLight.SetActive(true);
        tuto.SetActive(true);
        yield return new WaitForSeconds(tutoDelay);
        tuto.SetActive(false);
        roomLight.SetActive(false);
		
		InitRound();
    }

    private IEnumerator DelayBeforeNewRound()
    {
		state = GameState.RoundEnd;
        roomLight.SetActive(true);
        yield return new WaitForSeconds(endRoundDelay);
        roomLight.SetActive(false);
        InitRound();
    }

    private IEnumerator DelayBeforeGameEnd()
    {
        roomLight.SetActive(true);
        yield return new WaitForSeconds(delayBeforeBeingAbleToRestart);
        restartIndication.SetActive(true);
        state = GameState.GameEnd;
        roomLight.SetActive(false);
    }

    private IEnumerator DelayBeforeRoundText()
    {
		UpdateAudio();
		yield return new WaitForSeconds(1.2f);
        roundTexts[currentRound].SetActive(true);
    }
    
    private IEnumerator DelayBeforeFight()
    {
        state = GameState.RoundStart;
        roomLight.SetActive(beginRoundIsLit);
        StartCoroutine(DelayBeforeRoundText());
        yield return new WaitForSeconds(beginRoundDelay);
        roomLight.SetActive(false);
        roundTexts[currentRound].SetActive(false);
        state = GameState.Fight;
    }

    private IEnumerator DelayBeforeWinnerText()
    {
		audioManager.EndMatch();
		yield return new WaitForSeconds(1f);
		victoryTexts[winner].SetActive(true);
		if (winner == 1)
		{
			audioManager.PurpleWinsAudio();
		}
		else
		{
			audioManager.YellowWinsAudio();
		}
	}

    private void InitRound()
    {
        ++currentRound;
       
        state = GameState.RoundStart;
        for (int i = 0; i < 2; ++i)
        {
			HardResetTrails(fighters[i]);
			fighters[i].GetComponent<Physics>().DirectMoveAt(startingPositions[i]);
            fighters[i].Initialize();
            fighters[i].SetOpponent(fighters[(i + 1) % 2]);
        }
		
		StartCoroutine("DelayBeforeFight");
    }

    private void UpdateAudio()
    {
        switch (GetRoundId())
        {
            case 0:
                audioManager.Round1Audio();
			//	audioManager.Round1VoixAudio();
                break;
            case 1:
                audioManager.Round2Audio();
			//	audioManager.Round2VoixAudio();
                break;
            case 2:
                audioManager.Round3Audio();
			//	audioManager.Round3VoixAudio();
                break;
        }
    }

    private void LastHPOfTheGame()
    {
		bool b = false;
        for(int i = 0; i < 2; ++i)
        {
            if(victory[i] >= 1 && fighters[(i + 1) % 2].getHp() <= 1)
            {
				b = true;
            }
        }
		audioManager.lastPV(b);
    }

    //Public methods;

    public bool PlayerCanInteract()
    {
        return state == GameState.Fight || state == GameState.RoundEnd || state == GameState.Tuto;
    }

    public void RestartGame()
    {
        StartCoroutine("Restart");
    }

    public void GoToMenu()
    {
        StartCoroutine("Menu");
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private IEnumerator Menu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(menu.SceneName);
    }

    public void TryWin()
    {
        if(state != GameState.Fight)
        {
            return;
        }
        for(int i = 0; i < 2; ++i)
        {
            if (fighters[i].IsDead())
            {
                ++victory[(i + 1) % 2];
                if (victory[(i + 1) % 2] >= 2)
                {
                    winner = (i + 1) % 2;
                    StartCoroutine("DelayBeforeWinnerText");
                    state = GameState.RoundEnd;
                    StartCoroutine("DelayBeforeGameEnd");
                }
                else
                {
                    StartCoroutine("DelayBeforeNewRound");
                }
                return;
            }
        }
    }

    public int GetVictory(int index)
    {
        return victory[index];
    }

	public int GetRoundId()
	{
		return currentRound;
	}

    public int GetHP(int fighter)
    {
        return fighters[fighter].getHp();
    }

    public bool IsGameEnd()
    {
        return state == GameState.GameEnd;
    }

	private void HardResetTrails(Fighter f)
	{
		foreach(TrailRenderer t in f.GetComponentsInChildren<TrailRenderer>())
		{
			t.Clear();
			t.emitting = false;
		}
	}
}
