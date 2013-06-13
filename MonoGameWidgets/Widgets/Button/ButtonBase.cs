using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGameWidgets.Utils;

namespace MonoGameWidgets.Widgets.ButtonImpl
{
    public abstract class ButtonBase : IWidget
    {
        protected float Scale = 1f;
        protected float Rotation;
        private readonly Queue<IAnimation> _animationQueue = new Queue<IAnimation>(4);

        private bool _inTouch = false;
        private TouchLocation _touch;
        private bool _highlighted;
        public event Action OnTap;

        protected virtual void OnOnTap()
        {
            Action handler = OnTap;
            if (handler != null) handler();
        }

        public virtual void HandleInput(InputManager touches)
        {
            if (!_inTouch && touches.Count > 0 && touches[0].State == TouchLocationState.Pressed)
            {
                var touch = touches[0];
                if (InActiveArea(touch.Position))
                {
                    _touch = touch;
                    _inTouch = true;
                }
            }
            else if (_inTouch)
            {
                TouchLocation touch;
                if (!touches.FindById(_touch.Id, out touch))
                {
                    _inTouch = false;
                    BeginUnhighlight();
                }
                else
                {
                    if (touch.State == TouchLocationState.Released && InActiveArea(touch.Position))
                    {
                        if (!touches.IsHandled(touch.Id))
                        {
                            OnOnTap();
                            touches.MarkAsHandled(touch.Id);
                        }
                        _inTouch = false;
                        BeginUnhighlight();
                    }
                    else
                    {
                        if (InActiveArea(touch.Position))
                        {
                            BeginHighlight();
                        }
                        else
                        {
                            BeginUnhighlight();
                        }
                    }
                }
            }
        }

        private void BeginUnhighlight()
        {
            if (!_highlighted)
                return;
            _highlighted = false;
            _animationQueue.Clear();
            _animationQueue.Enqueue(Animation.Value(Scale, 1f, TimeSpan.FromSeconds(0.3f), (v) => { Scale = v; }));
        }

        private void BeginHighlight()
        {
            if (_highlighted)
                return;

            _highlighted = true;
            _animationQueue.Clear();
            _animationQueue.Enqueue(Animation.Value(Scale, 1.3f, TimeSpan.FromSeconds(0.3f), (v) => { Scale = v; }));
        }

      

        protected abstract bool InActiveArea(Vector2 position);

        public virtual void Update(GameTime gameTime)
        {
            if (_animationQueue.Count > 0)
            {
                IAnimation head = _animationQueue.Peek();
                if (!head.Started)
                    head.Start(gameTime);
                else if (!head.Done)
                    head.Update(gameTime);
                else
                    _animationQueue.Dequeue();
            }
        }

        public abstract void Draw(GameTime gameTime);

        public abstract void SetPosition(Vector2 position);
    }
}