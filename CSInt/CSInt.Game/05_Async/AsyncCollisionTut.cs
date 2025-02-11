using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;
using Stride.Rendering;

namespace CSInt_05_Async
{
   public class AsyncCollisionTut : AsyncScript
   {

      public StaticColliderComponent staticColliderComponent;

      private Material yellowMaterialToApply;



      public override async Task Execute()
      {
         StartAsyncStride();

         while (Game.IsRunning)
         {
            await UpdateAsyncStride();
            // await Script.NextFrame();     // if you are already waiting for the next frame you do not need to do this.
         }
      }

      private async Task StartAsyncStride()
      {

         yellowMaterialToApply = Content.Load<Material>("Materials/Yellow");

      }


      private async Task UpdateAsyncStride()
      {

         Collision collisionOfBall = await staticColliderComponent.NewCollision();  // until has a new collision, the script won't do anything, we are waiting on the collision

         PhysicsComponent ballColliderComponent;
         if(staticColliderComponent == collisionOfBall.ColliderA)
         {
            ballColliderComponent = collisionOfBall.ColliderB;
         }
         else  // ColliderB
         {
            ballColliderComponent = collisionOfBall.ColliderA;
         }

         ModelComponent ballModel = ballColliderComponent.Entity.Get<ModelComponent>();

         Material originalMaterial = ballModel.GetMaterial(0);

         // Material originalMaterial = ballModel.Materials[0];

         ballModel.Materials[0] = yellowMaterialToApply;

         await staticColliderComponent.CollisionEnded();

         ballModel.Materials[0] = originalMaterial;






      }

      



   }
}
