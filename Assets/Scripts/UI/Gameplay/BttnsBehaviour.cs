using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BttnsBahviour : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;

    private void Start()
    {
        button.onClick.AddListener(ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
