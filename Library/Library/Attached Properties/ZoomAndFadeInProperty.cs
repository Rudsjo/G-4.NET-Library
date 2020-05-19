using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Library
{
    public class ZoomAndFadeInProperty : AnimateBaseAttachedProperty<ZoomAndFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.ZoomAndFadeIn(FirstLoad ? 0 : 0.3f);
            else
                // Animate out
                await element.ZoomAndFadeOut();
        }

    }
}
