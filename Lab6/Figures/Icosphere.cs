using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using SharpGL;
using SharpGL.OpenGLAttributes;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Icosphere : Entity, IDrawable
    {
        public float radius;
        public List<Polygon> polys = new();
        public delegate void Log(string a);
 
        public Icosphere(float radius, int iterations, Vector3 position, Color color = default) : base(default, position, color)
        {
            this.radius = radius;
            Create(iterations);
        }

        public void Draw(OpenGL gl)
        {
            foreach( var poly in polys)
            {
                poly.Draw(gl);
            }
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            polys.ForEach(x => x.SetAng(angle));
        }

        private Vector3 Normalize(Vector3 vec)
        {
            return Vector3.Normalize(vec) * radius;
        }

        public void Create(int recursionLevel)
        {
            // create 12 vertices of a icosahedron
            var t = (1.0f + MathF.Sqrt(5.0f)) / 2.0f;

            //new Vector3(-1, t, 0), //2 3 4 5 6
            //new Vector3(0, -1, t), //8 9 10 12 13
            //new Vector3(-t, 0, 1), //5 6 12 13 14
            //new Vector3(-1, -t, 0), //10 11 13 14 15
            //new Vector3(0, 1, t), //2 6 7 8 12
            //new Vector3(-t, 0, -1), //4 5 14 15 16
            //new Vector3(0, -1, -t),//11 15 16 17 18
            //new Vector3(1, -t, 0), //9 10 11 18 19
            //new Vector3(t, 0, 1), //7 8 9 19 20
            //new Vector3(1, t, 0), //2 3 7 20 21
            //new Vector3(0, 1, -t), //3 4 16 17 21 
            //new Vector3(t, 0, -1), // 17 18 19 20 21

            void addPolygon(List<Vector3> vcs, List<Polygon> to)
            {
                vcs = vcs.Select(x => Normalize(x) + 2 * position).ToList();
                var normal = Utils.GetGeometricCenter(vcs) - 2 * position;
                to.Add(new Polygon(vcs, position, color, normal));
            };

            addPolygon(new List<Vector3> { new Vector3(-1, t, 0), new Vector3(1, t, 0), new Vector3(0, 1, t) }, polys); //2
            addPolygon(new List<Vector3> { new Vector3(-1, t, 0), new Vector3(1, t, 0), new Vector3(0, 1, -t) }, polys); //3
            addPolygon(new List<Vector3> { new Vector3(-1, t, 0), new Vector3(-t, 0, -1), new Vector3(0, 1, -t) }, polys); //4
            addPolygon(new List<Vector3> { new Vector3(-1, t, 0), new Vector3(-t, 0, -1), new Vector3(-t, 0, 1) }, polys); //5
            addPolygon(new List<Vector3> { new Vector3(-1, t, 0), new Vector3(0, 1, t), new Vector3(-t, 0, 1) }, polys); //6
             
            addPolygon(new List<Vector3> { new Vector3(1, t, 0), new Vector3(t, 0, 1), new Vector3(0, 1, t) }, polys); //7
            addPolygon(new List<Vector3> { new Vector3(0, -1, t), new Vector3(t, 0, 1), new Vector3(0, 1, t) }, polys); //8
            addPolygon(new List<Vector3> { new Vector3(t, 0, 1), new Vector3(0, -1, t), new Vector3(1, -t, 0) }, polys); //9
            addPolygon(new List<Vector3> { new Vector3(0, -1, t), new Vector3(-1, -t, 0), new Vector3(1, -t, 0) }, polys); //10
            addPolygon(new List<Vector3> { new Vector3(0, -1, -t), new Vector3(-1, -t, 0), new Vector3(1, -t, 0) }, polys); //11

            addPolygon(new List<Vector3> { new Vector3(0, -1, t), new Vector3(0, 1, t), new Vector3(-t, 0, 1) }, polys); //12
            addPolygon(new List<Vector3> { new Vector3(0, -1, t), new Vector3(-1, -t, 0), new Vector3(-t, 0, 1) }, polys); //13
            addPolygon(new List<Vector3> { new Vector3(-1, -t, 0), new Vector3(-t, 0, -1), new Vector3(-t, 0, 1) }, polys); //14
            addPolygon(new List<Vector3> { new Vector3(-1, -t, 0), new Vector3(-t, 0, -1), new Vector3(0, -1, -t) }, polys); //15
            addPolygon(new List<Vector3> { new Vector3(0, 1, -t), new Vector3(-t, 0, -1), new Vector3(0, -1, -t) }, polys); //16
            
            addPolygon(new List<Vector3> { new Vector3(0, 1, -t), new Vector3(t, 0, -1), new Vector3(0, -1, -t) }, polys); //17
            addPolygon(new List<Vector3> { new Vector3(0, -1, -t), new Vector3(1, -t, 0), new Vector3(t, 0, -1) }, polys); //18
            addPolygon(new List<Vector3> { new Vector3(t, 0, 1), new Vector3(1, -t, 0), new Vector3(t, 0, -1) }, polys); //19
            addPolygon(new List<Vector3> { new Vector3(t, 0, 1), new Vector3(1, t, 0), new Vector3(t, 0, -1) }, polys); //20
            addPolygon(new List<Vector3> { new Vector3(0, 1, -t), new Vector3(1, t, 0), new Vector3(t, 0, -1) }, polys); //21

            for (var i = 0; i < recursionLevel; i++)
            {
                var newPolys = new List<Polygon>();
                foreach (var polygon in polys)
                {
                    var v1 = polygon.oringinVertices[0] - 2 * position;
                    var v2 = polygon.oringinVertices[1] - 2 * position;
                    var v3 = polygon.oringinVertices[2] - 2 * position;

                    var v12 = (v1 + v2) / 2;
                    var v23 = (v2 + v3) / 2;
                    var v31 = (v3 + v1) / 2;

                    addPolygon(new List<Vector3> { v1, v12, v31 }, newPolys);
                    addPolygon(new List<Vector3> { v12, v23, v2 }, newPolys);
                    addPolygon(new List<Vector3> { v3, v23, v31 }, newPolys);
                    addPolygon(new List<Vector3> { v12, v23, v31 }, newPolys);
                }
                polys = newPolys;
            }
        }
    }
}
