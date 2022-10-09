using UnityEngine;
using GridBlast.GridSystem.Nodes;
using GridBlast.InputSystem;
using Zenject;

public class NodeInstaller : MonoInstaller<NodeInstaller>
{
    [SerializeField] private InputController inputController;
    [SerializeField] private DefaultNode defaultNodePrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(inputController).AsSingle().NonLazy();

        Container.BindFactory<DefaultNode, DefaultNode.Factory>().FromComponentInNewPrefab(defaultNodePrefab);
    }
}