using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision coliision)
    {
        //Debug.Log(gameObject.name + " collided with " + coliision.gameObject.name);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
    }
}