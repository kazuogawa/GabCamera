using System;

using Android.App;
//下記を追加
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//下記を追加
using Xamarin.Media;

namespace CameraApp.Droid
{
    [Activity(Label = "CameraApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //Appをappで定義
        App app;
        protected override void OnCreate(Bundle bundle)
        {
            var picker = new MediaPicker(this); //Pickerをinstance化
            //カメラが利用できない場合は、エラーをコンソールに吐き出す
            if (!picker.IsCameraAvailable) Console.WriteLine("No Camera Available");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            app = new App();
            app.takePic += () =>
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "gabphoto.jpg",
                    Directory = "GabCamera"
                });
                //撮影終了後OnActivityResultが呼ばれる
                StartActivityForResult(intent, 1);
            };

            LoadApplication(app);

        }
       
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //ここら辺予測変換でなかった・・
            if (resultCode == Result.Canceled) return;
            MediaFile file = await data.GetMediaFileExtraAsync(this);
            Console.WriteLine(file.Path);
            app.showImage(file.Path);
        }
    }
}

