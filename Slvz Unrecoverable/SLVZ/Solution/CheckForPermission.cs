using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slvz_Unrecoverable.SLVZ.Solution;

public class CheckForPermission
{
    //It returns a value that shows permissian granted or not
    //True = App has permission , False = App doesn't have permission 
    public static bool IsPermissionGranted(Context context)
    {
        if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R)
        {
            return Android.OS.Environment.IsExternalStorageManager;
        }
        else
        {
            return !(AndroidX.Core.Content.ContextCompat.CheckSelfPermission(View.Home.HomeActivity._HomeActivity, Android.Manifest.Permission.ReadExternalStorage) == View.Home.HomeActivity._HomeActivity.PackageManager.CheckPermission(Android.Content.PM.Permission.Granted.ToString(), View.Home.HomeActivity._HomeActivity.PackageName) &&
                   AndroidX.Core.Content.ContextCompat.CheckSelfPermission(View.Home.HomeActivity._HomeActivity, Android.Manifest.Permission.WriteExternalStorage) == View.Home.HomeActivity._HomeActivity.PackageManager.CheckPermission(Android.Content.PM.Permission.Granted.ToString(), View.Home.HomeActivity._HomeActivity.PackageName) &&
                   AndroidX.Core.Content.ContextCompat.CheckSelfPermission(View.Home.HomeActivity._HomeActivity, Android.Manifest.Permission.ManageExternalStorage) == View.Home.HomeActivity._HomeActivity.PackageManager.CheckPermission(Android.Content.PM.Permission.Granted.ToString(), View.Home.HomeActivity._HomeActivity.PackageName));
        }
    }
}
