using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerHUD
{
    public class PlayerHUDView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void SetHealth(float current, float max)
        {
            _healthSlider.maxValue = max;
            _healthSlider.value = current;
        }
    }
}