using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button play;
    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private string sceneName;

    private void Start()
    {
        loadCanvas.SetActive(false);
        play.onClick.AddListener(() => SceneLoad(sceneName));
    }

    public void SceneLoad(string sceneName)
    {
        loadCanvas.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            Debug.Log(asyncOperation.progress);

            slider.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
    }
}
