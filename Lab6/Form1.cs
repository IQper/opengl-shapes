using Lab6.Properties;
using Lab6.RegularPolyhedrons;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using Microsoft.FSharp.Core;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private readonly OpenGL gl;
        private readonly List<IDrawable> drawables = new();
        private readonly List<Entity> rotatingEntities = new();

        private readonly Polygon lightPolygonTriangle;
        private float rotation;
        private readonly Vector3 lightPos = new Vector3(1, 4, 5);
        public Form1()
        {
            InitializeComponent();
            SetupFigures();
            gl = openglControl1.OpenGL;
            lightPolygonTriangle = Polygon.GetRightPolygon(3, new Vector3(0.1f, 0.1f, 0), lightPos, Color.Yellow);
            SetupLighting();
            Polygon.doUseLighing = true;
            //Polygon.doDrawNormal = true;

            checkBoxOutline.Checked = Polygon.doDrawEdges;
            checkBoxNormals.Checked = Polygon.doDrawNormal;
            checkBoxLighting.Checked = Polygon.doUseLighing;
        }



        private void openglControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            Clear();
            gl.Translate(0.0f, 0.0f, -20.0f);

            drawables.ForEach(x => x.Draw(gl));
            rotatingEntities.ForEach(x => x.SetAng(new Vector3(rotation * 2 + Utils.DegToRad(180), Utils.DegToRad(45 + 180), 0)));
            DrawLightSource();
            gl.Flush();
            rotation += 0.01f;
        }

        private void DrawLightSource()
        {
            var color = Color.RebeccaPurple;
            var position = lightPos;
            var normalAbsolute1 = position + SceneSettings.lightSourceDirection;

            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color.R, color.G, color.B);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.Vertex(normalAbsolute1.X, normalAbsolute1.Y, normalAbsolute1.Z);
            gl.End();

            lightPolygonTriangle.Draw(gl);
        }

        public void Log(string s)
        {
            textBox1.AppendText(s + Environment.NewLine);
        }

        private void Clear()
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0, 0, 0, 1);
            gl.LoadIdentity();
        }
        
        private void SetupLighting()
        {
            SceneSettings.lightSourceDirection = Vector3.Normalize((Vector3.Zero - lightPos));
        }

        private void SetupFigures()
        {

            var position = new Vector3(-15, 10, -10);
            #region flat
            var flatNormal = new Vector3(0, 0, 1);
            var triangle = Polygon.GetRightPolygon(3, new Vector3(1, 1, 0), position, Color.Azure, flatNormal);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(triangle);
            drawables.Add(triangle);

            var quad = Polygon.GetRightPolygon(4, new Vector3(1, 1, 0), position, Color.Azure, flatNormal);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(quad);
            drawables.Add(quad);

            var poly = Polygon.GetRightPolygon(5, new Vector3(1, 1, 0), position, Color.Azure, flatNormal);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(poly);
            drawables.Add(poly);

            var circle = Polygon.GetRightPolygon(12, new Vector3(1, 1, 0), position, Color.Azure, flatNormal);
            rotatingEntities.Add(circle);
            drawables.Add(circle);
            #endregion

            position = new Vector3(-15, 5, -10);
            #region rect
            var rectangle = new Prism(4, new Vector3(0.5f, 1, 0.3f), new Vector3(0.5f, 1, 0.3f), position, Color.Aquamarine);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(rectangle);
            drawables.Add(rectangle);


            var cube = new Prism(4, new Vector3(1, 1, 1), new Vector3(1, 1, 1), position, Color.Aquamarine);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(cube);
            drawables.Add(cube);

            //var dots = new List<Entity>();
            //for(var i = 0; i < cube.faces[4].vertices.Count - 1; i++)
            //{
            //    var dot = Polygon.GetRightPolygon(7, new Vector3(0.1f, 0.1f, 0), cube.faces[3].vertices[i], Color.Wheat, Vector3.Zero);
            //    drawables.Add(dot);
            //    dots.Add(dot);
            //}
            //var lines = new List<Line>() { new Line(dots[1].position, dots[0].position, Color.Red), new Line(dots[1].position, dots[2].position, Color.Red) };
            //drawables.AddRange(lines);
            //var vec1 = lines[0].GetVector();
            //var vec2 = lines[1].GetVector();
            //var normal = Vector3.Cross(vec1, vec2);
            //Log($"normal = {normal}");
            //Log($"vec1 = {vec1}");
            //Log($"vec2 = {vec2}");
            //drawables.Add(new Line(dots[1].position, dots[1].position + normal, Color.Red));
            

            var trapezoid = new Prism(4, new Vector3(1, 1, 1), new Vector3(0.5f, 0.5f, 0.5f), position, Color.Aquamarine);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(trapezoid);
            drawables.Add(trapezoid);

            var octahedron = new Prism(4, new Vector3(1, 1, 1), Vector3.Zero, position, Color.Aquamarine);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(octahedron);
            drawables.Add(octahedron);
            #endregion

            position = new Vector3(-15, 0, -10);
            #region circle
            var polycone = new Prism(5, new Vector3(1, 1, 1), Vector3.Zero, position, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(polycone);
            drawables.Add(polycone);

            var cone = new Prism(20, new Vector3(1, 1, 1), Vector3.Zero, position, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(cone);
            drawables.Add(cone);

            var cylinder = new Prism(20, new Vector3(1, 1, 1), new Vector3(1, 1, 1), position, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(cylinder);
            drawables.Add(cylinder);

            var cursedCylinder = new Prism(50, new Vector3(1f, 1f, 1), new Vector3(0.5f, 0.5f, 0), position, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(cursedCylinder);
            drawables.Add(cursedCylinder);

            var torus = new Torus(position, Vector3.One * 0.8f, Vector3.One * 0.2f, 9, 20, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(torus);
            drawables.Add(torus);

            //position = new Vector3(0, 0, 15);

            var spiral = new Spiral(position, Vector3.One * 0.8f, Vector3.One * 0.2f, 0.05f, 2.5f, 9, 20, Color.LawnGreen);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(spiral);
            drawables.Add(spiral);
            #endregion

            position = new Vector3(-15, -5, -10);
            #region sphere
            //position = new Vector3(.5f, .5f, 10);
            var sphere = new Sphere(Vector3.One * 1, 15, 15, position, Color.HotPink);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(sphere);
            drawables.Add(sphere);

            var icosphere = new Icosphere(1, 0, position, Color.HotPink);
            position += new Vector3(3, 0, 0);
            drawables.Add(icosphere);
            rotatingEntities.Add(icosphere);

            icosphere = new Icosphere(1, 1, position, Color.HotPink);
            position += new Vector3(3, 0, 0);
            drawables.Add(icosphere);
            rotatingEntities.Add(icosphere);

            icosphere = new Icosphere(1, 2, position, Color.HotPink);
            position += new Vector3(3, 0, 0);
            drawables.Add(icosphere);
            rotatingEntities.Add(icosphere);

            icosphere = new Icosphere(1, 3, position, Color.HotPink);
            position += new Vector3(3, 0, 0);
            drawables.Add(icosphere);
            rotatingEntities.Add(icosphere);
            #endregion

            position = new Vector3(-15, -10, -10);
            #region regular polyhedrons
            var polyhedron = new RegularPolyhedron(1, position, RegularPolyhedrons.Type.Cube, Color.Coral);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(polyhedron);
            drawables.Add(polyhedron);

            polyhedron = new RegularPolyhedron(1, position, RegularPolyhedrons.Type.Tetrahedron, Color.Coral);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(polyhedron);
            drawables.Add(polyhedron);

            polyhedron = new RegularPolyhedron(1, position, RegularPolyhedrons.Type.Octahedron, Color.Coral);
            position += new Vector3(3, 0, 0);
            rotatingEntities.Add(polyhedron);
            drawables.Add(polyhedron);

            polyhedron = new RegularPolyhedron(1, position, RegularPolyhedrons.Type.Icosahedron, Color.Coral);
            rotatingEntities.Add(polyhedron);
            drawables.Add(polyhedron);
            #endregion
        }

        private void checkBoxOutline_CheckedChanged(object sender, EventArgs e)
        {
            Polygon.doDrawEdges = checkBoxOutline.Checked;
            Polygon.doDrawNormal = checkBoxNormals.Checked;
            Polygon.doUseLighing = checkBoxLighting.Checked;
        }
    }
}