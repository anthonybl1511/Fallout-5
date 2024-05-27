using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float speed = 11f;
    [SerializeField] private float airControl = 1f;

    [SerializeField] private LayerMask floorLayer;

    private Rigidbody rb;


    private bool isGrounded;
    private Vector2 input;


    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - Vector3.up, 0.1f, floorLayer);
    }

    public void Jump()
    {
        if (isGrounded && !PipBoy.instance.getActive())
        {
            rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        }
    }

    public void MovePlayer(Vector2 _input)
    {
        if(!PipBoy.instance.getActive())
        {
            input = _input;
        }
    }

    private void FixedUpdate()
    {
        float friction;

        if (isGrounded)
        {
            friction = 1;
            rb.drag = 8;
        }
        else
        {
            friction = 0.2f;
            rb.drag = 0.5f * airControl;
        }

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        Vector3 targetVelocity = cameraForward * input.y + Camera.main.transform.right * input.x;
        targetVelocity = targetVelocity.normalized * speed;


        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -10, 10), Mathf.Clamp(rb.velocity.y, -10, 10), Mathf.Clamp(rb.velocity.z, -10, 10));
        rb.AddForce(new Vector3(targetVelocity.x, 0, targetVelocity.z) * Time.fixedDeltaTime * 500 * friction, ForceMode.Force);
    }

}
