using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fsharp_for_the_masses
{
    class Program
    {

        #region Example 0

        public class Customer
        {
            private readonly string name;
            private readonly int age;
            public Customer(string name, int age)
            {
                this.name = name;
                this.age = age;
            }
            public string Name { get { return name; } }
            public int Age { get { return age; } }
            #region Equals/HashCode
            //public override bool Equals(object obj)
            //{
            //    /// ...
            //}
            //public override int GetHashCode()
            //{
            //    /// ...
            //}
            #endregion
        }

        #endregion

        #region Example 1
        Tuple<U, U, U> Map<T, U>(Func<T, U> f, Tuple<T, T, T> t)
        {
            return new Tuple<U, U, U>(f(t.Item1), f(t.Item2), f(t.Item3));
        }
        #endregion

        #region Example 2

        public  abstract class Expr 
        { 
            public abstract bool Eval(); 
        } 
   
        public class True : Expr 
        { 
            public override bool Eval() { return true; } 
        } 
        public class False : Expr 
        { 
            public override bool Eval() { return true; } 
        } 
  
        public class Base : Expr 
        { 
            private readonly bool value; 
            public Base(bool value) { this.value = value; } 
            public override bool Eval() 
            { 
                return value;  
            }
        }
        public class And : Expr 
        { 
            private readonly Expr expr1; 
            private readonly Expr expr2; 
            public And(Expr expr1, Expr expr2)  
            { 
              this.expr1 = expr1; 
              this.expr2 = expr2; 
            } 
            public override bool Eval() 
            { 
              return expr1.Eval() && expr2.Eval(); 
            } 
        } 
        public class Or : Expr 
        { 
            private readonly Expr expr1; 
            private readonly Expr expr2; 
            public Or(Expr expr1, Expr expr2)  
            { 
              this.expr1 = expr1; 
              this.expr2 = expr2; 
            } 
            public override bool Eval() 
            { 
              return expr1.Eval() || expr2.Eval(); 
            } 
        }
        public class Not : Expr
        {
            private readonly Expr expr;
            public Not(Expr expr)
            {
                this.expr = expr;
            }
            public override bool Eval()
            {
                return !expr.Eval();
            }
        }

        #endregion

        #region Example 3

        public List<int> Destutter(List<int> list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != list[i + 1])
                    result.Add(list[i]);
            }
            return result;
        }

        #endregion

        #region Example 4

        public enum ConnState
        {
            Connecting,
            Connected,
            Disconnected 
        }
        public class ConnInfo
        {
            private readonly ConnState state;
            private readonly IPAddress address;
            private readonly DateTime? lastPing;
            private readonly int? lastPingId;
            private readonly string sessionId;
            private readonly DateTime? whenInitiated;
            private readonly DateTime? whenDisconnected;

            public ConnInfo(ConnState state, IPAddress address, DateTime? lastPing,
                            int? lastPingId, string sessionId, DateTime? whenInitiated, DateTime? whenDisconnected)
            {
                this.state = state;
                this.address = address;
                this.lastPing = lastPing;
                this.lastPingId = lastPingId;
                this.sessionId = sessionId;
                this.whenInitiated = whenInitiated;
                this.whenDisconnected = whenDisconnected;
            }

            public ConnState State { get { return this.state; } }
            public IPAddress Address { get { return this.address; } }
            public DateTime? LastPing { get { return this.lastPing; } }
            public int? LastPingId { get { return this.lastPingId; } }
            public string SessionId { get { return this.sessionId; } }
            public DateTime? WhenInitiated { get { return this.whenInitiated; } }
            public DateTime? WhenDisconnected { get { return this.whenDisconnected; } }
        }

        #endregion

        static void Main(string[] args)
        {
        }
    }
}
