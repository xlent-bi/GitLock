using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Email;
using XLabs.Platform.Services.Media;
using XLabs.Serialization;

namespace XlentLock.Droid
{
    [Activity(Label = "XlentLock", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var resolverContainer = new SimpleContainer();

            resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice)
                .Register<IDisplay>(t => t.Resolve<IDevice>().Display)
                .Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
                //.Register<IJsonSerializer, Services.Serialization.JsonNET.JsonSerializer>()
                //.Register<IJsonSerializer, JsonSerializer>()
                //.Register<IEmailService, EmailService>()
                .Register<IMediaPicker, MediaPicker>()
                .Register<ITextToSpeechService, TextToSpeechService>()
                .Register<IDependencyContainer>(resolverContainer);
                //.Register<IXFormsApp>(app)
                //.Register<ISecureStorage>(t => new KeyVaultStorage(t.Resolve<IDevice>().Id.ToCharArray()))
                //.Register<ISimpleCache>(
                //    t => new SQLiteSimpleCache(new SQLitePlatformAndroid(),
                //        new SQLiteConnectionString(pathToDatabase, true), t.Resolve<IJsonSerializer>()));


            Resolver.SetResolver(resolverContainer.GetResolver());


            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

