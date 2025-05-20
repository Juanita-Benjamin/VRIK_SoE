using UnityEngine;

public class AvatarAligner : MonoBehaviour
{
    public Transform headBone;
    public Transform footBone;
    public Transform userHead;

    public void AlignAvatarScaleToUser()
    {
        Debug.Log("[AvatarAligner] Received Head: " + (headBone ? headBone.name : "NULL"));
        Debug.Log("[AvatarAligner] Head Position: " + (headBone ? headBone.position.ToString("F3") : "N/A"));

        Debug.Log("[AvatarAligner] Received Foot: " + (footBone ? footBone.name : "NULL"));
        Debug.Log("[AvatarAligner] Foot Position: " + (footBone ? footBone.position.ToString("F3") : "N/A"));

        Debug.Log("[AvatarAligner] Received UserHead: " + (userHead ? userHead.name : "NULL"));
        Debug.Log("[AvatarAligner] User Head Position: " + (userHead ? userHead.position.ToString("F3") : "N/A"));

        if (headBone == null || footBone == null || userHead == null)
        {
            Debug.LogWarning("[AvatarAligner] Missing reference(s): headBone, footBone, or userHead.");
            return;
        }

        // Get floor height
        float floorY = GetFloorY();
        Debug.Log($"[AvatarAligner] Detected floorY: {floorY:F3}");
        // Calculate user and avatar height
        //float simulatedUserHeight = 1.0f; // Uncomment this line for testing purposes.
        //float userHeight = simulatedUserHeight;// Uncomment this line for testing purposes.

        float userHeight = userHead.position.y - floorY;  // comment this line when testing
        float avatarHeight = headBone.position.y - footBone.position.y;
        Debug.Log($"[AvatarAligner] Computed UserHeight: {userHeight:F3}, AvatarHeight: {avatarHeight:F3}");

        if (avatarHeight <= 0.01f)
        {
            Debug.LogWarning("[AvatarAligner] Avatar height too small or invalid.");
            return;
        }

        // scaling
        // scaling
        float scaleRatio = userHeight / avatarHeight;
        Debug.Log("[AvatarAligner] Before scaling: " + transform.localScale);
        transform.localScale = new Vector3(1f, scaleRatio, 1f);  //  Only scale Y-axis
        Debug.Log("[AvatarAligner] After scaling: " + transform.localScale);
        // Rebind animator to avoid IK distortion
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }

        // After scaling, re-read foot Y position and compute offset
        float newFootY = footBone.position.y;
        float offsetToGround = newFootY - floorY;

        // Correct avatar position
        transform.position -= new Vector3(0f, offsetToGround, 0f);

        Debug.Log($"[AvatarAligner] FloorY={floorY:F3}, UserHeight={userHeight:F3}, AvatarHeight={avatarHeight:F3}, Scale={scaleRatio:F3}, OffsetY={offsetToGround:F3}");
    }

    private float GetFloorY()
    {
        if (OVRManager.boundary != null)
        {
            var geometry = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
            if (geometry != null && geometry.Length > 0)
            {
                Debug.Log("[AvatarAligner] FloorY from OVR Boundary.");
                return geometry[0].y;
            }
        }

        // If that fails, raycast from user's head downwards
        if (userHead != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(userHead.position, Vector3.down, out hit, 5f))
            {
                Debug.Log($"[AvatarAligner] FloorY from Raycast: {hit.point.y:F3}");
                return hit.point.y;
            }
        }

        // Fallback value（th current floor Y)
        Debug.LogWarning("[AvatarAligner] Failed to get floor. Using fallback -0.2977392");
        //Debug.LogWarning("[AvatarAligner] Failed to get floor. Using fallback 0");
        //return -0.2977392f; //use 0 in mine
        return 0; //use 0 in mine
    }

}
