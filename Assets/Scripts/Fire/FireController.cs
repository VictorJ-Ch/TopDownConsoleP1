using UnityEngine;
using System.Collections.Generic;

public class FireController : IFire
{
    private readonly Transform fireSpawn;
    private readonly GameObject bulletPrefab;
    private readonly int maxBullets;
    private readonly List<GameObject> bulletPool;
    private int currentIndex = 0;

    public FireController(Transform fireSpawn, GameObject bulletPrefab, int maxBullets = 10)
    {
        this.fireSpawn = fireSpawn;
        this.bulletPrefab = bulletPrefab;
        this.maxBullets = maxBullets;
        bulletPool = new List<GameObject>(maxBullets);

        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, fireSpawn.position, fireSpawn.rotation);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public void Fire()
    {
        if (currentIndex >= maxBullets)
        {
            Debug.Log("Out of bullets");
            return;
        }

        GameObject bullet = bulletPool[currentIndex];
        bullet.transform.position = fireSpawn.position;
        bullet.transform.rotation = fireSpawn.rotation;
        bullet.SetActive(true);

        currentIndex++;
        Debug.Log($"Bullet {currentIndex} fired");
    }
}