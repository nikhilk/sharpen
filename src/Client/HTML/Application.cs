// Application.cs
// Sharpen/Client/HTML
//

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sharpen.HTML {

    /// <summary>
    /// A page-level singleton representing the current application. It provides core
    /// services like settings, IoC, and pub/sub events-based messaging for decoupled
    /// components.
    /// </summary>
    public sealed class Application : IApplication {

        /// <summary>
        /// The current Application instance.
        /// </summary>
        public static readonly Application Current = new Application();

        private Application() {
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
    }
}
