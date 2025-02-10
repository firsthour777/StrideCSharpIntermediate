using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;

namespace CSInt_04_Project_UnProject
{
   public class ProjectTut : SyncScript
   {

      public Entity GlobalSphere;
      public Entity LocalSphere;

      public CameraComponent Camera;


      public override void Start()
      {
      }

      public override void Update()
      {

         Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.

         // get world positions
         Vector3 parentSpherePosition = GlobalSphere.Transform.WorldMatrix.TranslationVector;
         Vector3 childSpherePosition = LocalSphere.Transform.WorldMatrix.TranslationVector;


         LocalSphere.Transform.UpdateWorldMatrix();

         // calculate correct screen space value
         Vector3 parentScreenPosition = Vector3.Project(    // This is the screen position, 3d to 2d
            parentSpherePosition,   // where our sphere that we want is
            0, // x this is the viewport, which in our case is the entire screen itself, this changes depending on where you are trying to project
            0, // y
            backBuffer.Width, 
            backBuffer.Height,
            0, // can be zero as this is minZ and we are going from 2D to 3D
            0, // maxZ
            Camera.ViewProjectionMatrix
         );

         Vector3 childScreenPosition = Vector3.Project(     // This is the screen position, 3d to 2d
            childSpherePosition,
            0,
            0,
            backBuffer.Width,
            backBuffer.Height,
            0,
            0,
            Camera.ViewProjectionMatrix
         );

         // This will print it in the top left corner of the screen, because the positions are so close to 0, 
         // its looking at its world coords, so INCORRECT
         // DebugText.Print($"Parent Sphere Position: {parentSpherePosition}", new Int2(parentSpherePosition.XY()), Color.Red);
         // DebugText.Print($"Child Sphere Position: {childSpherePosition}", new Int2(childSpherePosition.XY()), Color.Green);
         DebugText.Print($"Parent Sphere Position: {parentSpherePosition}", new Int2(parentScreenPosition.XY()), Color.Red);
         DebugText.Print($"Child Sphere Position: {childSpherePosition}", new Int2(childScreenPosition.XY()), Color.Green);








      }
   }
}
