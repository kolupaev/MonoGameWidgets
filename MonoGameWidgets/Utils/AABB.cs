using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Utils
{
    public class AABB
    {
        private const float Epsilon = .00001f;
        private Vector2 _vector;
        internal Vector2 _max = Vector2.Zero;
        internal Vector2 _min = Vector2.Zero;

        public AABB()
        {
        }

        public AABB(AABB aabb)
        {
            _min = aabb.Min;
            _max = aabb.Max;
        }

        public AABB(Vector2 min, Vector2 max)
        {
            this._min = min;
            this._max = max;
        }

        
        /// <summary>
        /// Gets the _min.
        /// </summary>
        /// <Value>The _min.</Value>
        public Vector2 Min
        {
            get { return _min; }
        }

        /// <summary>
        /// Gets the _max.
        /// </summary>
        /// <Value>The _max.</Value>
        public Vector2 Max
        {
            get { return _max; }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <Value>The width.</Value>
        public float Width
        {
            get { return _max.X - _min.X; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <Value>The height.</Value>
        public float Height
        {
            get { return _max.Y - _min.Y; }
        }


        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCenter()
        {
            return new Vector2(Min.X + (Max.X - Min.X) / 2, Min.Y + (Max.Y - Min.Y) / 2);
        }

        

        /// <summary>
        /// Determines whether the AAABB contains the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// 	<c>true</c> if it contains the specified point; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Vector2 point)
        {
            //using _epsilon to try and gaurd against float rounding errors.
            if ((point.X > (_min.X + Epsilon) && point.X < (_max.X - Epsilon) &&
                 (point.Y > (_min.Y + Epsilon) && point.Y < (_max.Y - Epsilon))))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the AAABB contains the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// 	<c>true</c> if it contains the specified point; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ref Vector2 point)
        {
            //using _epsilon to try and gaurd against float rounding errors.
            if ((point.X > (_min.X + Epsilon) && point.X < (_max.X - Epsilon) &&
                 (point.Y > (_min.Y + Epsilon) && point.Y < (_max.Y - Epsilon))))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if 2 AABB's intersects
        /// </summary>
        /// <param name="aabb1">The aabb1.</param>
        /// <param name="aabb2">The aabb2.</param>
        /// <returns></returns>
        public static bool Intersect(AABB aabb1, AABB aabb2)
        {
            if (aabb1._min.X > aabb2._max.X || aabb2._min.X > aabb1._max.X)
            {
                return false;
            }

            if (aabb1._min.Y > aabb2.Max.Y || aabb2._min.Y > aabb1.Max.Y)
            {
                return false;
            }
            return true;
        }

        public AABB Inflate(Vector2 i)
        {
            var adjust = i/2f;
            return new AABB(Min - adjust, Max + adjust);
        }
    }
}