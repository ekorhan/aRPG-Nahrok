using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("Yetenekler")]
    public BaseSkill currentSkill; // Oyuncunun şu anki yeteneği
    private PlayerStats playerStats;

    private float skillCooldownTimer = 0f;

    protected override void Awake()
    {
        base.Awake(); // BaseCharacter'daki Awake'i çağırmak önemli!
        playerStats = GetComponent<PlayerStats>();
    }

    // Update metodunu basitleştiriyoruz.
    void Update()
    {
        HandleMovement();
        HandleCooldown();
        HandleAttack();

        // --- YENİ EKLENEN SATIR ---
        HandleStatSpending();
        // --- YENİ SATIR SONU ---

        // Sol tıka basıldığında ve bekleme süresi dolduğunda saldır
        if (Input.GetMouseButtonDown(0) && skillCooldownTimer <= 0)
        {
            Attack();
        }
    }

    private void HandleStatSpending()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Klavyenin üstündeki 1 tuşu
        {
            playerStats.SpendStatPoint(StatType.Strength);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 tuşu
        {
            playerStats.SpendStatPoint(StatType.Dexterity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 tuşu
        {
            playerStats.SpendStatPoint(StatType.Intelligence);
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void HandleCooldown()
    {
        if (skillCooldownTimer > 0)
        {
            skillCooldownTimer -= Time.deltaTime;
        }
    }

    // Attack metodu artık çok basit! Sadece kuşanılmış yeteneği aktive ediyor.
    public override void Attack()
    {
        if (currentSkill != null)
        {
            currentSkill.Activate(this); // "this" ile yeteneği kimin kullandığını (kendimizi) söylüyoruz.
            skillCooldownTimer = currentSkill.cooldown; // Yeteneğin bekleme süresini başlat.
        }
        else
        {
            Debug.LogWarning("Kuşanılmış bir yetenek yok!");
        }
    }
}