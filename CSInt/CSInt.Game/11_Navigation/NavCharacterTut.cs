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
      
      public Entity Character = null;        // The character Entity you want to move
      public Entity NavigationSphere = null;    // The nav sphere that gets placed down, think of it like a pathing line.
      public NavigationComponent NavigationComponentForCharacter = null;      // this is the nav component on the character entity.
      public CameraComponent ViewportCamera = null;   // this is the camera that you will be active while clicking.
      public float MovementSpeed = 1.7f;   

      private Simulation simulation;

      private List<Entity> waypointSpheres;        // list of entities that get spawned
      private List<Vector3> waypointPaths;         // list of locations to move to.

      private int waypointIndex;    // this allows us to go through the waypointPaths as we move to each one.
      
      private float DT => (float)Game.UpdateTime.Elapsed.TotalSeconds;     // delta time var

      


      public override void Start()
      {
         // inits
         simulation = this.GetSimulation();
         waypointSpheres = new List<Entity>();
         waypointPaths = new List<Vector3>();
         waypointIndex = 0;
      }

      public override void Update()
      {  

         if (Input.HasMouse)  
         {
            if (Input.Mouse.IsButtonPressed(MouseButton.Left)) // when clicking, set new nav
            {
               RemoveExistingNavSpheres();   
               SetNewTarget();
            }
         }

         UpdateMovement();    // updates the movement, checks if we need to move or not.

      }

      private void UpdateMovement()
      {
         if (waypointPaths.Count > 0)     // checks if we have waypoints.
         {
             // I think there is a better way to do this, but not for this tutorial.
            Vector3 nextWaypoint = waypointPaths[waypointIndex];     // gets the waypoint we need to move to
            Vector3 currentPosition = Character.Transform.WorldMatrix.TranslationVector;        // gets where we are

            float distance = Vector3.Distance(currentPosition, nextWaypoint);    // calculates the ditance to travel

            if(distance > 0.1f)  // check if distance is large enough to keep moving, basically we aren't there yet.
            {
               // performs movement using Delta Time and move speed.
               Vector3 velocity = nextWaypoint - currentPosition;    
               velocity.Normalize();
               velocity *= (DT * MovementSpeed);
               Character.Transform.Position += velocity;
               
            }
            else{    // if we have arrived at the waypoint, we need to increment the waypoint if it's not the last one.
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

      private void RemoveExistingNavSpheres()   // Removes nav spheres from scene and clears waypoints from list.
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
         Viewport viewPort = new Viewport(0, 0, backBuffer.Width, backBuffer.Height);     // gets the view of the cam

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

            if (isFoundPath)  // you don't have to have this, just check the count.
            {
               waypointIndex = 0;
               for (int i = 0; i < waypointPaths.Count; i++)      // cycle through list of waypoints
               {  // may want to set the entities to the scene in a parent entity.
                  Entity cloneOfNavSphere = NavigationSphere.Clone();      // clone
                  cloneOfNavSphere.Transform.Position = waypointPaths[i];  // set position
                  waypointSpheres.Add(cloneOfNavSphere); // add to list
                  Entity.Scene.Entities.Add(cloneOfNavSphere); // add to scene
               }
            }
         }
      }
   }
}
