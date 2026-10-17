using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float hunger = 100f;
    public float stamina = 100f;

    public float hungerDecreaseRate = 0.5f;
    public float staminaDecreaseRate = 15f;
    public float staminaRegenRate = 10f;

    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider staminaSlider;

    void Update()
    {
        // 1. Hunger turun terus
        hunger -= hungerDecreaseRate * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, 100);

        // 2. Cek lari (Shift + WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = (x != 0 || z != 0);
        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        if (isMoving && isHoldingShift && stamina > 0)
        {
            stamina -= staminaDecreaseRate * Time.deltaTime;
        }
        else if (!isHoldingShift || !isMoving)
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, 100);

        // 3. Update UI
        if (healthSlider != null) healthSlider.value = health;
        if (hungerSlider != null) hungerSlider.value = hunger;
        if (staminaSlider != null) staminaSlider.value = stamina;
    }
}