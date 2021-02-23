using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Shared;
using Network;
using System.Threading;
using Newtonsoft.Json;
namespace Waaa
{
	public class Window : GameWindow
	{


		//<3
		public List<Vector3> vertices = new List<Vector3>();

		public List<Vector3> colors = new List<Vector3>();
		public List<Vector2> texCoords = new List<Vector2>();
		public List<Vector3> vertices_1 = new List<Vector3>();
		public List<Vector3> colors_1 = new List<Vector3>();
		public List<Vector2> texCoords_1 = new List<Vector2>();
		public List<Vector3> nx = new List<Vector3>();
		public List<Player> players = new List<Player>();

		

		//Consider this more or less a pointer to your model for now
		private int vao;
		//Consider this a pointer to the buffer responsible for vertex positions
		private int vbov;
		//Consider this a pointer to the buffer responsible for vertex colors
		private int vboc;
		//Texture Coords
		private int vbo_;

		private int vao_1;
		//Consider this a pointer to the buffer responsible for vertex positions
		private int vbov_1;
		//Consider this a pointer to the buffer responsible for vertex colors
		private int vbc_1;
		//Texture Coords
		private int vbo_1;

		private int vbo___1;
		private Matrix4 _view;

		private Matrix4 _projection;

		private Shader shader;

		private Shader reflectsky_shader;

		private double _time;


		
		private Vector3[] object_ =
		{
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(2.0f, 5.0f, -15.0f),
			new Vector3(-1.5f, -2.2f, -2.5f),
			new Vector3(-3.8f, -2.0f, -12.3f),
			new Vector3(2.4f, -0.4f, -3.5f),
			new Vector3(-1.7f, 3.0f, -7.5f),
			new Vector3(1.3f, -2.0f, -2.5f),
			new Vector3(1.5f, 2.0f, -2.5f),
			new Vector3(1.5f, 0.2f, -1.5f),
			new Vector3(-1.3f, 1.0f, -1.5f)/*readonly */

		};
		private readonly float[] rect3d =
		{
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
			 0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
			-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
			-0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
			-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
			 0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
			 0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
			-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
		};
		private int rectv_;
		private int rect_; 
		private int rectnx;
		private int recttex;//

		private Texture container;
		float walking__ = 0.00002f;
		bool walking____ = false;


		public async void walk________()
		{
			if (walking____ == true)
			{
				for (int xj = 0; xj < 8; xj++)
				{
					walking__ += 0.001f;
					await Sys.waitFunction(1);
				}

			}

			if (walking____ == false)
			{
				for (int xj = 0; xj < 8; xj++)
				{
					walking__ -= 0.002f;
					await Sys.waitFunction(1);
				}
			}
			if (walking__ > 0.008f)
				walking____ = false;
			if (walking__ < 0.002)
				walking____ = true;
		}


		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			GL.Viewport(0, 0, Width, Height);
		}

		int newTexture(Bitmap textureBitmap)//, TextureMinFilter texture_filter
		{
			int texture;
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

			texture = GL.GenTexture();

			Use(texture);

			//GL.BindTexture(TextureTarget.Texture2D, texture);
			BitmapData data = textureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			textureBitmap.UnlockBits(data);


			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			return texture;
		}
		int newTexture(Bitmap textureBitmap, int texture_filter)
		{
			int texture;
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

			GL.GenTextures(1, out texture);
			GL.BindTexture(TextureTarget.Texture2D, texture);
			BitmapData data = textureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			textureBitmap.UnlockBits(data);

			if (texture_filter == 8)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
			}
			if (texture_filter == 9)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			}
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			return texture;

		}

		protected override void OnClosing(CancelEventArgs e)
		{
			Environment.Exit(420);
		}
		private Vector3 player = new Vector3(0.0f, 0.0f, -3.0f);
		private List<Models.Models> models = new List<Models.Models>();
		Models.Models model = new Models.Models();
		ConnectionResult connectionResult = ConnectionResult.TCPConnectionNotAlive;
		TcpConnection tcpConnection;
		protected override async void OnLoad(EventArgs e)
		{
			//Ignore this
			base.OnLoad(e);
			//Set clear color
			GL.ClearColor(Color4.CornflowerBlue);


			 tcpConnection = ConnectionFactory.CreateTcpConnection("127.0.0.1", 1234, out connectionResult);
			tcpConnection.KeepAlive = true;

			if (connectionResult == ConnectionResult.Connected)
				Console.WriteLine($"{tcpConnection.ToString()} Connection established");

			Random random = new Random();

			string username = random.Next(100, 1000).ToString();

			//model.Model_("jjj.3d");

			//Models.Models model_v = new Models.Models();
			//model.Model_("tabanca.3d");
			//models.Add(model_v);
			//models.Add(model);
			List<Vector3> rect = new List<Vector3>();
			List<Vector3> nx_ = new List<Vector3>();
			Models.Models.ModelLoading("tabanca.3d", vertices, new List<Vector3>(), texCoords,nx_);//
			Models.Models.ModelLoading("jjj.3d", vertices_1,  new List<Vector3>(), texCoords_1,nx);
			//	Models.Models.ModelLoading("jjj.3d", vertices, new List<Vector3>(), texCoords);

			container = new Texture("Resources/Container2.png");
			container.Use();
			container.Use(TextureUnit.Texture1);
			//container = newTexture(new Bitmap("Resources/Container2.png"));


			//Create the vao and vbos, don't worry about it now

			vao = GL.GenVertexArray();
			vbov = GL.GenBuffer();
			vboc = GL.GenBuffer();
			vbo_ = GL.GenBuffer();

			vao_1 = GL.GenVertexArray();
			vbov_1 = GL.GenBuffer();
			vbc_1 = GL.GenBuffer();
			vbo_1 = GL.GenBuffer();
			vbo___1 = GL.GenBuffer();

			rect_ = GL.GenVertexArray();
			rectv_ = GL.GenBuffer();
			rectnx = GL.GenBuffer();
			//Initialize the shader there
			//The file names for the respective shader program components
			shader = new Shader("vertex.vert", "fragment.frag");
			reflectsky_shader = new Shader("vertex.vert", "reflect.frag");

			//Bind the shader to set up some array pointers

			shader.Use();
			//
			container = new Texture("Resources/Container2.png");
			// Texture units are explained in Texture.cs, at the Use function.
			// Texture.Use will implicitly fill in Texture0 if you pass no arguments.
			// First texture goes in texture unit 0.
			container.Use();

			// This is helpful because System.Drawing reads the pixels differently than OpenGL expects


			// Next, we must setup the samplers in the shaders to use the right textures.
			// The int we send to the uniform is which texture unit the sampler should use.
			shader.SetInt("texture0", 0);



			//Bind the vertex array to set the shader pointers
			GL.BindVertexArray(vao);
			//No clue what I did exactly but array locations might be hardcoded in the shader
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
			_view = Matrix4.CreateTranslation(player);//



			_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Width / (float)Height, 0.1f, 100.0f);




			//Set some info for the shader to know how big each buffer, how many bytes each element take, their offset, etc their kind
			//Bind the buffer we want to be used in the "slot" we specify in the next call
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbov);
			//We have a vector 3 for the positions so 3 floats, not normalized, the stride is 3 because we don't store anything else in this buffer and offset 0 because we start from start
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			//Same but for the colors
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboc);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_);
			GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

			reflectsky_shader.Use();
			GL.BindVertexArray(vao_1);
			//No clue what I did exactly but array locations might be hardcoded in the shader
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
			//GL.EnableVertexAttribArray(3);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbov_1);
			//We have a vector 3 for the positions so 3 floats, not normalized, the stride is 3 because we don't store anything else in this buffer and offset 0 because we start from start
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			//Same but for the colors
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbc_1);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_1);
			GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
			//https://github.com/opentk/LearnOpenTK
			//rect_ =
			//rectv_ = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, rectv_);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices_1.Count *Vector3.SizeInBytes , vertices_1.ToArray(), BufferUsageHint.StaticDraw);//rect3d Length sizeof(float) ()
			GL.BindVertexArray(rect_);
			GL.BindBuffer(BufferTarget.ArrayBuffer, rectv_);
			var positionLoc = reflectsky_shader.GetAttribLocation("pos");
			GL.EnableVertexAttribArray(positionLoc);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float),0);
			var normalLocation = reflectsky_shader.GetAttribLocation("nx");
			GL.EnableVertexAttribArray(normalLocation);
			GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 0 * sizeof(float), 0 * sizeof(float));
			/*GL.BindBuffer(BufferTarget.ArrayBuffer,recttex);//vbo_1
			GL.BufferData(BufferTarget.ArrayBuffer,texCoords_1.Count * Vector2.SizeInBytes,texCoords_1.ToArray(), BufferUsageHint.StaticDraw);*/
			//GL.BindBuffer(BufferTarget.ArrayBuffer,recttex);/*//vbo_1*/
            
            var texLocation = reflectsky_shader.GetAttribLocation("tex");//Coord
			GL.EnableVertexAttribArray(texLocation);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 3 * sizeof(float), 1 * sizeof(float));
            //GL.VertexAttribPointer(texLocation, 2, VertexAttribPointerType.Float, false, 0 * sizeof(float), 0 * sizeof(float));/**/
			/*GL.BindBuffer(BufferTarget.ArrayBuffer, vbo___1);
			GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false,3 * sizeof(float), 3 * sizeof(float));*/

			//Use(container, TextureUnit.Texture0);
			//shader.SetInt("tex0", 0);/////////
			//shader.SetInt("tex1", 1);/////////

			//Why create the buffer there when you can do it on render and load the cpu every frame for creating the triangle

			/*Player player_ = new Player();
			players.Add(player_);
			players.Add(player_);*/

			bool player_ = false;

            username = Console.ReadLine();

            while (true)
			{

				

				// send an update to the server
				tcpConnection.Send(new PositionUpdate() { X = player.X, Y = player.Y , Z = player.Z, username = username,update = update});
				//, players = p
				//(float)random.NextDouble()
				//(float)random.NextDouble()
				//(float)random.NextDouble()
				// ask the server for our position
				PositionResponse myPositionFromServer = await tcpConnection.SendAsync<PositionResponse>(new PositionRequest());


				json_buffer = myPositionFromServer.players;
                List<Player> Playerbuffer;
                Playerbuffer = JsonConvert.DeserializeObject<List<Player>>(json_buffer);
                if (Playerbuffer.Count > 1)
                    players = Playerbuffer;
                if (player_ == false)
				{

					//players = JsonConvert.DeserializeObject<List<Player>>(json_buffer);
					player_ = true;
				}

				//.Add()

																				   //}
																				   //Console.WriteLine(players.Count);

				//Console.WriteLine(players[1] .x);//myPositionFromServer.players
				// dont stress the cpu
				Thread.Sleep(1); //
			}

		}




		float yaw = 0.0f;

		/*public void player__(string player_____)
		{
			char[]chars = new char[] { ' ', '\t' };

			string[] v = player_____.Split(chars,StringSplitOptions.RemoveEmptyEntries);
			//Console.WriteLine(player_____);
			// try
			// {
			//Console.WriteLine(players.Count);

			for (int xj = 0; xj < v.Length; xj++)
            {
				//if()
				if(v[xj] == "pos_update")
				{
					Console.WriteLine(v[xj + 1]);
					for (int xjj = 0; xjj < players.Count; xjj++)
					{
						
						if (v[xj+1].Contains(players[xjj].username))
						{
							//	Console.WriteLine(  + " UPDATE:"+update+" SERVERUPDATE:"+updateserver);
							//Console.WriteLine(players[xjj].x);
							//Console.WriteLine(Single.Parse(v[xj + 3]).ToString());
							players[xjj].setX(Single.Parse(v[xj + 2]));//.x =  //
							players[xjj].y = 1;

							players[xjj].z = Single.Parse(v[xj + 4]);
							//Console.WriteLine(players[xjj].y);
							//Console.WriteLine(players[xjj].x);
						}
					}/**/
			/*	}
			}
			/*}
			catch (IndexOutOfRangeException)
			{

				//throw;
			}*/
		/*}*/
		public string update = "";
		public string updateserver = "";
		public void walkForward(float distance)
		{
			// double yaw_ =  (Math.PI / 180) * yaw;
			player.X -= distance * (float)Math.Sin((Math.PI / 180) * yaw);
			player.Z += distance * (float)Math.Cos((Math.PI / 180) * yaw);
			
		}
		public void walkBackwards(float distance)
		{
			// double yaw_ =  (Math.PI / 180) * yaw;


			player.X += distance * (float)Math.Sin((Math.PI / 180) * yaw);
			player.Z -= distance * (float)Math.Cos((Math.PI / 180) * yaw);
		}
		public void walkRight(float distance)
		{
			// double yaw_ =  (Math.PI / 180) * yaw;

			player.X -= distance * (float)Math.Sin((Math.PI / 180) * -yaw + 90);
			player.Z += distance * (float)Math.Cos((Math.PI / 180) * -yaw - 90); //
		}

		public void walkLeft(float distance)
		{
			// double yaw_ =  (Math.PI / 180) * yaw;
			player.X += distance * (float)Math.Sin((Math.PI / 180) * -yaw + 90);
			player.Z -= distance * (float)Math.Cos((Math.PI / 180) * -yaw - 90); // // // // // // // //

		}
		public void walk__(float distance)
		{
			// double yaw_ =  (Math.PI / 180) * yaw;
			player.Y += distance * (float)Math.Sin((Math.PI / 180) * -yaw + 90);
			//player.Z -= distance * (float)Math.Cos((Math.PI / 180) * -yaw - 90); // // // // // // // //
			player.Y -= 0.1f;
		}

		//()
		string json_buffer;
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			//Ignore this too
			base.OnRenderFrame(e);
			//Clear the window
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);



			GL.Enable(EnableCap.DepthTest);
			_time += 4.0 * e.Time;

			//shader.SetInt("texture0", 0);
			container.Use();
			//shader.set
			yaw = Mouse.GetState().X;//* (float)e.Time
			_view = Matrix4.CreateTranslation(player) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw)) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Mouse.GetState().Y));//.RadiansToDegrees
			shader.SetFloat("time", (float)_time);
			/*
			shader.SetFloat("random", (float)new Random().NextDouble());
			*/
			//	shader.SetFloat("player_z_coord", player.Z);

			//Now we do the equivalent to GL.begin
		//	vertices = vertices;
			colors = new List<Vector3>();
			//texCoords = texCoords;//new List<Vector2>()
			//vertices_1 = vertices_1;//models[0] models[0] models[0] models[0]
			colors_1 = new List<Vector3>();
		//	texCoords_1 = texCoords_1;//new List<Vector2>()
			for (int i = 0; i < vertices.Count; i++)//_1
			{
				//	int rnd = new Random().Next(1, 5);
				//if(rnd == 1)
				colors.Add(new Vector3(1, 1, 1));
				colors_1.Add(new Vector3(1, 1, 1));
				//colors_1.Add(new Vector3(1, 1, 1));
				/*if (rnd == 2)
					colors.Add(new Vector3(1, 0, 1));
				if (rnd == 3)
					colors.Add(new Vector3(0, 0, 1));
				if (rnd == 4)
					colors.Add(new Vector3(0, 1, 1));
				if (rnd == 5)
					colors.Add(new Vector3(1, 1, 0));*/
			}
			//vertices = ;

			//Now we add vertices and colors
			//vertices.Add(new Vector3(-0.5f, -0.5f , 0 ));
			colors.Add(new Vector3(1f, 0f, 0f));//+ new Random().Next(1, 90)
												//vertices.Add(new Vector3(+0.5f , -0.5f, 0 ));
			colors.Add(new Vector3(0f, 1f, 0f));//+ new Random().Next(1, 90)
												//vertices.Add(new Vector3(+0.0f , +0.5f, 0));
			colors.Add(new Vector3(0f, 0f, 1f));



			//Bind the VAO 0 so we don't screw up the previous setup because you need something like gl.begin


			//Some bad equivalent to GL.End
			//Bind the buffer which to be used
		//	GL.BindVertexArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbov);
			//Send data to bound buffer in the specified "slot"
			//The kind of buffer, The size of the array i(the array length * size of an element), The data(because we had a list we convert it to an array), Buffer Usage, doesn't matter that much nowadays
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);
			//Same thing but for colors
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboc);
			GL.BufferData(BufferTarget.ArrayBuffer, colors.Count * Vector3.SizeInBytes, colors.ToArray(), BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_);
			GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Count * Vector2.SizeInBytes, texCoords.ToArray(), BufferUsageHint.StaticDraw);
			
			//	

			//Some bad equivalent to GL.End
			//Bind the buffer which to be used
			//GL.BindVertexArray(vbo_);
			//
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbov_1);
			//Send data to bound buffer in the specified "slot"
			//The kind of buffer, The size of the array i(the array length * size of an element), The data(because we had a list we convert it to an array), Buffer Usage, doesn't matter that much nowadays
			GL.BufferData(BufferTarget.ArrayBuffer, vertices_1.Count * Vector3.SizeInBytes, vertices_1.ToArray(), BufferUsageHint.StaticDraw);
			//Same thing but for colors
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbc_1);
			GL.BufferData(BufferTarget.ArrayBuffer, colors_1.Count * Vector3.SizeInBytes, colors_1.ToArray(), BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_1);
			GL.BufferData(BufferTarget.ArrayBuffer, texCoords_1.Count * Vector2.SizeInBytes, texCoords_1.ToArray(), BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo___1);
			GL.BufferData(BufferTarget.ArrayBuffer, nx.Count * Vector3.SizeInBytes, nx.ToArray(), BufferUsageHint.StaticDraw);

			//GL.BindVertexArray(rect_);
			
			GL.BindBuffer(BufferTarget.ArrayBuffer, recttex);//vbo_1
			GL.BufferData(BufferTarget.ArrayBuffer, texCoords_1.Count * Vector2.SizeInBytes, texCoords_1.ToArray(), BufferUsageHint.StaticDraw);
			//GL.BindBuffer(BufferTarget.ArrayBuffer, recttex);//vbo_1/**/
															 //GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
			//var texLocation = reflectsky_shader.GetAttribLocation("texCoord");
			//GL.EnableVertexAttribArray(texLocation);
			//GL.VertexAttribPointer(texLocation, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);/**/



			GL.BindVertexArray(0);
			//Bind the shader program to draw our triangle
			shader.Use();//reflectsky_shader
						 //Bind the vao and all the things we setup for it to be used by the shader
			


			GL.BindVertexArray(vao);//_1_1


			//Finally we DrawArrays to draw

			//We draw triangles, we start from 0 and we have0 3 vertices

			Matrix4 model = Matrix4.Identity;//= .Identity
											 //model = ;
											 //1
			model *= Matrix4.CreateTranslation(new Vector3(1,1 + walking__, 1 ));//object_[i]+walking__  walking__

			shader.SetMatrix4("model", model);
			GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Count);//.Quads ////////
			GL.BindVertexArray(rect_);//_1_1vao_1
								 ////////shader.SetFloat("Zcoord", player.Z);//lf
			reflectsky_shader.Use();
			reflectsky_shader.SetVector3("viewPos", player);

			//List<Player> old_players = players;
			
			for (int xj = 0; xj < players.Count; xj++)
             {
				

				Matrix4 model_ = Matrix4.Identity;
				model_ *= Matrix4.CreateTranslation(new Vector3(-players[xj].x, -players[xj].y + walking__, -players[xj].z + 11));//9+ walking__   walking__ -player.X - 10 - player.Y - player.Z   - 10
																			 //                                                                                               //Matrix4.Identity *			 																								 //Matrix4.Identity *
				reflectsky_shader.SetMatrix4("model", model_);//shader players[xj].z// 1, 1, 1
				reflectsky_shader.SetFloat("zcoord", player.Z);
				
				GL.DrawArrays(PrimitiveType.Triangles, 0, vertices_1.Count);

				Matrix4 model__ = Matrix4.Identity;//
				

				//try
				//{

				//GL.DrawArrays(PrimitiveType.Triangles, 0, vertices_1.Count);//.Quads



				//Console.WriteLine(players[1].x);
				//}
				//catch (ArgumentOutOfRangeException)
				//{

				//throw;
				//}
			}
			//players = JsonConvert.DeserializeObject<List<Player>>(json_buffer);//.Add()
			for (int i = 0; i < object_.Length; i++)
			{


			

				/*if (
	  ((int)player.X <= 10 + (int)object_[i].X && 1 + (int)player.X >= (int)object_[i].X) &&
	  ((int)player.Y <= 1 + (int)object_[i].Y && 1 + (int)player.Y >= (int)object_[i].Y) &&
	  ((int)player.Z <= 10 + (int)object_[i].Z && 1 + (int)player.Z >= (int)object_[i].Z)
	 )
				{
					
					//Console.WriteLine("Collision Detection");

				}*/
				/*Console.WriteLine("Vertices" + vertices.Count.ToString());
				Console.WriteLine("Colors" + colors.Count.ToString());
				Console.WriteLine("TexCoords" + texCoords.Count.ToString());
				Console.WriteLine("Vertices_1" + vertices_1.Count.ToString());
				Console.WriteLine("Colors_1" + colors_1.Count.ToString());
				Console.WriteLine("TexCoords_1" + texCoords_1.Count.ToString());*/
				

																		  //.Quads
			}



			shader.SetMatrix4("view", _view);
			shader.SetMatrix4("projection", _projection);
			reflectsky_shader.SetMatrix4("view", _view);
			reflectsky_shader.SetMatrix4("projection", _projection);


			SwapBuffers();
            
        }

		public void Use(int texture, TextureUnit unit = TextureUnit.Texture0)
		{
			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, texture);
		}


		protected override async void OnUpdateFrame(FrameEventArgs e)
		{
			var input = Keyboard.GetState();
			if(this.Focused == true)
			{
				if (input.IsKeyDown(Key.Escape))
			{
                    //Exit();
                    
                }
			if (input.IsKeyDown(Key.W))
			{
				walkForward(3 * (float)e.Time);
				walk________();
			}
			//player.Z += 0.1f;
			if (input.IsKeyDown(Key.S))
			{
				walkBackwards(3 * (float)e.Time);
				walk________();
			}
			//player.Z -= 0.1f;
			if (input.IsKeyDown(Key.D))
			{
				walkRight(3 * (float)e.Time);
				walk________();
			}

			//player.X-= 0.1f;
			if (input.IsKeyDown(Key.A))
			{
				walkLeft(3 * (float)e.Time);
				walk________();
			}
			//player.X += 0.1f;
			if (input.IsKeyDown(Key.E))
			{
				player.Y -= 0.1f;
				walk________();
			}
			if (input.IsKeyDown(Key.V))
			{
				//walk__(3 * (float)e.Time);
				player.Y += 0.1f;
				walk________();
			}
			if(input.IsKeyDown(Key.Space))
			{
                for (int xj = 0; xj < 9; xj++)
                {
					player.Y -= 0.1f*(float)e.Time;
					await Sys.waitFunction(1);
                }
			}
			base.OnUpdateFrame(e);
		}

		}
	}
	public static class Sys
	{

		public static async Task waitFunction(int time)
		{
			await Task.Delay(time);
		}

	}
}
/*public static async void wait(int time)
{
	await waitFunction(time);

}*/