using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Utils;
using MonoGameWidgets.Widgets.Common;

namespace MonoGameWidgets.Widgets
{
    public class OffsetCompositeWidget : IWidget
    {
        struct Item
        {
            public Vector2 StaticOffset;
            public IWidget Widget;
        }
        private readonly List<Item> _items = new List<Item>();
        private Vector2 _drawOffset;

        public OffsetCompositeWidget()
        {
        }


        public void Add(Vector2 offset, IWidget widget)
        {
            var cell = new Item() {StaticOffset = offset, Widget = widget};
            _items.Add(cell);
            UpdatePosition(cell);
        }

        private void UpdatePosition(Item item)
        {
            item.Widget.SetDrawOffset(_drawOffset + item.StaticOffset);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var cell in _items)
            {
                cell.Widget.Draw(gameTime);
            }
        }

        public void HandleInput(InputManager inputManager)
        {
            foreach (var item in _items)
            {
                item.Widget.HandleInput(inputManager);
            }
        }

        public void SetDrawOffset(Vector2 offset)
        {
            _drawOffset = offset;
            foreach (var cell in _items)
            {
                UpdatePosition(cell);
            }
        }

    }
}