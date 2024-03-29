﻿using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject bullet; 	// RS: currently our blue circle sprite
								// JH changed to a smaller black circle, more like a bullet :-p
	// public GameObject bullet2;
	// public GameObject bullet3;

	public GameObject grenade;

	public float shotCD = 0.5f;	// RS: can be changed in inspector, may not be the best implementation
	public KeyCode code = KeyCode.Z;

	// JH adding code for rapid fire gun
	public KeyCode daka = KeyCode.X;

	// JH adding code for grenade gun
	public KeyCode boom = KeyCode.C;

	// JH adding code for shotgun
	public KeyCode shotgun = KeyCode.V;

	private float multiplier; 	// RS: is the distance between player and bullet's instantiation
	private Movement player; 	// RS: gets the player's movement script for "facing"

	private float shotStamp;

	public bool shooting;
	Vector3 firingUpAdj = new Vector3(-18, 2, 0); 
	Vector3 firingLeftAdj = new Vector3(-50, 10, 0); // 
	Vector3 firingDownAdj = new Vector3(5, 0, 0);
	Vector3 firingRightAdj = new Vector3(50, 10, 0);

	//JH added ammo implementation
	private Ammo playerAmmo;
	Health health;

	int pushForce = 250;
	int grenForce = 150;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<Movement>();
		shotStamp = 0;
		multiplier = 1;
		shooting = false;
		playerAmmo = GameObject.Find("Player").GetComponent<Ammo>();
		health = GameObject.Find("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// RS: if the player wants to shoot, we make sure that it has been 
		//  long enough of a cooldown
		if(Input.GetKeyDown (code) && (! shooting) && (health.health > 0) )
		{
			GameObject shooter;

			switch(player.facing)
			{
			case 1:
				// shooter = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				shooter = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + multiplier * Vector3.up , Quaternion.identity);
				shooter.rigidbody2D.velocity = new Vector2(0, pushForce);
				break;
			case 2:
				shooter = (GameObject) Instantiate(bullet, (transform.position + firingLeftAdj) + multiplier * Vector3.left , Quaternion.identity);
				shooter.rigidbody2D.velocity = new Vector2(-pushForce, 0);
				break;
			case 3:
				shooter = (GameObject) Instantiate(bullet, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				shooter.rigidbody2D.velocity = new Vector2(0, -pushForce);
				break;
			case 4:
				shooter = (GameObject) Instantiate(bullet, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				shooter.rigidbody2D.velocity = new Vector2(pushForce, 0);
				break;
			}

			// RS: means next time player can shoot will be the current time + however long
			//	the cooldown is
			shotStamp = Time.time + shotCD;
			shooting = true;
		}

		// JH if/switch for machine gun attack
		// fires 3 bullets at one
		// each bullet has a slightly different start point and a 
		// slightly different speed
		if( (Input.GetKeyDown (daka) ) && (! shooting) && (playerAmmo.rapidFire > 0) && (health.health > 0))
		{
			GameObject rapid1;
			GameObject rapid2;
			GameObject rapid3;

			switch(player.facing)
			{
			case 1:
				rapid1 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 35 * multiplier * Vector3.up , Quaternion.identity);
				rapid1.rigidbody2D.velocity = (new Vector2(0, pushForce) );

				rapid2 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 30 * multiplier * Vector3.up , Quaternion.identity);
				rapid2.rigidbody2D.velocity = (new Vector2(0, pushForce - 40) );

				rapid3 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				rapid3.rigidbody2D.velocity = (new Vector2(0, pushForce - 80) );
				break;

			case 2:
				Vector3 fire1left = firingLeftAdj + new Vector3(-10, 0, 0);
				rapid1 = (GameObject) Instantiate(bullet, (transform.position + fire1left) + multiplier * Vector3.left , Quaternion.identity);
				rapid1.rigidbody2D.velocity = (new Vector2(-pushForce, 0) );

				Vector3 fire2left = firingLeftAdj + new Vector3(-5, 0, 0);
				rapid2 = (GameObject) Instantiate(bullet, (transform.position + fire2left) + multiplier * Vector3.left, Quaternion.identity);
				rapid2.rigidbody2D.velocity = (new Vector2(-pushForce + 40, 0) );


				rapid3 = (GameObject) Instantiate(bullet, (transform.position + firingLeftAdj) + multiplier * Vector3.left, Quaternion.identity);
				rapid3.rigidbody2D.velocity = (new Vector2(-pushForce + 80, 0) );

				break;

			case 3:
				Vector3 fire1down = firingDownAdj + new Vector3(0, -10, 0);
				rapid1 = (GameObject) Instantiate(bullet, (transform.position + fire1down) + multiplier * Vector3.down , Quaternion.identity);
				rapid1.rigidbody2D.velocity = (new Vector2(0, -pushForce) );

				Vector3 fire2down = firingDownAdj + new Vector3(0, -5, 0);
				rapid2 = (GameObject) Instantiate(bullet, (transform.position + fire2down) + multiplier * Vector3.down , Quaternion.identity);
				rapid2.rigidbody2D.velocity = (new Vector2(0, -pushForce + 40) );

				rapid3 = (GameObject) Instantiate(bullet, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				rapid3.rigidbody2D.velocity = (new Vector2(0, -pushForce + 80) );
				break;

			case 4:
				Vector3 fire1right = firingRightAdj + new Vector3(10, 0, 0);
				rapid1 = (GameObject) Instantiate(bullet, (transform.position + fire1right) + multiplier * Vector3.right , Quaternion.identity);
				rapid1.rigidbody2D.velocity = (new Vector2(pushForce, 0) );

				Vector3 fire2right = firingRightAdj + new Vector3(5, 0, 0); // 
				rapid2 = (GameObject) Instantiate(bullet, (transform.position + fire2right) + multiplier * Vector3.right , Quaternion.identity);
				rapid2.rigidbody2D.velocity = (new Vector2(pushForce - 40, 0) );

				rapid3 = (GameObject) Instantiate(bullet, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				rapid3.rigidbody2D.velocity = (new Vector2(pushForce - 80, 0) );
				break;
			}

			// RS: means next time player can shoot will be the current time + however long
			//	the cooldown is
			shotStamp = Time.time + shotCD;
			shooting = true;

			// JH costs one bullet to fire rapid fire
			playerAmmo.rapidFire -=1;
		}

		if(Input.GetKeyDown (boom) && (! shooting) && (playerAmmo.grenades > 0) &&(health.health > 0) )
		{
			GameObject gren1;
			
			switch(player.facing)
			{
			case 1:
				gren1 = (GameObject) Instantiate(grenade, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				gren1.rigidbody2D.velocity = new Vector2(0, grenForce);
				gren1.transform.Rotate(new Vector3(0, 0, 1), 270);
				break;
			case 2:
				gren1 = (GameObject) Instantiate(grenade, (transform.position + firingLeftAdj) + multiplier * Vector3.left , Quaternion.identity);
				gren1.rigidbody2D.velocity = new Vector2(-grenForce, 0);
				break;
			case 3:
				firingDownAdj = new Vector3(5, -35, 0);
				gren1 = (GameObject) Instantiate(grenade, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				gren1.rigidbody2D.velocity = new Vector2(0, -grenForce);
				gren1.transform.Rotate(new Vector3(0, 0, 1), 90);
				break;
			case 4:
				gren1 = (GameObject) Instantiate(grenade, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				gren1.rigidbody2D.velocity = new Vector2(grenForce, 0);
				gren1.transform.Rotate(Vector3.up, 180);
				break;
			}
			
			// RS: means next time player can shoot will be the current time + however long
			//	the cooldown is
			shotStamp = Time.time + shotCD;
			shooting = true;

			playerAmmo.grenades -= 1;
		}

		// JH created a similar if/switch shotgun attacks
		// shotgun shoots 3 bullets, but the range is short 
		// need to create a new prefab to be bullets that are short range 
		if( (Input.GetKeyDown (shotgun) ) && (! shooting) && (playerAmmo.sGun > 0) && (health.health > 0) )
		{
			GameObject shell1;
			GameObject shell2;
			GameObject shell3;

			switch(player.facing)
			{
			case 1:
				shell1 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				shell1.rigidbody2D.velocity = new Vector2(0, pushForce);
				
				shell2 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				shell2.rigidbody2D.velocity = (new Vector2(100, pushForce) );
				
				shell3 = (GameObject) Instantiate(bullet, (transform.position + firingUpAdj) + 25 * multiplier * Vector3.up , Quaternion.identity);
				shell3.rigidbody2D.velocity += (new Vector2(-100, pushForce) );
				break;
			case 2:
				shell1 = (GameObject) Instantiate(bullet, (transform.position + firingLeftAdj) + multiplier * Vector3.left , Quaternion.identity);
				shell1.rigidbody2D.velocity = new Vector2(-pushForce, 0);

				shell2 = (GameObject) Instantiate(bullet, (transform.position + firingLeftAdj) + multiplier * Vector3.left , Quaternion.identity);
				shell2.rigidbody2D.velocity = (new Vector2(-pushForce, 100) );
				
				shell3 = (GameObject) Instantiate(bullet, (transform.position + firingLeftAdj) + multiplier * Vector3.left , Quaternion.identity);
				shell3.rigidbody2D.velocity = (new Vector2(-pushForce, -100) );
				break;
			case 3:
				shell1 = (GameObject) Instantiate(bullet, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				shell1.rigidbody2D.velocity = new Vector2(0, -pushForce);

				shell2 = (GameObject) Instantiate(bullet, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				shell2.rigidbody2D.velocity = (new Vector2(-100, -pushForce) );
				
				shell3 = (GameObject) Instantiate(bullet, (transform.position + firingDownAdj) + multiplier * Vector3.down , Quaternion.identity);
				shell3.rigidbody2D.velocity = (new Vector2(100, -pushForce) );
				break;
			case 4:
				shell1 = (GameObject) Instantiate(bullet, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				shell1.rigidbody2D.velocity = new Vector2(pushForce, 0);

				shell2 = (GameObject) Instantiate(bullet, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				shell2.rigidbody2D.velocity = (new Vector2(pushForce, -100) );
				
				shell3 = (GameObject) Instantiate(bullet, (transform.position + firingRightAdj) + multiplier * Vector3.right , Quaternion.identity);
				shell3.rigidbody2D.velocity = (new Vector2(pushForce, 100) );
				break;
			}
			
			// RS: means next time player can shoot will be the current time + however long
			//	the cooldown is
			shotStamp = Time.time + shotCD;
			shooting = true;

			// JH costs one shell to fire the S gun
			playerAmmo.sGun -= 1;
		}

		if(Time.time > shotStamp)
		{
			shooting = false;
		}
	}
}
