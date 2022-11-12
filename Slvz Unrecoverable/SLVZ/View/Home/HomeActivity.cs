using Android.Content;
using Android.Provider;
using Android.Views;
using Slvz_Unrecoverable.SLVZ.Solution;
using Xamarin.Essentials;

namespace Slvz_Unrecoverable.SLVZ.View.Home;

[Activity(Theme = "@style/LightTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
public class HomeActivity : Activity
{
    public static HomeActivity _HomeActivity;

    HomeDefaultView hdv;
    HomeAskView hav;
    HomeCleaningView hcv;
    HomeAfterClean hac;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.home_layout);
        // Create your application here

        _HomeActivity = this;

        Main();
    }

    //Main function
    private async void Main()
    {
        //Create chanels
        CreateChannle();

        //Check for permission
        if (!SLVZ.Solution.CheckForPermission.IsPermissionGranted(this))
        {
            //Application doesn't has permission
            StartActivity(new Intent(this, typeof(Permission.PermissionActivity)));
            Finish();
        }

        //On web click
        FindViewById<RelativeLayout>(Resource.Id.re_web_home_layout).Touch += async (o, e) =>
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(20, 0, 0, 0));
                    }
                    break;
                case MotionEventActions.Up:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
                        Library.ButtonClickAnimation.Animation(o as RelativeLayout);
                        //Do the job                        
                        await Xamarin.Essentials.Browser.OpenAsync(new System.Uri("https://slvz.ir"), new BrowserLaunchOptions
                        {
                            LaunchMode = BrowserLaunchMode.SystemPreferred,
                            TitleMode = BrowserTitleMode.Show,
                            PreferredToolbarColor = System.Drawing.Color.White,
                        });
                    }
                    break;
            }
        };

        //On rate click
        FindViewById<RelativeLayout>(Resource.Id.re_rate_home_layout).Touch += (o, e) =>
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(20, 0, 0, 0));
                    }
                    break;
                case MotionEventActions.Up:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
                        Library.ButtonClickAnimation.Animation(o as RelativeLayout);
                        //Do the job

                        Intent intent = new Intent(Intent.ActionView);
                        intent.SetData(Android.Net.Uri.Parse("market://details?id=com.slvz.unrecoverable"));
                        StartActivity(intent);
                    }
                    break;
            }
        };

        //On premium click
        FindViewById<RelativeLayout>(Resource.Id.re_premium_home_layout).Touch += async (o, e) =>
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(20, 0, 0, 0));
                    }
                    break;
                case MotionEventActions.Up:
                    {
                        (o as RelativeLayout).SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
                        Library.ButtonClickAnimation.Animation(o as RelativeLayout);
                        //Do the job
                        await Xamarin.Essentials.Browser.OpenAsync(new System.Uri("https://slvz.ir/en/android"), new BrowserLaunchOptions
                        {
                            LaunchMode = BrowserLaunchMode.SystemPreferred,
                            TitleMode = BrowserTitleMode.Show,
                            PreferredToolbarColor = System.Drawing.Color.White,
                        });
                    }
                    break;
            }
        };

        //Set ask view
        hav = new HomeAskView(FindViewById<LinearLayout>(Resource.Id.li_home_ask_question_view));

        //Set homeCleaning view
        hcv = new HomeCleaningView(FindViewById<LinearLayout>(Resource.Id.li_home_cleaning_view), ShowDefaultView, hav.ShowMessage, AfterClean);

        //Set default view control
        hdv = new HomeDefaultView(FindViewById<LinearLayout>(Resource.Id.li_home_default_view), StartCleaning, hav.ShowMessage, ShowCleaningView);

        //Set after clean
        hac = new HomeAfterClean(FindViewById<LinearLayout>(Resource.Id.li_after_clean_home_view));

        if (await Solution.CheckForPremium.Premium())
            FindViewById<RelativeLayout>(Resource.Id.re_premium_home_layout).Visibility = ViewStates.Visible;
    }

    //Creat channel
    private void CreateChannle()
    {
        NotificationChannel channel = new NotificationChannel("UnrecoverableCleaning", "Unrecoverable Cleaning", NotificationImportance.Default);

        channel.SetSound(null, null);
        channel.EnableVibration(false);
        channel.EnableLights(false);

        NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);

        manager.CreateNotificationChannel(channel);

        channel = new NotificationChannel("UnrecoverableMessage", "Unrecoverable Message", NotificationImportance.Default);

        channel.SetSound(null, null);
        channel.EnableVibration(false);
        channel.EnableLights(false);

        manager.CreateNotificationChannel(channel);
    }

    private void AfterClean(bool IsFinished = false)
    {
        hac.ShowView(IsFinished);
    }

    //Start cleaning
    private void StartCleaning()
    {
        hcv.StartCleaning();
    }

    //Show cleaning view
    private void ShowCleaningView()
    {
        hcv.ShowView();
    }

    //Show default view
    private void ShowDefaultView()
    {
        hdv.ShowView();
    }

    //Creat channel



}//End