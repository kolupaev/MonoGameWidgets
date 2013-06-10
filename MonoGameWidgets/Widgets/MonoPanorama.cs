using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace MonoGameWidgets.Widgets
{
    public class MonoPanorama
    {
        private readonly Queue<IAnimation> _animationQueue = new Queue<IAnimation>(4);

        private readonly List<IMonoPanoramaItem> _background = new List<IMonoPanoramaItem>();
        private readonly List<IMonoPanoramaItem> _drawables = new List<IMonoPanoramaItem>();
        private readonly List<IMonoPanoramaItem> _items = new List<IMonoPanoramaItem>();
        private readonly List<IMonoPanoramaItem> _updateables = new List<IMonoPanoramaItem>();

        private int _selectedIndex;
        private TouchLocation? _startTouch;
        private bool _readonly;


        protected double DragToFlipTheshold
        {
            get { return 0.4; }
        }

        protected bool InFlip { get; set; }

        protected float CurrentPageOffset { get; set; }
        protected float BackgroundOffset { get; set; }

        public void AddBackgorund(MonoPanoramaBackgroundItem item)
        {
            if(_readonly)
                throw new InvalidOperationException("Can't add background to initialized panorama.");
            _background.Add(item);
            _drawables.Add(item);
        }

        public void AddPanoramaItem(IMonoPanoramaItem item)
        {
            if (_readonly)
                throw new InvalidOperationException("Can't add background to initialized panorama.");

            _items.Add(item);
            _updateables.Add(item);
            _drawables.Add(item);
        }

        public void HandleInput()
        {
            TouchCollection state = TouchPanel.GetState();
            if (_startTouch == null)
            {
                if (state.Count > 0)
                    _startTouch = state[0];
            }
            else
            {
                TouchLocation currentTouch;
                bool touchFound = state.FindById(_startTouch.Value.Id, out currentTouch);
                if (touchFound)
                {
                    CurrentPageOffset = (currentTouch.Position.X - _startTouch.Value.Position.X);
                }
                else
                {
                    _startTouch = null;
                    if (!InFlip)
                    {
                        if (CurrentPageOffset < -TouchPanel.DisplayWidth * DragToFlipTheshold)
                        {
                            BeginFlip(1);
                        }
                        else if (CurrentPageOffset > TouchPanel.DisplayWidth * DragToFlipTheshold)
                        {
                            BeginFlip(-1);
                        }
                        else
                        {
                            BeginFlip(0);
                        }
                    }
                }
            }
           
        }

        private int FixIndex(int index)
        {
            if (index >= _items.Count)
                return 0;
            if (index < 0)
                return _items.Count - 1;
            return index;
        }

        private void BeginFlip(int direction)
        {
            float target = 0f;
            int newIndex = FixIndex(_selectedIndex + direction);
            if (direction > 0)
                target = -_items[_selectedIndex].Width;
            else if (direction < 0)
                target = _items[newIndex].Width;

            InFlip = true;
            _animationQueue.Enqueue(Animation.Value(CurrentPageOffset, target, TimeSpan.FromSeconds(0.2),
                                                    (v) => CurrentPageOffset = v));
            _animationQueue.Enqueue(Animation.Action(() =>
                {
                    BackgroundOffset += CurrentPageOffset;
                    CurrentPageOffset = 0;
                    _items[_selectedIndex].SetActive(false);
                    _items[newIndex].SetActive(true);
                    _selectedIndex = newIndex;
                    InFlip = false;
                }));

            InFlip = true;
        }

        public void Initialize()
        {
            _readonly = true;
            for (int i = 0; i < _items.Count; i++)
            {
                IMonoPanoramaItem item = _items[i];
                item.SetActive(i == _selectedIndex);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var drawable in _drawables)
            {
                if (drawable.Visible)
                    drawable.Draw(gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();
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

            if (Math.Abs(CurrentPageOffset - 0) > 0.1)
            {
                int sign = Math.Sign(CurrentPageOffset);
                IMonoPanoramaItem currentItem = _items[_selectedIndex];
                IMonoPanoramaItem nextItem = _items[FixIndex(_selectedIndex - sign)];

                nextItem.SetActive(true);
                currentItem.SetDrawOffset(new Vector2(CurrentPageOffset, 0));
                if (sign == 1)
                    nextItem.SetDrawOffset(new Vector2(-nextItem.Width + CurrentPageOffset, 0));
                else if (sign == -1)
                {
                    nextItem.SetDrawOffset(new Vector2(currentItem.Width + CurrentPageOffset, 0));
                }
            }
            var bgOffset = BackgroundOffset + CurrentPageOffset;

            foreach (var bgItem in _background)
            {
                bgItem.SetDrawOffset(new Vector2(bgOffset, 0));
            }

            foreach (var updateable in _updateables)
            {
                updateable.Update(gameTime);
            }
        }
    }
}