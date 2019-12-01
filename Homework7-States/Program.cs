using System;

namespace Homework7_States
{
    public enum DeviceType
    {
        FlashDrive,
        WiFi
    }

    public class PrinterContext
    {
        public IState State { get; set; }

        public int Money { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Document { get; set; }

        public PrinterContext()
        {
            State = new InitState();
        }

        public void Start()
        {
            State.Start(this);
        }

        public void Pay(int money)
        {
            Money += money;
            State.Pay(this);
        }

        public void ChooseDevice(DeviceType deviceType)
        {
            DeviceType = deviceType;
            State.ChooseDevice(this);
        }

        public void ChooseDocument(string document)
        {
            Document = document;
            State.ChooseDocument(this);
        }

        public void PrintDocument()
        {
            State.PrintDocument(this);
        }

        public void GetCash()
        {
            State.GetCash(this);
        }

        public void Stop()
        {
            State.Stop(this);
        }
    }

    public interface IState
    {
        void Start(PrinterContext context);
        void Pay(PrinterContext context);
        void ChooseDevice(PrinterContext context);
        void ChooseDocument(PrinterContext context);
        void PrintDocument(PrinterContext context);
        void GetCash(PrinterContext context);
        void Stop(PrinterContext context);
    }

    public abstract class StateBase : IState
    {
        public virtual void Start(PrinterContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void Pay(PrinterContext context)
        {
            throw new NotImplementedException();
        }
        
        public virtual void ChooseDevice(PrinterContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void ChooseDocument(PrinterContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void PrintDocument(PrinterContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void GetCash(PrinterContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void Stop(PrinterContext context)
        {
            context.State = new StoppedState();
        }
    }

    public class InitState : StateBase
    {
        public override void Start(PrinterContext context)
        {
            context.State = new PayState();
        }
    }

    public class PayState : StateBase
    {
        public override void Pay(PrinterContext context)
        {
            Console.WriteLine($"Payed the money {context.State}");
            context.State = new ChooseDeviceState();
        }
    }

    public class ChooseDeviceState : StateBase
    {
        public override void ChooseDevice(PrinterContext context)
        {
            Console.WriteLine($"Chose the device type {context.DeviceType}");
            context.State = new ChooseDocumentState();
        }

        public override void Stop(PrinterContext context)
        {
            context.State = new GetCashState();
        }
    }

    public class ChooseDocumentState : StateBase
    {
        public override void ChooseDocument(PrinterContext context)
        {
            Console.WriteLine($"Chose the document {context.Document}");
            context.State = new PrintDocumentState();
        }

        public override void Stop(PrinterContext context)
        {
            context.State = new GetCashState();
        }
    }

    public class PrintDocumentState : StateBase
    {
        public override void PrintDocument(PrinterContext context)
        {
            Console.WriteLine($"Printed the document {context.Document}");
            context.Money--;

            if (context.Money > 0)
            {
                context.State = new ChooseDocumentState();
            }
            else
            {
                context.State = new PayState();
            }
        }

        public override void Stop(PrinterContext context)
        {
            context.State = new GetCashState();
        }
    }

    public class GetCashState : StateBase
    {
        public override void GetCash(PrinterContext context)
        {
            context.Money = 0;
            context.State = new StoppedState();
        }
    }

    public class StoppedState : StateBase
    {
        public override void Start(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }

        public override void Pay(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }

        public override void ChooseDevice(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }

        public override void ChooseDocument(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }

        public override void PrintDocument(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }

        public override void GetCash(PrinterContext context)
        {
            throw new Exception("Interaction is stopped");
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
        }
    }
}
