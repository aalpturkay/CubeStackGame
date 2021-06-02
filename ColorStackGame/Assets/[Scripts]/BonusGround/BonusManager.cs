using System.Collections.Generic;
using UnityEngine;

namespace BonusGround
{
    public class BonusManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _bonusAreas;
        private string _bonusTextString;
        private float bonusVal;
        public List<Transform> BonusAreas => _bonusAreas;

        public float BonusVal
        {
            get => bonusVal;
            set => bonusVal = value;
        }
        public string BonusTextString
        {
            get => _bonusTextString;
            set => _bonusTextString = value;
        }
    }
}