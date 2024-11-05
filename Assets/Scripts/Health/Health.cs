using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[Header ("Health")]
	[SerializeField] private float startingHealth;
	public float currentHealth { get; private set; }
	private Animator anim;
	private bool dead;

	[Header ("iFrames")]
	[SerializeField] private float iFramesDuration;
	[SerializeField] private float numFlashes;
	private SpriteRenderer spriteRend;

	[Header("Components")]
	[SerializeField] private Behaviour[] components;
	private bool invulnerable;

	[Header ("Hurt Sound")]
	[SerializeField] private AudioClip hurtSound;
	[SerializeField] private AudioClip hurtSound2;

	[Header ("Death Sound")]
	[SerializeField] private AudioClip deathSound;

	private void Awake()
	{
		currentHealth = startingHealth;
		anim = GetComponent<Animator>();
		spriteRend = GetComponent<SpriteRenderer>();
	}
	public void TakeDamage(float _damage)
	{
		currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

		// Trigger hurt animations and sounds
		if (currentHealth > 0)
		{
			anim.SetTrigger("hurt");
			SoundManager.instance.PlaySound(hurtSound);
			SoundManager.instance.PlaySound(hurtSound2);

			//iframes
			StartCoroutine(Invulnerability());

		}
		else
		{
			//Death
			if (!dead)
			{
				anim.SetTrigger("die");

				//Deactivate all attached component classes
				foreach (Behaviour component in components)
					component.enabled = false;

				dead = true;
				SoundManager.instance.PlaySound(deathSound);
			}
		}
	}
	public void AddHealth(float _value)
	{
		currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
	}

	public void Respawn()
	{
		dead = false;
		AddHealth(startingHealth);
		anim.ResetTrigger("die");
		anim.Play("Idle");
		StartCoroutine(Invulnerability());

		//Activate all attached component classes
		foreach (Behaviour component in components)
			component.enabled = true;

	}
	private IEnumerator Invulnerability()
	{
		Physics2D.IgnoreLayerCollision(8, 9, true);

		//invulnerability duration
		for (int i = 0; i < numFlashes; i++)
		{
			spriteRend.color = new Color(1, 1, 1, 0.8f);
			yield return new WaitForSeconds(iFramesDuration / (numFlashes * 2));
			spriteRend.color = Color.white;
			yield return new WaitForSeconds(iFramesDuration / (numFlashes * 2));
		}

		Physics2D.IgnoreLayerCollision(8, 9, false);
		invulnerable = false;

	}

	private void Deactivate()
	{
		gameObject.SetActive(false);
	}

	//Test Damage
	// private void Update()
	// {
	//     if (Input.GetKeyDown(KeyCode.E))
	//         TakeDamage(1);
	// }
}