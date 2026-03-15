using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom")]
    public float zoomSpeed = 5f;          // How fast zoom changes
    public float minOrthoSize = 2f;       // Smallest zoom (closest)
    public float maxOrthoSize = 20f;      // Largest zoom (farthest)

    [Header("Zoom Toward Mouse (2D feel)")]
    public bool zoomTowardMouse = true;
    public float panOnZoomStrength = 0.5f; // 0 = no pan, 1 = strong

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y; // + up, - down
        if (Mathf.Approximately(scroll, 0f)) return;

        if (cam.orthographic)
        {
            ZoomOrthographic(scroll);
        }
        else
        {
            ZoomPerspective(scroll);
        }
    }

    void ZoomOrthographic(float scroll)
    {
        // World point under mouse before zoom
        Vector3 mouseWorldBefore = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldBefore.z = transform.position.z;

        // Adjust ortho size (smaller = zoom in)
        float target = cam.orthographicSize - scroll * zoomSpeed;
        float clamped = Mathf.Clamp(target, minOrthoSize, maxOrthoSize);

        // If clamped prevents change, do nothing
        if (Mathf.Approximately(clamped, cam.orthographicSize)) return;

        cam.orthographicSize = clamped;

        if (!zoomTowardMouse) return;

        // World point under mouse after zoom
        Vector3 mouseWorldAfter = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldAfter.z = transform.position.z;

        // Move camera so the point under cursor stays (roughly) under cursor
        Vector3 delta = mouseWorldBefore - mouseWorldAfter;
        transform.position += delta * panOnZoomStrength;
    }

    void ZoomPerspective(float scroll)
    {
        // Perspective zoom by moving camera along its forward axis
        // (not common for pure 2D, but included)
        float move = scroll * zoomSpeed;
        transform.position += transform.forward * move;
    }
}
