using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace Basic3DExample
{
	/// <summary>
	/// A class for rendering a cube
	/// </summary>
	public class Cube
	{
		/// <summary>
		/// The vertices of the cube
		/// </summary>
		private VertexBuffer _vertices;

		/// <summary>
		/// The vertex indices of the cube
		/// </summary>
		private IndexBuffer _indices;

		/// <summary>
		/// The effect to use rendering the cube
		/// </summary>
		private BasicEffect _effect;

		/// <summary>
		/// The game this cube belongs to
		/// </summary>
		private readonly Game _game;

		/// <summary>
		/// Construct a cube instance
		/// </summary>
		/// <param name="game">The game that is creating the cube</param>
		public Cube(Game game)
		{
			_game = game;
			InitializeVertices();
			InitializeIndices();
			InitializeEffect();
		}

		/// <summary>
		/// Initialize the vertex buffer
		/// </summary>
		public void InitializeVertices()
		{
			VertexPositionColor[] vertexData = {
				new() { Position = new Vector3(-3, 3, -3), Color = Color.Blue },
				new() { Position = new Vector3(3, 3, -3), Color = Color.Green },
				new() { Position = new Vector3(-3, -3, -3), Color = Color.Red },
				new() { Position = new Vector3(3, -3, -3), Color = Color.Cyan },
				new() { Position = new Vector3(-3, 3, 3), Color = Color.Blue },
				new() { Position = new Vector3(3, 3, 3), Color = Color.Red },
				new() { Position = new Vector3(-3, -3, 3), Color = Color.Green },
				new() { Position = new Vector3(3, -3, 3), Color = Color.Cyan }
			};
			_vertices = new VertexBuffer(
				_game.GraphicsDevice,			// the graphics device
				typeof(VertexPositionColor),	// the type of the vertex data
				8,					// the count of the vertices
				BufferUsage.None				// how the buffer will be used
			);
			_vertices.SetData(vertexData);
		}

		/// <summary>
		/// Initialize the index buffer
		/// </summary>
		public void InitializeIndices()
		{
			short[] indexData =
			{
				0, 1, 2,	// Side 0
				2, 1, 3,
				4, 0, 6,	// Side 1
				6, 0, 2,
				7, 5, 6,	// Side 2
				6, 5, 4,
				3, 1, 7,	// Side 3
				7, 1, 5,
				4, 5, 0,	// Side 4
				0, 5, 1,
				3, 7, 2,	// Side 5
				2, 7, 6
			};
			_indices = new IndexBuffer(
				_game.GraphicsDevice,			// The graphics devices to use
				IndexElementSize.SixteenBits,	// The size of the index
				36,					// The count of the indices
				BufferUsage.None				// How the buffer will be used
			);
			_indices.SetData(indexData);
		}

		/// <summary>
		/// Initializes the BasicEffect to render our cube
		/// </summary>
		private void InitializeEffect()
		{
			_effect = new BasicEffect(_game.GraphicsDevice);
			_effect.World = Matrix.Identity;
			_effect.View = Matrix.CreateLookAt(
				new Vector3(0, 0, 4),	// The camera position
				new Vector3(0, 0, 4),	// The camera target
				Vector3.Up							// The camera up vector
			);
			_effect.Projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,							// The field-of-view
				_game.GraphicsDevice.Viewport.AspectRatio,	// The aspect ratio
				0.1f,	// The near plane distance
				100.0f	// The far plane distance
			);
			_effect.VertexColorEnabled = true;
		}

		/// <summary>
		/// Updates the Cube
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			float angle = (float)gameTime.TotalGameTime.TotalSeconds;
			// Look at the cube from farther away
			_effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(
				new Vector3(0, 5, -10),
				Vector3.Zero,
				Vector3.Up
			);
		}

		/// <summary>
		/// Draws the Cube
		/// </summary>
		public void Draw()
		{
			// apply the effect
			_effect.CurrentTechnique.Passes[0].Apply();
			// set the vertex buffer
			_game.GraphicsDevice.SetVertexBuffer(_vertices);
			// set the index buffer
			_game.GraphicsDevice.Indices = _indices;
			// draw the triangles
			_game.GraphicsDevice.DrawIndexedPrimitives(
				PrimitiveType.TriangleList,	// The type to draw
				0,					// The first vertex to use
				0,					// The first index to use
				12				// The number of triangles to draw
			);
		}
	}
}
