using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Detects when the player enters the hazard trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call TakeDamage from GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(1);
            }
        }
    }
}