using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için bu satır gerekli!

public class MainMenuManager : MonoBehaviour
{
    // Bu metot, "Başla" butonuna tıklandığında çalışacak.
    // Sahne adının doğru yazıldığından emin olmalıyız.
    public void StartGame()
    {
        // "Game" isimli sahneyi yükle.
        SceneManager.LoadScene("Game");
    }

    // Bu metot, "Çık" butonuna tıklandığında çalışacak.
    public void QuitGame()
    {
        // Konsola bir mesaj yazarak çalıştığını görelim.
        Debug.Log("Çıkış yapıldı!");

        // Oyunu kapat. (Bu komut sadece build alınmış oyunda çalışır, editörde değil)
        Application.Quit();
    }
}