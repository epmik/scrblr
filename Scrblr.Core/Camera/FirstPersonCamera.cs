using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Scrblr.Core
{
    public class FirstPersonCamera : AbstractCamera
    {
        // x-axis rotation
        private float _pitch;

        // y-axis rotation
        private float _yaw = -MathHelper.PiOver2;
        
        //private Vector2? _lastMousePosition = null;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);

                _pitch = MathHelper.DegreesToRadians(angle);

                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);

                UpdateVectors();
            }
        }

        public float MouseMoveSensitivity = 0.2f;
        public float MoveSpeed = 2.5f;
        public float ScrollSpeed = 12f;

        private bool _firstMouseMove = true;

        public override void Update(FrameEventArgs a)
        {
            base.Update(a);

            var ElapsedTime = a.Time;

            var input = KeyboardState;


            if (input.IsKeyDown(Keys.W))
            {
                Position += DirectionVector * MoveSpeed * (float)ElapsedTime; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                Position -= DirectionVector * MoveSpeed * (float)ElapsedTime; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                Position -= RightVector * MoveSpeed * (float)ElapsedTime; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                Position += RightVector * MoveSpeed * (float)ElapsedTime; // Right
            }
            if (input.IsKeyDown(Keys.Q))
            {
                Position += UpVector * MoveSpeed * (float)ElapsedTime; // Up
            }
            if (input.IsKeyDown(Keys.E))
            {
                Position -= UpVector * MoveSpeed * (float)ElapsedTime; // Down
            }
        }

        public override void MouseMove(MouseMoveEventArgs a)
        {
            base.MouseMove(a);

            if(_firstMouseMove)
            {
                _firstMouseMove = false;

                return;
            }

            Yaw += a.DeltaX * MouseMoveSensitivity;
            Pitch -= a.DeltaY * MouseMoveSensitivity;
        }

        public override void MouseWheel(MouseWheelEventArgs a)
        {
            base.MouseWheel(a);

            Position += a.Offset.Y * ScrollSpeed * DirectionVector * MoveSpeed * (float)ElapsedTime; // forward/backwards
        }

        private void UpdateVectors()
        {
            DirectionVector = new Vector3(
                MathF.Cos(_pitch) * MathF.Cos(_yaw),
                MathF.Sin(_pitch),
                MathF.Cos(_pitch) * MathF.Sin(_yaw));

            DirectionVector = Vector3.Normalize(DirectionVector);

            RightVector = Vector3.Normalize(Vector3.Cross(DirectionVector, Vector3.UnitY));
            UpVector = Vector3.Normalize(Vector3.Cross(RightVector, DirectionVector));
        }
    }
}