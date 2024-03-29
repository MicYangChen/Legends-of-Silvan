using TMPro;
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
    PlayerArtifacts playerArtifacts;

    public GameObject subWeaponSlotObject;
    private EquippedSlot subWeaponEquippedSlot;
    public GameObject accessorySlotObject;
    private EquippedSlot accessoryEquippedSlot;

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
        else if (context.started && doubleJump && CanMove && IsAlive && !uiManager.openUI && (accessoryEquippedSlot.itemName == "Skybound Grimoire" || accessoryEquippedSlot.itemName == "Goddess Grace"))
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
        }
        else if (!subWeaponEquippedSlot.slotInUse)
        {
            Debug.Log("Player does not have a bow equipped!");
        }
    }

    // Fire Attack
    public float castFireCooldown = 0.5f;
    private bool isCastFireOnCooldown = false;
    private float castFireCooldownTimer = 0f;

    // Wind Attack
    public float castWindCooldown = 0.5f;
    private bool isCastWindOnCooldown = false;
    private float castWindCooldownTimer = 0f;

    // Electric Attack
    public float castElectricCooldown = 1.5f;
    private bool isCastElectricOnCooldown = false;
    private float castElectricCooldownTimer = 0f;

    public void OnCast(InputAction.CallbackContext context)
    {
        if ((playerManaSystem.currentMana >= 15) && !isCastFireOnCooldown && context.started && !uiManager.openUI && playerArtifacts.fireArtifactInUse)
        {
            Debug.Log("Cast condition met. Triggering action.");
            animator.SetTrigger(AnimationStrings.fireAttackTrigger);
            playerManaSystem.UseMana(15);

            isCastFireOnCooldown = true;
            castFireCooldownTimer = castFireCooldown;
        }
        if ((playerManaSystem.currentMana >= 20) && !isCastWindOnCooldown && context.started && !uiManager.openUI && playerArtifacts.windArtifactInUse)
        {
            Debug.Log("Cast condition met. Triggering action.");
            animator.SetTrigger(AnimationStrings.windAttackTrigger);
            playerManaSystem.UseMana(20);

            isCastWindOnCooldown = true;
            castWindCooldownTimer = castWindCooldown;
        }
        if ((playerManaSystem.currentMana >= 30) && !isCastElectricOnCooldown && context.started && !uiManager.openUI && playerArtifacts.electricArtifactInUse)
        {
            Debug.Log("Cast condition met. Triggering action.");
            animator.SetTrigger(AnimationStrings.electricAttackTrigger);
            playerManaSystem.UseMana(30);

            isCastElectricOnCooldown = true;
            castElectricCooldownTimer = castElectricCooldown;
        }
        else if (uiManager.openUI)
        {
            Debug.Log("UI is open!");
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
        playerArtifacts = GetComponent<PlayerArtifacts>();
        subWeaponSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/SubWeaponSlot");
        accessorySlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/LeftPanel/AccessorySlot");
        subWeaponEquippedSlot = subWeaponSlotObject.GetComponent<EquippedSlot>();
        accessoryEquippedSlot = accessorySlotObject.GetComponent<EquippedSlot>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void Update()
    {
        // Fire Attack Cooldown
        if (isCastFireOnCooldown)
        {
            castFireCooldownTimer -= Time.deltaTime;

            if (castFireCooldownTimer <= 0)
            {
                // Cooldown is over
                isCastFireOnCooldown = false;
            }
        }

        // Wind Attack Cooldown
        if (isCastWindOnCooldown)
        {
            castWindCooldownTimer -= Time.deltaTime;

            if (castWindCooldownTimer <= 0)
            {
                // Cooldown is over
                isCastWindOnCooldown = false;
            }
        }

        // Electric Attack Cooldown
        if (isCastElectricOnCooldown)
        {
            castElectricCooldownTimer -= Time.deltaTime;

            if (castElectricCooldownTimer <= 0)
            {
                // Cooldown is over
                isCastElectricOnCooldown = false;
            }
        }
    }
}
