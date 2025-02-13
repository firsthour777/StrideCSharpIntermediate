using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Animations;

namespace CSInt_07_Animation
{
   public class AnimationTut : SyncScript
   {

      public float AnimationSpeed = 1.0f;

      public AnimationComponent animationComponent;

      private PlayingAnimation playingAnimation;




      public override void Start()
      {
         playingAnimation = animationComponent.Play(AnimationState.Idling);
      }

      public override void Update()
      {

         if (Input.HasKeyboard)
         {

            if (Input.IsKeyPressed(Keys.I))
            {
               playingAnimation = animationComponent.Play(AnimationState.Idling);    // plays according to passed in string.
               playingAnimation.TimeFactor = AnimationSpeed;   // sets play speed
            }



            if (Input.IsKeyPressed(Keys.R))
            {
               playingAnimation = animationComponent.Crossfade(AnimationState.Running, TimeSpan.FromSeconds(0.5));
               playingAnimation.TimeFactor = AnimationSpeed;
            }


            AdjustAnimationSpeed();

            if (!animationComponent.IsPlaying(AnimationState.Punching))
            {

               if (Input.IsKeyPressed(Keys.P))     // press P while not punching to punch.
               {
                  playingAnimation = animationComponent.Crossfade(AnimationState.Punching, TimeSpan.FromSeconds(0.1));
                  playingAnimation.TimeFactor = AnimationSpeed;
                  playingAnimation.RepeatMode = AnimationRepeatMode.PlayOnce;
               }

            }

            if (!animationComponent.IsPlaying(AnimationState.Punching))
            {


               if (playingAnimation.Name == AnimationState.Punching)
               { // this gets the latest Animation, so if the last animation was Punch, but is not playing, do this.
                  // sets it back to idle after done with a punch.
                  playingAnimation = animationComponent.Play(AnimationState.Idling);
                  playingAnimation.TimeFactor = AnimationSpeed;
                  playingAnimation.RepeatMode = AnimationRepeatMode.LoopInfinite;

               }

            }



            PauseAnimations();









         }
      }


      private void PauseAnimations()
      {
         if (Input.IsKeyPressed(Keys.Space))
         {
            for(int i = 0; i < animationComponent.PlayingAnimations.Count; i++)
            {
               animationComponent.PlayingAnimations[i].Enabled = !animationComponent.PlayingAnimations[i].Enabled;
            }
         }
      }




      private void AdjustAnimationSpeed()
      {


         if (Input.IsKeyPressed(Keys.Up))
         {
            AnimationSpeed += 0.1f;
         }

         if (Input.IsKeyPressed(Keys.Down))
         {
            AnimationSpeed -= 0.1f;
         }

         playingAnimation.TimeFactor = AnimationSpeed;


      }



   }
}
