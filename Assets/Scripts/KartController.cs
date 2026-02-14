using UnityEngine;

public class KartController
{
    public float maxSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    public float turnSpeed = 100f;
    public float driftFactor = 0.95f;

    private Rigidbody rb;
    private ColorDetecter _rigthPS;
    private ColorDetecter _leftPS;
    private float currentSpeed = 0f;

    public KartController(int speed, Rigidbody rb, ColorDetecter left, ColorDetecter right)
    {
        this.maxSpeed = speed;
        this.rb = rb;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rigthPS = right;
        _leftPS = left;
    }

    public void Move(Transform transform)
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            currentSpeed += verticalInput * acceleration * Time.fixedDeltaTime;
        }
        else
        {
            if (currentSpeed > 0f)
                currentSpeed -= deceleration * Time.fixedDeltaTime;
            else if (currentSpeed < 0f)
                currentSpeed += deceleration * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        Vector3 forwardMove = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);

        if (Mathf.Abs(horizontalInput) > 0.9f)
        {
            float turn =
                horizontalInput * turnSpeed * Time.fixedDeltaTime * (currentSpeed / maxSpeed);
            if (Mathf.Abs(turn) >= 1.7)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, 100f))
                {
                    Debug.Log("hit");
                    Debug.DrawRay(transform.position, -transform.up, Color.red);
                }
                if (!_rigthPS.isPlaying || !_leftPS.isPlaying)
                {
                    _rigthPS.StartParticle();
                    _leftPS.StartParticle();
                }
            }

            //Debug.Log(turn);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }
        else
        {
            if (_leftPS.isPlaying || _rigthPS.isPlaying)
            {
                _leftPS.StopParticle();
                _rigthPS.StopParticle();
            }
            ;
        }

        Vector3 lateralVelocity = Vector3.Dot(rb.linearVelocity, transform.right) * transform.right;
        rb.linearVelocity -= lateralVelocity * (1 - driftFactor);
    }
}
