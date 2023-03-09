using TMPro;
using UnityEngine;

namespace Runner.Features.Reward
{
    internal class CurrencySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;

        public void SetData(int count) =>
            _count.text = count.ToString();
    }
}
