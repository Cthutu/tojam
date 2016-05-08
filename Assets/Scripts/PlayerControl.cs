using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private bool moving = false;
	private Vector3 target;
	public float speed = 1.0f;
	private Animator anim;
	private int mapX = 8, mapY = 4;
    private bool inputEnabled = false;
    private int lives = 3;

	Vector3 getWorldPosition()
	{
		return new Vector3(-8f + (float)mapX, 4f - (float)mapY, -1f);
	}

	int GetWorldObject(int x, int y)
	{
		return GameObject.Find("Grid").GetComponent<GridRenderer>().GetWorldId(x, y);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		transform.position = getWorldPosition();
	}
	
	// Update is called once per frame
	void Update () {

        GameManager gMan = GameManager.GetInstance();
        if(gMan == null || gMan.levelLoaded == false)
        {
            return;
        }

        gMan.UpdatePlayerLives(lives);

        float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		if (!moving && inputEnabled)
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
						if (mapX == 0 || GetWorldObject(mapX - 1, mapY) == 0)
						{
							// Can't move left!
							moving = false;
                            OnHitWall();
						}
						else
						{
							target.x -= 1f;
							--mapX;
						}
					}
					else
					{
						// Move right
						if (mapX == 15 || GetWorldObject(mapX + 1, mapY) == 0)
						{
							// Can't move right
							moving = false;
                            OnHitWall();
                        }
                        else
						{
							target.x += 1f;
							++mapX;
						}
					}
				}
				else
				{
					if (inputY < 0.0f)
					{
						// Move down
						if (mapY == 7 || GetWorldObject(mapX, mapY + 1) == 0)
						{
							// Can't move down!
							moving = false;
                            OnHitWall();
                        }
                        else
						{
							target.y -= 1f;
							++mapY;
						}
					}
					else
					{
						// Move up
						if (mapY == 0 || GetWorldObject(mapX, mapY - 1) == 0)
						{
							// Can't move up
							moving = false;
                            OnHitWall();
                        }
                        else
						{
							target.y += 1f;
							--mapY;
						}
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
				Vector3 movement = dir;
				anim.SetFloat("SpeedX", dir.x);
				anim.SetFloat("SpeedY", dir.y);
				anim.SetFloat("LastMoveX", dir.x);
				anim.SetFloat("LastMoveY", dir.y);
				Debug.Log(dir);
				anim.SetBool("Walking", true);

				movement *= Time.deltaTime * speed;

				Vector3 newPos = oldPos + movement;
				
				// Test to see if we've past the target
				if ((target - oldPos).sqrMagnitude <= (newPos - oldPos).sqrMagnitude)
				{
					// We've reached (and passed?) the target.
					newPos = target;
					moving = false;
					anim.SetFloat("SpeedX", 0f);
					anim.SetFloat("SpeedY", 0f);
					anim.SetFloat("LastMoveX", dir.x);
					anim.SetFloat("LastMoveY", dir.y);
					Debug.Log(dir);
					anim.SetBool("Walking", false);
					GameObject.Find("Grid").GetComponent<GridRenderer>().Colour(mapX, mapY);
				}
				transform.position = newPos;
			}
		}
	}

    public void EnablePlayerInput(bool _enabled)
    {
        inputEnabled = _enabled;
    }

    void OnHitWall()
    {
        GetComponent<ActorSounds>().TriggerSound("wallbump");
    }
}
