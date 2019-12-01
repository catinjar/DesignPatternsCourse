using System;
using System.Collections.Generic;

namespace Homework8_Visitor
{
    public interface IDrawerVisitor
    {
        void Visit(Rectangle rectangle);
        void Visit(Triangle triangle);
        void Visit(Circle circle);
    }

    public class ConsoleDrawerVisitor : IDrawerVisitor
    {
        public float X { get; set; }
        public float Y { get; set; }

        public void Visit(Rectangle rectangle)
        {
            rectangle.Draw(X, Y);
        }

        public void Visit(Triangle triangle)
        {
            triangle.Draw(X, Y);
        }

        public void Visit(Circle circle)
        {
            circle.Draw(X, Y);
        }
    }

    public interface IShape
    {
        void Accept(IDrawerVisitor visitor);

        void Draw(float x, float y);
        float GetArea();
        float GetPerimeter();
    }

    public class Rectangle : IShape
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public void Accept(IDrawerVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Draw(float x, float y)
        {
            float area = GetArea();
            float perimeter = GetPerimeter();
            Console.WriteLine($"Rectangle at ({x}, {y}) with Width = {Width}, Height = {Height}, Area = {area}, and Perimeter = {perimeter}");
        }

        public float GetArea()
        {
            return Width * Height;
        }

        public float GetPerimeter()
        {
            return Width * 2 + Height * 2;
        }
    }

    public class Triangle : IShape
    {
        public float Side1 { get; set; }
        public float Side2 { get; set; }
        public float Side3 { get; set; }

        public void Accept(IDrawerVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Draw(float x, float y)
        {
            float area = GetArea();
            float perimeter = GetPerimeter();
            Console.WriteLine($"Triangle at ({x}, {y}) with sides = {Side1}, {Side2}, {Side3}, Area = {area}, and Perimeter = {perimeter}");
        }

        public float GetArea()
        {
            float hp = GetPerimeter() / 2.0f;
            return (float)Math.Sqrt(hp * ((hp - Side1) * (hp - Side2) * (hp - Side3)));
        }

        public float GetPerimeter()
        {
            return Side1 + Side2 + Side3;
        }
    }

    public class Circle : IShape
    {
        public float Radius { get; set; }

        public void Accept(IDrawerVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Draw(float x, float y)
        {
            float area = GetArea();
            float perimeter = GetPerimeter();
            Console.WriteLine($"Circle at ({x}, {y}) with Radius = {Radius}, Area = {area}, and Perimeter = {perimeter}");
        }

        public float GetArea()
        {
            return (float)Math.PI * Radius * Radius;
        }

        public float GetPerimeter()
        {
            return 2.0f * (float)Math.PI * Radius;
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            var consoleDrawer = new ConsoleDrawerVisitor();

            var shapes = new List<IShape>()
            {
                new Rectangle() { Width = 10.0f, Height = 5.0f },
                new Triangle() { Side1 = 5.0f, Side2 = 10.0f, Side3 = 12.0f },
                new Circle() { Radius = 30.0f }
            };

            foreach (var shape in shapes)
            {
                consoleDrawer.X += 100.0f;
                consoleDrawer.Y += 100.0f;

                shape.Accept(consoleDrawer);
            }

            Console.ReadKey();
        }
    }
}
