﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 50;		// RS: self-explanatory 
	public bool poisoned = false;	// RS: will make life easier for animation

	private float poisonDamage;		// RS: damage to deal over time
	private float poisonedTime;		// RS: marker for when poison ends

	public Texture death;
	public float timeReload = 0;

	GameObject message;

	// Use this for initialization
	void Start () {
		poisoned = false;
		poisonedTime = Time.time;
	}

	// RS: applies poison damage
	void ApplyPoison()
	{
		ApplyDamage (poisonDamage);
	}

	// RS: sets poison damage
	void SetPoisonDamage(int damage)
	{
		poisonDamage = damage;
	}

	// RS: poisons player and creates DOT (damage over time)
	void PoisonPlayer(float duration){
		poisoned = true;
		poisonedTime = Time.time + duration;
		InvokeRepeating ("ApplyPoison", 1, 1);
	}

	// RS: player takes i damage
	void ApplyDamage (float i) 
	{
		health -= i;

		if (health <= 0) 
		{ // RS: kill
			health = 0;

			if(gameObject.tag != "Player")
			{
				Destroy (gameObject);
			}

			else
			{
				if(timeReload < 1)
				{
					timeReload = Time.time + 5;
					gameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if ( Time.time >= poisonedTime) 
		{
			poisoned = false;
			CancelInvoke("ApplyPoison");
		}
	}

	void OnGUI()
	{
		if( (gameObject.tag == "Player") && (health == 0) )
		{
			if(Time.time <= timeReload)
			{
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), death); 
			}

			else
			{
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
