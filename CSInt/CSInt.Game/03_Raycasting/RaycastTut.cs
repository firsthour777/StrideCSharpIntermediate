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
   public class RaycastTut : SyncScript
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
         Vector3 targetPosition = startPosition + new Vector3(0, 0, 3.0f);

      //    /// <summary>
      //   /// Raycasts, returns true when it hit something
      //   /// </summary>
      //   /// <param name="from">The starting point of this raycast</param>
      //   /// <param name="to">The end point of this raycast</param>
      //   /// <param name="result">Information about this test</param>
      //   /// <param name="filterGroup">The collision group of this raycast</param>
      //   /// <param name="filterFlags">The collision group that this raycast can collide with</param>
      //   /// <param name="hitTriggers">Whether this test should collide with <see cref="PhysicsTriggerComponentBase"/></param>
      //   /// <param name="eFlags">Flags that control how this ray test is performed</param>
      //   /// <returns>True if the test collided with an object in the simulation</returns>
      //   public bool Raycast(Vector3 from, Vector3 to, out HitResult result, CollisionFilterGroups filterGroup = DefaultGroup, CollisionFilterGroupFlags filterFlags = DefaultFlags, bool hitTriggers = false, EFlags eFlags = EFlags.None)
         
         // Although there is a second raycast, use this recast to get a true false when it hits something and the hit result
         bool isHit = simulation.Raycast(
            startPosition, 
            targetPosition, 
            out HitResult hitResult,
            CollisionFilterGroups.DefaultFilter, // Sets the collision group that hte raycast is part of
            CollideWith, // Determines what the raycast can hit based on selecting the Collision Group it can collide with
            isCollideWithTriggers // whether it hits triggers or not.
         );

         if(isHit){
            float distanceToScale = Vector3.Distance(startPosition, hitResult.Point);

            laser.Transform.Scale.Z = distanceToScale;


         }
         else{
            float distanceToScale = Vector3.Distance(startPosition, targetPosition);

            laser.Transform.Scale.Z = distanceToScale;

         }








      }
   }
}
