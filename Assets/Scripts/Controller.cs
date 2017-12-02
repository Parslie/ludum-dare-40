using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller : MonoBehaviour {

    private float skinWidth = 0.01f;

    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float unitsPerRay;
    private int horizontalRayCount, verticalRayCount;
    private float horizontalRaySpacing, verticalRaySpacing;

    private BoxCollider2D coll;
    private RaycastOrigins raycastOrigins;
    public CollisionInfo collInfo;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        CalculateRaySpacing();
    }

    public void Move(Vector2 velocity)
    {
        collInfo.Reset();
        CalculateRayOrigins();

        if (velocity.x != 0)
            HorizontalRayCasting(ref velocity);
        if (velocity.y != 0)
            VerticalRayCasting(ref velocity);

        transform.Translate(velocity);
    }

    private void HorizontalRayCasting(ref Vector2 velocity)
    {
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        float dir = Mathf.Sign(velocity.x);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (dir == 1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            rayOrigin += Vector2.up * horizontalRaySpacing * i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dir, rayLength, collisionMask);

            if (hit)
            {
                rayLength = hit.distance;
                velocity.x = (hit.distance - skinWidth) * dir;

                collInfo.right = dir == 1;
                collInfo.left = dir == -1;

                SendMessage("OnCollisionH", hit.transform.gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void VerticalRayCasting(ref Vector2 velocity)
    {
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        float dir = Mathf.Sign(velocity.y);

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (dir == 1) ? raycastOrigins.topLeft : raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * verticalRaySpacing * i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dir, rayLength, collisionMask);

            if (hit)
            {
                rayLength = hit.distance;
                velocity.y = (hit.distance - skinWidth) * dir;

                collInfo.top = dir == 1;
                collInfo.bottom = dir == -1;
            }
        }
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(-2 * skinWidth);

        horizontalRayCount = (int)Mathf.Ceil(bounds.size.y / unitsPerRay);
        verticalRayCount = (int)Mathf.Ceil(bounds.size.x / unitsPerRay);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private void CalculateRayOrigins()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(-2 * skinWidth);

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    private struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool top, bottom;
        public bool right, left;

        public void Reset()
        {
            top = bottom = false;
            right = left = false;
        }
    }
}
