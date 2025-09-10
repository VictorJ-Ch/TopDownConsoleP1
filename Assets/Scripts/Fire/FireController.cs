using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireController : IFire
{
    [Header("Bullets")]
    private readonly Transform fireSpawn;
    private readonly GameObject bulletPrefab;
    private readonly int maxBullets;
    private readonly List<GameObject> bulletPool;
    private int currentIndex = 0;

    [Header("Animation")]
    private UIManager uIManager;
    private System.Action<System.Collections.IEnumerator> coroutineRunner;
    private bool isReloading = false;
    private bool fireTextShown = false;

    public FireController(Transform fireSpawn, GameObject bulletPrefab, UIManager uIManager, System.Action<IEnumerator> coroutineRunner, int maxBullets = 10)
    {
        this.fireSpawn = fireSpawn;
        this.bulletPrefab = bulletPrefab;
        this.uIManager = uIManager;
        this.coroutineRunner = coroutineRunner;
        this.maxBullets = maxBullets;
        bulletPool = new List<GameObject>(maxBullets);

        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, fireSpawn.position, fireSpawn.rotation);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        uIManager?.UpdateBulletUI(maxBullets, maxBullets);
    }

    public void Fire()
    {
        if (currentIndex >= maxBullets)
        {
            if (!fireTextShown)
            {
                uIManager?.UpdateBulletUI(0, maxBullets);
                fireTextShown = true;
                return;
            }

            isReloading = true;
            fireTextShown = false;
            coroutineRunner?.Invoke(ReloadAfterDelay());
            return;
        }

        GameObject bullet = bulletPool[currentIndex];
        bullet.transform.position = fireSpawn.position;
        bullet.transform.rotation = fireSpawn.rotation;
        bullet.SetActive(true);

        currentIndex++;
        uIManager?.UpdateBulletUI(maxBullets - currentIndex, maxBullets);
    }


    private IEnumerator ReloadAfterDelay()
    {
        uIManager?.StartRechargingAnim();

        yield return new WaitForSeconds(3f);

        for (int bullet = 0; bullet < bulletPool.Count; bullet++)
        {
            bulletPool[bullet].SetActive(false);
        }

        currentIndex = 0;
        isReloading = false;
        uIManager?.UpdateBulletUI(maxBullets, maxBullets);
    }
}