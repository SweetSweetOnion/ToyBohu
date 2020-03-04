using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelect : MonoBehaviour
{
	public Selectable toSelect;

	private void Awake()
	{
	
	}

	private void Update()
	{
		UpdateSelect();
	}

	private void UpdateSelect()
	{
		bool b = false;
		foreach (Selectable s in Selectable.allSelectablesArray)
		{
			if(EventSystem.current.currentSelectedGameObject == s.gameObject)
			{
				b = true;
			}
		}

		if (!b)
		{
			toSelect.Select();
			EventSystem.current.SetSelectedGameObject(toSelect.gameObject);
		}
		
	}

  
}
