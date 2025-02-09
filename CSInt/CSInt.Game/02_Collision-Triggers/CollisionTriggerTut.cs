using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;
using Stride.Core.Collections;
using System.Collections.Specialized;
using BulletSharp;

namespace CSInt_02_Collision_Triggers
{
   public class CollisionTriggerTut : SyncScript
   {

      public StaticColliderComponent staticColliderComponent;

      private string collisionStatus = "";

      public override void Start()
      {
         staticColliderComponent = Entity.Get<StaticColliderComponent>();

         staticColliderComponent.Collisions.CollectionChanged += BoxTriggerCollidedWith;
      }

      public override void Update()
      {
         List<Collision> collisionsInBoxList = staticColliderComponent.Collisions.ToList<Collision>();

         for(int i = 0; i < collisionsInBoxList.Count; i++)
         {
            Collision collision = collisionsInBoxList[i];

            DebugText.Print($"Collision {i} detected with {collision.ColliderA.Entity.Name}", new Int2(500, 300));   // ColliderA is the object you have the trigger set on, Box
            DebugText.Print($"Collision {i} detected with {collision.ColliderB.Entity.Name}", new Int2(500, 320));   // ColliderB is the object that is colliding, Sphere



            



         }

         DebugText.Print(collisionStatus, new Int2(600, 500));

         
   



      }

      private void BoxTriggerCollidedWith(object sender, TrackingCollectionChangedEventArgs e)
      {
         Collision collisionFromEventTrigger = (Collision)e.Item;

         var ballCollider = staticColliderComponent == collisionFromEventTrigger.ColliderA ? collisionFromEventTrigger.ColliderB : collisionFromEventTrigger.ColliderA;

         if(e.Action == NotifyCollectionChangedAction.Add){ // enters collision

            collisionStatus = $"{staticColliderComponent.Entity.Name} Collision detected with {ballCollider.Entity.Name}";

            
         }

         if(e.Action == NotifyCollectionChangedAction.Remove){ // exits collision

            collisionStatus = $"{staticColliderComponent.Entity.Name} Collision ended with {ballCollider.Entity.Name}";

            
         }



         
      }




      private void CollisionTriggerEvent(object sender, TrackingCollectionChangedEventArgs e)
      {
         Collision collisionFromTriggerEventThatIsHappening = (Collision)e.Item;

         // There are a few ways to figure out which Collder is Which, do not assume A is always the Entity with the trigger.

         // decide by name, problem because its a string
         // collisionFromTriggerEventThatIsHappening.ColliderA.Entity.Name;
         // collisionFromTriggerEventThatIsHappening.ColliderB.Entity.Name;

         // the tutlorial way is to predefine your entities so for example

         // StaticColliderComponent boxStaticCollider = Entity.Get<StaticColliderComponent>(); // get the collider component from the Entity with the trigger
         // if(boxStaticCollider == collisionFromTriggerEventThatIsHappening.ColliderB) 
         // // You can also do comparisons on the entity
         // if(Entity == collisionFromTriggerEventThatIsHappening.ColliderA.Entity) // if the collider is the one with the trigger
         
         // // After finding out which collider is which, you can check to see if the Add or Remove event happened when they collide or exit collision
         // if(e.Action == NotifyCollectionChangedAction.Add){ // enters collision
         // if(e.Action == NotifyCollectionChangedAction.Remove){ // exits collision


         

         

         // var ballCollider = staticColliderComponent == collisionFromEventTrigger.ColliderA ? collisionFromEventTrigger.ColliderB : collisionFromEventTrigger.ColliderA;

         // if(e.Action == NotifyCollectionChangedAction.Add){ // enters collision

         //    collisionStatus = $"{staticColliderComponent.Entity.Name} Collision detected with {ballCollider.Entity.Name}";

            
         // }

         // if(e.Action == NotifyCollectionChangedAction.Remove){ // exits collision

         //    collisionStatus = $"{staticColliderComponent.Entity.Name} Collision ended with {ballCollider.Entity.Name}";

            
         // }

      }









   }
}
