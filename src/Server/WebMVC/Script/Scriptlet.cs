// Scriptlet.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;

namespace Sharpen.Server.Script {

    internal sealed class Scriptlet {

        private string _scriptletType;
        private object _parameters;

        public Scriptlet(string scriptletType, object parameters) {
            _scriptletType = scriptletType;
            _parameters = parameters;
        }

        public object Parameters {
            get {
                return _parameters;
            }
        }

        public string ScriptletType {
            get {
                return _scriptletType;
            }
        }
    }
}
