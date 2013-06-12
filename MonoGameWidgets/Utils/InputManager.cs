using System.Collections.Generic;
using Microsoft.Xna.Framework.Input.Touch;

namespace MonoGameWidgets.Utils
{
    public class InputManager
    {
        private TouchCollection _touches = new TouchCollection(new TouchLocation[0]);
        private Dictionary<int, TouchLocation> _releasedTouches = new Dictionary<int, TouchLocation>();
        private HashSet<int> _handledInputs = new HashSet<int>(); 
        public void ReadInput()
        {
            var oldCollection = _touches;
            _touches = TouchPanel.GetState();

            _releasedTouches.Clear();
            _handledInputs.Clear();
            TouchLocation ignore;
            foreach (var oldTouch in oldCollection)
            {
                if (!_touches.FindById(oldTouch.Id, out ignore))
                {
                    _releasedTouches[oldTouch.Id] = new TouchLocation(oldTouch.Id, TouchLocationState.Released,
                                                           oldTouch.Position, oldTouch.State, oldTouch.Position);
                }
            }
        }

        public bool FindById(int id, out TouchLocation touchLocation)
        {
            return _touches.FindById(id, out touchLocation) || _releasedTouches.TryGetValue(id, out touchLocation);
        }

        public int Count
        {
            get { return _touches.Count; }
        }

        public TouchLocation this[int index]
        {
            get { return _touches[index]; }
            set { _touches[index] = value; }
        }

        public IEnumerator<TouchLocation> GetEnumerator()
        {
            return _touches.GetEnumerator();
        }

        public bool IsHandled(int id)
        {
            return _handledInputs.Contains(id);
        }

        public void MarkAsHandled(int id)
        {
            _handledInputs.Add(id);
        }
    }
}