﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Roslyner.Web.Models
{
    public class CompiledCode : IEnumerable<byte>
    {
        private readonly string _code;

        public CompiledCode(string code)
        {
            this._code = code;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            using (var dll = new MemoryStream())
            {
                CSharpCompilation.Create(
                    "Foo",
                    new[] { CSharpSyntaxTree.ParseText(_code) },
                    references: new MetadataReference[]
                    {
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(Program).Assembly.Location)
                    },
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                ).Emit(dll);
                return dll
                    .ToArray()
                    .ToList()
                    .GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}