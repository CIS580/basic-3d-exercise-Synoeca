using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
	/// <summary>
	/// A class for rendering a single triangle
	/// </summary>
	public class Triangle
	{
		/// <summary>
		/// The vertices of the triangle
		/// </summary>
		private VertexPositionColor[] _vertices;

		/// <summary>
		/// The effect to use rendering the triangle
		/// </summary>
		private BasicEffect _effect;

		/// <summary>
		/// The game this triangle belongs to
		/// </summary>
		private readonly Game _game;

		/// <summary>
		/// Constructs a triangle instance
		/// </summary>
		/// <param name="game">The game that is creating the triangle</param>
		public Triangle(Game game)
		{
			this._game = game;
			InitializeVertices();
			InitializeEffect();
		}

		/// <summary>
		/// Initializes the vertices of the triangle
		/// </summary>
		private void InitializeVertices()
		{
			_vertices = new VertexPositionColor[3];
			// vertex 0
			_vertices[0].Position = new Vector3(0, 1, 0);
			_vertices[0].Color = Color.Red;
			// vertex 1
			_vertices[1].Position = new Vector3(1, 1, 0);
			_vertices[1].Color = Color.Green;
			// vertex 2
			_vertices[2].Position = new Vector3(1, 0, 0);
			_vertices[2].Color = Color.Blue;
		}

		/// <summary>
		/// Initializes the BasicEffect to render our triangle
		/// </summary>
		private void InitializeEffect()
		{
			_effect = new BasicEffect(_game.GraphicsDevice);
			_effect.World = Matrix.Identity;
			_effect.View = Matrix.CreateLookAt(
				new Vector3(0, 0, 4),	// The camera position
				new Vector3(0, 0, 0),	// The camera target,
				Vector3.Up							// The camera up vector
			);
			_effect.Projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,								// The field-of-view
				_game.GraphicsDevice.Viewport.AspectRatio,		// The aspect ratio
				0.1f,	// The near plane distance
				100.0f	// The far plane distance
			);
			_effect.VertexColorEnabled = true;
		}

		/// <summary>
		/// Rotates the triangle around the y-axis
		/// </summary>
		/// <param name="gameTime">The GameTime object</param>
		public void Update(GameTime gameTime)
		{
			float angle = (float)gameTime.TotalGameTime.TotalSeconds;
			_effect.World = Matrix.CreateRotationY(angle);
		}
		
		/// <summary>
		/// Draws the triangle
		/// </summary>
		public void Draw()
		{
			// Cache old rasterizer State
			RasterizerState oldState = _game.GraphicsDevice.RasterizerState;

			// Disable backface culling
			RasterizerState rasterizerState = new();
			rasterizerState.CullMode = CullMode.None;
			_game.GraphicsDevice.RasterizerState = rasterizerState;

			// Apply our effect
			_effect.CurrentTechnique.Passes[0].Apply();

			// Draw the triangle
			_game.GraphicsDevice.DrawUserPrimitives(
				PrimitiveType.TriangleList,
				_vertices,
				0,
				1
			);

			// Restore the prior rasterizer state
			_game.GraphicsDevice.RasterizerState = oldState;
		}
	}
}
