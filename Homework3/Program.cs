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
        private EmailLetter letter = new EmailLetter();

        public EmailBuilder(string receiver, string body) {
            letter.Receivers.Add(receiver);
            letter.Body = body;
        }

        public EmailBuilder AddReceiver(string receiver) {
            letter.Receivers.Add(receiver);
            return this;
        }

        public EmailBuilder SetBody(string body) {
            letter.Body = body;
            return this;
        }

        public EmailBuilder SetTheme(string theme) {
            letter.Theme = theme;
            return this;
        }

        public EmailLetter Result {
            get {
                if (letter.Receivers.Count == 0) {
                    throw new FormatException("The letter must have at least one receiver!");
                }

                if (string.IsNullOrEmpty(letter.Body)) {
                    throw new FormatException("The letter must have a body!");
                }

                return letter;
            }
        }
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
