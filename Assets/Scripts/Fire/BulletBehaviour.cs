using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3 origin;

    private void Start()
    {
        origin = transform.position;
    }


    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableEnemy>(out var enemy)) { enemy.TakeDamage(); }
        lineRenderer.enabled = false;
        gameObject.SetActive(false);
    }

    public void ResetBullet(Vector3 spawnPosition)
    {
            origin = spawnPosition;

            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, origin);
                lineRenderer.SetPosition(1, origin);
            }
        }
}