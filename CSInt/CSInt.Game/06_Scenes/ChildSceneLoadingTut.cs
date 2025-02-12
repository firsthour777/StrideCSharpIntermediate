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
   public class ChildSceneLoadingTut : SyncScript
   {

      public UrlReference<Scene> ChildSceneToLoad;

      private Scene childScene;

      public int timesLoaded = 0;

      public override void Start()
      {
      }

      public override void Update()
      {

         if( Input.IsKeyPressed(Keys.Space))
         {
            if (childScene == null)
            {
               // loads the scene into memory
               childScene = Content.Load<Scene>(ChildSceneToLoad);   // you don't have to specify Scene type because we already did that with UrlReference

               // if we want to load it in a particular transform position, we can do
               childScene.Offset = new Vector3(0, timesLoaded * 0.25f, 0);   // this is the position of the child scene in the parent scene.

               // sets the parent of the scene
               childScene.Parent = Entity.Scene;   // sets it to this sceen.
               // Entity.Scene.Children.Add(childScene);   // same thing but calls the Entity itself to load.

               timesLoaded++;

            }
            else{

               childScene.Parent = null;  // sets the parent to null, removing it from parent scene, which causes it to be unrendered, but still in memory.
               Content.Unload(childScene);  // removes the child scene from memory.
               childScene = null;   // does a similar thing, removes everything


            }

         }
      }
   }
}
