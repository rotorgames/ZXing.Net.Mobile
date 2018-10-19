﻿using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing.Net.Mobile.Forms.Android;
using Android.Runtime;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Widget;
using ZXing.Mobile;
using Android.Graphics;

[assembly:ExportRenderer(typeof(ZXingBarcodeImageView), typeof(ZXingBarcodeImageViewRenderer))]
namespace ZXing.Net.Mobile.Forms.Android
{
    [Preserve(AllMembers = true)]
    public class ZXingBarcodeImageViewRenderer : ViewRenderer<ZXingBarcodeImageView, ImageView>
    {       
        public static void Init ()
        {
            var temp = DateTime.Now;
        }

        ZXingBarcodeImageView formsView;
        ImageView imageView;

        protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            regenerate ();

            base.OnElementPropertyChanged (sender, e);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ZXingBarcodeImageView> e)
        {
            formsView = Element;

            if (imageView == null) {

                imageView = new ImageView (Xamarin.Forms.Forms.Context);

                base.SetNativeControl (imageView);     
            }

            regenerate ();

            base.OnElementChanged (e);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                formsView = null;
                imageView = null;
            }

            base.Dispose(disposing);
        }

        void regenerate ()
        {
            if (formsView != null && formsView.BarcodeValue != null)
            {
                var writer = new ZXing.Mobile.BarcodeWriter();

                if (formsView != null && formsView.BarcodeOptions != null)
                    writer.Options = formsView.BarcodeOptions;
                if (formsView != null && formsView.BarcodeFormat != null)
                    writer.Format = formsView.BarcodeFormat;

                var value = formsView != null ? formsView.BarcodeValue : string.Empty;

                Device.BeginInvokeOnMainThread (() => 
                {
                    Bitmap image = null;

                    if(!string.IsNullOrEmpty(value))
                        image = writer.Write(value);

                    if(imageView != null)
                        imageView.SetImageBitmap (image);
                });
            }
        }
    }
}

