using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private bool moving = false;
	private Vector3 target;
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

		if (!moving)
		{
			if (inputX != 0f || inputY != 0f)
			{
				// Started moving.
				moving = true;
				target = transform.position;
				if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
				{
					// Prefer horizontal movement.
					if (inputX < 0.0f)
					{
						// Move left
						target.x -= 1f;
					}
					else
					{
						target.x += 1f;
					}
				}
				else
				{
					// Prefer vertical movement
					if (inputY < 0f)
					{
						// Move down
						target.y -= 1f;
					}
					else
					{
						// Move up
						target.y += 1f;
					}
				}
			}
		}

		// Move to our target
		if (moving)
		{
			Vector3 oldPos = transform.position;
			Vector3 dir = target - oldPos;

			if (dir.sqrMagnitude != 0f)
			{
				dir.Normalize();
				anim.SetFloat("SpeedX", dir.x);
				anim.SetFloat("SpeedY", dir.y);
				anim.SetBool("Walking", true);

				dir *= Time.deltaTime * speed;

				Vector3 newPos = oldPos + dir;
				
				// Test to see if we've past the target
				if ((target - oldPos).sqrMagnitude <= (newPos - oldPos).sqrMagnitude)
				{
					// We've reached (and passed?) the target.
					newPos = target;
					moving = false;
					anim.SetFloat("SpeedX", 0f);
					anim.SetFloat("SpeedY", 0f);
					anim.SetBool("Walking", false);
				}
				transform.position = newPos;
			}
		}
	}

	void FixedUpdate()
	{
		float lastInputX = Input.GetAxis("Horizontal");
		float lastInputY = Input.GetAxis("Vertical");

		anim.SetFloat("LastMoveX", lastInputX < 0.0f ? -1.0f : lastInputX > 0.0f ? 1.0f : 0.0f);
		anim.SetFloat("LastMoveY", lastInputY < 0.0f ? -1.0f : lastInputY > 0.0f ? 1.0f : 0.0f);

	}

}
