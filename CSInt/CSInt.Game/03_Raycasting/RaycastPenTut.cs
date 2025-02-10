using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace CSInt_03_Raycasting
{
   public class RaycastPenTut : SyncScript
   {




      public CollisionFilterGroupFlags CollideWith;   // Set your collission group via editor or code, select all if hits with all, but you don't want to do this in practice.

      public bool isCollideWithTriggers;  // If a Physics Component is a trigger, then it gets ignored by the raycast unless this is set to true.

      private Simulation simulation;   // This is the physics simulation of the Entity, use this. to get it.

      private Entity laser;   // This entity is something like a bullet or projeticle to show where the ray cast is.

      public override void Start()
      {

         simulation = this.GetSimulation();
         laser = Entity.FindChild("Laser");
      }

      public override void Update()
      {
         Vector3 startPosition = Entity.Transform.Position;
         Vector3 targetPosition = startPosition + new Vector3(0, 0, -3.0f);

         float distance = Vector3.Distance(startPosition, targetPosition); // This is the distance between the start and target position.

         laser.Transform.Scale.Z = distance;



         /// <summary>
        /// Raycasts penetrating any shape the ray encounters.
        /// Filtering by CollisionGroup
        /// </summary>
        /// <param name="from">The starting point of this raycast</param>
        /// <param name="to">The end point of this raycast</param>
        /// <param name="resultsOutput">The collection to add intersections to</param>
        /// <param name="filterGroup">The collision group of this raycast</param>
        /// <param name="filterFlags">The collision group that this raycast can collide with</param>
        /// <param name="hitTriggers">Whether this test should collide with <see cref="PhysicsTriggerComponentBase"/></param>
        /// <param name="eFlags">Flags that control how this ray test is performed</param>


         List<HitResult> hitResultsList = new List<HitResult>();
         
         simulation.RaycastPenetrating(
            startPosition,
            targetPosition,
            hitResultsList,   // This gets filled, basically every entity the ray hits
            CollisionFilterGroups.DefaultFilter, 
            CollideWith, 
            isCollideWithTriggers 
         );

         int drawX = 300;
         int drawY = 300;

         if(hitResultsList.Count > 0)
         {
            for(int i = 0; i < hitResultsList.Count; i++)
            {

               drawY += 40;
               DebugText.Print($"Hit {i} {hitResultsList[i].Collider.Entity.Name}", new Int2(drawX, drawY), Color.Red);

               HitResult hitResultFromList = hitResultsList[i];


            }




         }
         else
         {
            DebugText.Print("No hits", new Int2(drawX, drawY), Color.Red);

         }









      }
   }
}
