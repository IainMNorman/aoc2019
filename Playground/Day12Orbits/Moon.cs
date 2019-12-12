using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Day12Orbits
{
    public class Moon
    {
        public string Name { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Velocity { get; set; }

        public float TotalEnergy
        {
            get
            {
                var p = Math.Abs(this.Position.X) + Math.Abs(this.Position.Y) + Math.Abs(this.Position.Z);
                var k = Math.Abs(this.Velocity.X) + Math.Abs(this.Velocity.Y) + Math.Abs(this.Velocity.Z);
                return p * k;
            }
        }

        public void ApplyVelocity()
        {
            this.Position = Vector3.Add(this.Velocity, this.Position);
        }
    }
}
