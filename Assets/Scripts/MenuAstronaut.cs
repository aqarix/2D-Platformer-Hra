using UnityEngine;

public class MenuAstronaut : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;

    [Header("Direction Changes (movement only)")]
    public float directionChangeInterval = 1.2f; // seconds between new target direction
    public float directionJitterDeg = 60f;       // max deviation from current direction
    public float turnSpeedDegPerSec = 180f;      // how fast movement direction can turn

    [Header("Constant Spin (visual only)")]
    public float spinDegPerSec = 180f;           // constant rotation speed (degrees/sec)

    [Header("Wrap")]
    public float padding = 0.1f;

    Camera cam;
    SpriteRenderer sr;

    Vector2 currentDir;
    Vector2 targetDir;
    float nextChangeTime;

    void Awake()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();

        currentDir = Random.insideUnitCircle.normalized;
        if (currentDir == Vector2.zero) currentDir = Vector2.right;

        targetDir = currentDir;
        nextChangeTime = Time.time + directionChangeInterval;
    }

    void Update()
    {
        // Pick new movement target direction occasionally
        if (Time.time >= nextChangeTime)
        {
            targetDir = PickNewDirection(currentDir);
            nextChangeTime = Time.time + directionChangeInterval;
        }

        // Smoothly steer movement direction (does NOT affect visual rotation)
        float maxStep = turnSpeedDegPerSec * Time.deltaTime;
        currentDir = RotateTowards(currentDir, targetDir, maxStep).normalized;

        // Move
        transform.position += (Vector3)(currentDir * speed * Time.deltaTime);

        // Constant spin (independent of direction)
        transform.Rotate(0f, 0f, spinDegPerSec * Time.deltaTime);

        // Screen wrap
        Wrap();
    }

    Vector2 PickNewDirection(Vector2 fromDir)
    {
        float baseAngle = Mathf.Atan2(fromDir.y, fromDir.x) * Mathf.Rad2Deg;
        float newAngle = baseAngle + Random.Range(-directionJitterDeg, directionJitterDeg);
        float rad = newAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    Vector2 RotateTowards(Vector2 from, Vector2 to, float maxDegrees)
    {
        float fromAngle = Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg;
        float toAngle = Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg;
        float newAngle = Mathf.MoveTowardsAngle(fromAngle, toAngle, maxDegrees);
        float rad = newAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    void Wrap()
    {
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        Vector2 half = sr.bounds.extents;
        Vector3 p = transform.position;

        if (p.x > max.x + half.x + padding) p.x = min.x - half.x - padding;
        else if (p.x < min.x - half.x - padding) p.x = max.x + half.x + padding;

        if (p.y > max.y + half.y + padding) p.y = min.y - half.y - padding;
        else if (p.y < min.y - half.y - padding) p.y = max.y + half.y + padding;

        transform.position = p;
    }
}
