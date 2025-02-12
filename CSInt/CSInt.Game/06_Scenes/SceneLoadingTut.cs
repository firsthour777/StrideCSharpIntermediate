using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Core.Serialization;

namespace CSInt_06_Scenes
{
   public class SceneLoadingTut : SyncScript
   {
      public UrlReference<Scene> sceneToLoad;

      public override void Start()
      {
         // Initialization of the script.
      }

      public override void Update()
      {
         if (Input.IsKeyPressed(Keys.Z))
         {

            // In order to unload the root secene, you can't just expect Entity.Scene to be the root.
            // It must be the SceneSystem.
            Content.Unload(SceneSystem.SceneInstance.RootScene);

            SceneSystem.SceneInstance.RootScene = Content.Load<Scene>(sceneToLoad); // this will load the scene then put it as root all in one.

         }
      }
   }
}
