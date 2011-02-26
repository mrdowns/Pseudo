using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace PseudoFixture
{
    //aim to get rid of default constructor restriction
    public class Pseudo<T> where T : class, new()
    {
        private readonly Random _random;
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        public Pseudo(Random random) { _random = random; }

        public Pseudo() { _random = new Random(); }

        public T Create()
        {
            var pseudoInterceptor = new PseudoInterceptor(_random);

            return Generator.CreateClassProxy<T>(pseudoInterceptor);
        }
    }

    public class PseudoInterceptor : IInterceptor
    {
        private readonly Random _random;
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public PseudoInterceptor(Random random)
        {
            _random = random;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var method = invocation.Method.Name;

            if (!_values.ContainsKey(method)) 
                _values[method] = GetValueForType(invocation.Method.ReturnType);
            
            invocation.ReturnValue = _values[method];
        }

        private object GetValueForType(Type t)
        {
            if (t == typeof(int))
                return _random.Next();

            if (t == typeof(long))
                return (long) _random.Next();

            if (t == typeof (short))
                return (short) _random.Next(short.MaxValue);

            if (t == typeof(float))
                return (float) _random.NextDouble();

            if (t == typeof(double))
                return _random.NextDouble();

            if (t == typeof(string))
                return _random.Next().ToString();

            if (t == typeof(char))
                return (char) _random.Next(33, 126);

            if (t == typeof(byte))
            {
                var b = new byte[1];
                _random.NextBytes(b);
                return b[0];
            }

            if (t == typeof (bool))
                return false;

            throw new ArgumentException("Tried to generate a random value for unrecognized type: " + t.Name);
        }
    }
}
