using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public float health = 1;

    public void TakeDamage(float dmgAmount)
    {
        health -= dmgAmount;

        if (health <= 0f)
            Destroy(gameObject);
        
    }
}