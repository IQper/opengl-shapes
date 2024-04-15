using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Line : IDrawable
    {
        public Vector3 pos1;
        public Vector3 pos2;
        public Color color;

        public Line(Vector3 pos1, Vector3 pos2, Color color)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;
            this.color = color;
        }

        public void Draw(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color.R, color.G, color.B);
            gl.Vertex(pos1.X, pos1.Y, pos1.Z);
            gl.Vertex(pos2.X, pos2.Y, pos2.Z);
            gl.End();
        }

        public Vector3 GetVector()
        {
            return pos2 - pos1;
        }
    }
}
