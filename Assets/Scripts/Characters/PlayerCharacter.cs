using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(CharacterEquipment))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : BaseCharacter
{
    [Header("Yetenekler")]
    public BaseSkill currentSkill;

    private Rigidbody rb;
    private PlayerStats playerStats;
    private Animator animator;
    private Vector3 movementInput;
    private float skillCooldownTimer = 0f;
    private bool isMovementLocked = false;
    [Header("Rotation Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 10f;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMovementLocked)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            movementInput = new Vector3(horizontal, 0, vertical);
        }
        else // Hareket kilitliyse, girdi vektörünü sıfırla.
        {
            movementInput = Vector3.zero;
        }

        UpdateAnimator(movementInput);

        HandleAttackInput();
        HandleStatSpending();
        HandleItemUseInput();
        HandleCooldown();
        HandleRotation();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleRotation()
    {
        // Ana kameradan fare imlecinin olduğu yere bir ışın oluştur.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Bu ışın "Ground" katmanındaki bir nesneye çarparsa...
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            // Karakterin pozisyonundan ışının çarptığı noktaya bir yön belirle.
            var direction = hitInfo.point - transform.position;
            direction.y = 0; // Karakterin yukarı/aşağı eğilmesini engelle.

            // Eğer bir yön varsa (karakter kendi üzerine tıklamadıysa)
            if (direction.magnitude > 0.1f)
            {
                // O yöne doğru bir rotasyon oluştur.
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // Karakterin mevcut rotasyonundan hedef rotasyona doğru yumuşak bir geçiş yap.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void LockMovement()
    {
        isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        isMovementLocked = false;
    }

    private void UpdateAnimator(Vector3 movement)
    {
        animator.SetFloat("Speed", movement.magnitude);
        Vector3 localVelocity = transform.InverseTransformDirection(movement.normalized);
        animator.SetFloat("VelocityX", localVelocity.x);
        animator.SetFloat("VelocityZ", localVelocity.z);
    }

    private void HandleMovement()
    {
        Vector3 normalizedMovement = movementInput.normalized;
        // Time.deltaTime yerine Time.fixedDeltaTime kullanıyoruz.
        rb.MovePosition(rb.position + normalizedMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && skillCooldownTimer <= 0)
        {
            Attack();
        }
    }

    private void HandleItemUseInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.UseFirstConsumable(this);
        }
    }

    public override void Attack()
    {
        if (currentSkill != null)
        {
            bool isTriggred = currentSkill.Activate(this);
            skillCooldownTimer = currentSkill.cooldown;
            if (isTriggred)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    protected override void Die()
    {
        animator.SetTrigger("Die");
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void HandleCooldown()
    {
        if (skillCooldownTimer > 0)
        {
            skillCooldownTimer -= Time.deltaTime;
        }
    }

    private void HandleStatSpending()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { playerStats.SpendStatPoint(StatType.Strength); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { playerStats.SpendStatPoint(StatType.Dexterity); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { playerStats.SpendStatPoint(StatType.Intelligence); }
    }
}