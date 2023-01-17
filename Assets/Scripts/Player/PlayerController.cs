using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float speed = 5;
	private float inputX = 0;
	private float inputY = 0;
	private Rigidbody2D body;
    private bool canMove = true;

    [SerializeField] private float interactionRadius = 1.5f;

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	void Update() {
        if (canMove)
        {
            CaptureInput();
            CheckForInteraction();
        }
	}

	void FixedUpdate() {
		PlayerMovement();
	}

	void CaptureInput() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
    }

	void PlayerMovement() {
		body.velocity = new Vector2(inputX, inputY).normalized * speed;
	}

    void CheckForInteraction() {
        var allInteractables = new List<Interactable>(FindObjectsOfType<Interactable>());
        var target = allInteractables.Find(delegate (Interactable interactable) {
            return (interactable.transform.position - this.transform.position) // check if in range
                .magnitude <= interactionRadius;
        });
        if (target != null) {
            if (Input.GetKeyDown(KeyCode.Space)){
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
}