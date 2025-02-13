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
   public class AudioTut : SyncScript
   {
      public Sound UkeleleSound;

      private SoundInstance ukeleleSoundInstance;

      public AudioEmitterComponent GunAudioEmitterComponent;

      private AudioEmitterSoundController gunAudioEmitterSoundController;

      public override void Start()
      {
         ukeleleSoundInstance = UkeleleSound.CreateInstance();

         gunAudioEmitterSoundController = GunAudioEmitterComponent[SoundsFromEntity.GunShot];

         

      }

      public override void Update()
      {

         if(Input.HasKeyboard){

            if (Input.IsKeyPressed(Keys.U))
            {
               ukeleleSoundInstance.Play();
            }

            if (Input.IsKeyPressed(Keys.E))
            {
               ukeleleSoundInstance.PlayExclusive();  // makes it so its the only sound playing, stops all other sound instances.
            }

            if (Input.IsKeyPressed(Keys.G))
            {
               gunAudioEmitterSoundController.Play();
            }


            
         }

      }
   }
}
