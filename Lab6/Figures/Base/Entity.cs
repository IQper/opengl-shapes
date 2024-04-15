using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Entity
    {
        public Vector3 size;
        public Vector3 angle;
        public Vector3 position;
        public Color color;
        public Vector3 rotationPivotOffset;

        public Entity(Color color)
        {
            this.color = color;
        }

        public Entity(Vector3 size, Vector3 position, Color color = default)
        {
            if (color == default) color = Color.Red;
            this.size = size;
            this.position = position;
            this.color = color;
            rotationPivotOffset = Vector3.Zero;
            angle = Vector3.Zero;
        }

        public virtual void SetAng(Vector3 angle)
        {
            this.angle = angle;
        }
        public virtual void SetRotationPivotOffset(Vector3 pivotPos)
        {
            rotationPivotOffset = pivotPos;
        }

        public virtual void SetPos(Vector3 position)
        {
            this.position = position;
        }
    }
}
