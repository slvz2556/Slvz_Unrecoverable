using Android.Content;

namespace Slvz_Unrecoverable;

[Activity(Theme= "@style/SplashScreenTheme",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.splashscreen_layout);
        Main();
    }

    //Main function
    private async void Main()
    {
        //Wait for 2.5 second
        await Task.Delay(2500);

        //Check for permission
        if (SLVZ.Solution.CheckForPermission.IsPermissionGranted(this))
        {
            //App has permission
            StartActivity(new Intent(this, typeof(SLVZ.View.Home.HomeActivity)));
            Finish();
        }
        else
        {
            //App needs permission
            StartActivity(new Intent(this, typeof(SLVZ.View.Permission.PermissionActivity)));
            Finish();
        }
    }
}

//SLVZ