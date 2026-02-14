using UnityEngine;

public class Kart : MonoBehaviour
{
    private KartController controller;
    public Rigidbody rb;

    [SerializeField]
    private ColorDetecter _rightPS;

    [SerializeField]
    private ColorDetecter _leftPS;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        controller = new KartController(5, rb, _leftPS, _rightPS);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(transform);
    }
}
