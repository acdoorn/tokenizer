using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tokenizer.Compiler
{
    class FactoryCompiler
    {
        private static FactoryCompiler _instance = null;
        private static Dictionary<String, BaseCompiler> _dictionary;
        private String _namespace = "Tokenizer.Compiler.";

        public static FactoryCompiler Instance()
        {
            if (_instance == null)
            {
                _instance = new FactoryCompiler();
                _dictionary = new Dictionary<String, BaseCompiler>();
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Type[] currentTypes = currentAssembly.GetTypes();
                foreach (Type t in currentTypes)
                {
                    if (t.GetInterface(typeof(ICompiler).ToString()) != null)
                        _dictionary.Add(t.ToString(), (BaseCompiler)Activator.CreateInstance(t));
                }
            }

            return _instance;
        }

        public static BaseCompiler Create(String compiler)
        {
            return Instance()._create(compiler);
        }

        private BaseCompiler _create(String compiler)
        {
            if (_dictionary.ContainsKey(_namespace + compiler))
                return _dictionary[_namespace + compiler];
 
            return null;
        }
    }
}
