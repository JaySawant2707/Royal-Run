using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    Vector2 movement;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 newPos = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        newPos.x = Mathf.Clamp(newPos.x, -xClamp, xClamp);
        newPos.z = Mathf.Clamp(newPos.z, -zClamp, zClamp);
        rb.MovePosition(newPos);
    }
}
