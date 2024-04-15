using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.RegularPolyhedrons
{
    internal class Octahedron : Entity, IDrawable
    {
        private List<Polygon> polys = new();
        public Octahedron(float radius, Vector3 position, Color color = default) : base(default, position, color)
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

            var t = new Vector3(0, 1, 0);
            var b = new Vector3(0, -1, 0);

            var m1 = new Vector3(1, 0, 1);
            var m2 = new Vector3(-1, 0, 1);
            var m3 = new Vector3(-1, 0, -1);
            var m4 = new Vector3(1, 0, -1);

            addPolygon(new List<Vector3> { t, m1, m2 }, polys);
            addPolygon(new List<Vector3> { t, m2, m3 }, polys);
            addPolygon(new List<Vector3> { t, m3, m4 }, polys);
            addPolygon(new List<Vector3> { t, m4, m1 }, polys);

            addPolygon(new List<Vector3> { b, m1, m2 }, polys);
            addPolygon(new List<Vector3> { b, m2, m3 }, polys);
            addPolygon(new List<Vector3> { b, m3, m4 }, polys);
            addPolygon(new List<Vector3> { b, m4, m1 }, polys);
        }
    }
}
