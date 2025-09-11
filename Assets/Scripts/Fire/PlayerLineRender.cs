using UnityEngine;

public class PlayerLineRender : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxDistance;

    private void Update()
    {
        if (lineRenderer == null || bulletSpawn == null) return;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, bulletSpawn.position);

        Vector3 direction = bulletSpawn.forward;
        Vector3 endPoint = bulletSpawn.position + direction * maxDistance;

        lineRenderer.SetPosition(1, endPoint);
    }
}
