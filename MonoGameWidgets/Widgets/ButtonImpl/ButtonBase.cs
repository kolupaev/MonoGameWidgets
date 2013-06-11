using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace MonoGameWidgets.Widgets.ButtonImpl
{
    public abstract class ButtonBase
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

        public virtual void HandleInput()
        {
            var touches = TouchPanel.GetState();
            if (!_inTouch && touches.Count > 0 && touches[0].State == TouchLocationState.Pressed)
            {
                var touch = touches[0];
                if (InActiveArea(touch.Position))
                {
                    BeginSelection(touch);
                }
            }
            else if (_inTouch)
            {
                TouchLocation touch;
                if (!touches.FindById(_touch.Id, out touch))
                {
                    BeginUnselection();
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

        private void BeginUnselection()
        {
            if (_highlighted)
            {
                OnOnTap();
            }
            BeginUnhighlight();
            _inTouch = false;
        }

        private void BeginSelection(TouchLocation touch)
        {
            _inTouch = true;
            _touch = touch;
            BeginHighlight();
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
    }
}