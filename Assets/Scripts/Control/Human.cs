using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : Control
{
    [SerializeField] private Fighter fighter;
   // [SerializeField] private int controller;
    private PlayerInput playerInput;

    //Buffers:
    private Vector2 lstickBuffer;
    private bool dashBuffer;
    private bool attackBuffer;

	private void Awake()
	{
		fighter = GetComponent<Fighter>();
	}

	

	private void OnEnable()
    {
		playerInput = GetComponent<PlayerInput>();

		/* playerInput.currentActionMap["Move"].performed += ctx => lstickBuffer = ctx.ReadValue<Vector2>();
		 playerInput.currentActionMap["Move"].canceled += ctx => lstickBuffer = Vector2.zero;
		 playerInput.currentActionMap["Dash"].performed += ctx => fighter.DashButton();
		 playerInput.currentActionMap["Attack"].performed += ctx => fighter.AttackButton();*/

		playerInput.currentActionMap["Attack"].performed += Attack;
		playerInput.currentActionMap["Dash"].performed += Dash;
		playerInput.currentActionMap["Move"].performed += ctx => lstickBuffer = ctx.ReadValue<Vector2>();
		playerInput.currentActionMap["Move"].canceled += ctx => lstickBuffer = Vector2.zero;
	}

	private void OnDisable()
	{
		playerInput.currentActionMap["Attack"].performed -= Attack;
		playerInput.currentActionMap["Dash"].performed -= Dash;
		playerInput.currentActionMap["Move"].performed -= MovePerformed;
		playerInput.currentActionMap["Move"].canceled -= MoveCanceled;
	}

	private void Attack(InputAction.CallbackContext obj)
	{
		fighter.AttackButton();
	}

	private void Dash(InputAction.CallbackContext obj)
	{
		fighter.DashButton();
	}

	private void MovePerformed(InputAction.CallbackContext obj)
	{
		lstickBuffer = obj.ReadValue<Vector2>();
	}

	private void MoveCanceled(InputAction.CallbackContext obj)
	{
		lstickBuffer = Vector2.zero;
	}





	// Update is called once per frame
	void Update()
    {
        if (playerInput != null) {

            if (!lstickBuffer.Equals(Vector2.zero))
            {
                fighter.Move(lstickBuffer);
            }
        }
    }
}
