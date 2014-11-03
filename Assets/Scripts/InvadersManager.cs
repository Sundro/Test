using UnityEngine;
using System.Collections;

public class InvadersManager : MonoBehaviour
{
	public Transform invader1Prefab;
	public Transform invader2Prefab;
	public Transform invader3Prefab;
	public Transform invader;
	public Transform[] invaderTab;
	public float vitesseDeplacement;
	public int score = 0;
	public bool defaite;
	private GUIText victoireLabel;
	public bool vide = true;
	public Transform flyingSaucerPrefab;
	public Transform saucer;
	public bool saucerBool = false;
	public Transform saucerBulletPrefab;
	public Transform saucerBullet;
	public float timerSaucer = 0f;
	
	private bool goingRight = true;
	
	// Use this for initialization
	void Start ()
	{
		victoireLabel = GameObject.Find("VictoireLabel").guiText;
		vitesseDeplacement = 10f;
		invaderTab = new Transform[55];
		int cpt = 0;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j<11; j++){
				if (i == 0)
				{
					invader = Instantiate(invader3Prefab) as Transform;
				}
				if (i > 0 && i < 3)
				{
					invader = Instantiate(invader2Prefab) as Transform;
				}
				if (i >= 3) 
				{
					invader = Instantiate(invader1Prefab) as Transform;
				}
				invader.transform.position = new Vector3(j*3f - 5.5f*3f, i*3f - 2*3f, 0f);
				invader.parent = transform;
				invaderTab[cpt] = invader;
				cpt++;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		timerSaucer += Time.deltaTime;
		//Gestion du FlyingSaucer
		if (!saucerBool) {
			if (Random.value+Random.value > 1.9) {
					saucer = Instantiate (flyingSaucerPrefab) as Transform;
					saucer.transform.position = new Vector3 (-38f, 10f, 0f);
					saucerBool = true;
			}
		} else {
			saucer.transform.Translate(new Vector3(0.2f, 0f, 0f));	
			shoot ();
		}

		//Deplacement des invaders si le tableau n'est pas vide
		vide = true;
		for (int i = 0; i < 55; i++) {
			if (invaderTab [i] != null) {
				vide = false;
				if (goingRight) {
					invaderTab [i].transform.Translate (new Vector3 (vitesseDeplacement* Time.deltaTime, 0f, 0f));
				} else {
					invaderTab [i].transform.Translate (new Vector3 (-vitesseDeplacement * Time.deltaTime, 0f, 0f));
				}
			}	
		}
		if (vide) {
			victoireLabel.text = "Victoire !";
			Time.timeScale = 0f;	
			if (saucerBool)
			{
				destroySaucer(saucer);
			}
		}
	}

	public void moveInvadersCloser ()
	{
		goingRight = !goingRight;
		Debug.Log (goingRight);
		vitesseDeplacement += 3f;
		for (int i = 0; i < 55; i++)
		{
			if (invaderTab[i] != null)
			{
				invaderTab[i].transform.Translate(new Vector3(0f, -40f*Time.deltaTime, 0f));
			}
		}
	}

	private void shoot()
	{
		if (timerSaucer > 0.4f) {
			saucerBullet = Instantiate(saucerBulletPrefab) as Transform;
			if (saucerBullet != null) 
			{
				saucerBullet.transform.position = new Vector3(saucer.transform.position.x, saucer.transform.position.y, 0f);
				saucerBullet.rigidbody2D.velocity = new Vector2(0f, -15f);
			}
			timerSaucer = 0f;
		}
	}

	public void destroySaucer (Transform saucer)
	{
		Destroy (saucer.gameObject);
		saucerBool = false;
	}

	public void destroyInvader (Transform invader)
	{
		score += 1;
		for (int i = 0; i < 55; i++) {
			if (invaderTab[i] == invader)
			{
				Destroy (invader.gameObject);
				Destroy (invaderTab[i].gameObject);
			}
		}
	}
}
