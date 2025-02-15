using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Navigation;
using Stride.Graphics;
using Stride.Physics;

namespace CSInt_11_Navigation
{
   public class NavCharacterTut : SyncScript
   {
      
      public Entity Character = null;
      public Entity NavigationSphere = null;
      public NavigationComponent NavigationComponentForCharacter = null;
      public CameraComponent ViewportCamera = null;
      public float MovementSpeed = 1.7f;

      private Simulation simulation;

      private List<Entity> waypointSpheres;
      private List<Vector3> waypointPaths;

      private int waypointIndex;    // this allows us to go through the waypointPaths as we move to each one.
      
      private float DT => (float)Game.UpdateTime.Elapsed.TotalSeconds;

      


      public override void Start()
      {
         simulation = this.GetSimulation();
         waypointSpheres = new List<Entity>();
         waypointPaths = new List<Vector3>();

         waypointIndex = 0;
      }

      public override void Update()
      {
         if (Input.HasMouse)
         {

            if(Input.Mouse.IsButtonPressed(MouseButton.Left))
            {
               var mouseState = Input.Mouse;
               if (mouseState.IsButtonDown(MouseButton.Left))
               {
                  RemoveExistingNavSpheres();
                  SetNewTarget();
               }

            }

            

         }

         
         UpdateMovement();



      }

      private void UpdateMovement()
      {
         if (waypointPaths.Count > 0)
         {
               // I think there is a better way to do this, but not for this tutorial.
            Vector3 nextWaypoint = waypointPaths[waypointIndex];
            Vector3 currentPosition = Character.Transform.WorldMatrix.TranslationVector;

            float distance = Vector3.Distance(currentPosition, nextWaypoint);

            if(distance > 0.1f)  // check if distance is large enough to keep moving, basically we aren't there yet.
            {
               Vector3 velocity = nextWaypoint - currentPosition;
               velocity.Normalize();
               velocity *= (DT * MovementSpeed);
               Character.Transform.Position += velocity;
               
            }
            else{

               // if we have arrived at the waypoint, we need to incremenet the waypoint if it's not the last one.
               if(waypointIndex < waypointPaths.Count - 1)
               {
                  waypointIndex++;
               }
               else{ // they are equal or somehow greater so we are done with the movement.
                  RemoveExistingNavSpheres();
               }


            }
         }
      }

      private void RemoveExistingNavSpheres()   // Removes them from scene and lists.
      {
         if (waypointSpheres != null)
         {
            for(int i = 0; i < waypointSpheres.Count; i++)
            {
               Entity.Scene.Entities.Remove(waypointSpheres[i]);
            }

            waypointSpheres.Clear();
            waypointPaths.Clear();

         }

         waypointSpheres = new List<Entity>();
      }


      private void SetNewTarget()
      {
         // Determine the 3D Position in our Sceen based on where our mouse is.
         Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.
         Viewport viewPort = new Viewport(0, 0, backBuffer.Width, backBuffer.Height);

         Vector3 nearPosition = viewPort.Unproject(
                  new Vector3(Input.AbsoluteMousePosition, 0),
                  ViewportCamera.ProjectionMatrix,
                  ViewportCamera.ViewMatrix,
                  Matrix.Identity
               );

         Vector3 farPosition = viewPort.Unproject(
            new Vector3(Input.AbsoluteMousePosition, 1.0f),
            ViewportCamera.ProjectionMatrix,
            ViewportCamera.ViewMatrix,
            Matrix.Identity
         );

         bool isHit = simulation.Raycast(
            nearPosition,
            farPosition,
            out HitResult hitResult
         );

         if (hitResult.Succeeded)
         {



            bool isFoundPath = NavigationComponentForCharacter.TryFindPath(    // This generates a path for travel, basically giving positions
            // this you can then use on your entities to travel.
               hitResult.Point,     // Position it needs to find a path to.
               waypointPaths    // End Result which gets stored into a List
            );

            if (isFoundPath)
            {

               waypointIndex = 0;

               for (int i = 0; i < waypointPaths.Count; i++)      // cycle through list of waypoints
               // clone, set its position from waypoints, add to list, then add to scene..
               // may want to set the entities to the scene in a parent entity.
               {
                  Entity cloneOfNavSphere = NavigationSphere.Clone();
                  cloneOfNavSphere.Transform.Position = waypointPaths[i];

                  waypointSpheres.Add(cloneOfNavSphere);

                  Entity.Scene.Entities.Add(cloneOfNavSphere);
                  
               }

            }






         }




      }





   }
}
