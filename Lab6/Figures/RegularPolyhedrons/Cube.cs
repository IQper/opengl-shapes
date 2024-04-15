using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Cube : Entity, IDrawable
    {
        private List<Polygon> polys = new();
        public Cube(float radius, Vector3 position, Color color = default) : base(default, position, color)
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

            var top1 = new Vector3(1, 1, 1);
            var top2 = new Vector3(1, 1, -1);
            var top3 = new Vector3(-1, 1, -1);
            var top4 = new Vector3(-1, 1, 1);

            var bot1 = new Vector3(1, -1, 1);
            var bot2 = new Vector3(1, -1, -1);
            var bot3 = new Vector3(-1, -1, -1);
            var bot4 = new Vector3(-1, -1, 1);

            addPolygon(new List<Vector3> { top1, top2, top3, top4 }, polys);
            addPolygon(new List<Vector3> { bot1, bot2, bot3, bot4 }, polys);

            addPolygon(new List<Vector3> { bot1, bot2, top2, top1 }, polys);
            addPolygon(new List<Vector3> { bot2, bot3, top3, top2 }, polys);
            addPolygon(new List<Vector3> { bot3, bot4, top4, top3 }, polys);
            addPolygon(new List<Vector3> { bot4, bot1, top1, top4 }, polys);
        }
    }

}
