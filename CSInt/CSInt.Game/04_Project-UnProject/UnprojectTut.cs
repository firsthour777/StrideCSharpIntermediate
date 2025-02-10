using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;
using Stride.Physics;

namespace CSInt_04_Project_UnProject
{
   public class UnprojectTut : SyncScript
   {

      public Entity GoldenSphere;

      public CameraComponent ViewportCamera;

      private Simulation simulation; 

      public override void Start()
      {

         simulation = this.GetSimulation();
      }

      public override void Update()
      {

         if(Input.HasMouse){

            if(Input.IsMouseButtonPressed(MouseButton.Left)){

               Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.
               Viewport viewPort = new Viewport(0,0,backBuffer.Width,backBuffer.Height);

               var nearPosition = viewPort.Unproject(
                  new Vector3(Input.AbsoluteMousePosition, 0), 
                  ViewportCamera.ProjectionMatrix,
                  ViewportCamera.ViewMatrix,
                  Matrix.Identity
               );

               var farPosition = viewPort.Unproject(
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

               if(hitResult.Succeeded){
                  Entity cloneOfGoldenSphere = GoldenSphere.Clone();
                  cloneOfGoldenSphere.Transform.Position = hitResult.Point;
                  Entity.Scene.Entities.Add(cloneOfGoldenSphere);
               }





            }



         }
      }
   }
}
