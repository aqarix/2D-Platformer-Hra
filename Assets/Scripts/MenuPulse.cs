using UnityEngine;

public class JupiterPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float baseScale = 1f;        // normal size
    public float pulseAmount = 0.2f;    // how much bigger/smaller (0.2 = ±20%)
    public float pulseSpeed = 2f;       // pulses per second-ish (higher = faster)

    Vector3 initialScale;

    void Awake()
    {
        // Keep the original proportions (useful if your sprite isn't 1,1,1)
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Sine wave from -1 to +1
        float t = Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2f);

        // Convert to a multiplier around baseScale
        float scaleMultiplier = baseScale + (t * pulseAmount);

        transform.localScale = initialScale * scaleMultiplier;
    }
}
