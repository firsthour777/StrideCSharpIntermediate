using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace CSInt_09_FirstPersonCamera
{
   public class PlayerMoverTut : SyncScript
   {
      public Vector3 MovementSpeed = new Vector3(3, 0, 4);  // base movement speed, x is left/right, z is forward/backward
      // could add y here for something like jumping or levitation, but not for this tutorial.

      public CharacterComponent FirstPersonCharacterComponent; // set in editor, this is the component which has the colliders

      public override void Start()
      {
         // Initialization of the script.
      }

      public override void Update()
      {
         var velocity = new Vector3(); // the velocity for how much we are going to move an entity.

         if (Input.HasKeyboard)
         {

            // WASD movement
            if (Input.IsKeyDown(Keys.W))
            {
               velocity.Z++;
            }

            if (Input.IsKeyDown(Keys.S))
            {
               velocity.Z--;
            }

            if (Input.IsKeyDown(Keys.A))
            {
               velocity.X++;
            }

            if (Input.IsKeyDown(Keys.D))
            {
               velocity.X--;
            }

            velocity.Normalize();   // Normalize it so we don't move faster diagonally.

            velocity *= MovementSpeed; // multiply by the base speed

            
            velocity = Vector3.Transform(velocity, Entity.Transform.Rotation);   // transform movement into the look direction

            FirstPersonCharacterComponent.SetVelocity(velocity);  // does the movement using the special Character collider



         }
      }
   }
}