using UnityEngine;

public class CameraFollow : MonoBehaviour


{
    public Transform rig;                 
    public float smoothTimeXZ = 0.2f;     
    public float smoothTimeY = 0.3f;      
    private float rotationSmoothTime = 0.03f; 

    private Vector3 targetPos;           
    private Vector3 velocityXZ = Vector3.zero;
    private float velocityY = 0f;

    void FixedUpdate()
    {
       
        if (rig != null)
        {
            targetPos = rig.position;
        }
    }

    void LateUpdate()
    {
        if (rig == null) return;

        Vector3 currentPos = transform.position;

 
        Vector3 newPosXZ = Vector3.SmoothDamp(
            new Vector3(currentPos.x, 0, currentPos.z),
            new Vector3(targetPos.x, 0, targetPos.z),
            ref velocityXZ,
            smoothTimeXZ
        );

     
        float newY = Mathf.SmoothDamp(currentPos.y, targetPos.y, ref velocityY, smoothTimeY);

        transform.position = new Vector3(newPosXZ.x, newY, newPosXZ.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, rig.rotation, rotationSmoothTime);
    }
}
