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
   public class FirstPersonCamTut : SyncScript
   {

      private bool isActive = true; // is your camera active, meaning not paused with mouse screen

      public float MouseSpeed = 0.5f;  // This gets applied to camera rotation as the mouse moves in an FPS

      private Vector3 cameraRotation;     

      public bool isXAxisInverted;     // set in editor if you want it to be inverted, will likely be read in from settings later on.
      public bool isYAxisInverted;

      public float MaxCameraAngleDegrees = 50.0f;  // Prevents continued rotation, you can set it in the editor.
      public float MinCameraAngleDegrees = -50.0f;

      private float maxCameraAngleRadians;   // holder for when we convert to radians
      private float minCameraAngleRadians;

      public Entity CameraPivotPoint;  // this is the camera's pivot point which you should have for any type of camera on model situation.

      public CharacterComponent FirstPersonCharacterComponent; // set in editor, this is the component which ahs the colliders

      public override void Start()
      {

         Game.IsMouseVisible = false;  // sets state to not show the mouse.

         cameraRotation = Entity.Transform.RotationEulerXYZ;   // You must set this to get the correct camera position as it is in the editor.

         maxCameraAngleRadians = MathUtil.DegreesToRadians(MaxCameraAngleDegrees);  // conversion to Radians.
         minCameraAngleRadians = MathUtil.DegreesToRadians(MinCameraAngleDegrees);



      }

      public override void Update()
      {
         if (Input.HasKeyboard)
         {
            if (Input.IsKeyPressed(Keys.Escape))
            {
               isActive = !isActive;   // pauses fps camera and stops looking around to instead use the mouse.
               Game.IsMouseVisible = !isActive;
               Input.UnlockMousePosition();
            }

         }

         if (isActive)
         {

            Input.LockMousePosition();    // locks mouse so it doesn't appear on the screen.

            var mouseMovement = Input.MouseDelta * MouseSpeed;    // gets the delta version of the mouse speed.

            cameraRotation.Y -= mouseMovement.X * ConvertInversionBool(isXAxisInverted);   // x movement is rotating the y axis, its negative because it most go the opposite way as if you are looking that way.
            cameraRotation.X += mouseMovement.Y * ConvertInversionBool(isYAxisInverted);  // this does not need negative as its the opposite of the y axis.

            cameraRotation.X = MathUtil.Clamp(cameraRotation.X, minCameraAngleRadians,  maxCameraAngleRadians);   // had to change this in the tutorial this clamps and prevents over rotation.


            // Entity.Transform.Rotation = Quaternion.RotationY(cameraRotation.Y);     // sets the rotation for Y, which is looking along the x axis movement.
            //commented out the above because ow that we have a character component, we want to rotate the character component instead
            FirstPersonCharacterComponent.Orientation = Quaternion.RotationY(cameraRotation.Y);   // this rotates the character component instead of the entire entity, which is what we want.
            CameraPivotPoint.Transform.Rotation = Quaternion.RotationX(cameraRotation.X);    // sets the rotation for X, based on the pivot point, otherwise you move the entire mesh.
            // you don't want to move the entire mesh on the Y axis as it will rotate the entire model, but its fine for X axis because that makes sense.
            // it might make sense if the model is completely symetrical on all sides, like a sphere, but idk

         }
      }


      public int ConvertInversionBool(bool isInverted)
      {
         if (isInverted)
         {
            return -1;
         }
         else
         {
            return 1;
         }
      }




   }

}
