// ScriptBlock.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;

namespace Sharpen.Server.Script {

    internal sealed class ScriptBlock {

        private string _code;
        private string[] _dependencies;

        public ScriptBlock(string code, string[] dependencies) {
            _code = code;
            _dependencies = dependencies;
        }

        public string Code {
            get {
                return _code;
            }
        }

        public string[] Dependencies {
            get {
                return _dependencies;
            }
        }
    }
}
