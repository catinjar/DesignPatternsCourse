using System;
using System.Collections.Generic;
using System.Text;

namespace Homework6
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        int Value { get; }
    }

    public class BankRoll
    {
        public IBanknote Banknote { get; set; }
        public int Count { get; set; }
    }

    public class Bancomat
    {
        private readonly IBanknoteHandler _handler;

        public Bancomat()
        {
            _handler = new TenRubleHandler(null);
            _handler = new FiftyRubleHandler(_handler);
            _handler = new HundredRubleHandler(_handler);
            _handler = new TwoHundredRubleHandler(_handler);
            _handler = new FiveHundredRubleHandler(_handler);
            _handler = new ThousandRubleHandler(_handler);
            _handler = new TwoThousandRubleHandler(_handler);

            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }

        public bool Validate(IBanknote banknote)
        {
            return _handler.Validate(banknote);
        }

        public (List<BankRoll> money, bool success) Withdraw(CurrencyType currencyType, int value)
        {
            return _handler.Withdraw(currencyType, value);
        }
    }

    public interface IBanknoteHandler
    {
        bool Validate(IBanknote banknote);
        (List<BankRoll> money, bool success) Withdraw(CurrencyType currencyType, int value);
    }

    public abstract class BanknoteHandler<BanknoteType> : IBanknoteHandler
        where BanknoteType : IBanknote
    {
        private readonly IBanknoteHandler _nextHandler;

        protected BanknoteHandler(IBanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public abstract BanknoteType Banknote { get; }

        public virtual bool Validate(IBanknote banknote)
        {
            if (banknote.Currency == Banknote.Currency && banknote.Value == Banknote.Value)
                return true;

            return _nextHandler != null && _nextHandler.Validate(banknote);
        }

        public virtual (List<BankRoll> money, bool success) Withdraw(CurrencyType currencyType, int value)
        {
            if (currencyType != Banknote.Currency && _nextHandler != null)
            {
                return _nextHandler.Withdraw(currencyType, value);
            }

            if (value % Banknote.Value == 0)
            {
                return (new List<BankRoll>() { new BankRoll() { Banknote = Banknote, Count = value / Banknote.Value } }, true);
            }

            if (_nextHandler != null)
            {
                (var money, var success) = _nextHandler.Withdraw(currencyType, value - value / Banknote.Value * Banknote.Value);
                
                if (success && value / Banknote.Value > 0)
                    money.Add(new BankRoll() { Banknote = Banknote, Count = value / Banknote.Value });

                return (money, success);
            }
            else
            {
                return (null, false);
            }
        }
    }


    public abstract class RubleBanknote : IBanknote
    {
        public CurrencyType Currency => CurrencyType.Ruble;

        public abstract int Value { get; }
    }

    public abstract class RubleHandlerBase<BanknoteType> : BanknoteHandler<BanknoteType>
        where BanknoteType : RubleBanknote, new()
    {
        public override BanknoteType Banknote => new BanknoteType();

        protected RubleHandlerBase(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TenRuble : RubleBanknote
    {
        public override int Value => 10;
    }

    public class TenRubleHandler : RubleHandlerBase<TenRuble>
    {
        public TenRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiftyRuble : RubleBanknote
    {
        public override int Value => 50;
    }

    public class FiftyRubleHandler : RubleHandlerBase<FiftyRuble>
    {
        public FiftyRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class HundredRuble : RubleBanknote
    {
        public override int Value => 100;
    }

    public class HundredRubleHandler : RubleHandlerBase<HundredRuble>
    {
        public HundredRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TwoHundredRuble : RubleBanknote
    {
        public override int Value => 200;
    }

    public class TwoHundredRubleHandler : RubleHandlerBase<TwoHundredRuble>
    {
        public TwoHundredRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiveHundredRuble : RubleBanknote
    {
        public override int Value => 500;
    }

    public class FiveHundredRubleHandler : RubleHandlerBase<FiveHundredRuble>
    {
        public FiveHundredRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class ThousandRuble : RubleBanknote
    {
        public override int Value => 1000;
    }

    public class ThousandRubleHandler : RubleHandlerBase<ThousandRuble>
    {
        public ThousandRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TwoThousandRuble : RubleBanknote
    {
        public override int Value => 2000;
    }

    public class TwoThousandRubleHandler : RubleHandlerBase<TwoThousandRuble>
    {
        public TwoThousandRubleHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }


    public abstract class DollarBanknote : IBanknote
    {
        public CurrencyType Currency => CurrencyType.Dollar;

        public abstract int Value { get; }
    }

    public abstract class DollarHandlerBase<BanknoteType> : BanknoteHandler<BanknoteType>
        where BanknoteType : DollarBanknote, new()
    {
        public override BanknoteType Banknote => new BanknoteType();

        protected DollarHandlerBase(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class HundredDollar : DollarBanknote
    {
        public override int Value => 100;
    }

    public class HundredDollarHandler : DollarHandlerBase<HundredDollar>
    {
        public HundredDollarHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiftyDollar : DollarBanknote
    {
        public override int Value => 50;
    }

    public class FiftyDollarHandler : DollarHandlerBase<FiftyDollar>
    {
        public FiftyDollarHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TenDollar : DollarBanknote
    {
        public override int Value => 10;
    }

    public class TenDollarHandler : DollarHandlerBase<TenDollar>
    {
        public TenDollarHandler(IBanknoteHandler nextHandler) : base(nextHandler) { }
    }

    class Program
    {
        private static void PrintMoney(List<BankRoll> money, bool success)
        {
            if (!success)
            {
                Console.WriteLine("Invalid sum!");
                return;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < money.Count; ++i)
            {
                sb.Append($"{money[i].Banknote.Value} * {money[i].Count}");
                if (i < money.Count - 1)
                    sb.Append(" + ");
            }

            Console.WriteLine(sb.ToString());
        }

        private static void Main(string[] args)
        {
            var bancomat = new Bancomat();

            (var money, var success) = bancomat.Withdraw(CurrencyType.Ruble, 2050);
            PrintMoney(money, success);

            (money, success) = bancomat.Withdraw(CurrencyType.Ruble, 2033);
            PrintMoney(money, success);

            Console.ReadKey();
        }
    }
}
