using UnityEngine;
using UnityEngine.UI; // Required for the Slider
using TMPro;          // Required for TextMeshPro

public class HeightProgressTracker : MonoBehaviour
{
    [Header("Tracking Objects")]
    [Tooltip("The player object to track.")]
    public Transform player;
    [Tooltip("The object representing the finish line/top.")]
    public Transform finishLine;

    [Header("UI Components")]
    [Tooltip("The UI Slider acting as the progress meter.")]
    public Slider progressMeter;
    [Tooltip("The TextMeshPro object to show percentage.")]
    public TextMeshProUGUI percentageText;

    private float _startY;
    private float _totalDistance;

    void Start()
    {
        if (player == null || finishLine == null)
        {
            Debug.LogError("HeightProgressTracker: Player or Finish Line not assigned!");
            enabled = false;
            return;
        }

        // Record where the player started
        _startY = player.position.y;

        // Calculate the total vertical distance to travel
        _totalDistance = finishLine.position.y - _startY;
    }

    void Update()
    {
        CalculateProgress();
    }

    void CalculateProgress()
    {
        // 1. Calculate how far the player has climbed from the start
        float currentDistance = player.position.y - _startY;

        // 2. Get a value between 0 and 1 (0% to 100%)
        // Mathf.Clamp01 ensures the meter doesn't go below 0 or above 1
        float progress = Mathf.Clamp01(currentDistance / _totalDistance);

        // 3. Update the Slider
        if (progressMeter != null)
        {
            progressMeter.value = progress;
        }

        // 4. Update the Text (Format as integer percentage)
        if (percentageText != null)
        {
            percentageText.text = Mathf.RoundToInt(progress * 100) + "%";
        }
    }
}