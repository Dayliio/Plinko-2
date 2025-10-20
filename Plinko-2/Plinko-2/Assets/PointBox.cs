using UnityEngine;

public class PointBox : MonoBehaviour
{
    public string collisionTag = "Ball";
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(collisionTag))
        {
            Destroy(other.gameObject);
        }
        
    }

}
