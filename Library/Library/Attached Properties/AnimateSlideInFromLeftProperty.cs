using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Animates a framework element sliding in from the left on show and out to left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseAttachedProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromLeft(0.3f);
            else
                // Animate out
                await element.SlideAndFadeOutToLeft(0.3f);
        }

    }
}
