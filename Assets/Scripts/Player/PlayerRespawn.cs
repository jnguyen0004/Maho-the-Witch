using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] private AudioClip checkpoint;
	private Transform currentCheckpoint;
	private Health playerHealth;
	private UIManager uiManager;

	private void Awake()
	{
		playerHealth = GetComponent<Health>();
		uiManager = FindObjectOfType<UIManager>();
	}

	public void CheckRespawn()
	{
		//if no checkpoint triggered
		if (currentCheckpoint == null) 
        {
            uiManager.GameOver(); //Show game over screen
            return; // Don't execute the rest of this function
        }

		playerHealth.Respawn(); //Restore player health and reset animation
		transform.position = currentCheckpoint.position; //Move player to checkpoint location

		//Move the camera to the checkpoint's room
		Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
	}

	//Activate Checkpoints
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Checkpoint")
		{
			currentCheckpoint = collision.transform; //Store previously activated checkpoint as current one
			SoundManager.instance.PlaySound(checkpoint);
			collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
			collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
		}
	}
}