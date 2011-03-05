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
            var pseudoInterceptor = new PseudoInterceptor(_random, Generator);

            return Generator.CreateClassProxy<T>(pseudoInterceptor);
        }
    }

    public class PseudoInterceptor : IInterceptor
    {
        private readonly Random _random;
        private readonly ProxyGenerator _generator;
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        private readonly Dictionary<Type, Func<Random, object>> _getRandom 
            = new Dictionary<Type, Func<Random, object>>
            {
                {typeof(int), r => r.Next()},
                {typeof(long), r => (long) r.Next()},
                {typeof(short), r => (short) r.Next(short.MaxValue)},
                {typeof(float), r => (float) r.NextDouble()},
                {typeof(double), r => r.NextDouble()},
                {typeof(char), r => (char) r.Next(33,126)},
                {typeof(byte), r => { var b = new byte[1];
                                        r.NextBytes(b);
                                        return b[0]; 
                                        }},
                {typeof(bool), r => false },
                {typeof(string), r => r.Next().ToString() }
            };

        public PseudoInterceptor(Random random, ProxyGenerator generator)
        {
            _random = random;
            _generator = generator;
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
            if (_getRandom.ContainsKey(t))
                return _getRandom[t](_random);

            //leave this statement after reference types for which we know how to return a random value, e.g. string
            if (!t.IsValueType)
                return _generator.CreateClassProxy(t, new PseudoInterceptor(_random, _generator));

            throw new ArgumentException("Tried to generate a random value for unrecognized type: " + t.Name);
        }
    }
}
