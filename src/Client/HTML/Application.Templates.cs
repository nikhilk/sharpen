// Application.Templates.cs
// Sharpen/Client/HTML
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Diagnostics;
using System.Html;

namespace Sharpen.Html {

    public sealed partial class Application {

        /// <summary>
        /// Gets a template instance from the specified template name. The specified name
        /// is used to lookup a script element with a matching id.
        /// </summary>
        /// <param name="name">The name to lookup a template.</param>
        /// <returns>The template instance if the associated element could be found and successfully parsed.</returns>
        public Template GetTemplate(string name) {
            Debug.Assert(String.IsNullOrEmpty(name) == false);

            // TODO: Should we allow plugging in a template selector callback?
            //       We would need to have a value parameter to pass in the data value into GetTemplate.

            // Check if there is a previously parsed or registered template
            Template template = _registeredTemplates[name];
            if (template != null) {
                return template;
            }

            ScriptElement templateElement = (ScriptElement)Document.GetElementById(name);
            Debug.Assert(templateElement != null, "Could not find a template with the name '" + name + "'.");
            if (templateElement == null) {
                return null;
            }

            // Parse the template using the associated template engine
            string templateMimeType = templateElement.Type;
            Debug.Assert(String.IsNullOrEmpty(templateMimeType) == false, "A template must have a valid type attribute set.");

            TemplateEngine templateEngine = _registeredTemplateEngines[templateMimeType];
            Debug.Assert(templateEngine != null, "No template engine was found to be able to process the templated named '" + name + "'.");

            if (templateEngine == null) {
                return null;
            }

            // Cache the newly parsed template for future use
            _registeredTemplates[name] = template = templateEngine(templateElement.TextContent);

            return template;
        }

        /// <summary>
        /// Registers a template with the specified name.
        /// </summary>
        /// <param name="name">The name of template.</param>
        /// <param name="template">The template to be used.</param>
        public void RegisterTemplate(string name, Template template) {
            Debug.Assert(String.IsNullOrEmpty(name) == false);
            Debug.Assert(template != null);
            Debug.Assert(_registeredTemplates.ContainsKey(name) == false, "A template with name '" + name + "' was already registered.");

            _registeredTemplates[name] = template;
        }

        /// <summary>
        /// Registers a template engine. The supplied name is used to match against the
        /// mime type attribute on a template script element.
        /// </summary>
        /// <param name="name">The name of template engine.</param>
        /// <param name="engine">The engine to be used to handle the supplied name.</param>
        public void RegisterTemplateEngine(string name, TemplateEngine engine) {
            Debug.Assert(String.IsNullOrEmpty(name) == false);
            Debug.Assert(engine != null);

            string templateMimeType = "text/template-" + name;
            Debug.Assert(_registeredTemplateEngines.ContainsKey(templateMimeType) == false,
                         "A template engine with name '" + name + "' was already registered.");

            _registeredTemplateEngines[templateMimeType] = engine;
        }
    }
}
