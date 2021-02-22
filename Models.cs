using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Models
{
    public class Models
    {
     
        public static  void ModelLoading(string file, List<Vector3> tri, List<Vector3> rect, List<Vector2> texcoord, List<Vector3> nx)
        {
            // async
            string model_data = File.ReadAllText(file);
            string[] lines = model_data.Split(' ');
            string[] ohherewego = { "wutt" };
            string contenta = model_data;
         
            string line = "e";
            line = contenta;
            char[] delimiters = new char[] { ' ', '\t' };

            ohherewego = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string obj_ = "rect";

            int contents2 = contenta.Split(' ').Length/*;*/;
           // Console.WriteLine(String.Concat(ohherewego));
            for (int i = 0; i < contents2-2;  /*18*/ /*;*/ i++)
            {
                //Console.WriteLine(GL.GetError());

              
               
                if (ohherewego[i].Contains("begin_tri"))
                {
                    obj_ = "tri";
                  //  GL.PushMatrix();

                  //  GL.Begin(BeginMode.Triangles);

                }
                if (ohherewego[i].Contains("begin_triangle_strip"))
                {
                    GL.Begin(BeginMode.TriangleStrip);
                }
                if (ohherewego[i].Contains("begin_triangle_fan"))
                {
                    GL.Begin(BeginMode.TriangleFan);
                }
                if (ohherewego[i].Contains("begin_quads"))
                {



                    obj_ = "rect";
                  //  Console.WriteLine("rect");
                  //  GL.Begin(BeginMode.Quads);
                }
                if (ohherewego[i].Contains("begin_lines"))
                {




                   // GL.Begin(BeginMode.Lines);
                }

                if (ohherewego[i].Contains("begin_quad_strip"))
                {



                    GL.Begin(BeginMode.QuadStrip);
                }

                if (ohherewego[i].Contains("nx"))
                {
                    nx.Add(new Vector3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3])));

                 //   GL.Normal3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3]));
                    //GL.Normal3(Single.Parse(ohherewego[i+1]),Single.Parse(ohherewego[i+2]),Single.Parse(ohherewego[i+3]));
                }

                if (ohherewego[i]=="color")//.Contains()
                {
                    GL.Color3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3]));
                }
                if (ohherewego[i].Contains("tx"))
                {
                    texcoord.Add(new Vector2(Single.Parse(ohherewego[i+1]), Single.Parse(ohherewego[i + 2])));
                   // texcoord.Add(new Vector2(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2])));
                    // GL.TexCoord2(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]));
                    //GL.Normal3(Single.Parse(ohherewego[i+1]),Single.Parse(ohherewego[i+2]),Single.Parse(ohherewego[i+3]));
                }

                if (ohherewego[i].Contains("v"))
                {
                    try
                    {

                   
                    if (obj_ == "tri")
                        tri.Add(new Vector3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3])));
                    if (obj_ == "rect")
                        rect.Add(new Vector3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3])));
                    }
                    catch (NullReferenceException)
                    {

                       // throw;
                    }

                    //	GL.TexCoord2(0.0f,0.0f);


                    //GL.Vertex3(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3]));
                    //GL.Vertex3(Single.Parse(ohherewego[i+4]),Single.Parse(ohherewego[i+5]),Single.Parse(ohherewego[i+6]));
                    //	GL.Vertex3(Single.Parse(ohherewego[i+7]),Single.Parse(ohherewego[i+8]),Single.Parse(ohherewego[i+9]));                                                                                                                                  

                    //Application.Exit();

                }


                if (ohherewego[i].Contains("box"))
                {

                   // Iedx.Utils.Primitives.primitives.Block(Single.Parse(ohherewego[i + 1]), Single.Parse(ohherewego[i + 2]), Single.Parse(ohherewego[i + 3]), Single.Parse(ohherewego[i + 4]), Single.Parse(ohherewego[i + 5]), Single.Parse(ohherewego[i + 6]), false);
                }
                if (ohherewego[i].Contains("color"))
                {
                    //ohherewego.SetValue("test",i+1);
                    //ohherewego.SetValue("test",i+2);
                    //ohherewego.SetValue("test",i+3);
                    //	ohherewego[i+1] = "test";
                    //ohherewego[i+2] = "test";
                    //ohherewego[i+3] = "test";
                    //string result = string.Join(" ", ohherewego);
                    //Clipboard.SetText(result);
                    //GL.Color3(Single.Parse(ohherewego[i+1]),Single.Parse(ohherewego[i+2]),Single.Parse(ohherewego[i+3]));
                }
                if (ohherewego[i].Contains("end_"))
                {
                    GL.End();
                  //  GL.PopMatrix();
                }
                
               
            }

            //  public List<Vector3> tri = new List<Vector3>();
       // public List<Vector3> rect  = new List<Vector3>();
        //public List<Vector2> texcoord = new List<Vector2>();

            // }
        }
    public void Model_(string file)
        { //Task.Run(() => ModelLoading(file));
        }
        //int texture_coord
        public  void Render()
        {
            GL.Begin(BeginMode.Triangles);
            int i_ = 80; 
           /* for (int i = 0; i < texcoord.Count; i++)
            {
                GL.TexCoord2(texcoord[i]);
                for (int x = 0; x < tri.Count; x += 4)
                {

                    GL.Vertex3(tri[x]);
                    // 

                    //GL.TexCoord2(0.0f, 0.0f);
                    GL.Vertex3(tri[x + 1]);

                    // GL.TexCoord2(texcoord[i_ + 1]);
                    GL.TexCoord2(1.0f, 0.0f);
                    GL.Vertex3(tri[x + 2]);
                    // GL.TexCoord2(texcoord[i_ + 2]);
                    GL.TexCoord2(1.0f, 1.0f);
                    GL.Vertex3(tri[x + 3]);
                    GL.TexCoord2(0.0f, 1.0f);
                    // GL.TexCoord2(texcoord[i_ + 3]);
                }

                // i_ = i;
                //Console.WriteLine(texcoord[i]);
            }*/
           
            GL.End();
        
        }
        //;

    }

}
