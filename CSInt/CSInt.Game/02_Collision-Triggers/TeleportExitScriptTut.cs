using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace CSInt_02_Collision_Triggers
{
    public class TeleportExitScriptTut : SyncScript
    {

      public Entity Sphere;

        public override void Start()
        {

        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Keys.Space))
            {
                Sphere.Transform.Position = Entity.Transform.WorldMatrix.TranslationVector;
                Sphere.Transform.UpdateWorldMatrix();

                RigidbodyComponent rigidbodyComponent = Sphere.Get<RigidbodyComponent>();
                rigidbodyComponent.LinearVelocity = Vector3.Zero;
                rigidbodyComponent.UpdatePhysicsTransformation();
            }
        }
    }
}
