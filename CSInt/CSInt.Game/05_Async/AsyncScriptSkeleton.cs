using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace CSInt_05_Async
{
   public class AsyncScriptSkeleton : AsyncScript
   {
      public override async Task Execute()
      {
         StartAsyncStride();

         while (Game.IsRunning)
         {
            UpdateAsyncStride();
            await Script.NextFrame();
         }
      }

      private async Task StartAsyncStride()
      {

      }


      private async Task UpdateAsyncStride()
      {

      }

      



   }
}
