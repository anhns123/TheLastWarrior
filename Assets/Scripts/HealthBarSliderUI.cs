using UnityEngine;
using UnityEngine.UI;

public class HealthBarSliderUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    /// <summary>
    /// Gán giá trị máu tối đa và hiện đầy máu
    /// </summary>
    public void SetMaxHealth(float health)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
    }

    /// <summary>
    /// Cập nhật thanh máu theo lượng máu hiện tại
    /// </summary>
    public void SetHealth(float health)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }
}
