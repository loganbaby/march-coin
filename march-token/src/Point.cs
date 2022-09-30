﻿using System;
using System.Runtime.CompilerServices;

namespace march_token.src
{
    internal class Point
    {
        private FieldElement x;

        private FieldElement y;

        private FieldElement a;

        private FieldElement b;

        public Point(FieldElement x, FieldElement y, FieldElement a, FieldElement b)
        {
            try
            {
                this.x = x;
                this.y = y;
                this.a = a;
                this.b = b;

                if (this.x is null && this.y is null) return;
                else if (this.y.powFieldElement(2) != this.x.powFieldElement(3) + this.a * this.x + this.b) throw new Exception("Exception: point not on the curve");
            }

            catch(Exception exception)
            {
                Console.WriteLine(exception);
                Environment.Exit(1);
            }
        }

        public static Point operator +(Point current, Point other)
        {
            try
            {
                if (current.a != other.a && current.b != other.b) throw new Exception("Exception: point not on the curve");

                else if(current.x != other.x)
                {
                    var incline = (other.y - current.y) / (other.x - current.x);
                    var new_x = incline.powFieldElement(2) - current.x - other.x;
                    var new_y = incline * (current.x - new_x) - current.y;

                    return new Point(new_x, new_y, current.a, current.b);
                }

                else if(current == other)
                {
                    var incline = (new FieldElement(3, current.x.getPrime()) * current.x.powFieldElement(2) + current.a) / (new FieldElement(2, current.y.getPrime()) * current.y);
                    var new_x = incline.powFieldElement(2) - new FieldElement(2, current.x.getPrime()) * current.x;
                    var new_y = incline * (current.x - new_x) - current.y;

                    return new Point(new_x, new_y, current.a, current.b);
                }

                else if(current.x == other.x)
                {
                    return new Point(null, null, current.a, current.b);
                }

                else if(current.x is null && current.y is null)
                {
                    return new Point(null, null, current.a, current.b);
                }

                else if(current == other && current.y.getNumber() == 0 && other.y.getNumber() == 0)
                {
                    return new Point(null, null, current.a, current.b);
                }

                else if(current.x is null)
                {
                    return other;
                }

                else if(current.y is null)
                {
                    return current;
                }
            }

            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(1);
            }

            return new Point(null, null, null, null);            // unknown error
        }

        public static bool operator ==(Point current, Point other)
        {
            if (current.x is null && current.y is null && other.x is null && other.y is null) return true;
            return current.x == other.x && current.y == other.y && current.a == other.a && current.b == other.b;
        }

        public static bool operator !=(Point current, Point other)
        {
            return !(current == other);
        }

        public FieldElement getX() => this.x;

        public FieldElement getY() => this.y;
    }
}
