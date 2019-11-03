using System;
using System.Collections.Generic;

namespace Homework3 {
    public class EmailLetter {
        public List<string> Receivers { get; set; } = new List<string>();
        public string Body { get; set; }
        public string Theme { get; set; }

        public void Print() {
            Console.WriteLine($"Receivers: {string.Join(", ", Receivers)}");
            if (!string.IsNullOrEmpty(Theme)) {
                Console.WriteLine($"Theme: {Theme}");
            }
            Console.WriteLine($"Body: {Body}");
        }
    }

    public class EmailBuilder {
        public EmailBuilder(string receiver, string body) {
            Result.Receivers.Add(receiver);
            Result.Body = body;
        }

        public EmailBuilder AddReceiver(string receiver) {
            Result.Receivers.Add(receiver);
            return this;
        }

        public EmailBuilder SetBody(string body) {
            Result.Body = body;
            return this;
        }

        public EmailBuilder SetTheme(string theme) {
            Result.Theme = theme;
            return this;
        }

        public EmailLetter Result { get; } = new EmailLetter();
    }

    class Program {
        private static void Main(string[] args) {
            var emailBuilder = new EmailBuilder("someone@gmail.com", "Hello!");

            var letter = emailBuilder
                .AddReceiver("anotherone@gmail.com")
                .SetTheme("This is a test")
                .Result;

            letter.Print();

            Console.ReadKey();
        }
    }
}
