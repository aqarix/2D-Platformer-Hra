using UnityEngine;

public class ToggleUIWithTab : MonoBehaviour
{
    [Header("Assign the root UI GameObject (panel that contains everything)")]
    public GameObject uiRoot;

    [Header("Optional")]
    public bool startOpen = true;

    void Start()
    {
        if (uiRoot != null)
            uiRoot.SetActive(startOpen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (uiRoot == null) return;

            bool newState = !uiRoot.activeSelf;
            uiRoot.SetActive(newState);
        }
    }
}
