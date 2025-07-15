using UnityEngine;

// Bu satır, Unity editöründe Sağ Tık > Create menüsüne yeni bir seçenek ekler.
// fileName: Oluşturulacak dosyanın varsayılan adı.
// menuName: Menüdeki yolu.
[CreateAssetMenu(fileName = "New_BaseSkill", menuName = "aRPG-Nahrok/Skills/Base Skill")]
public abstract class BaseSkill : ScriptableObject // MonoBehaviour yerine ScriptableObject'ten miras alıyor.
{
    [Header("Yetenek Bilgileri")]
    public string skillName;
    [TextArea] public string skillDescription; // TextArea, editörde daha büyük bir yazı alanı sağlar.
    public float cooldown = 1f;

    // Her yetenek, nasıl aktive olacağını kendi içinde tanımlamalı.
    // "caster" parametresi, bu yeteneği kimin kullandığını belirtir (Oyuncu mu, canavar mı?).
    public abstract void Activate(BaseCharacter caster);
}