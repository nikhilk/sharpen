// ElementQuery.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.IO;

namespace Sharpen.Server {

    public sealed class ElementQuery : IJsonSerializable {

        private string _selector;

        public ElementQuery(string selector) {
            _selector = selector;
        }

        #region Implementation of IJsonSerializable
        void IJsonSerializable.Write(TextWriter writer) {
            writer.Write("document.querySelector('" + _selector + ")");
        }
        #endregion
    }
}
