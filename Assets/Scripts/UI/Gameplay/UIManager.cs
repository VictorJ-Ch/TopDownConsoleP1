using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Animation")]
    private Coroutine rechargingAnim;
    [SerializeField] private Animator waveTextAnimator;


    [Header("Health")]
    [SerializeField] private Slider playersUIHealth;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameOverDefaultButton;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject winDefaultButton;

    [Header("Bullets")]
    [SerializeField] private TextMeshProUGUI bulletStatusText;
    [SerializeField] private TextMeshProUGUI bulletCountText;

    [Header("Waves")]
    [SerializeField] private TextMeshProUGUI wavesCount;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }
    public void UpdateHealthSlider(int currentHealth, int maxHealth)
    {
        if (playersUIHealth == null) return;

        playersUIHealth.value = (float)currentHealth / maxHealth;
    }

    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameOverDefaultButton);
        Time.timeScale = 0;
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(winDefaultButton);
        Time.timeScale = 0;
    }

    public void UpdateWaveCounter(int waveNumber)
    {
        if (wavesCount == null) return;

        wavesCount.text = $"Wave {waveNumber}";
        waveTextAnimator.Play("FadeIn", 0, 0f);
    }


    public void UpdateBulletUI(int available, int total)
    {
        if (available > 0)
        {
            bulletStatusText.text = "Bullets";
            bulletCountText.text = $"{available} / {total}";

            if (rechargingAnim != null)
            {
                StopCoroutine(rechargingAnim);
                rechargingAnim = null;
            }
        }
        else
        {
            bulletStatusText.text = ">> Fire <<";
            bulletCountText.text = $"0 / {total}";
        }
    }

    public void StartRechargingAnim()
    { 
        bulletStatusText.text = "Recharging";

        if (rechargingAnim != null) { StopCoroutine(rechargingAnim); }

        rechargingAnim = StartCoroutine(AnimateRechargingText());
    }
    private IEnumerator AnimateRechargingText()
    {
        string[] frames = { ".", "..", "...", "....", "....." };
        int i = 0;

        while (true)
        {
            bulletCountText.text = frames[i % frames.Length];
            i++;
            yield return new WaitForSeconds(0.3f);
        }
    }


    [ContextMenu("Manual UI Update Bullets")]
    private void TestUGUI()
    {
        bulletStatusText.text = "TEST";
        bulletCountText.text = "12 / 34";
    }

    [ContextMenu("Manual UI Restart Bullets")]
    private void RestartTestUGUI()
    {
        bulletStatusText.text = "Bullets";
        bulletCountText.text = "10 / 10";
    }
}
