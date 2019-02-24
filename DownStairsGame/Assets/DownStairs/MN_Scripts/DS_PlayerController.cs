using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class DS_PlayerController : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	public GameObject controller;

	private bool autoMove;
	private DS_Spawner obj;
	float newx = 0f;
	private int score;
	float directionX;
	// Use this for initialization
	void Start () {
		score = 0;
		rb = this.GetComponent<Rigidbody2D> ();
		obj = controller.GetComponent<DS_Spawner> ();
		autoMove = false;
		obj.scoreText.text = "" + score;
	}
	// Update is called once per frame
	void FixedUpdate () {
		directionX = CrossPlatformInputManager.GetAxis ("Horizontal");

		if (directionX < 0) {
			newx = directionX;
		} else if(directionX>0) {
			newx = directionX;
		}
		if (autoMove) {
			
			if (newx > 0) {
				newx = 1.5f;
			} else {
				newx = -1.5f;
			}


			rb.MovePosition (rb.position + Vector2.right * newx * Time.fixedDeltaTime*speed);
		} else {
			
			rb.MovePosition (rb.position + Vector2.right * directionX * Time.fixedDeltaTime * speed);

		}
	}
		

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("top")) {
			obj.LifeCount ();

		}else if(coll.gameObject.CompareTag ("LossLife"))
			{
			obj.LifeCount ();
			playerScore ();
			}
		 else if (coll.gameObject.CompareTag ("Disappear")) {
			StartCoroutine (waitDisappear (coll));
			playerScore ();
		} else if (coll.gameObject.CompareTag ("Slippery")) {
			autoMove = true;
			playerScore ();
		}
		else if(coll.gameObject.CompareTag ("Slow")||coll.gameObject.CompareTag ("General"))
		{
			playerScore ();
		}
			
	}

	void playerScore()
	{
		score++;
		obj.scoreText.text = "" + score;
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("bottom")) {
			
			obj.gameOverPanel.gameObject.SetActive (true);
			this.gameObject.SetActive (false);
			Time.timeScale = 0;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Slippery")) {
			autoMove = false;
		}
	}

	IEnumerator waitDisappear(Collision2D coll)
	{
		yield return new WaitForSeconds (.5f);
		Destroy (coll.gameObject);
	}
}
