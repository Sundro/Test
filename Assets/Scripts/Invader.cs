using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour
{
	/// <summary>
	/// The invaders manager.
	/// </summary>
	private InvadersManager invadersManager;
	
	private GUIText scoreLabel;
	
	// Use this for initialization
	void Start ()
	{
		invadersManager = GetComponentInParent<InvadersManager>();
		scoreLabel = GameObject.Find("ScoreLabel").guiText;
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreLabel.text = "Score: " + invadersManager.score;
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.CompareTag("Bullet"))
		{
			Destroy (collision.gameObject);
			invadersManager.destroyInvader(transform);
		}
	}
}
