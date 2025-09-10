using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        IHealth playersHealth = collision.collider.GetComponent<IHealth>();
        if (playersHealth != null)
        {
            playersHealth.TakeDamage(damageAmount);
            gameObject.SetActive(false);
        }
    }
}
