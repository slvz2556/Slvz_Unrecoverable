using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slvz_Unrecoverable.SLVZ.Solution;

[Service]
public class CleaningService : Service
{
    public static bool IsServiceRunnign = false;

    public static event EventHandler<Library.ProgressChangeModel> OnProgressChange = delegate { };

    public static event EventHandler OnFinish = delegate { };


    public override IBinder? OnBind(Intent? intent) => null;

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        Intent homeActivity = new Intent(this, typeof(SLVZ.View.Home.HomeActivity));
        PendingIntent pending = PendingIntent.GetActivity(this, 0, homeActivity, PendingIntentFlags.Mutable);

        Notification notification = new NotificationCompat.Builder(Application.Context, "UnrecoverableCleaning")
            .SetContentTitle("Slvz Unrecoverable")
            .SetContentText("Cleaning your phone ...")
            .SetColor(Android.Graphics.Color.Argb(255, 255, 255, 0))
            .SetSmallIcon(Resource.Mipmap.logo)
            .SetContentIntent(pending)
            .Build();

        StartForeground(1, notification);

        IsServiceRunnign = true;

        Main();

        return StartCommandResult.NotSticky;
    }

    private void Main()
    {
        Thread thread = new Thread(() =>
        {

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Deleting all files",
                Progress = 0
            });            
            ChangeNotification("Deleting all files");

            //Delete all files
            foreach (string f in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath))
            {
                try
                {
                    File.Delete(f);
                }
                catch { }
            }

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Deleting all folders",
                Progress = 0
            });
            ChangeNotification("Deleting all folders");

            //Delete all directories
            foreach (string directory in Directory.GetDirectories(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath))
            {
                try
                {
                    Directory.Delete(directory, true);
                }
                catch { }
            }

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Getting things ready",
                Progress = 0
            });
            ChangeNotification("Getting things ready");

            byte[] file;

            if (Android.OS.Environment.ExternalStorageDirectory.FreeSpace >= 100000000)
                file = ReturnBytes.GetBytes(2);
            else if (Android.OS.Environment.ExternalStorageDirectory.FreeSpace >= 1000000000)
                file = ReturnBytes.GetBytes(3);
            else
                file = ReturnBytes.GetBytes();


            int filecounting = 0;

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Overwriting data",
                Progress = 1
            });
            ChangeNotification("Overwriting data...");

            try
            {
                do
                {

                    using (FileStream fsStream = new FileStream(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + $"/Data{filecounting}.slvz", FileMode.Create))
                    using (BinaryWriter writer = new BinaryWriter(fsStream, Encoding.UTF8))
                    {
                        bool IsEveryThingAllright = true;
                        do
                        {
                            try
                            {
                                writer.Write(file);
                            }
                            catch { IsEveryThingAllright = false; }

                        }
                        while (IsEveryThingAllright);
                        writer.Close();
                        fsStream.Close();
                    }
                    filecounting++;
                }
                while (Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath) && Android.OS.Environment.ExternalStorageDirectory.FreeSpace >= 10000000);
            }
            catch { }

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Deleting all files",
                Progress = 1
            });
            ChangeNotification("Deleting all files");

            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath))
            {
                StopSelf();
            }

            //Delete all files
            foreach (string f in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath))
            {
                try
                {
                    File.Delete(f);
                }
                catch { }
            }

            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Deleting all folders",
                Progress = 1
            });
            ChangeNotification("Deleting all folders");

            //Delete all directories
            foreach (string directory in Directory.GetDirectories(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath))
            {
                try
                {
                    Directory.Delete(directory, true);
                }
                catch { }
            }


            OnProgressChange.Invoke(this, new Library.ProgressChangeModel
            {
                Status = "Done",
                Progress = 1
            });

            OnFinish.Invoke(this,null);
            
            Intent homeActivity = new Intent(this, typeof(SLVZ.View.Home.HomeActivity));
            PendingIntent pending = PendingIntent.GetActivity(this, 0, homeActivity, 0);

            Notification notification = new NotificationCompat.Builder(Application.Context, "UnrecoverableMessage")
               .SetContentTitle("Slvz Unrecoverable")
               .SetContentText("All done ✅")
               .SetContentIntent(pending)
               .SetColor(Android.Graphics.Color.Argb(255, 255, 255, 0))
               .SetSmallIcon(Resource.Mipmap.logo)
               .Build();

            NotificationManager mNotificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            mNotificationManager.Notify(2, notification);

            StopSelf();
        });
        thread.IsBackground = true;
        thread.Start();
    }

    private void ChangeNotification(string status)
    {
        Intent homeActivity = new Intent(this, typeof(SLVZ.View.Home.HomeActivity));
        PendingIntent pending = PendingIntent.GetActivity(this, 0, homeActivity, PendingIntentFlags.Mutable);

        Notification notification = new NotificationCompat.Builder(Application.Context, "UnrecoverableCleaning")
            .SetContentTitle("Slvz Unrecoverable")
            .SetContentText(status)
            .SetColor(Android.Graphics.Color.Argb(255, 255, 255, 0))
            .SetSmallIcon(Resource.Mipmap.logo)
            .SetContentIntent(pending)
            .Build();

        NotificationManager mNotificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
        mNotificationManager.Notify(1, notification);
    }


    public override void OnDestroy()
    {
        IsServiceRunnign = false;

        base.OnDestroy();
    }

}
