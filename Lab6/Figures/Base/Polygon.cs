using System;
using System.Collections.Generic;
using System.Numerics;
using SharpGL;
using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using System.Drawing.Imaging;
using MathNet.Numerics;
using SharpGL.OpenGLAttributes;

namespace Lab6
{
    internal class Polygon : Entity, IDrawable
    {
        public List<Vector3> vertices = new();
        public readonly List<Vector3> oringinVertices = new();
        public Vector3 normal;
        public Vector3 oringinNormal { get; private set; }
        public static bool doDrawNormal = false;
        public static bool doDrawEdges = false;
        public static bool doUseLighing = false;
        public Polygon(List<Vector3> vertices, Vector3 position, Color color, Vector3 normal) : base(Vector3.Zero, position, color)
        {
            oringinVertices = vertices;
            this.vertices = oringinVertices.Select(x => ApplyRotation(x)).ToList();
            oringinNormal = normal.Length() == 0 ? Vector3.Zero : normal;
            this.normal = oringinNormal;
        }

        public void Draw(OpenGL gl)
        {
            if (doDrawNormal)
            {
                DrawNormal(gl);
            }

            if (color == Color.Transparent) return;

            if (doDrawEdges)
            {
                gl.Begin(OpenGL.GL_LINE_LOOP);
                foreach (var vertex in vertices)
                {
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(vertex.X, vertex.Y, vertex.Z);
                }
                gl.End();
            }
            else
            {
                gl.Begin(OpenGL.GL_POLYGON);
                foreach (var vertex in vertices)
                {
                    var color = ApplyLighting(this.color, normal, SceneSettings.lightSourceDirection);
                    gl.Color(color.R, color.G, color.B);
                    gl.Vertex(vertex.X, vertex.Y, vertex.Z);
                }
                gl.End();
            }
        }

        private void DrawNormal(OpenGL gl)
        {
            var color = Color.Red;
            var position = Utils.GetGeometricCenter(vertices);
            var normalAbsolute = position + Vector3.Normalize(normal);
            //var normalAbsolute = Se;
            gl.Color(color.R, color.G, color.B);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.Vertex(normalAbsolute.X, normalAbsolute.Y, normalAbsolute.Z);
            gl.End();
        }

        public static Polygon GetRightPolygon(int verticesCount, Vector3 size, Vector3 position, Color color = default, Vector3 normal = default)
        {
            var vertices = new List<Vector3>();
            var angleStep = (float)Math.PI * 2f / verticesCount; 

            for (var i = 0; i < verticesCount; i++)
            {
                var currentAngle = angleStep * i + Utils.DegToRad(45);
                var vertex = position + new Vector3((float)Math.Cos(currentAngle) * size.X, (float)Math.Sin(currentAngle) * size.Y, 0f);
                vertices.Add(vertex + position);
            }

            return new Polygon(vertices, position, color, normal);
        }

        private Vector3 ApplyRotation(Vector3 vertex)
        {
            return Vector3.Transform(vertex - (rotationPivotOffset + 2 * position), Matrix4x4.CreateFromYawPitchRoll(angle.X, angle.Y, angle.Z)) + (rotationPivotOffset + position);
        }

        private Vector3 ApplyRotationWithNoOffset(Vector3 vertex)
        {
            return Vector3.Transform(vertex, Matrix4x4.CreateFromYawPitchRoll(angle.X, angle.Y, angle.Z));
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            vertices = oringinVertices.Select(x => ApplyRotation(x)).ToList();
            if(oringinNormal.Length() > 0) normal = ApplyRotationWithNoOffset(oringinNormal);
        }
        public override void SetPos(Vector3 position)
        {
            base.SetPos(position);
            vertices = oringinVertices.Select(x =>
            {
                return x + position; 
            }).ToList();
        }

        public void SetNormal(Vector3 normal)
        {
            oringinNormal = normal;
        }

        private Color ApplyLighting(Color color, Vector3 normal, Vector3 lightingDirection)
        {
            if (normal.Length() == 0 || !doUseLighing) return color;
            var colorVec = new Vector3(color.R, color.G, color.B);
            var blackColorVec = new Vector3(10, 10, 10);
            var shadowStrength = Utils.AngleBetween(normal, lightingDirection) / (MathF.PI / 2) - 1;
            var outColorVec = Vector3.Lerp(blackColorVec, colorVec, Math.Clamp(shadowStrength, 0, 1));
            return Color.FromArgb((byte)outColorVec.X, (byte)outColorVec.Y, (byte)outColorVec.Z);
        }
    }
}
