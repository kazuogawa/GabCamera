using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CameraApp
{
    public partial class App : Application
    {
        //ButtonClassをインスタンス化？
        Button takePhotoButton = new Button() { Text = "take photo" };

        Image image = new Image();

        public App()
        {

            takePhotoButton.Command = new Command(o => takePic());
            var content = new ContentPage
            {
                Title = "GabCamera",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        takePhotoButton,
                        image,
                    }
                }
            };
            MainPage = new NavigationPage(content);
        }

        // イベントアクション
        public event Action takePic = () => { };

        public void showImage(String filePath)
        {
            image.Source = ImageSource.FromFile(filePath);
        }
    }
}
