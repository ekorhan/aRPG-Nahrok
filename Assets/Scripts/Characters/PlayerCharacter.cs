using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("Yetenekler")]
    public BaseSkill currentSkill; // Oyuncunun şu anki yeteneği

    private float skillCooldownTimer = 0f;

    // Update metodunu basitleştiriyoruz.
    void Update()
    {
        HandleMovement();
        HandleCooldown();

        // Sol tıka basıldığında ve bekleme süresi dolduğunda saldır
        if (Input.GetMouseButtonDown(0) && skillCooldownTimer <= 0)
        {
            Attack();
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