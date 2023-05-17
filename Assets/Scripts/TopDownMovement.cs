using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1f;

    private Vector2 input;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        rigidbody.velocity = input * moveSpeed;    
        
    }
}
