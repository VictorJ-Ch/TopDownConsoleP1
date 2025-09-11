using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject controls;

    private void Start()
    {
        controls.SetActive(false);
    }
}
