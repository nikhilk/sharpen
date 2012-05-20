// ElementReference.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.IO;

namespace Sharpen.Server {

    public sealed class ElementReference : IJsonSerializable {

        private string _id;

        public ElementReference(string id) {
            _id = id;
        }

        #region Implementation of IJsonSerializable
        void IJsonSerializable.Write(TextWriter writer) {
            writer.Write("document.getElementById('" + _id + "')");
        }
        #endregion
    }
}
