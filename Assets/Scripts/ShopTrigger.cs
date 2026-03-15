using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string shopRootTag;

    private GameObject shopPanel;

    void Start()
    {
        GameObject root = GameObject.FindGameObjectWithTag(shopRootTag);

        if (root == null)
        {
            Debug.LogError("Shop root with tag not found: " + shopRootTag);
            return;
        }

        // vezme první child panel
        shopPanel = root.transform.GetChild(0).gameObject;

        shopPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (shopPanel != null)
            shopPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (shopPanel != null)
            shopPanel.SetActive(false);
    }
}