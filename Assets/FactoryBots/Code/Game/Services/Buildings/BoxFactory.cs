using FactoryBots.App.Services.Assets;
using UnityEngine;
using UnityEngine.Pool;

namespace FactoryBots.Game.Services.Buildings
{
    public class BoxFactory
    {
        private readonly IAppAssetProvider _assets;
        private readonly Transform _parent;
        private readonly ObjectPool<Box> _boxPool;

        private int _nextBoxId;

        public BoxFactory(IAppAssetProvider assets)
        {
            _assets = assets;
            _parent = new GameObject("Boxes").transform;

            _boxPool = new ObjectPool<Box>(
                CreateBox,
                OnGetBox,
                OnReleaseBox,
                OnDestroyBox,
                collectionCheck: false
            );
        }

        public Box GetBox(Vector3 instantiationPosition)
        {
            Box box = _boxPool.Get();
            box.transform.position = instantiationPosition;
            box.SetId(_nextBoxId++);
            box.name = $"Box {box.Id + 1}";
            return box;
        }

        public void ReturnToParentTransform(Box box) => 
            box.transform.SetParent(_parent.transform);

        public void ReturnBox(Box box) => 
            _boxPool.Release(box);

        private Box CreateBox()
        {
            GameObject boxObject = _assets.Instantiate(AssetPath.BOX, _parent);
            Box box = boxObject.GetComponent<Box>();
            return box;
        }

        private void OnGetBox(Box box) => 
            box.gameObject.SetActive(true);

        private void OnReleaseBox(Box box) => 
            box.gameObject.SetActive(false);

        private void OnDestroyBox(Box box) => 
            Object.Destroy(box.gameObject);
    }
}
