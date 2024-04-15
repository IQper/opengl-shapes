using SharpGL;
using System.Numerics;

namespace Lab6
{
    internal class Sphere : Entity, IDrawable
    {
        public List<Polygon> faces = new();
        private int circleQuality;
        private int layerQuality;
        public List<List<Vector3>> layersOfVertices = new();
        private List<Vector3> originVs = new();
        private List<Vector3> vs = new();
        public Sphere(Vector3 size, int circleQuality, int layerQuality, Vector3 position, Color color = default) : base(size, position, color)
        {
            this.circleQuality = circleQuality;
            this.layerQuality = layerQuality;

            BuildFaces();
        }

        private void BuildFaces()
        {
            var layerStep = MathF.PI / layerQuality;
            var circleStep = MathF.PI * 2f / circleQuality;
            for (var i = 0; i <= layerQuality; i++)
            {
                layersOfVertices.Add(new List<Vector3>());

                var layerAng = layerStep * i;
                var radius = MathF.Sin(layerAng) * size.Z;
                var circlePos = MathF.Cos(layerAng) * size.Z;

                for (var j = 0; j < circleQuality; j++)
                {
                    var circleAng = circleStep * j;
                    var vertex = new Vector3(MathF.Cos(circleAng) * size.X * radius, MathF.Sin(circleAng) * size.Y * radius, circlePos) + 2 * position;
                    layersOfVertices[i].Add(vertex);
                    originVs.Add(vertex);
                }
                vs = originVs;
            }

            for (var i = 0; i < layerQuality; i++)
            {
                for (var j = 0; j < circleQuality; j++)
                {
                    var prikol = 0;
                    var vertices = new List<Vector3>();
                    if (i != 0) vertices.Add(layersOfVertices[i][j]);
                    if (i != layerQuality - 1) vertices.Add(layersOfVertices[i + 1][(j + prikol) % circleQuality]);
                    vertices.Add(layersOfVertices[i + 1][(j + 1 + prikol) % circleQuality]);
                    vertices.Add(layersOfVertices[i][(j + 1) % circleQuality]);
                    faces.Add(new Polygon(vertices, position, color, Utils.GetNormal(vertices, 0)));
                }
            }
            vs = originVs;
        }

        public void Draw(OpenGL gl)
        {
            faces.ForEach(x => x.Draw(gl));
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            faces.ForEach(x => x.SetAng(angle));
            vs = originVs.Select(vertex => Vector3.Transform(vertex - position, Matrix4x4.CreateFromYawPitchRoll(angle.X, angle.Y, angle.Z)) + position).ToList();
        }

        public override void SetRotationPivotOffset(Vector3 pivotPos)
        {
            base.SetRotationPivotOffset(pivotPos);
            faces.ForEach(x => x.SetRotationPivotOffset(pivotPos));
        }
    }
}
