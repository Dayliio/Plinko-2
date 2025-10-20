using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ball_bounce : MonoBehaviour
{
    [Tooltip("The material whose bounciness you want to randomize.")]
    public PhysicsMaterial2D targetMaterial;

    [Tooltip("The minimum bounciness value (inclusive).")]
    [Range(0.1f, 0.7f)]
    public float minBounciness = 0.1f;

    [Tooltip("The maximum bounciness value (inclusive).")]
    [Range(0.1f, 0.7f)]
    public float maxBounciness = 0.7f;

    [Tooltip("Whether to randomize the bounciness on Start.")]
    public bool randomizeOnStart = true;

    void Start()
    {
        if (randomizeOnStart)
        {
            RandomizeBounciness();
        }
    }

    /// <summary>
    /// Randomizes the bounciness of the target material within the specified range.
    /// </summary>
    public void RandomizeBounciness()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Target Material is not assigned!");
            return;
        }

        // Generate a random float between minBounciness and maxBounciness.
        float randomBounciness = Random.Range(minBounciness, maxBounciness);

        // Apply the random bounciness to the material.
        targetMaterial.bounciness = randomBounciness;

        Debug.Log($"Bounciness randomized to: {randomBounciness} on material: {targetMaterial.name}");
    }

}
