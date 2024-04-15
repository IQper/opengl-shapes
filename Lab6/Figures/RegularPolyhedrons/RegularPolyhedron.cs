using SharpGL;
using System.Numerics;

namespace Lab6.RegularPolyhedrons
{
    public class RegularPolyhedron : Entity, IDrawable
    {
        private readonly IDrawable drawable;
        private readonly Entity entity = new(default);

        public RegularPolyhedron(float radius, Vector3 position, Type type, Color color = default) : base(default, position, color)
        {
            switch (type)
            {
                case Type.Tetrahedron:
                    var tet = new Tetrahedron(radius, position, color);
                    drawable = tet;
                    entity = tet;
                break;
                case Type.Cube:
                    var cube = new Cube(radius, position, color);
                    drawable = cube;
                    entity = cube;
                break;
                case Type.Octahedron:
                    var octa = new Octahedron(radius, position, color);
                    drawable = octa;
                    entity = octa;
                    break;
                default:
                    var ico = new Icosphere(radius, 0, position, color);
                    drawable = ico;
                    entity = ico;
                break;
            }
        }

        public override void SetAng(Vector3 angle)
        {
            base.SetAng(angle);
            entity.SetAng(angle);
        }

        public void Draw(OpenGL gl)
        {
            drawable.Draw(gl);
        }
    }

    public enum Type
    {
        Tetrahedron,
        Cube,
        Octahedron,
        Icosahedron,
    }
}
