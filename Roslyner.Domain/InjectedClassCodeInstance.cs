﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Roslyner.Domain
{
    public class InjectedClassCodeInstance<T> : IClassInstance<T>
    {
        private readonly IClassInstance<T> _class;
        public InjectedClassCodeInstance(string code)
            : this(
                new ClassInstance<T>(
                    new CompiledCode(
                        code,
                        new References(
                            new TypesAssemblyLocation(
                                typeof(object),
                                typeof(T)
                            )
                        )
                    ),
                    new CodeTemplateForInterface<T>().NameWithNamespace()
                )
             )
        { }

        public InjectedClassCodeInstance(IClassInstance<T> classInstance)
        {
            _class = classInstance;
        }

        public T Instance()
        {
            return _class.Instance();
        }
    }
}
