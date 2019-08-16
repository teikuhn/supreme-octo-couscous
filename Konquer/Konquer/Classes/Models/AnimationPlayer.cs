using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konquer.Classes.Models
{
    // De AnimationPlayer klasse maakt het afspelen van animations mogelijk, waar de Animation een object is wijst de AnimationPlayer gedrag toe aan animations.
    struct AnimationPlayer
    {
        Animation animation;
        Rectangle drawRectangle;
        public Animation Animation
        {
            get { return animation; }
        }

        int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }

        private float timer;
        public Vector2 Origin
        {
            get { return new Vector2(animation.FrameWidth / 2, animation.FrameHeight); }
        }

        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;

            animation = newAnimation;
            frameIndex = 0;
            timer = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation selected");
            if (drawRectangle == null)
                new Rectangle(frameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;

                if (animation.IsLooping)
                    frameIndex = (frameIndex + 1) % animation.FrameCount;
                else frameIndex = Math.Min(frameIndex + 1, animation.FrameCount - 1);
            }

            drawRectangle.X = frameIndex * Animation.FrameWidth;
            drawRectangle.Y = 0;
            drawRectangle.Width = Animation.FrameWidth;
            drawRectangle.Height = Animation.FrameHeight;

            position.Y += 40;
            position.X += 16;

            spriteBatch.Draw(Animation.Texture, position, drawRectangle, Color.White, 0f, Origin, 1f, spriteEffects, 0f);
        }
    }
}
