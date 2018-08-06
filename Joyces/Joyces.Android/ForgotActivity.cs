using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Text;

namespace Joyces.Droid
{
    [Activity(Label = "Glömt Lösenord")]
    public class ForgotActivity : Activity
    {
        ProgressDialog progress;
        Button btnSubmit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ForgotView);

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage(Lang.LOADING);
            progress.SetCancelable(false);


            btnSubmit = FindViewById<Button>(Resource.Id.btnForgotViewSubmit);
            btnSubmit.Click += btnSubmit_Click;

            btnSubmit.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
            btnSubmit.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));
            setCurrentClientTheme();
        }

        async private void btnSubmit_Click(object sender, EventArgs e)
        {
            progress.Show();

            string sEmail = FindViewById<EditText>(Resource.Id.txtForgotViewEmail).Text;

            bool bSuccess = await RestAPI.ForgotPassword(sEmail);

            if (bSuccess)
                Alert(Lang.MESSAGE_HEADLINE, Lang.RESET_PASSWORD_LINK, Lang.BUTTON_OK);
            else
                Alert(Lang.MESSAGE_HEADLINE, Lang.NO_EMAIL_FOUND, Lang.BUTTON_OK);

            progress.Hide();
        }

        private void Alert(string sHeadLine, string sMessage, string sOKButtonText)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog ad = builder.Create();
            ad.SetTitle(sHeadLine);
            ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            ad.SetMessage(sMessage);
            ad.SetButton(sOKButtonText, (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
            ad.Show();
        }

        private void setCurrentClientTheme()
        {
            try
            {
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
                EditText editTextEmail = FindViewById<EditText>(Resource.Id.txtForgotViewEmail);
                editTextEmail.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextEmail.Hint = Lang.EMAIL;

              
                Button btnRegister = FindViewById<Button>(Resource.Id.btnForgotViewSubmit);
                btnRegister.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                btnRegister.Text = Lang.RESET_PASSWORD;
                try
                {
                    //Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);

                    SpannableString st = new SpannableString(Lang.FORGOT_PASSWORD);
                   // st.SetSpan(new TypefaceSpan(this, "Signika-Regular.otf"), 0, st.Length(), SpanTypes.ExclusiveExclusive);
                   
                    st.SetSpan(tf, 0, st.Length(), SpanTypes.ExclusiveExclusive);

                    ActionBar.TitleFormatted = st;
                }
                catch(Exception ee)
                {

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}