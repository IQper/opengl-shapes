using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Spiral: Entity, IDrawable
    {
        public Vector3 sectionSize;
        public int sectionQuality;
        public int ringQuality;
        public List<Polygon> faces = new();
        private List<Section> sections = new();
        private float dense;
        private float turnsCount;

        public Spiral(Vector3 position, Vector3 size, Vector3 sectionSize, float dense, float turnsCount, int sectionQuality, int ringQuality, Color color) 
            : base(size, position, color)
        {
            this.sectionSize = sectionSize;
            this.sectionQuality = sectionQuality;
            this.ringQuality = ringQuality;
            rotationPivotOffset = Vector3.Zero;
            this.dense = dense;
            this.turnsCount = turnsCount;
            BuildTorus();
        }

        private void BuildTorus()
        {
            var angleStep = (float)Math.PI * 2f / ringQuality;

            for (var i = 0; i < turnsCount * ringQuality; i++)
            {
                var currentAngle = angleStep * i;
                var sectionPos = new Vector3(MathF.Cos(currentAngle) * size.X, MathF.Sin(currentAngle) * size.Y, dense * i);
                sections.Add(new Section(sectionQuality, sectionPos + 2 * position - new Vector3(0, 0, turnsCount / 2), sectionSize, currentAngle + Utils.DegToRad(90)));
            }

            for (var i = 0; i < sections.Count - 1; i++)
            {
                for (var j = 0; j < sectionQuality; j++)
                {
                    var prikol = 0;
                    var vertices = new List<Vector3>();
                    vertices.Add(sections[i].vertices[j]);
                    vertices.Add(sections[(i + 1) % sections.Count].vertices[(j + prikol) % sectionQuality]);
                    vertices.Add(sections[(i + 1) % sections.Count].vertices[(j + 1 + prikol) % sectionQuality]);
                    vertices.Add(sections[i].vertices[(j + 1) % sectionQuality]);
                    faces.Add(new Polygon(vertices, position, color, -Utils.GetNormal(vertices, 0)));
                }
            }
            faces.Add(new Polygon(sections[0].vertices, position, color, -Utils.GetNormal(sections[0].vertices, 0)));
            faces.Add(new Polygon(sections[sections.Count - 1].vertices, position, color, Utils.GetNormal(sections[sections.Count - 1].vertices, 0)));
        }

        public void Draw(OpenGL gl)
        {
            faces.ForEach(x => x.Draw(gl));
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            faces.ForEach(x => x.SetAng(angle));
        }

        class Section
        {
            public List<Vector3> vertices = new();

            public Section(int verticesCount, Vector3 position, Vector3 size, float angle)
            {
                var angleStepJ = (float)Math.PI * 2f / verticesCount;
                for (var j = 0; j < verticesCount; j++)
                {
                    var currentAngleJ = angleStepJ * j;
                    var vertex = new Vector3(0, MathF.Cos(currentAngleJ) * size.X, MathF.Sin(currentAngleJ) * size.Y);
                    vertices.Add(ApplyRotation(vertex, new Vector3(0, 0, angle)) + position);
                }
            }

            private Vector3 ApplyRotation(Vector3 vertex, Vector3 angle)
            {
                return Vector3.Transform(vertex, Matrix4x4.CreateFromYawPitchRoll(angle.X, angle.Y, angle.Z));
            }
        }
    }
}
