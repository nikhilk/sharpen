// SharpenSection.cs
// Sharpen/Server
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web.Configuration;

namespace Sharpen.Server.Configuration {

    public sealed class SharpenSection : ConfigurationSection {

        private static readonly ConfigurationProperty ScriptsProperty =
            new ConfigurationProperty("", typeof(ScriptCollection), null,
                                      ConfigurationPropertyOptions.IsDefaultCollection);

        private static readonly ConfigurationProperty ClientScriptStorageCookieProperty =
            new ConfigurationProperty("clientScriptStorageCookie", typeof(string), "",
                                      new WhiteSpaceTrimStringConverter(),
                                      null,
                                      ConfigurationPropertyOptions.None);

        private static ConfigurationPropertyCollection AllProperties = BuildProperties();

        private static SharpenSection SectionInstance;

        [ConfigurationProperty("clientScriptStorageCookie", DefaultValue = "")]
        public string ClientScriptStorageCookie {
            get {
                return (string)base[ClientScriptStorageCookieProperty];
            }
            set {
                base[ClientScriptStorageCookieProperty] = value;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ScriptCollection Scripts {
            get {
                return (ScriptCollection)base[ScriptsProperty];
            }
        }

        protected override ConfigurationPropertyCollection Properties {
            get {
                return AllProperties;
            }
        }

        private static ConfigurationPropertyCollection BuildProperties() {
            ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
            props.Add(ScriptsProperty);
            props.Add(ClientScriptStorageCookieProperty);

            return props;
        }

        private static void EnsureSection() {
            if (SectionInstance == null) {
                SectionInstance = (SharpenSection)WebConfigurationManager.GetSection("scriptSharp");
                if (SectionInstance == null) {
                    SectionInstance = new SharpenSection();
                }
            }
        }

        internal static SharpenSection GetSettings() {
            EnsureSection();
            return SectionInstance;
        }
    }
}
