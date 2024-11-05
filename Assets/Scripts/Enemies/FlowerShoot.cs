using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerShoot : MonoBehaviour
{
	[SerializeField] private float attackCooldown;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject[] pellets;
	private float cooldownTimer;

	private void Attack()
	{
		cooldownTimer = 0;

		pellets[FindPellet()].transform.position = firePoint.position;
		pellets[FindPellet()].GetComponent<EnemyProjectile>().ActivateProjectile();
	}
	private int FindPellet()
	{
		for (int i = 0; i < pellets.Length; i++)
		{
			if (!pellets[i].activeInHierarchy)
				return i;
		}
		return 0;
	}
	private void Update()
	{
		cooldownTimer += Time.deltaTime;

		if (cooldownTimer >= attackCooldown)
			Attack();
	}
	
}