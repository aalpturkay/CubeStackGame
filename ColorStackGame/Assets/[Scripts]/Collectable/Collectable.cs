using ColorSpace;
using UnityEngine;

namespace Collectable
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private ColorType.Colors collectableColor;

        public ColorType.Colors CollectableColor => collectableColor;
    }
}