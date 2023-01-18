using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float inputX = 0;
    private float inputY = 0;
    // Needs direction for animation
    private float directionX = 0;
    private float directionY = 0;
    private Rigidbody2D body;
    private bool canMove = true;

    public RuntimeAnimatorController defaultLegsAnimator;
    public Animator legsAnimator;
    public SpriteRenderer legsSprite;
    public int CurrentLegsItemId { get; private set; } = 0; // 0 is the default value, an item with id 0 must not exist in the game

    public RuntimeAnimatorController defaultHeadAnimator;
    public Animator headAnimator;
    public SpriteRenderer headSprite;
    public int CurrentHeadItemId { get; private set; } = 0; // 0 is the default value, an item with id 0 must not exist in the game

    public RuntimeAnimatorController defaultTorsoAnimator;
    public Animator torsoAnimator;
    public SpriteRenderer torsoSprite;
    public int CurrentTorsoItemId { get; private set; } = 0; // 0 is the default value, an item with id 0 must not exist in the game

    [SerializeField] private float interactionRadius = 1.5f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            CaptureInput();
            CheckForInteraction();
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
        SetAnimationValues();
    }

    void SetAnimationValues()
    {
        var inputIsBeingPressed = Mathf.Abs(inputY) + Mathf.Abs(inputX) > 0;
        if (inputIsBeingPressed)
        {
            directionX = inputX;
            directionY = inputY;
        }

        legsAnimator.SetFloat("moveX", directionX);
        legsAnimator.SetFloat("moveY", directionY);
        headAnimator.SetFloat("moveX", directionX);
        headAnimator.SetFloat("moveY", directionY);
        torsoAnimator.SetFloat("moveX", directionX);
        torsoAnimator.SetFloat("moveY", directionY);

        if (directionX != 0)
        {
            legsSprite.flipX = directionX > 0;
            headSprite.flipX = directionX > 0;
            torsoSprite.flipX = directionX > 0;
        }

        legsAnimator.SetBool("walking", inputIsBeingPressed);
        headAnimator.SetBool("walking", inputIsBeingPressed);
        torsoAnimator.SetBool("walking", inputIsBeingPressed);
    }

    void CaptureInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    void PlayerMovement()
    {
        body.velocity = new Vector2(inputX, inputY).normalized * speed;
    }

    void CheckForInteraction()
    {
        var allInteractables = new List<Interactable>(FindObjectsOfType<Interactable>());
        var target = allInteractables.Find(delegate (Interactable interactable)
        {
            return (interactable.transform.position - this.transform.position) // check if in range
                .magnitude <= interactionRadius;
        });
        if (target != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                target.GetComponent<Interactable>().Interact();
            }
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void ChangeHead(int itemId = 0, RuntimeAnimatorController animatorController = null)
    {
        CurrentHeadItemId = itemId;

        if (animatorController != null)
        {
            headAnimator.runtimeAnimatorController = animatorController;

            return;
        }

        headAnimator.runtimeAnimatorController = defaultHeadAnimator;
    }

    public void ChangeTorso(int itemId = 0, RuntimeAnimatorController animatorController = null)
    {
        CurrentTorsoItemId = itemId;

        if (animatorController != null)
        {
            torsoAnimator.runtimeAnimatorController = animatorController;

            return;
        }

        torsoAnimator.runtimeAnimatorController = defaultTorsoAnimator;
    }

    public void ChangeLegs(int itemId = 0, RuntimeAnimatorController animatorController = null)
    {
        CurrentLegsItemId = itemId;

        if (animatorController != null)
        {
            legsAnimator.runtimeAnimatorController = animatorController;

            return;
        }

        legsAnimator.runtimeAnimatorController = defaultLegsAnimator;
    }
}