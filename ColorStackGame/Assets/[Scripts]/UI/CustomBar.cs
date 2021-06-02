using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CustomBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public float BarVal => slider.value;
        public float MaxVal => slider.maxValue;

        public void SetMaxVal(float val)
        {
            slider.maxValue = val;
        }

        public void SetVal(float val)
        {
            slider.value = val;
        }

        public void AddVal(float val)
        {
            slider.value += val;
        }
    }
}