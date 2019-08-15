
using Konquer.Classes.Models;
using Konquer.Classes.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konquer.Classes.Sprites
{
    // In de Player klasse wordt gameplay input verwerkt en positie/beweging berekend.
    public class Player : Sprite
    {
        private Vector2 pastPosition;
        public Vector2 Movement { get; set; }

        public bool IsGrounded()
        {
            Rectangle onePixelBelow = Bounds;
            onePixelBelow.Offset(0, 1);
            return !Board.CurrentBoard.HasRoomForRectangle(onePixelBelow);
        }

        public Player(Texture2D texture, Vector2 position, SpriteBatch spriteBatch)
            : base(texture, position, spriteBatch)
        {
        }



        public void Update(GameTime gameTime)
        {
            CaptureInputUpdateMovement();
            ApplyNewton();
            SimulateFriction();
            MoveIfOk(gameTime);
            StopWhenBlocked();
        }

        private void ApplyNewton()
        {
            Movement += Vector2.UnitY * .9f;
        }

        private void MoveIfOk(GameTime gameTime)
        {
            pastPosition = Position;
            UpdatePosition(gameTime);
            Position = Board.CurrentBoard.AllowedMovement(pastPosition, Position, Bounds);
        }

        private void StopWhenBlocked()
        {
            Vector2 lastMovement = Position - pastPosition;
            if(lastMovement.X == 0) { Movement *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { Movement *= Vector2.UnitX; }
        }

        private void CaptureInputUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) { Movement += new Vector2(-1, 0); }
            if (keyboardState.IsKeyDown(Keys.Right)) { Movement += new Vector2(1, 0); }
            if (keyboardState.IsKeyDown(Keys.Space) && IsGrounded()) { Movement = -Vector2.UnitY * 55;}
        }

        private void SimulateFriction()
        {
            if (IsGrounded()) { Movement -= Movement * Vector2.One * .2f; }
            else { Movement -= Movement * Vector2.One * .05f; }
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }
    }
}
