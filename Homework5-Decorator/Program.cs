using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework5 {
    public interface IChatClient {
        void SendMessage(Message message);
        List<Message> GetMessages();
    }

    public class Message {
        public string Sender   { get; set; }
        public string Receiver { get; set; }
        public string Text     { get; set; }
    }

    public class ChatClient : IChatClient {
        private List<Message> messages = new List<Message>();

        public void SendMessage(Message message) {
            messages.Add(message);
        }

        public List<Message> GetMessages() {
            return messages;
        }
    }

    public class ChatClientDecoratorBase : IChatClient {
        protected readonly IChatClient decoratee;

        protected ChatClientDecoratorBase(IChatClient chatClient) {
            decoratee = chatClient;
        }

        public void SendMessage(Message message) {
            message = OnBeforeSendMessage(message);
            decoratee.SendMessage(message);
            OnAfterSendMessage(message);
        }

        public List<Message> GetMessages() {
            OnBeforeGetMessages();
            var messages = decoratee.GetMessages();
            return OnAfterGetMessages(messages);
        }

        protected virtual Message OnBeforeSendMessage(Message message) {
            return message;
        }

        protected virtual void OnAfterSendMessage(Message message) { }

        protected virtual void OnBeforeGetMessages() { }

        protected virtual List<Message> OnAfterGetMessages(List<Message> messages) {
            return messages;
        }
    }

    public class HideSenderDecorator : ChatClientDecoratorBase {
        public HideSenderDecorator(IChatClient chatClient) : base(chatClient) { }

        protected override Message OnBeforeSendMessage(Message message) {
            message.Sender = new string('?', message.Sender.Length);
            message.Receiver = new string('?', message.Receiver.Length);
            return message;
        }
    }

    public class EncodeDecorator : ChatClientDecoratorBase {
        public EncodeDecorator(IChatClient chatClient) : base(chatClient) { }

        protected override Message OnBeforeSendMessage(Message message) {
            message.Text = $"<encoded>{message.Text}</encoded>";
            return message;
        }

        protected override List<Message> OnAfterGetMessages(List<Message> messages) {
            return messages.Select(message => {
                return new Message() {
                    Sender = message.Sender,
                    Receiver = message.Receiver,
                    Text = message.Text
                        .Remove(message.Text.Length - "</encoded>".Length, "</encoded>".Length)
                        .Remove(0, "<encoded>".Length)
                };
            })
            .ToList();
        }
    }

    class Program {
        private static void Main(string[] args) {
            var chatClient = new ChatClient();
            var decoratedChatClient = new EncodeDecorator(new HideSenderDecorator(chatClient));

            decoratedChatClient.SendMessage(new Message() { Sender = "First", Receiver = "Second", Text = "Hi!" });
            decoratedChatClient.SendMessage(new Message() { Sender = "Second", Receiver = "First", Text = "What's up?" });

            var messages = decoratedChatClient.GetMessages();

            foreach (var message in messages) {
                Console.WriteLine($"{message.Sender} to {message.Receiver}: {message.Text}");
            }

            Console.ReadKey();
        }
    }
}
