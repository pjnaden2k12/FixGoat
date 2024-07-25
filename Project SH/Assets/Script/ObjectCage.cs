using UnityEngine;

public class ObjectCage1 : MonoBehaviour
{
    public Rigidbody2D objectToContain;
    public BoxCollider2D cageBounds;

    void FixedUpdate()
    {
        Vector2 clampedPosition = objectToContain.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, cageBounds.bounds.min.x, cageBounds.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, cageBounds.bounds.min.y, cageBounds.bounds.max.y);
        objectToContain.position = clampedPosition;
    }
}
