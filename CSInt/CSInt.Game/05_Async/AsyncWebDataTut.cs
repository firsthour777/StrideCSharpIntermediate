using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Net.Http;
using System.Text.Json;

namespace CSInt_05_Async
{
   public class AsyncWebDataTut : AsyncScript
   {

      private List<MeoFacts> stackOverflowData = new List<MeoFacts>();


      public List<string> mewoData = new List<string>();

      public override async Task Execute()
      {

         // anything above the while function only executes once, anything in the while happens repeteadly just like Update.
         stackOverflowData = new List<MeoFacts>();

         StartAsyncStride();

         while (Game.IsRunning)
         {
            await UpdateAsyncStride();
            await Script.NextFrame();    // Tells game to move to next frame, if you don't have this game is frozen.
         }


      }

      private void StartAsyncStride()
      {
         stackOverflowData = new List<MeoFacts>();

         mewoData = new List<string>();

      }

      private async Task UpdateAsyncStride()
      {

         if (Input.HasKeyboard)
         {
            if (Input.Keyboard.IsKeyPressed(Keys.Space))
            {
               await RetrieveData();
            }

            int drawX = 200;
            int drawY = 200;
            for (int i = 0; i < mewoData.Count; i++)
            {
               drawY += 20;
               DebugText.Print($"mewoData: {mewoData[i]}", new Int2(drawX, drawY), Color.Red);
            }
         }
      }





      private async Task RetrieveData()
      {

         var client = new HttpClient();

         HttpResponseMessage response = await client.GetAsync("https://meowfacts.herokuapp.com/");

         if (response.StatusCode == System.Net.HttpStatusCode.OK)
         {

            string contentOfResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<MeoFacts>(contentOfResponse);
            mewoData = result.data; // Extracts the list of facts

            // stackOverflowData = JsonSerializer.Deserialize<List<MeoFacts>>(contentOfResponse);

            Log.Info($"contentOfResponse: {contentOfResponse}");

            Log.Info($"newData: {mewoData}");

            
         }








      }


      public class MeoFacts
      {
         public List<string> data { get; set; }

      }





   }
}
