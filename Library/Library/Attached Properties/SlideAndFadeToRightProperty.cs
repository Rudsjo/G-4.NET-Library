using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SlideAndFadeToRightProperty : AnimateBaseAttachedProperty<SlideAndFadeToRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate out
                await element.SlideAndFadeOutToRight(0.3f, keepMargin: false);
            else
                await element.ZoomAndFadeIn(0.3f);

        }
    }
}
