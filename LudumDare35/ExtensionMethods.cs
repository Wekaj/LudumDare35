using SFML.Graphics;
using SFML.System;
using System;

namespace LudumDare35
{
    internal static class ExtensionMethods
    {
        public static float GetAngle(this Direction2D direction)
        {
            switch (direction)
            {
                case Direction2D.Up:
                    return 0;
                case Direction2D.Right:
                    return 90;
                case Direction2D.Down:
                    return 180;
                case Direction2D.Left:
                    return 270;
            }
            throw new Exception("Could not get an angle.");
        }

        public static Vector2i GetUnitVector(this Direction2D direction)
        {
            switch (direction)
            {
                case Direction2D.Up:
                    return new Vector2i(0, -1);
                case Direction2D.Right:
                    return new Vector2i(1, 0);
                case Direction2D.Down:
                    return new Vector2i(0, 1);
                case Direction2D.Left:
                    return new Vector2i(-1, 0);
            }
            return new Vector2i(0, 0);
        }

        public static int GetDirectionID(this Direction2D direction)
        {
            switch (direction)
            {
                case Direction2D.Up:
                    return 0;
                case Direction2D.Right:
                    return 1;
                case Direction2D.Down:
                    return 2;
                case Direction2D.Left:
                    return 3;
            }
            return -1;
        }

        public static float Left(this View view) => view.Center.X - view.Size.X / 2f;
        public static float Top(this View view) => view.Center.Y - view.Size.Y / 2f;
        public static float Right(this View view) => view.Center.X + view.Size.X / 2f;
        public static float Bottom(this View view) => view.Center.Y + view.Size.Y / 2f;
        public static Vector2f TopLeft(this View view) => view.Center - view.Size / 2f;
        public static Vector2f BottomRight(this View view) => view.Center + view.Size / 2f;

        public static Vector2f ToFloat(this Vector2i vector) => new Vector2f(vector.X, vector.Y);
        public static Vector2f ToFloat(this Vector2u vector) => new Vector2f(vector.X, vector.Y);
        public static Vector2f Round(this Vector2f vector) => new Vector2f((float)Math.Round(vector.X), (float)Math.Round(vector.Y));
    }
}
