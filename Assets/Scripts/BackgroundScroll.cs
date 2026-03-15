using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speed2;

    [SerializeField] Renderer BgRenderer;
    void Update()
    {
        BgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime,speed2 * Time.deltaTime);
    }
}
