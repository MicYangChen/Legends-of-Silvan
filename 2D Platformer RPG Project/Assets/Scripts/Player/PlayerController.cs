using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;
    PlayerStats playerStats;
    UIManager uiManager;
    PlayerManaSystem playerManaSystem;

    public GameObject subWeaponSlotObject;
    private EquippedSlot subWeaponEquippedSlot;
    public GameObject artifactSlotObject;
    private EquippedSlot artifactEquippedSlot;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float airWalkSpeed = 6f;
    public float airRunSpeed = 12;
    public float jumpImpulse = 8f;
    public bool doubleJump;

    Vector2 moveInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Air
                        if (IsRunning)
                        {
                            return airRunSpeed;
                        }
                        else
                        {
                            return airWalkSpeed;
                        }
                    }
                }
                else
                {
                    // Idle
                    return 0;
                }
            }
            else if (IsAttacking && !CanMove)
            {
                return walkSpeed / 2.5f;
            }
            else
            {
                // Movement locked
                return 0; 
            }
        }
    }

    [SerializeField] private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField] private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool IsAttacking
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAttacking);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight 
    { 
        get 
        { 
            return _isFacingRight;  
        }
        private set
        {
            _isFacingRight = value;
        } 
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight && !uiManager.openUI)
        {
            // Face the right
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight && !uiManager.openUI)
        {
            // Face the left
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive && !uiManager.openUI)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started && !uiManager.openUI)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove && IsAlive && !uiManager.openUI)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            doubleJump = true;
        }
        else if (context.started && doubleJump && CanMove && IsAlive && !uiManager.openUI && (artifactEquippedSlot.itemName == "Skybound Grimoire" || artifactEquippedSlot.itemName == "Goddess Grace"))
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse - 0.5f);
            doubleJump = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started && !uiManager.openUI)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && subWeaponEquippedSlot.slotInUse && !uiManager.openUI)
        {
            Debug.Log("Ranged attack condition met. Triggering action.");
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
            playerManaSystem.UseMana(20);
        }
        else if (uiManager.openUI)
        {
            Debug.Log("UI is open!");
        }
        else if (!subWeaponEquippedSlot.slotInUse)
        {
            Debug.Log("Player does not have a bow equipped!");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerManaSystem = GetComponent<PlayerManaSystem>();
        subWeaponSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/LeftPanel/SubWeaponSlot");
        artifactSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactSlot");
        subWeaponEquippedSlot = subWeaponSlotObject.GetComponent<EquippedSlot>();
        artifactEquippedSlot = artifactSlotObject.GetComponent<EquippedSlot>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
}
