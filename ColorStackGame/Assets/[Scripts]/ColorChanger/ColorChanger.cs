using System;
using ColorSpace;
using UnityEngine;

namespace ColorChanger
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private ColorType.Colors colorChangerColor;


        private ColorType.Colors _changerColorType;

        public ColorType.Colors ChangerColorType => _changerColorType;

        private void Awake()
        {
            _changerColorType = colorChangerColor;
        }
    }
}