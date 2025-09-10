using Mono.Cecil.Cil;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableEnemy>(out var enemy)) { enemy.TakeDamage(); }
        gameObject.SetActive(false);
    }
}