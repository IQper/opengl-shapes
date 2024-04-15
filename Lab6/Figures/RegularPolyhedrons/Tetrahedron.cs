using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.RegularPolyhedrons
{
    internal class Tetrahedron : Entity, IDrawable
    {
        private List<Polygon> polys = new();

        public Tetrahedron(float radius, Vector3 position, Color color = default) : base(default, position, color)
        {
            Create(radius);
        }

        public void Draw(OpenGL gl)
        {
            polys.ForEach(x => x.Draw(gl));
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            polys.ForEach(x => x.SetAng(angle));
        }

        private void Create(float radius)
        {
            void addPolygon(List<Vector3> vcs, List<Polygon> to)
            {
                Vector3 Normalize(Vector3 vec)
                {
                    return Vector3.Normalize(vec) * radius;
                }

                vcs = vcs.Select(x => Normalize(x) + 2 * position).ToList();
                var normal = Utils.GetGeometricCenter(vcs) - 2 * position;
                to.Add(new Polygon(vcs, position, color, normal));
            };

            var t = 1;

            var v1 = new Vector3(t, t, t);
            var v2 = new Vector3(-t, -t, t);
            var v3 = new Vector3(-t, t, -t);
            var v4 = new Vector3(t, -t, -t);

            addPolygon(new List<Vector3> {v1, v2, v3}, polys);
            addPolygon(new List<Vector3> {v1, v3, v4}, polys);
            addPolygon(new List<Vector3> {v2, v3, v4}, polys);
            addPolygon(new List<Vector3> {v1, v2, v4}, polys);
        }
    }
}
