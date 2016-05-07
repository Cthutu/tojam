using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed = 1.0f;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
	
		Vector3 movement = new Vector3(inputX, inputY, 0.0f);
		movement.Normalize();
		movement *= Time.deltaTime * speed;

		transform.Translate(movement);
	}

	void FixedUpdate()
	{
		float lastInputX = Input.GetAxis("Horizontal");
		float lastInputY = Input.GetAxis("Vertical");

		anim.SetBool("Walking", lastInputX != 0.0f || lastInputY != 0.0f);
	}

}
