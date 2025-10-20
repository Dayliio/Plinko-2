using UnityEngine;
using TMPro;

public class Player_Movement : MonoBehaviour
{
    // variables for spawning balls
    public GameObject ballPrefab;
    public Transform spawnPoint;
    private float spacebarHoldTime = 0f;
    private bool isSpacebarHeld = false;
    private float spawnInterval = 0.25f;
    private float nextSpawnTime = 0f;
    
    //variables for movement of spawn block
    public float moveSpeed = 5f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    private Rigidbody2D rb;

    //For ball count system
    public Ball_Count ball_Count;
    double Balls = Ball_Count.balls;
    public TextMeshProUGUI countText;
    void Start()
    {
        SetCountText();
        
    
    // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.  Please attach a Rigidbody2D to the GameObject.");
            // You might want to add a Rigidbody2D component in code if one doesn't exist.
            // rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }
    void FixedUpdate()
    {
        float horizontalInput = 0f; // Initialize to 0

        if(Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        // Calculate the velocity
        Vector2 velocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Apply the velocity to the Rigidbody2D
        rb.linearVelocity = velocity;

        // Clamp the position of the object within the boundaries
        Vector2 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        transform.position = clampedPosition;
    }
     // Update is called once per frame
    void Update()
    {
        // Check if the space bar is being held down
        if (Input.GetKey(KeyCode.Space))
        {
            isSpacebarHeld = true;
            spacebarHoldTime += Time.deltaTime;

            if (spacebarHoldTime >= .5f && Time.time >= nextSpawnTime && Balls > 0)
            {
                SummonBallObject();
                nextSpawnTime = Time.time + spawnInterval; // Set next spawn time
            }
        }
        // Check if the space bar is released
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (spacebarHoldTime < .5f && Balls > 0)
            {
                SummonBallObject(); // Spawn one ball on tap
            }
            spacebarHoldTime = 0f; // Reset
            isSpacebarHeld = false;
        }
        else if (isSpacebarHeld)
        {
             if (spacebarHoldTime >= .5f && Time.time >= nextSpawnTime && Balls > 0)
            {
                SummonBallObject();
                nextSpawnTime = Time.time + spawnInterval; // Set next spawn time
                
            }
        }
    }

    void SummonBallObject()
    {
        Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        Balls = Balls -1;
        Debug.Log(Balls);
        SetCountText();
    }
    void SetCountText()
    {
        countText.text = "Balls: " + Balls.ToString();
    }
    
    
}
