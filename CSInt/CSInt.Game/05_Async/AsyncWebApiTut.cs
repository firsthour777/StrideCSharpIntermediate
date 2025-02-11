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
    public class AsyncWebApiTut : AsyncScript
    {

        public override async Task Execute()
        {
            while(Game.IsRunning)
            {
                await Script.NextFrame();
            }
        }
    }
}
