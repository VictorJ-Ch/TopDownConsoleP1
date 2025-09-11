using UnityEngine;
using UnityEngine.UI;

public class QuitApp : MonoBehaviour
{ 
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}