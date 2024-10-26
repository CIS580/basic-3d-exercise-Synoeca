using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
	/// <summary>
	/// A class representing a quad (a rectangle composed of two triangles)
	/// </summary>
	public class Quad
	{
		/// <summary>
		/// The vertices of the quad
		/// </summary>
		private VertexPositionTexture[] _vertices;

		/// <summary>
		/// The vertex indices of the quad
		/// </summary>
		private short[] _indices;

		/// <summary>
		/// The effect to use rendering the triangle
		/// </summary>
		private BasicEffect _effect;

		/// <summary>
		/// The game this cube belongs to
		/// </summary>
		private Game _game;

		/// <summary>
		/// Construct the Quad
		/// </summary>
		/// <param name="game">The Game the Quad belongs to</param>
		public Quad(Game game)
		{
			_game = game;
			InitializeVertice();
			InitializeIndices();
			InitializeEffect();
		}

		/// <summary>
		/// Initializes the vertices of our quad
		/// </summary>
		public void InitializeVertice()
		{
			_vertices = new VertexPositionTexture[4];
			// define vertex 0 (top left)
			_vertices[0].Position = new Vector3(-1, 1, 0);
			_vertices[0].TextureCoordinate = new Vector2(0, -1);
			// define vertex 1 (top right)
			_vertices[1].Position = new Vector3(1, 1, 0);
			_vertices[1].TextureCoordinate = new Vector2(1, -1);
			// define vertex 2 (bottom right)
			_vertices[2].Position = new Vector3(1, -1, 0);
			_vertices[2].TextureCoordinate = new Vector2(1, 0);
			// define vertex 3 (bottom left)
			_vertices[3].Position = new Vector3(-1, -1, 0);
			_vertices[3].TextureCoordinate = new Vector2(0, 0);
		}

		/// <summary>
		/// Initialize the indices of our quad
		/// </summary>
		public void InitializeIndices()
		{
			_indices = new short[6];

			// define triangle 0
			_indices[0] = 0;
			_indices[1] = 1;
			_indices[2] = 2;
			// define triangle 1
			_indices[3] = 2;
			_indices[4] = 3;
			_indices[5] = 0;
		}

		/// <summary>
		/// Initializes the basic effect used to draw the quad
		/// </summary>
		public void InitializeEffect()
		{
			_effect = new BasicEffect(_game.GraphicsDevice);
			_effect.World = Matrix.Identity;
			_effect.View = Matrix.CreateLookAt(
				new Vector3(0, 0, 4),	// The camera position
				new Vector3(0, 0, 0),	// The camera target
				Vector3.Up
			);
			_effect.Projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,								// The field-of-view
				_game.GraphicsDevice.Viewport.AspectRatio,		// The aspect ratio
				0.1f,							// The near plane distance
				100.0f							// The far plane distance
			);
			_effect.TextureEnabled = true;
			_effect.Texture = _game.Content.Load<Texture2D>("monogame-logo");
		}

		/// <summary>
		/// Draws the quad
		/// </summary>
		public void Draw()
		{
			// cache the old blend state
			BlendState oldBlendState = _game.GraphicsDevice.BlendState;

			// enable alpha blending
			_game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

			// apply our effect
			_effect.CurrentTechnique.Passes[0].Apply();

			// render the quad
			_game.GraphicsDevice.DrawUserIndexedPrimitives(
				PrimitiveType.TriangleList,
				_vertices,		// the vertex collection
				0,	// the starting index in the vertex array
				4,	// the number of indices in the shape
				_indices,		// the index collection
				0,	// the starting index in the index array
				2	// the number of triangles to draw
			);

			// restore the old blend state
			_game.GraphicsDevice.BlendState = oldBlendState;
		}
	}
}
