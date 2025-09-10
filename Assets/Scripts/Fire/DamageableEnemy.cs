using UnityEngine;

public class DamageableEnemy : MonoBehaviour
{
    public bool isEnemy;

    public void TakeDamage()
    {
        if (isEnemy) { gameObject.SetActive(false);  }
    }
}