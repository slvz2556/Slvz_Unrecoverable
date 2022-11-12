using Android.Animation;
using Android.Content;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Slvz_Unrecoverable.SLVZ.Solution;

//Default view control
public class HomeDefaultView
{
    LinearLayout linear;
    Action StartCleaning, ShowCleaningvView;

    Action<string, Action> ShowMessage;

    public HomeDefaultView(LinearLayout linear, Action StartCleaning, Action<string, Action> ShowMessage, Action ShowCleaningvView)
    {
        this.linear = linear;
        this.StartCleaning = StartCleaning;
        this.ShowMessage = ShowMessage;
        this.ShowCleaningvView = ShowCleaningvView;

        //Call Main
        Main();
    }

    //Main function
    private void Main()
    {
        if (CleaningService.IsServiceRunnign)
        {
            //Disable the button
            linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view).Enabled = false;

            //Hide view
            linear.Visibility = ViewStates.Gone;

            //Show cleaning view
            ShowCleaningvView();
        }

        //On button start cleaning click
        linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view).Click += (o, e) =>
        {
            //Ask for insurence
            ShowMessage("Are you sure you want to clean your phone?\nAll your files will remove for ever\nAll cideos, musics, images and files\n\nNote this, For best results, factory reset your phone then do this\n", HideView);
        };


        double maxSize, freeSize;

        maxSize = Android.OS.Environment.ExternalStorageDirectory.TotalSpace;
        freeSize = Android.OS.Environment.ExternalStorageDirectory.FreeSpace;
        //Set information
        if (freeSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 1000000000),2)} GB free of ";
        else if (freeSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 10000000), 2)} MB free of ";
        else if (freeSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 1000), 2)} KB free of ";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{freeSize} B free of ";
        //Set total space
        if (maxSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 1000000000), 2)} GB";
        else if (maxSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 10000000), 2)} MB";
        else if (maxSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 1000), 2)} KB";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{maxSize} B";
    }

    //Hide this view
    public async void HideView()
    {
        ObjectAnimator animator;

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "Alpha", 0f);
        animator.SetDuration(500);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "translationY", -100f);
        animator.SetDuration(500);
        animator.Start();


        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "Alpha", 0f);
        animator.SetDuration(500);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "translationY", 100f);
        animator.SetDuration(500);
        animator.Start();

        await Task.Delay(500);

        linear.Visibility = ViewStates.Gone;

        StartCleaning();
    }

    //Show this view
    public async void ShowView()
    {
        linear.Visibility = ViewStates.Visible;

        //Enable the button
        linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view).Enabled = true;


        double maxSize, freeSize;

        maxSize = Android.OS.Environment.ExternalStorageDirectory.TotalSpace;
        freeSize = Android.OS.Environment.ExternalStorageDirectory.FreeSpace;
        //Set information
        if (freeSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 1000000000), 2)} GB free of ";
        else if (freeSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 10000000), 2)} MB free of ";
        else if (freeSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{Math.Round((float)(freeSize / 1000), 2)} KB free of ";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text = $"{freeSize} B free of ";
        //Set total space
        if (maxSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 1000000000), 2)} GB";
        else if (maxSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 10000000), 2)} MB";
        else if (maxSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{Math.Round((float)(maxSize / 1000), 2)} KB";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_default_view).Text += $"{maxSize} B";

        ObjectAnimator animator;

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "Alpha", 0f);
        animator.SetDuration(0);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "translationY", -100f);
        animator.SetDuration(0);
        animator.Start();


        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "Alpha", 1f);
        animator.SetDuration(0);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "translationY", 100f);
        animator.SetDuration(0);
        animator.Start();





        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "Alpha", 1f);
        animator.SetDuration(1000);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ImageView>(Resource.Id.img_home_default_view), "translationY", 0f);
        animator.SetDuration(1000);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "Alpha", 1f);
        animator.SetDuration(1000);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<Button>(Resource.Id.btn_clean_home_default_view), "translationY", 0f);
        animator.SetDuration(1000);
        animator.Start();

        await Task.Delay(1000);
    }

}//End

//Cleaning view control
public class HomeCleaningView
{
    LinearLayout linear;

    Action ShowDefaultView;
    Action<bool> FinishedView;
    Action<string, Action> ShowMessage;


    public HomeCleaningView(LinearLayout linear, Action ShowDefaultView, Action<string, Action> ShowMessage, Action<bool> FinishedView)
    {
        this.linear = linear;
        this.ShowDefaultView = ShowDefaultView;
        this.FinishedView = FinishedView;
        this.ShowMessage = ShowMessage;

        CleaningService.OnProgressChange += CleaningService_OnProgressChange;
        CleaningService.OnFinish += CleaningService_OnFinish;

        Main();
    }

    private async void CleaningService_OnFinish(object? sender, EventArgs e)
    {
        linear.FindViewById<Button>(Resource.Id.btn_cancel_home_cleaning_view).Visibility = ViewStates.Gone;

        linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = 100;
        linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view).Text = $"100%";

        linear.FindViewById<TextView>(Resource.Id.lbl_status_home_cleaning_view).Text = "Done";

        await Task.Delay(1250);

        for (int i = 100; i >= 0; i--)
        {
            linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = i;
            await Task.Delay(10);
        }

        await Task.Delay(1000);

        HideView();
        FinishedView(true);
    }

    private async void CleaningService_OnProgressChange(object? sender, Library.ProgressChangeModel e)
    {
        linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = e.Progress;
        linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view).Text = $"{e.Progress}%";

        await SetProgressBar(e.Progress == 0 ? false : true);

        linear.FindViewById<TextView>(Resource.Id.lbl_status_home_cleaning_view).Text = e.Status;

    }

    //Main function
    private void Main()
    {
        linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = 0;

        //On stop cleaning click
        linear.FindViewById<Button>(Resource.Id.btn_cancel_home_cleaning_view).Click += (o, e) =>
        {
            ShowMessage("Are you sure you want to cancel cleaning?", StopCleaning);
        };
    }

    //Start cleaning
    public void StartCleaning()
    {
        //Show view
        ShowView();

        //Start service
        Application.Context.StartService(new Intent(Application.Context, typeof(CleaningService)));
    }

    //Show view
    public void ShowView()
    {
        linear.Visibility = ViewStates.Visible;

        linear.FindViewById<Button>(Resource.Id.btn_cancel_home_cleaning_view).Visibility = ViewStates.Visible;
        linear.FindViewById<Button>(Resource.Id.btn_cancel_home_cleaning_view).Enabled = true;

        linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = 0;
        linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view).Text = $"";

        linear.FindViewById<TextView>(Resource.Id.lbl_status_home_cleaning_view).Text = "";

    }

    //Hide view
    public async void HideView()
    {
        ObjectAnimator animator;
        animator = ObjectAnimator.OfFloat(linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view), "Alpha", 0f);
        animator.SetDuration(400);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view), "translationY", -100f);
        animator.SetDuration(400);
        animator.Start();


        animator = ObjectAnimator.OfFloat(linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view), "Alpha", 0f);
        animator.SetDuration(400);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view), "translationY", -100f);
        animator.SetDuration(400);
        animator.Start();


        await Task.Delay(400);

        animator = ObjectAnimator.OfFloat(linear, "Alpha", 0f);
        animator.SetDuration(400);
        animator.Start();

        await Task.Delay(400);

        linear.Visibility = ViewStates.Gone;

        animator = ObjectAnimator.OfFloat(linear, "Alpha", 1f);
        animator.SetDuration(0);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view), "Alpha", 1f);
        animator.SetDuration(0);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view), "translationY", 0f);
        animator.SetDuration(0);
        animator.Start();


        animator = ObjectAnimator.OfFloat(linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view), "Alpha", 1f);
        animator.SetDuration(0);
        animator.Start();

        animator = ObjectAnimator.OfFloat(linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view), "translationY", 0f);
        animator.SetDuration(0);
        animator.Start();

        ShowDefaultView();
    }

    //Stop cleaning
    private async void StopCleaning()
    {
        linear.FindViewById<Button>(Resource.Id.btn_cancel_home_cleaning_view).Enabled = false;

        try
        {
            //Stop service
            Application.Context.StopService(new Intent(Application.Context, typeof(CleaningService)));
        }
        catch { }

        linear.FindViewById<TextView>(Resource.Id.lbl_status_home_cleaning_view).Text = "Canceled";
        linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view).Text = $"";
        for (int i = linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress; i >= 0; i--)
        {
            linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = i;
            await Task.Delay(10);
        }

        await Task.Delay(1500);

        FinishedView(false);

        HideView();
    }

    //Set progressbar
    private async Task SetProgressBar(bool bl)
    {
        double maxSize, freeSize;

        maxSize = Android.OS.Environment.ExternalStorageDirectory.TotalSpace;
        freeSize = Android.OS.Environment.ExternalStorageDirectory.FreeSpace;

        if (bl)
        {
            int percent = (int)((100 * (maxSize - freeSize)) / maxSize);

            for (int i = linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress; i <= percent; i++)
            {
                linear.FindViewById<ProgressBar>(Resource.Id.prg_home_cleaning_view).Progress = i;
                linear.FindViewById<TextView>(Resource.Id.lbl_progress_home_cleaning_view).Text = $"{i}%";
                await Task.Delay(30);
            }
        }


        if (freeSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text = $"{Math.Round((float)(freeSize / 1000000000), 2)} GB free of ";
        else if (freeSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text = $"{Math.Round((float)(freeSize / 10000000), 2)} MB free of ";
        else if (freeSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text = $"{Math.Round((float)(freeSize / 1000), 2)} KB free of ";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text = $"{freeSize} B free of ";


        //Set total space
        if (maxSize > 1000000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text += $"{Math.Round((float)(maxSize / 1000000000), 2)} GB";
        else if (maxSize >= 10000000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text += $"{Math.Round((float)(maxSize / 10000000), 2)} MB";
        else if (maxSize >= 1000)
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text += $"{Math.Round((float)(maxSize / 1000), 2)} KB";
        else
            linear.FindViewById<TextView>(Resource.Id.lbl_info_home_cleaning_view).Text += $"{maxSize} B";

    }

}

//Ask for insurence
public class HomeAskView
{
    LinearLayout linear;
    Action action;

    public HomeAskView(LinearLayout linear)
    {
        this.linear = linear;

        Main();
    }

    //Main function
    private void Main()
    {
        //Set background color
        linear.SetBackgroundColor(Android.Graphics.Color.Argb(100, 20, 20, 20));

        //On no click
        linear.FindViewById<Button>(Resource.Id.btn_no_home_ask_question_view).Click += (o, e) =>
        {
            linear.Visibility = ViewStates.Gone;
        };

        //On yes click
        linear.FindViewById<Button>(Resource.Id.btn_yes_home_ask_question_view).Click += (o, e) =>
        {
            action();
            linear.Visibility = ViewStates.Gone;
        };

        linear.Touch += Linear_Touch;
    }

    private void Linear_Touch(object sender, Android.Views.View.TouchEventArgs e)
    {
        linear.Visibility = ViewStates.Gone;
    }

    public void ShowMessage(string messagetext, Action action)
    {
        //Show the message
        linear.Visibility = ViewStates.Visible;

        //Set message text
        linear.FindViewById<TextView>(Resource.Id.lbl_home_ask_question_view).Text = messagetext;

        this.action = action;
    }

}

//After clean
public class HomeAfterClean
{
    LinearLayout linear;

    public HomeAfterClean(LinearLayout linear)
    {
        this.linear = linear;

        Main();
    }

    //Main function
    private void Main()
    {
        linear.SetBackgroundColor(Android.Graphics.Color.Argb(100, 0, 0, 0));

        //On linear touch
        linear.Touch += (o, e) =>
        {
            HideView();
        };

        //On button close click
        linear.FindViewById<RelativeLayout>(Resource.Id.re_close_after_clean_home_view).Touch += (o, e) =>
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(30, 0, 0, 0));
                    break;
                case MotionEventActions.Up:
                    (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
                    HideView();
                    break;
            }
        };

        //On get windows app
        linear.FindViewById<Button>(Resource.Id.btn_get_windows_app_after_clean_home_view).Click += async (o, e) =>
        {
            await Browser.OpenAsync(new System.Uri("https://slvz.ir/windows/slvzunrecoverable"), new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = System.Drawing.Color.White,
            });
        };

        //On rate click
        linear.FindViewById<LinearLayout>(Resource.Id.li_rate_after_clean_home_view).Click += (o, e) =>
        {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse("market://details?id=com.slvz.unrecoverable"));
            View.Home.HomeActivity._HomeActivity.StartActivity(intent);
        };
    }

    //Show view
    public async void ShowView(bool IsFinished = false)
    {
        linear.Visibility = ViewStates.Visible;

        if (!IsFinished)
            linear.FindViewById<LinearLayout>(Resource.Id.li_rate_after_clean_home_view).Visibility = ViewStates.Gone;
        else
        {
            linear.FindViewById<LinearLayout>(Resource.Id.li_rate_after_clean_home_view).Visibility = ViewStates.Visible;
            ObjectAnimator animator;

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star1_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 0f),
            PropertyValuesHolder.OfFloat("scaleY", 0f));
            animator.SetDuration(0);
            animator.Start();
            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star2_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 0f),
            PropertyValuesHolder.OfFloat("scaleY", 0f));
            animator.SetDuration(0);
            animator.Start();
            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star3_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 0f),
            PropertyValuesHolder.OfFloat("scaleY", 0f));
            animator.SetDuration(0);
            animator.Start();
            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star4_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 0f),
            PropertyValuesHolder.OfFloat("scaleY", 0f));
            animator.SetDuration(0);
            animator.Start();
            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star5_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 0f),
            PropertyValuesHolder.OfFloat("scaleY", 0f));
            animator.SetDuration(0);
            animator.Start();

            //Load animations

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star1_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
            animator.SetDuration(250);
            animator.Start();
            await Task.Delay(100);

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star2_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
            animator.SetDuration(250);
            animator.Start();
            await Task.Delay(100);

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star3_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
            animator.SetDuration(250);
            animator.Start();
            await Task.Delay(100);

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star4_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
            animator.SetDuration(250);
            animator.Start();
            await Task.Delay(100);

            animator = ObjectAnimator.OfPropertyValuesHolder(linear.FindViewById<ImageView>(Resource.Id.img_star5_after_clean_home_view),
            PropertyValuesHolder.OfFloat("scaleX", 1f),
            PropertyValuesHolder.OfFloat("scaleY", 1f));
            animator.SetDuration(250);
            animator.Start();
        }
    }

    //Hide view
    private void HideView()
    {
        linear.Visibility = ViewStates.Gone;
    }

}//End
