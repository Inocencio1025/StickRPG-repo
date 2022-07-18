using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOverWorldController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
  
    

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(movementInput != Vector2.zero)  // if not zero, try to move
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    bool TryMove(Vector2 directions)
    {
        int count = rb.Cast(
          directions,
          movementFilter,
          castCollisions,
          moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * directions);
            return true;
        }
        else return false; 
    }

    public void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    
  
}
