// ScriptLiteral.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.IO;

namespace Sharpen.Server {

    public sealed class ScriptLiteral : IJsonSerializable {

        private string _script;

        public ScriptLiteral(string script) {
            _script = script;
        }

        #region Implementation of IJsonSerializable
        void IJsonSerializable.Write(TextWriter writer) {
            writer.Write(_script);
        }
        #endregion
    }
}
