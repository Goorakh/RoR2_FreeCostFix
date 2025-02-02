using RoR2;
using UnityEngine;

namespace FreeCostFix
{
    public class RefreshDisplayOnAwake : MonoBehaviour
    {
        public CostHologramContent HologramContent;

        void Awake()
        {
            if (!HologramContent)
            {
                HologramContent = GetComponent<CostHologramContent>();
            }

            if (HologramContent)
            {
                // HACK: "Random" value to semi-guarantee it gets refreshed the first frame
                HologramContent.oldDisplayValue = -346893567;
            }
        }
    }
}
