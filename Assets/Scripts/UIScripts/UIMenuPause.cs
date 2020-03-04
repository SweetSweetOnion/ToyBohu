using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIMenuPause : MonoBehaviour
{
	private bool isMenu = false;
	[SerializeField]
	private InputActionAsset map;

	public GameObject panel;
	public Selectable resumeButton;
	
	[SerializeField]
	private Vector3 outPosition;
	private Vector3 startPos;

	private PlayerInput playerInput;

    void Awake()
    {
		startPos = transform.position;
		transform.position = startPos + outPosition;
		playerInput = FindObjectOfType<PlayerInput>();
    }

	private void OnEnable()
	{
		 if(playerInput)playerInput.currentActionMap["Start"].performed += ActionTriggered;
	}

	private void OnDisable()
	{
		if (playerInput) playerInput.currentActionMap["Start"].performed -= ActionTriggered;
	}

	private void ActionTriggered(InputAction.CallbackContext obj)
	{
		Debug.Log("StartPress");
		ToggleMenu();	
	}

	public void ToggleMenu()
	{
		
		if (isMenu)
		{
			CloseMenu();
		}
		else
		{
			OpenMenu();
		}
	}

	public void OpenMenu()
	{
		StopAllCoroutines();
		if (!isMenu)
		{
			isMenu = true;
			StartCoroutine(Translate(startPos, 1000));
			
		}
	}

	public void CloseMenu()
	{
		StopAllCoroutines();
		if (isMenu)
		{
			isMenu = false;
			StartCoroutine(Translate(startPos + outPosition,1000));
			
		}
	}


	private IEnumerator Translate(Vector3 to, float speed)
	{
		
		panel.SetActive(true);
		Time.timeScale = 0;
		

		while(transform.position != to)
		{
			transform.position = Vector3.MoveTowards(transform.position, to, Time.unscaledDeltaTime * speed);
			yield return null;
		}


		if (isMenu)
		{
			Time.timeScale = 0;
			panel.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			resumeButton?.Select();
			//Selectable.allSelectablesArray[2]?.Select();

			
		}
		else
		{
			Time.timeScale = 1;
			panel.SetActive(false);
		}
			
		yield return null;
	}
}
