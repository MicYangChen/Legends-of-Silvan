using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses the collider to check direction, if the object is ground, wall or ceiling
public class TouchingDirections : MonoBehaviour
{
    CapsuleCollider2D touchingCol;
    Animator animator;

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField] private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        { 
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }


}
