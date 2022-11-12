using Android.Content;
using Android.OS;
using Android.Runtime;

namespace Slvz_Unrecoverable.SLVZ.View.Permission;

[Activity(Theme = "@style/LightTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
public class PermissionActivity : Activity
{

    bool AnimationLoop = true;

    private static string[] permission =
    {
        Android.Manifest.Permission.ReadExternalStorage,
        Android.Manifest.Permission.WriteExternalStorage,
    };

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.permission_layout);
        // Create your application here

        Main();
    }

    //Main function
    private async void Main()
    {
        if (SLVZ.Solution.CheckForPermission.IsPermissionGranted(this))
        {
            //Go to home
            StartActivity(new Intent(this, typeof(Home.HomeActivity)));
            Finish();
        }

        //On open setting click
        FindViewById<Button>(Resource.Id.btn_permission_layout).Click += (o, e) =>
        {
            if (SLVZ.Solution.CheckForPermission.IsPermissionGranted(this))
            {
                //Go to home
                StartActivity(new Intent(this, typeof(Home.HomeActivity)));
                Finish();
            }
            else { takePermission(); (o as Button).Enabled = false; }
        };

        await Animation();
    }

    protected override void OnResume()
    {
        if (SLVZ.Solution.CheckForPermission.IsPermissionGranted(this))
        {
            //Go to home page
            StartActivity(new Intent(this, typeof(Home.HomeActivity)));
            Finish();
        }
        else FindViewById<Button>(Resource.Id.btn_permission_layout).Enabled = true;
        base.OnResume();
    }

    protected override void OnDestroy()
    {
        AnimationLoop = false;
        base.OnDestroy();
    }

    //Onback press
    public override void OnBackPressed()
    {
        Finish();
    }

    //Request for permission
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        if (grantResults.Length > 0)
        {
            if (grantResults[0] != PackageManager.CheckPermission(Android.Content.PM.Permission.Granted.ToString(), this.PackageName))
                takePermission();
        }
    }

    private void takePermission()
    {
        if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R)
        {
            try
            {
                Intent intent = new Intent(Android.Provider.Settings.ActionManageAllFilesAccessPermission);
                intent.AddCategory("android.intent.category.DEFAULT");
                intent.SetData(Android.Net.Uri.Parse(string.Format("package:%s", this.PackageName)));
                StartActivityForResult(intent, 100);
            }
            catch
            {
                Intent intent = new Intent(Android.Provider.Settings.ActionManageAllFilesAccessPermission);
                intent.SetAction(Android.Provider.Settings.ActionManageAllFilesAccessPermission);
                StartActivityForResult(intent, 100);
            }
        }
        else
        {
            RequestPermissions(permission, 30);            
        }
    }


    private async Task Animation()
    {
        int imagename = 1;
        while (AnimationLoop)
        {            
            FindViewById<ImageView>(Resource.Id.img_permission).SetImageResource(imagename switch
            {
                1 => Resource.Drawable.img_permission_animation_1,
                2 => Resource.Drawable.img_permission_animation_2,
                3 => Resource.Drawable.img_permission_animation_3,
                4 => Resource.Drawable.img_permission_animation_4,
                _ => Resource.Drawable.img_permission_animation_5,
            });
            imagename++;
            if (imagename >= 5) imagename = 1;

            await Task.Delay(200);
        }
    }

}//End