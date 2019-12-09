using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FighterState {Idle, SetUpAttack, Block, Parry, Attack, AttackLag, Dash, Hit, Throw, Death};

public class Fighter : MonoBehaviour
{
    //Stats
    private float speed = 6f;
    private int maxHP = 3;
	

    //Values
    private int hp;
	[SerializeField][DisplayWithoutEdit]
    private FighterState state;
    private Vector2 currentDirection;
	private Physics physics;
    private bool moved = false;

    public Hitbox currentHitbox;
	private float lastDash;
    private Impact impact;
    private Animator animator;
	private FXManager fxManager;
	private Dash dash;
    private PlayerAudioManager playerAudioManager;
    private bool attackBuffer;

	//Counters
	private float counterInState;

    //Serialized fields
    [SerializeField]private Fighter opponent;
	[SerializeField]private float dashCooldown = 0.2f;
	[SerializeField]private float setUpAttackDuration = 0.1f;
	[SerializeField]private float blockDuration = 0.2f;
	[SerializeField]private float attackDuration = 0.2f;
	[SerializeField]private float attackLagDuration = 0.2f;
	//accessors
	public FighterState currentState => state;
	public Vector2 direction => currentDirection;
	public float stateCounter => counterInState;

    public void Initialize()
    {
        attackBuffer = false;
        hp = maxHP;
        state = FighterState.Idle;
        impact.ResetImpact();
    }

    /**
     * Turn the fighter to look at the opponent
     */
    private void FaceOpponent()
    {
        Vector3 dir = opponent.transform.position - transform.position;
		dir = Vector3.ProjectOnPlane(dir, Vector3.up);
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
		transform.rotation = rot;
    }

	private void Awake()
	{
		currentHitbox = GetComponentInChildren<Hitbox>();
		currentHitbox.opponent = opponent;
		physics = GetComponent<Physics>();
        impact = GetComponent<Impact>();
        animator = GetComponentInChildren<Animator>();
		fxManager = GetComponent<FXManager>();
		dash = GetComponent<Dash>();
        playerAudioManager = GetComponentInChildren<PlayerAudioManager>();
    }

	void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!GameManager.Instance.PlayerCanInteract()) {
            return;
        }

        counterInState += Time.deltaTime;
        switch (state)
        {
            case FighterState.Idle:
                {
                    Idle();
                    break;
                }
            case FighterState.Dash:
                {
                    Dash();
                    break;
                }
            case FighterState.SetUpAttack:
                {
                    SetUpAttack();
                    break;
                }
            case FighterState.Block:
                {
                    Block();
                    break;
                }
            case FighterState.Attack:
                {
                    Attack();
                    break;
                }
            case FighterState.AttackLag:
                {
                    AttackLag();
                    break;
                }
        }

        if (moved)
        {
            moved = false;
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
		animator.SetFloat("SpeedScale", physics.orientationVelocity * physics.velocity);
    }

    /**
     * Called to change the fighter state.
     * Initializes attributes depending on the state
     * @param nextState the state to which the fighter will go
     */
    private void ChangeState(FighterState nextState)
    {
        //Initialisations
        counterInState = 0f;
        switch (nextState)
        {
            case FighterState.Dash:
                {
					dash.InitDash();
                    TensionManager.Instance.AddTension(10f);
					fxManager.DashFx();
                    playerAudioManager.AudioDash();
                    break;
                }
            case FighterState.SetUpAttack:
                {
                    animator.SetTrigger("Attack");
                    TensionManager.Instance.AddTension(15f);
                    playerAudioManager.AudioEpee_Out();
                    playerAudioManager.AudioAttaque1_Woosh();
                    break;
                }
            case FighterState.Attack:
                {
					currentHitbox.SetAttacking(true);
					break;
                }
            case FighterState.AttackLag:
                {
                    //playerAudioManager.AudioEpee_in();
                    currentHitbox.SetAttacking(false);
					break;
                }
            case FighterState.Death:
                {
                   // Destroy(gameObject);
                    break;
                }
        }
		if(nextState != state)
		{
			state = nextState;
        }
        attackBuffer = false;
    }
    

    //***************************************
    // Specific states methods called in Idle
    //***************************************

    private void Idle()
    {
        FaceOpponent();
    }

    private void Dash()
    {
		
    }

    private void SetUpAttack()
    {
        FaceOpponent();
        if(counterInState > setUpAttackDuration)
        {
            ChangeState(FighterState.Block);
        }
    }

    private void Block()
    {
        if(counterInState > blockDuration)
        {
            ChangeState(FighterState.Attack);
        }
    }

    private void Attack()
    {
        if(counterInState > attackDuration)
        {
            ChangeState(FighterState.AttackLag);
        }
    }
    
    private void AttackLag()
    {
        if(counterInState > attackLagDuration)
        {
            ChangeState(FighterState.Idle);
        }
    }

    //****************
    // Utility methods
    //****************

    private void Movement(Vector2 movement)
    {
        Vector3 dir = new Vector3(movement.x, 0f, movement.y);
		physics.AddForce(dir);
    }

    //************************************************************************
    // Public methods to control the character in Control class and derivatives
    //************************************************************************

    public bool Move(Vector2 dir)
    {
        if (!GameManager.Instance.PlayerCanInteract())
        {
            return false;
        }
        if (dir.magnitude.Equals(0f) || (state != FighterState.Idle && state != FighterState.Dash))
        {
            return false;
        }
        currentDirection = dir.normalized;
        if (state == FighterState.Dash)
        {
            return true;
        }
        //We ensure magnitude is <= 1f not to move faster than the character speed
        if (dir.magnitude > 1f)
        {
            dir.Normalize();
        }
        Vector2 movement = dir * speed;
        Movement(movement);
        animator.SetFloat("Speed",movement.magnitude);
        moved = true;
        return true;
    }

    public bool DashButton()
    {
        if (!GameManager.Instance.PlayerCanInteract())
        {
            return false;
        }
        if (state == FighterState.Idle && Time.time > lastDash + dashCooldown)
        {
            ChangeState(FighterState.Dash);
            return true;
        }
        return false;
    }

    public bool AttackButton()
    {
        if (!GameManager.Instance.PlayerCanInteract())
        {
            return false;
        }
        if (state == FighterState.Idle)
        {
            ChangeState(FighterState.SetUpAttack);
            return true;
        }
        if(state == FighterState.Dash)
        {
            attackBuffer = true;
        }
        return false;
    }



    //*************************************
    // Public methods for other scripts
    // (Do not call them in the controller)
    //*************************************

    public void Damage(int amount)
    {
        if (!GameManager.Instance.PlayerCanInteract())
        {
            return;
        }
        if (state == FighterState.Block)
        {
            TensionManager.Instance.AddTension(35f);
            ChangeState(FighterState.Idle);
            ParryFeedback();
            opponent.ImpulseOppositToOpponent(7f);
            return;
        }

		fxManager.HitFX();
        hp -= amount;
        if (hp <= 0)
        {
            ChangeState(FighterState.Death);
            animator.SetTrigger("Death");
            ImpulseOppositToOpponent(20f);
            Gamefeel.Instance.InitScreenshake(0.3f, 0.8f);
            Gamefeel.Instance.InitFreezeFrame(0.3f, 0.003f);
            return;
        }
        else
        {
            animator.SetTrigger("Hit");
        }
        ImpulseOppositToOpponent(15f);
        Gamefeel.Instance.InitFreezeFrame(0.1f, 0.002f);
        Gamefeel.Instance.InitScreenshake(0.2f, 0.4f);
    }

    public bool IsDead()
    {
        return state == FighterState.Death;
    }

    public void ImpulseOppositToOpponent(float force)
    {
        Vector3 impulseDir = (transform.position - opponent.transform.position).normalized;
        impulseDir.y = 0f;
        impact.AddImpact(impulseDir, force);
    }

    private void ParryFeedback()
    {
        ImpulseOppositToOpponent(7f);
        fxManager.ParryFX();
        playerAudioManager.AudioBlockArmes();
        Gamefeel.Instance.InitScreenshake(0.1f, 0.3f);
    }

    public void HitboxCollide()
    {
        ParryFeedback();
        currentHitbox.SetAttacking(false);
    }

    public void SucceedAttack()
    {
        if (!GameManager.Instance.PlayerCanInteract())
        {
            return;
        }
        if (state == FighterState.Attack)
        {
            GameManager.Instance.TryWin();
			currentHitbox.SetAttacking(false);
            ImpulseOppositToOpponent(4f);
            TensionManager.Instance.AddTension(50f);
            playerAudioManager.AudioHitBody();
        }
    }

    public void SetOpponent(Fighter op)
    {
        opponent = op;
    }

	public void DashEnd()
	{
		if(state == FighterState.Dash)
		{
			lastDash = Time.time;
            if (attackBuffer)
            {
                ChangeState(FighterState.SetUpAttack);
            }
            else
            {
                ChangeState(FighterState.Idle);
            }
		}
	}


    public int getHp()
    {
        return hp;
    }

}
