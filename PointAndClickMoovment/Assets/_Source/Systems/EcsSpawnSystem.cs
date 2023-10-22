using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class EcsSpawnSystem : IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<EcsSpawnerComponent> _pool;
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<EcsSpawnerComponent>().End();
            _pool = world.GetPool<EcsSpawnerComponent>();


            foreach (int entity in _filter)
            {
                ref EcsSpawnerComponent testComponent = ref _pool.Get(entity);

                ref var prefab = ref testComponent.Prefab;
                ref var amount = ref testComponent.Amount;
                for (int i = 0; i < amount; i++)
                {
                    GameObject prefabInstance = GameObject.Instantiate(prefab);
                    prefabInstance.transform.position = new Vector3(Random.Range(-30, 30), 0f, Random.Range(-30, 30));
                }
            }
        }


    }
}