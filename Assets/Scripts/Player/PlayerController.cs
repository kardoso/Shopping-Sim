using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] public float speed = 5;
	private float inputX = 0;
	private float inputY = 0;
	private Rigidbody2D body;

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	void Update() {
		CaptureInput();
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
}