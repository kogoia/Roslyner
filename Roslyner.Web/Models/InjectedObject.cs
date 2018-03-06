﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Roslyner.Web.Models
{
    public class InjectedObject : IFoo
    {
        private readonly Lazy<Assembly> _assembly;
        private readonly string _namespace;

        public InjectedObject(IEnumerable<byte> compiledCode, string _namespace)
        {
            _assembly = new Lazy<Assembly>(() => Assembly.Load(compiledCode.ToArray()));
            this._namespace = _namespace;
        }
        public int Sum(int a, int b)
        {
            var type = _assembly.Value.GetType(_namespace);
            var obj = (IFoo)Activator.CreateInstance(type);
            return obj.Sum(a, b);
        }
    }
}
