using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
	[SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.

	[Range(0, 1)] [SerializeField]
	private float crouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%

	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
	[SerializeField] private bool airControl; // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
	[SerializeField] private Transform ceilingCheck; // A position marking where to check for ceilings
	[SerializeField] private Collider2D crouchDisableCollider; // A collider that will be disabled when crouching

	private float mayJump;
	private const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public GameObject interactiveButton;
	

	public bool grounded; // Whether or not the player is grounded.
	private const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private new Rigidbody2D rigidbody2D;
	private bool facingRight = true; // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private Collider2D[] _colliders;
	private FlashController flashController;
	private bool _fallingThroughGround;
	private Collider2D _ceiling;
	private Collider2D _ground;
	private BloodSplatter _bloodScript;

	[Header("Events")] [Space] public UnityEvent onLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool>
	{
	}

	public BoolEvent onCrouchEvent;
	private bool wasCrouching;

	private void Awake()
	{
		_fallingThroughGround = false;
		_colliders = GetComponentsInChildren<Collider2D>();


		rigidbody2D = GetComponent<Rigidbody2D>();
		if (onLandEvent == null)
			onLandEvent = new UnityEvent();

		if (onCrouchEvent == null)
			onCrouchEvent = new BoolEvent();

	}

	private void Start()
	{
		flashController = GameObject.FindWithTag("Player").GetComponentInChildren<FlashController>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			flashController.CameraFlash();
		}
	}

	private void FixedUpdate()
	{



		bool wasGrounded = grounded;
		grounded = false;
		if (_fallingThroughGround && Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
		{
			/*Collider2D ceiling = Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround)
				.GetComponent<Collider2D>();
			foreach (var collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, ceiling, false);
			}*/
			_fallingThroughGround = false;

		}

		if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
		{
			
			Collider2D ceiling = Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround);
			if (ceiling.CompareTag("Stairs"))
			{
				foreach (var collider in _colliders)
				{
					Physics2D.IgnoreCollision(collider, ceiling, true);

				}
			}

		}

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				if (colliders[i].CompareTag("Stairs") && !_fallingThroughGround)
				{
					foreach (Collider2D playerCollider in _colliders)
					{
						Physics2D.IgnoreCollision(playerCollider, colliders[i], false);
					}
				}

				else if (!colliders[i].gameObject.CompareTag("Enemy"))
				{
					if (!wasGrounded)
						onLandEvent.Invoke();
				}
				grounded = true;

			}
		}

	}

	private void Flip()
	{
		Vector3 button = interactiveButton.transform.localScale;
		button.x *= -1;
		interactiveButton.transform.localScale = button;
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public void Move(float move, bool crouch, bool jump)
	{

		if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
		{
			_ceiling = Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround);
		}
		else
		{
			_ceiling = null;
		}

		if (Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround))
		{
			_ground = Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround);
		}
		else
		{
			_ground = null;
		}

		if (_ceiling && _ceiling.CompareTag("Stairs"))
		{
			
			foreach (Collider2D collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, _ceiling, true);
			}
		}

		/*
		if (!_fallingThroughGround && _ground && _ground.CompareTag("Stairs") && !grounded )
		{
			Debug.Log("what?");
			Debug.Log(groundCheck.position.y > _ground.transform.position.y);
			
			foreach (Collider2D collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, _ground, false);
			}
		}*/


		
		
		
		if (move == 0 && grounded && !_fallingThroughGround && !jump)
		{
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else
		{
			/*Debug.Log("grounded: ");
			Debug.Log(grounded);*/
			/*Debug.Log("falling through ground:");
			Debug.Log(_fallingThroughGround);*/
			/*Debug.Log("crouch: ");
			Debug.Log(crouch);
			Debug.Log("jumpo");
			Debug.Log(jump);
			Debug.Log("move");
			Debug.Log(move);*/
			rigidbody2D.constraints = RigidbodyConstraints2D.None;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}


		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (_ceiling && !_ceiling.CompareTag("Stairs"))
			{
				crouch = true;
			}
		}
		

		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{

			// If crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					onCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					onCrouchEvent.Invoke(false);
				}
			}

			if (Input.GetKey("s") && _ground && _ground.CompareTag("Stairs"))
			{
				_fallingThroughGround = true;
				foreach (var collider in _colliders)
				{
					Physics2D.IgnoreCollision(_ground, collider, true);
				}


			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidbody2D.velocity =
				Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
			{
				// ... flip the player, like a burger.
				Flip();
			}


			// If the player should jump...

			if (grounded)
			{
				mayJump = 0.2f;
			}

			mayJump -= Time.deltaTime;
			if (mayJump > 0 && jump)
			{
				// Add a vertical force to the player.
				mayJump = 0;
				grounded = false;
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			}
		}
		/*if (_ground && _ground.CompareTag("Stairs") && !_fallingThroughGround)
		{
			foreach (Collider2D collider in _colliders)
			{
				Physics2D.IgnoreCollision(collider, _ground, false);
			}
		}*/
	}
	public void showInteractiveButton(bool set)
	{
		interactiveButton.SetActive(set);
	}
}

	

	