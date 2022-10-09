using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using GridBlast.InputSystem;

namespace GridBlast.GridSystem.Nodes
{
    public class DefaultNode : Node, IClickable, ICollectable
    {
        [SerializeField] private SpriteRenderer icon;

        [SerializeField] private int minCollectionAmount;

        [SerializeField] private float clickAnimationScaleAmount;
        [SerializeField] private float clickAnimationDuration;

        [Space]
        [SerializeField] private float collectAnimationScaleAmount;
        [SerializeField] private float collectAnimationDuration;

        public bool Clicked { get; set; }

        private Vector3 defaultScale;
        private Vector3 defaultIconScale;

        private InputController inputController;

        [Inject]
        public void Construct(InputController inputController)
        {
            this.inputController = inputController;
        }

        private void Start()
        {
            defaultScale = transform.localScale;
            defaultIconScale = icon.transform.localScale;

            icon.enabled = false;
        }

        public void Click()
        {
            if(Clicked)
                return;

            Clicked = true;
            inputController.enabled = false;

            icon.enabled = true;
            icon.transform.localScale = Vector3.zero;

            Sequence clickSequence = DOTween.Sequence();
            clickSequence.AppendCallback(() =>
            {
                transform.localScale = defaultScale;
            });
            clickSequence.Append(transform.DOPunchScale(Vector3.one * clickAnimationScaleAmount, clickAnimationDuration, 1, 1));
            clickSequence.Join(icon.transform.DOScale(defaultIconScale, clickAnimationDuration).SetEase(Ease.OutBack));
            clickSequence.AppendCallback(() =>
            {
                List<Node> clickedNodes = GetClickedNeighbours();
                clickedNodes.Add(this);

                if(clickedNodes.Count >= minCollectionAmount)
                {
                    Debug.Log($"{clickedNodes.Count} nodes collected");

                    foreach(var node in clickedNodes)
                    {
                        if(node.TryGetComponent(out ICollectable collectable))
                        {
                            collectable.Collect();
                        }
                    }
                }
                else
                {
                    inputController.enabled = true;
                }
            });
        }

        public void Collect()
        {
            Clicked = false;

            Sequence collectionSequence = DOTween.Sequence();
            collectionSequence.AppendCallback(() =>
            {
                transform.localScale = defaultScale;
            });
            collectionSequence.Append(transform.DOPunchScale(Vector3.one * collectAnimationScaleAmount, collectAnimationDuration, 1, 1));
            collectionSequence.Join(icon.transform.DOScale(0f, collectAnimationDuration));
            collectionSequence.AppendCallback(() =>
            {
                if(!inputController.enabled)
                {
                    inputController.enabled = true;
                }
            });
        }

        public class Factory : PlaceholderFactory<DefaultNode>
        {

        }
    }
}
