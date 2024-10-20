using RoR2;
using UnityEngine;

namespace FreeCostFix
{
    public class RefreshDisplayOnAwake : MonoBehaviour
    {
        public CostHologramContent HologramContent;

        void Awake()
        {
            // HACK: "Random" value to semi-guarantee it gets refreshed the first frame
            HologramContent.oldDisplayValue = -346893567;
        }
    }
}
