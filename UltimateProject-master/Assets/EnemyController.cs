using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour
{
	public float Speed = 0.0f;
	public bool IsDead =false;
	public AudioClip DieClip;
	public AudioClip GrowlClip;
	public float GrowlInterval = 0.0f;

	private Rigidbody2D selfRigidbody;
	private AudioSource selfAudioSource;
	private Renderer selfRenderer;
	private int currentDirection = -1;
	private float lastGrowl = 0;
	public void Die()
	{
		Destroy(gameObject);
	}

	void Start()
	{
		selfRigidbody = GetComponent<Rigidbody2D>();
		selfAudioSource = GetComponent<AudioSource>();
		selfRenderer = GetComponent<Renderer>();
	}



	void FixedUpdate()
	{
		selfRigidbody.velocity = new Vector2(currentDirection * Speed, selfRigidbody.velocity.y);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enviroment"))
		{
			currentDirection *= -1;
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
		

		
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Platform"))
		{
			currentDirection *= -1;
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}


	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<PlayerController>() != null && col.gameObject.GetComponent<PlayerController>().IsAttacking)
		{
			GetComponent<Animator>().SetBool("Die", true);
			GetComponent<Collider2D>().enabled = false;
			IsDead = true;
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}
		
	}
}


	
