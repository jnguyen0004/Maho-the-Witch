using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
    
    
}