using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Lab6
{
    internal class Prism : Entity, IDrawable
    {
        public int facesCount;
        public List<Polygon> faces = new();
        public Vector3 capSize;
        public Prism(int facesCount, Vector3 size, Vector3 capSize, Vector3 position, Color color) : base(size, position, color)
        {
            this.facesCount = facesCount;
            this.capSize = capSize;
            faces = GetBuildedFaces();
        }

        public void Draw(OpenGL gl)
        {
            foreach(var polygon in faces)
            {
                polygon.Draw(gl);
            }
        }

        private List<Polygon> GetBuildedFaces()
        {
            var faces = new List<Polygon>();
            var posOffset = new Vector3(0, 0, size.Z / 1.5f);
            Polygon top;
            if (capSize.Length() != 0)
            {
                top = Polygon.GetRightPolygon(facesCount, capSize, position + posOffset, color, Vector3.Zero);
            }
            else
            {
                top = Polygon.GetRightPolygon(1, capSize, position + posOffset, color, Vector3.Zero);
            }
            top.SetNormal(posOffset);
            top.SetRotationPivotOffset(-posOffset);
            faces.Add(top);

            var bot = Polygon.GetRightPolygon(facesCount, size, position - posOffset, color, Vector3.Zero);
            bot.SetNormal(-posOffset);
            bot.SetRotationPivotOffset(posOffset);
            faces.Add(bot);

            for (var i = 0; i < facesCount; i++)
            {
                var vertices = new List<Vector3>();
                var prikol = 0;

                vertices.Add(top.vertices[i % top.vertices.Count] + position);
                vertices.Add(top.vertices[(i + 1) % top.vertices.Count] + position);
                vertices.Add(bot.vertices[(i + 1 + prikol) % bot.vertices.Count] + position);
                vertices.Add(bot.vertices[(i + prikol)  % bot.vertices.Count] + position);

                var normal = -Utils.GetNormal(vertices, 0);
                //var face = new Polygon(vertices, position, color, geometricCenter / 4 - position);
                var face = new Polygon(vertices, position, color, normal);
                
                faces.Add(face);
            }

            return faces;
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            faces.ForEach(x => x.SetAng(angle));
        }
    }
}
