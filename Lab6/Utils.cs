using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Lab6
{
    internal class Utils
    {
        public static float DegToRad(float degrees)
        {
            return (float)Math.PI / 180 * degrees;
        }

        public static float RadToDeg(float radians)
        {
            return 180 / (float)Math.PI * radians;
        }

        public static float AngleBetween(Vector3 vec1, Vector3 vec2)
        {
            return (float)Math.Acos(Vector3.Dot(vec1, vec2) / (vec1.Length() * vec2.Length()));
        }

        public static Vector3 GetGeometricCenter(List<Vector3> vecs)
        {
            var outvec = Vector3.Zero;
            foreach (var vec in vecs) outvec += vec;
            return outvec / vecs.Count;
        }

        public static Vector3 GetNormal(List<Vector3> vertices, int offset)
        {
            var n = vertices.Count;
            var vecA = vertices[(1 + offset) % n] - vertices[(2 + offset) % n];
            var vecB = vertices[(3 + offset) % n] - vertices[(2 + offset) % n];
            var normal = Vector3.Cross(vecB, vecA);
            //if (normal.Length() == 0) return GetNormal(vertices, offset + 1);
            return normal;
        }
    }
}
