// Application.cs
// Sharpen/Client/HTML
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Html;

namespace Sharpen.Html {

    /// <summary>
    /// A page-level singleton representing the current application. It provides core
    /// services like settings, IoC, and pub/sub events-based messaging for decoupled
    /// components.
    /// </summary>
    public sealed partial class Application : IApplication, IContainer {

        /// <summary>
        /// The current Application instance.
        /// </summary>
        public static readonly Application Current = new Application();

        private Dictionary<string, object> _catalog;

        private Application() {
            _catalog = new Dictionary<string, object>();
        }

        private string GetTypeKey(Type type) {
            return type.FullName;
        }

        #region Implementation of IApplication

        /// <summary>
        /// Gets the value of the specified setting.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the specified setting.</returns>
        public string GetSetting(string name) {
            // TODO: Implement access to query string settings as well as maybe
            //       a few other things like JSON data written out as page-level
            //       metadata, or maybe a JSON block in an embedded script element.
            return null;
        }

        #endregion

        #region Implementation of IContainer

        /// <summary>
        /// Gets an instance of the specified object type. An instance is created
        /// if one has not been registered with the container. A factory method is
        /// called if a factory has been registered or implemented on the object type.
        /// </summary>
        /// <param name="objectType">The type of object to retrieve.</param>
        /// <returns>The object instance.</returns>
        public object GetObject(Type objectType) {
            Debug.Assert(objectType != null, "Expected an object type when creating objects.");

            string catalogKey = GetTypeKey(objectType);
            object catalogItem = _catalog[catalogKey];

            if (catalogItem == null) {
                Function objectFactory = Type.GetField(objectType, "factory") as Function;
                if (objectFactory != null) {
                    catalogItem = objectFactory;
                }
                else {
                    return Type.CreateInstance(objectType);
                }
            }

            if (catalogItem is Function) {
                return ((Function)catalogItem).Call(null, (IContainer)this, objectType);
            }

            return catalogItem;
        }

        /// <summary>
        /// Registers an object factory with the container. The factory is called
        /// when the particular object type is requested. The factory can decide whether to
        /// cache object instances it returns or not, based on its policy.
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectFactory"></param>
        public void RegisterFactory(Type objectType, Func<IContainer, Type, object> objectFactory) {
            Debug.Assert(objectType != null, "Expected an object type when registering object factories.");
            Debug.Assert(objectFactory != null, "Expected an object factory when registering object factories.");

            _catalog[GetTypeKey(objectType)] = objectFactory;
        }

        /// <summary>
        /// Registers an object instance for the specified object type.
        /// </summary>
        /// <param name="objectType">The type that can be used to lookup the specified object instance.</param>
        /// <param name="objectInstance">The object instance to use.</param>
        public void RegisterObject(Type objectType, object objectInstance) {
            Debug.Assert(objectType != null, "Expected an object type when registering objects.");
            Debug.Assert(objectInstance != null, "Expected an object instance when registering objects.");

            _catalog[GetTypeKey(objectType)] = objectInstance;
        }

        #endregion
    }
}
