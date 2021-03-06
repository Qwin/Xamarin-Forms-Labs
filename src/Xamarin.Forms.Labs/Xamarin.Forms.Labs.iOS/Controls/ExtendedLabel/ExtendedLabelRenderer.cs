﻿using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs.iOS;
using Xamarin.Forms.Labs.iOS.Controls;
using MonoTouch.Foundation;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]

namespace Xamarin.Forms.Labs.iOS.Controls
{
    /// <summary>
    /// The extended label renderer.
    /// </summary>
    public class ExtendedLabelRenderer : LabelRenderer
    {
        /// <summary>
        /// The on element changed callback.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var view = (ExtendedLabel)Element;

            UpdateUi(view, this.Control);
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="control">
        /// The control.
        /// </param>
        private static void UpdateUi(ExtendedLabel view, UILabel control)
        {
            if (!string.IsNullOrEmpty(view.FontName))
            {
                var font = UIFont.FromName(
                    view.FontName,
                    (view.FontSize > 0) ? (float)view.FontSize : 12.0f);

                if (font != null)
                {
                    control.Font = font;
                }
            }

            if (!string.IsNullOrEmpty(view.FontNameIOS))
            {
                var font = UIFont.FromName(
                    view.FontNameIOS,
                   (view.FontSize > 0) ? (float)view.FontSize : 12.0f);

                if (font != null)
                {
                    control.Font = font;
                }
            }

            var attrString = new NSMutableAttributedString(control.Text);

            if (view.IsUnderline)
            {
                //control.AttributedText = new NSAttributedString(
                //    control.Text,
                //    underlineStyle: NSUnderlineStyle.Single);

                attrString.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), new NSRange(0, attrString.Length));
            }

            if (view.IsStrikeThrough)
            {
                attrString.AddAttribute(UIStringAttributeKey.StrikethroughStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), new NSRange(0, attrString.Length));
            }

            control.AttributedText = attrString;
        }
    }
}

