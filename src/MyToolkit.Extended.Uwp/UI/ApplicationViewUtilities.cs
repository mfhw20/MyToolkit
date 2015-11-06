﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationViewUtilities.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/MyToolkit/MyToolkit/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace MyToolkit.UI
{
    public static class ApplicationViewUtilities
    {
        /// <summary>
        /// Registers events to automatically adjust the size of the current window so that it 
        /// does not overlap the visible content. On Windows 10 Mobile, this is needed to avoid the 
        /// overlapping of the virtual keyboard over the app content.
        /// </summary>
        public static void ConnectRootElementSizeToVisibleBounds()
        {
            var rootElement = (FrameworkElement)Window.Current.Content;
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            ApplicationView.GetForCurrentView().VisibleBoundsChanged += (sender, args) => OnVisibleBoundsChanged(rootElement);
            OnVisibleBoundsChanged(rootElement);
        }

        private static void OnVisibleBoundsChanged(FrameworkElement rootElement)
        {
            var visibleBound = ApplicationView.GetForCurrentView().VisibleBounds;
            var windowHeight = Window.Current.Bounds.Height;

            var difference = Math.Ceiling(windowHeight - visibleBound.Bottom);
            rootElement.Margin = new Thickness(0, 0, 0, difference);
        }
    }
}
