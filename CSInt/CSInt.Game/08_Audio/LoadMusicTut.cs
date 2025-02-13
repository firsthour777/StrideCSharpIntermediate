using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Audio;

namespace CSInt_08_Audio
{
   public class LoadMusicTut : AsyncScript
   {

      public Sound BackgroundMusic;

      private SoundInstance musicInstance;



      public override async Task Execute()
      {

         musicInstance = BackgroundMusic.CreateInstance();

         await musicInstance.ReadyToPlay();

         while (Game.IsRunning)
         {

            if (Input.HasKeyboard)
            {
               if (Input.IsKeyPressed(Keys.B))
               {
                  if(musicInstance.PlayState == Stride.Media.PlayState.Playing)
                  {
                     musicInstance.Pause();
                  }
                  else
                  {
                     musicInstance.Play();
                  }
               }
            }



            await Script.NextFrame();
         }
      }
   }
}
