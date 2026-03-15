using UnityEngine;

public class AimGun : MonoBehaviour
{
    [Header("References")]
    public Camera cam; // assign or it will use Camera.main

    [Header("Options")]
    public float angleOffsetDegrees = 0f; // if sprite points up by default, try 90 (or -90)
    public bool flipOnLeft = true;

    private Vector3 initialScale;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (Vector2)(mouseWorld - transform.position);

        if (dir.sqrMagnitude < 0.0001f) return;

        bool isLeft = dir.x < 0f;

        // Flip only the X scale when aiming left
        if (flipOnLeft && isLeft)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);

        // Angle to mouse (0° = right)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // When flipped, rotate 180 so the sprite's "front" still points at the cursor
        if (flipOnLeft && isLeft)
            angle += 180f;

        angle += angleOffsetDegrees;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
