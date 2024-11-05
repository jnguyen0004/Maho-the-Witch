using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private float attackCooldown;
	[SerializeField] private Transform wavePoint;
	[SerializeField] private GameObject[] magics;
	[SerializeField] private AudioClip attackSound;

	private Animator anim;
	private PlayerMovement playerMovement;
	private float cooldownTimer = Mathf.Infinity;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	//Player input for attack
	private void Update()
	{
		if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
			Attack();

		cooldownTimer += Time.deltaTime;
	}

	private void Attack()
	{
		//Call attack sound upon trigger
		SoundManager.instance.PlaySound(attackSound);

		//Play attack animation
		anim.SetTrigger("attack");
		cooldownTimer = 0;

		//Sets direction of magic attack
		magics[FindMagic()].transform.position = wavePoint.position;
		magics[FindMagic()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
	}

	private int FindMagic()
	{
		for (int i = 0; i < magics.Length; i++)
		{
			if (!magics[i].activeInHierarchy)
				return i;
		}
		return 0;
	}
}