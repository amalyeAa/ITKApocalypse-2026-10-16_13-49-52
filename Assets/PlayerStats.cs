using UnityEngine;
using UnityEngine.UI;
using TMPro; // Penting buat TextMeshPro

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
    public float hunger = 100f;
    public float stamina = 100f;

    [Header("Rates")]
    public float hungerDecreaseRate = 0.5f;
    public float staminaDecreaseRate = 15f;
    public float staminaRegenRate = 10f;
    public float minStaminaToRun = 10f; // Minimal stamina buat bisa lari

    [Header("UI References")]
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider staminaSlider;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI staminaText;

    void Update()
    {
        // 1. Hunger turun terus tiap detik
        hunger -= hungerDecreaseRate * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, 100);

        // 2. Logika Stamina
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = (x != 0 || z != 0);
        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        if (isMoving && isHoldingShift && stamina > minStaminaToRun)
        {
            // Kurangi stamina kalau lari
            stamina -= staminaDecreaseRate * Time.deltaTime;
        }
        else
        {
            // Pulihkan stamina kalau jalan biasa atau diam
            stamina += staminaRegenRate * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, 100);

        // 3. Update UI Slider & Teks
        if (healthSlider != null) healthSlider.value = health;
        if (healthText != null) healthText.text = Mathf.RoundToInt(health).ToString();

        if (hungerSlider != null) hungerSlider.value = hunger;
        if (hungerText != null) hungerText.text = Mathf.RoundToInt(hunger).ToString();

        if (staminaSlider != null) staminaSlider.value = stamina;
        if (staminaText != null) staminaText.text = Mathf.RoundToInt(stamina).ToString();
    }

    // Fungsi buat makan (nanti dipanggil saat pakai item makanan)
    public void Eat(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    // Fungsi buat minum obat
    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100);
    }

    // Fungsi buat menerima damage
    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);
        if (health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}