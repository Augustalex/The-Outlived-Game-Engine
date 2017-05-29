using System;
using Microsoft.Xna.Framework;
using Spelkonstruktionsprojekt.ZEngine.Components;
using Spelkonstruktionsprojekt.ZEngine.Constants;
using Spelkonstruktionsprojekt.ZEngine.Helpers;
using Spelkonstruktionsprojekt.ZEngine.Managers;
using Spelkonstruktionsprojekt.ZEngine.Systems.Bullets;
using Spelkonstruktionsprojekt.ZEngine.Systems.InputHandler;
using ZEngine.EventBus;

namespace Game.Entities.Zones
{
    public class DowntownZone
    {
        public const string Event = "DowntownZone_DisplayLabel";
        private ComponentManager ComponentManager = ComponentManager.Instance;
        
        public void Start()
        {
            EventBus.Instance.Subscribe<uint>(Event, DisplayLabel);
        }

        public void Stop()
        {
            EventBus.Instance.Unsubscribe<uint>(Event, DisplayLabel);
        }
        
        public void DisplayLabel(uint zoneId)
        {
            var entityId = new EntityBuilder()
                .SetPosition(new Vector2(200, 50), ZIndexConstants.InGameText)
                .SetText("Downtown", 40, "ZEone")
                .BuildAndReturnId();

            var animationComponent = ComponentManager.ComponentFactory.NewComponent<AnimationComponent>();
            ComponentManager.AddComponentToEntity(animationComponent, entityId);

            var animation = new GeneralAnimation()
            {
                AnimationType = "InGameText",
                StartOfAnimation = 0,
                Length = 8000,
                Unique = true
            };
            NewBulletAnimation(animation, entityId);
            animationComponent.Animations.Add(animation);
        }

        // Animation for when the bullet should be deleted.
        public void NewBulletAnimation(GeneralAnimation generalAnimation, uint entityId)
        {
            var startSet = false;
            generalAnimation.Animation = delegate(double currentTimeInMilliseconds)
            {
                if (!startSet)
                {
                    generalAnimation.StartOfAnimation = currentTimeInMilliseconds;
                    startSet = true;
                }
                
                if (currentTimeInMilliseconds - generalAnimation.StartOfAnimation > generalAnimation.Length)
                {
                    var tagComponent = ComponentManager.GetEntityComponentOrDefault<TagComponent>(entityId);
                    if (tagComponent == null)
                    {
                        throw new Exception(
                            "Entity does not have a tag component which is needed to remove the entity.");
                    }
                    tagComponent.Tags.Add(Tag.Delete);
                    generalAnimation.IsDone = true;
                }
            };
        }
    }
}