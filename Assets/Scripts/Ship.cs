using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	/// <summary>
	/// The bullet prefab.
	/// </summary>
	public Transform bulletPrefab;
	private Transform bullet;
	private GUIText livesLabel;
	public float timer = 0f;
	public float vitesseTir = 0.3f;
	public int nblives = 3;
	private GUIText defaiteLabel;
	public bool boolDefaite = false;
	
	// Use this for initialization
	void Start ()
	{
		livesLabel = GameObject.Find("LivesLabel").guiText;
		defaiteLabel = GameObject.Find("DefaiteLabel").guiText;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (nblives <= 0) {
			defaiteLabel.text = "Défaite !";
			boolDefaite = true;
		}
		if (boolDefaite) {
			Time.timeScale = 0;		
		}
		livesLabel.text = "Lives: " + nblives;
		
		// Left movement
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (-16 * Time.deltaTime, 0, 0);
		}
		// Right movement
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (16 * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey (KeyCode.Space)) {
			if (timer > vitesseTir) {
				shoot ();
				timer = 0f;
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			vitesseTir -= 0.1f;
		}
		timer += Time.deltaTime;
	}
	
	private void shoot()
	{
		bullet = Instantiate(bulletPrefab) as Transform;
		if (bullet != null) 
		{
			bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			bullet.rigidbody2D.velocity = new Vector2(0f, 15f);
		}
	}
	
	public bool getBoolDefaite()
	{
		return boolDefaite;
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.CompareTag("Invader"))
		{
			Destroy(collision.collider.gameObject);
			nblives -= 1;
			transform.position = new Vector3(0, transform.position.y, 0);
		}
	}
}
