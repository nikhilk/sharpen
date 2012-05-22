// ContentBinder.cs
// Sharpen/Client/HTML
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Html;

namespace Sharpen.Html.Bindings {

    /// <summary>
    /// Binds the content (textContent/innerHTML) of element to an expression.
    /// </summary>
    internal sealed class ContentBinder : PropertyBinder {

        public ContentBinder(Element target, string property, Expression expression)
            : base(target, property, expression) {
        }

        protected override void UpdateTarget(object target, string propertyName, object value) {
            Element targetElement = (Element)target;

            // Raise contentUpdating event, so it allows any behaviors attached to this
            // element to be notified
            MutableEvent updatingEvent = Document.CreateEvent("Custom");
            updatingEvent.InitCustomEvent("contentUpdating", /* canBubble */ false, /* canCancel */ false, null);
            targetElement.DispatchEvent(updatingEvent);

            // Deactivate the content, in case current content has attached behaviors or bindings.
            Application.Current.DeactivateFragment(targetElement, /* contentOnly */ true);

            // Update the element with the new value.
            base.UpdateTarget(targetElement, propertyName, value);

            // If the property that was updated was the innerHTML, then activate any
            // binding expressions or behaviors that might have been specified within the
            // new markup.
            if (propertyName == "innerHTML") {
                BinderManager binderManager = (BinderManager)Behavior.GetBehavior(targetElement, typeof(BinderManager));
                if (binderManager != null) {
                    Application.Current.ActivateFragment(targetElement, /* contentOnly */ true, binderManager.Model);
                }
            }

            // Raise contentUpdated event, so it allows any behaviors attached to this
            // element to be notified
            MutableEvent updatedEvent = Document.CreateEvent("Custom");
            updatedEvent.InitCustomEvent("contentUpdated", /* canBubble */ false, /* canCancel */ false, null);
            targetElement.DispatchEvent(updatedEvent);
        }
    }
}
