using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class TextureTest
    {
        private static Bitmap? textureImage;
        private static string path = "D:\\Visual Studio Projects\\GraphicsLabs\\Lab6\\amogus.jpg";
        private static Texture? texture = new Texture();
        public static void Draw(OpenGL gl, Vector3 pos)
        {
            textureImage = new Bitmap(path);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0, 0, 0, 1);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -10.0f);
            gl.Rotate(0, 0.0f, 1.0f, 0.0f);
            
            texture.Create(gl, textureImage);
            texture.Bind(gl);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(int.MaxValue, int.MaxValue, int.MaxValue);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, -1.0f, 1.0f); // Bottom Left Of The Texture and
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, -1.0f, 1.0f); // Bottom Right Of The Texture and
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, 1.0f, 1.0f); // Top Right Of The Texture and Qu
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, 1.0f, 1.0f); // Top Left Of The Texture and Qua
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.End();

            texture.Destroy(gl);
            gl.Flush();

        }
    }
}
