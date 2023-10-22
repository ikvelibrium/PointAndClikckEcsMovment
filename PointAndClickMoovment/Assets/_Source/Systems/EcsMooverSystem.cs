using Leopotam.EcsLite;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client 
{
    sealed class EcsMooverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private InputAction _mouseClick;
        private float _speed;
        private Transform _soldierPosition;
        private EcsFilter _filter;
        private EcsPool<EcsMooverComponent> _pool;
        private Camera _camera = Camera.main;
        private bool _IsGoing = false;
        private bool _isDestonationReached = true;
        private Vector3 _target;
        public void Init (IEcsSystems systems) 
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<EcsMooverComponent>().End();
            _pool = world.GetPool<EcsMooverComponent>();

            foreach (int entity in _filter)
            {
                
                ref EcsMooverComponent testComponent = ref _pool.Get(entity);
                ref var _speed = ref testComponent.Speed;
                ref var _soldierPosition = ref testComponent.SoldierPosition;
                ref var _mouseClick = ref testComponent.MouseClick;
                ref var _camera = ref testComponent.Camera;
                _mouseClick.Enable();
                _mouseClick.performed += Kostl;
               
            }

        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            { 
                if (_IsGoing == true)
                {
                    Moove( entity);
                }
            }
        }
        private void Kostl(InputAction.CallbackContext context)
        {

            _IsGoing = true;
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider)
            {
                 _target = hit.point;
            }

        }
       
        private void Moove(int entity)
        {
            
            ref EcsMooverComponent testComponent = ref _pool.Get(entity);
            ref var _speed = ref testComponent.Speed;
            ref var _soldierPosition = ref testComponent.SoldierPosition;
            ref var _mouseClick = ref testComponent.MouseClick;
            ref var _camera = ref testComponent.Camera;

            if (Vector3.Distance(_soldierPosition.position, _target) > 0.5f)
            {
                   
                    Vector3 destonation = Vector3.MoveTowards(_soldierPosition.position, _target, _speed * Time.deltaTime);
                    _soldierPosition.position = destonation;
                    
            } else if (Vector3.Distance(_soldierPosition.position, _target) < 0.5f)
            {
                _IsGoing = false;
            }
        }
    }
}