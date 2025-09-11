using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TogglePanels : MonoBehaviour
{
    [SerializeField] private Button triggerButton;
    [SerializeField] private GameObject panelToEnable;
    [SerializeField] private GameObject panelToDisable;

    private void Start()
    {
        triggerButton.onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        panelToEnable.SetActive(true);
        panelToDisable.SetActive(false);

        Button defaultButton = panelToEnable.GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }
}
