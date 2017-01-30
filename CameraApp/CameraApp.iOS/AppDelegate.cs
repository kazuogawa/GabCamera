using System;
using System.Collections.Generic;
using System.Linq;

//追加
using Xamarin.Media;

using Foundation;
using UIKit;

namespace CameraApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        MediaFile file;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            App ap = new App();
            var picker = new MediaPicker();

            //asyncは非同期の意味
            ap.takePic += async () =>
            {
                //カメラが使えなかった場合、コンソールに出力
                if (!picker.IsCameraAvailable) Console.WriteLine("No camera available");
                else
                {
                    try
                    {
                        //awaitは待機中のタスクが完了するまでメソッドの実行を中断
                        file = await picker.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            Name = "gabphoto.jpg",
                            Directory = "GabCamera"
                        });
                        Console.WriteLine(file.Path);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    //写真が撮れた場合、画像表示
                    if(file != null)
                    {
                        ap.showImage(file.Path);
                    }
                }
            };

            LoadApplication(ap);

            return base.FinishedLaunching(app, options);
        }
    }
}
