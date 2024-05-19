using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float speed = 11f;
    [SerializeField] private float airControl = 1f;
    [SerializeField] private float jumpHeight = 3.5f;

    [SerializeField] private LayerMask floorLayer;

    private Rigidbody rb;
    private InputManager InputManager;


    bool isGrounded, isJumping;


    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        InputManager = GetComponent<InputManager>();

        InputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - Vector3.up, 0.1f, floorLayer);


    }

    public InputManager GetInputManager()
    {
        return InputManager;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction;
        direction.y = InputManager.inputMaster.Movement.Forward.ReadValue<float>();
        direction.x = InputManager.inputMaster.Movement.Right.ReadValue<float>();

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

        Vector3 targetVelocity = cameraForward * direction.y + Camera.main.transform.right * direction.x;
        targetVelocity = targetVelocity.normalized * speed;


        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -10, 10), Mathf.Clamp(rb.velocity.y, -10, 10), Mathf.Clamp(rb.velocity.z, -10, 10));
        rb.AddForce(new Vector3(targetVelocity.x, 0, targetVelocity.z) * Time.fixedDeltaTime * 500 * friction, ForceMode.Force);

    }

}
