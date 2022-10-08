using UnityEngine;
using DG.Tweening;

namespace GridBlast.GridSystem.Nodes
{
    public class DefaultNode : Node, IClickable
    {
        [SerializeField] private SpriteRenderer icon;

        [SerializeField] private float clickAnimationScaleAmount;
        [SerializeField] private float clickAnimationDuration;

        public bool Clicked { get; set; }

        private void Start()
        {
            icon.enabled = false;
        }

        public void Click()
        {
            if(Clicked)
                return;

            icon.enabled = true;

            Vector3 targetIconScale = icon.transform.localScale;
            icon.transform.localScale = Vector3.zero;

            transform.DOPunchScale(Vector3.one * clickAnimationScaleAmount, clickAnimationDuration, 1, 1);
            icon.transform.DOScale(targetIconScale, clickAnimationDuration).SetEase(Ease.OutBack);

            Clicked = true;
        }
    }
}
