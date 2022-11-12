using Android.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slvz_Unrecoverable.SLVZ.Library;

public class ButtonClickAnimation
{
    public static async void Animation(RelativeLayout relative)
    {
        ObjectAnimator animator = ObjectAnimator.OfPropertyValuesHolder(relative,
            PropertyValuesHolder.OfFloat("scaleX", 0.8f),
            PropertyValuesHolder.OfFloat("scaleY", 0.8f));
        animator.SetDuration(150);
        animator.Start();

        await Task.Delay(200);

        animator = ObjectAnimator.OfPropertyValuesHolder(relative,
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
        animator.SetDuration(100);
        animator.Start();
    }
}
