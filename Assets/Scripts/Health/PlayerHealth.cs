using UnityEngine;

public class PLayerHealth : MonoBehaviour, IHealth
{
    private UIManager uIManager;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentPlayersHealth;

    public int currentHealth => currentPlayersHealth;

    private void Awake()
    {
        currentPlayersHealth = maxHealth;

        uIManager = FindAnyObjectByType<UIManager>();
        uIManager?.UpdateHealthSlider(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentPlayersHealth -= amount;
        currentPlayersHealth = Mathf.Max(currentHealth, 0);
        Debug.Log($"+ {amount} damage. Current health: {currentPlayersHealth}");

        uIManager?.UpdateHealthSlider(currentHealth, maxHealth);

        if (currentPlayersHealth <= 0)
        {
            uIManager?.GameOverScreen();
        }
    }
}