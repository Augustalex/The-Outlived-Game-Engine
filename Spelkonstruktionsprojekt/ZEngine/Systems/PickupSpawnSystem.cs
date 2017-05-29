﻿using Microsoft.Xna.Framework;
using Spelkonstruktionsprojekt.ZEngine.Components.PickupComponents;
using Spelkonstruktionsprojekt.ZEngine.Constants;
using Spelkonstruktionsprojekt.ZEngine.Helpers;
using Spelkonstruktionsprojekt.ZEngine.Managers;
using System;
using System.Linq;
using Spelkonstruktionsprojekt.ZEngine.Components;
using ZEngine.Components;
using ZEngine.EventBus;
using ZEngine.Managers;

namespace ZEngine.Systems
{
    public class PickupSpawnSystem : ISystem
    {
        private EventBus.EventBus EventBus = ZEngine.EventBus.EventBus.Instance;
        private ComponentManager ComponentManager = ComponentManager.Instance;
        private Random rand;

        public void Start()
        {
            EventBus.Subscribe<uint>(EventConstants.Death, Handle);
            rand = new Random();
        }

        private void Handle(uint entityId)
        {
            if (!ComponentManager.EntityHasComponent<AIComponent>(entityId)) { return; }
            PositionComponent positionComponent = ComponentManager.GetEntityComponentOrDefault<PositionComponent>(entityId);
            if (positionComponent == null) { return; }
            if (rand.Next(0, 1) == 0)
            {
                if (rand.Next(0, 2) == 0)
                {
                    AddPickup("healthpickup", positionComponent.Position);
                }
                else
                {
                    AddPickup("healthpickup", positionComponent.Position);
                }

            }
        }
        private void AddPickup(string spritename, Vector2 position)
        {
            var flyweightEntityList = ComponentManager.GetEntitiesWithComponent(typeof(FlyweightPickupComponent));
            if (flyweightEntityList.Count == 0){return;}
            var flyweightEntity = flyweightEntityList.FirstOrDefault();
            var spriteComponent = ComponentManager.GetEntityComponentOrDefault<SpriteComponent>(flyweightEntity.Key);
            var soundComponent = ComponentManager.GetEntityComponentOrDefault<SoundComponent>(flyweightEntity.Key);



            var entity = new EntityBuilder()
                .SetRendering(40, 40)
                .SetRectangleCollision()
                .SetPosition(position, 500)
                
                .Build()
                .GetEntityKey();



            ComponentManager.AddComponentToEntity(soundComponent, entity);


            if (spritename == "healthpickup")
            {
                var pick = ComponentManager.Instance.ComponentFactory.NewComponent<HealthPickupComponent>();
                ComponentManager.Instance.AddComponentToEntity(pick, entity);
                ComponentManager.AddComponentToEntity(spriteComponent, entity);
            }
            else if (spritename == "ammopickup")
            {
                var pick = ComponentManager.Instance.ComponentFactory.NewComponent<AmmoPickupComponent>();
                ComponentManager.Instance.AddComponentToEntity(pick, entity);
                ComponentManager.AddComponentToEntity(spriteComponent, entity);
            }
        }
    }
}