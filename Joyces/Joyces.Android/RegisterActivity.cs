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
using Joyces.Repository;
using System.Net;
using System.Threading.Tasks;
using Android.Graphics;

namespace Joyces.Droid
{
    [Activity(Label = "Skapa konto")]
    public class RegisterActivity : Activity
    {
        private Button btnRegister;
        private Button buttonRegisterViewTermAndCondition;
        private Button buttonRegisterViewPrivacyPolicy;
        ApplicationTokenModel appTokenModel;
        ProgressDialog progress;

        async protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RegisterViewParadiset);

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage(Lang.LOADING);
            progress.SetCancelable(false);
 
            btnRegister = FindViewById<Button>(Resource.Id.btnRegisterViewRegister);
            btnRegister.Click += btnRegister_Click;
            btnRegister.SetBackgroundColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonBackgroundColor));
            btnRegister.SetTextColor(Android.Graphics.Color.ParseColor(GeneralSettings.ButtonTextColor));

            //buttonRegisterViewTermAndCondition = FindViewById<Button>(Resource.Id.buttonRegisterViewTermAndCondition);
            //buttonRegisterViewTermAndCondition.Click += buttonRegisterViewTermAndCondition_Click;

            //buttonRegisterViewPrivacyPolicy = FindViewById<Button>(Resource.Id.buttonRegisterViewPrivacyPolicy);
            //buttonRegisterViewPrivacyPolicy.Click += buttonRegisterViewPrivacyPolicy_Click;


            
            // EditText txtEditMobileNo = FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNo);
            // txtEditMobileNo.AddTextChangedListener(this);

            //HÄR SKA VI SÄTTA TELEFON LANDSNUMMER
            FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNoCountry).Text = GeneralSettings.TelephoneNoMasking;
            setCurrentClientTheme();
            //Hämtar ner ApplicationToken
            appTokenModel = await RestAPI.GetApplicationToken();

            // FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNo).Hint = GeneralSettings.TelephoneNoMasking;
        }

        private void setCurrentClientTheme()
        {
            try
            {
                Typeface tf = Typeface.CreateFromAsset(Assets, Joyces.Helpers.Settings.MainFont);
                EditText editTextEmail = FindViewById<EditText>(Resource.Id.txtRegisterViewEmail);
                editTextEmail.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextEmail.Hint = Lang.EMAIL;

                EditText editTextMobileCountry = FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNoCountry);
                editTextMobileCountry.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextMobileCountry.Text = GeneralSettings.TelephoneNoMasking;

                EditText editTextMobile = FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNo);
                editTextMobile.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextMobile.Hint = Lang.MOBILEPHONE;

                EditText editTextPersonalNumber = FindViewById<EditText>(Resource.Id.txtRegisterViewPersonalNumber);
                editTextPersonalNumber.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextPersonalNumber.Hint = Lang.PERSONALNUMBER;

                EditText editTextPassword1 = FindViewById<EditText>(Resource.Id.txtRegisterViewPassword1);
                editTextPassword1.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextPassword1.Hint = Lang.PASSWORD;

                EditText editTextPassword2 = FindViewById<EditText>(Resource.Id.txtRegisterViewPassword2);
                editTextPassword2.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                editTextPassword2.Hint = Lang.PASSWORD_CONFIRM;

                TextView txtTermsHeader = FindViewById<TextView>(Resource.Id.txtTermsHeader);
                txtTermsHeader.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtTermsHeader.Text = Lang.CREATE_ACCOUNT_TERMS_HEADER;

                TextView txtTerms = FindViewById<TextView>(Resource.Id.txtTerms);
                txtTerms.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                txtTerms.Text = Lang.CREATE_ACCOUNT_TERMS;

                CheckBox chkAccept = FindViewById<CheckBox>(Resource.Id.chbRegisterViewTerms);
                chkAccept.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                chkAccept.Text = Lang.CREATE_ACCOUNT_ACCEPT_TERMS;


                Button btnRegister = FindViewById<Button>(Resource.Id.btnRegisterViewRegister);
                btnRegister.SetTypeface(tf, Android.Graphics.TypefaceStyle.Normal);
                btnRegister.Text = Lang.CREATE_ACCOUNT;
            }
            catch (Exception ex)
            {
            }
        }
        private void buttonRegisterViewTermAndCondition_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse(GeneralSettings.TERM_AND_CONDITION_URL);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        private void buttonRegisterViewPrivacyPolicy_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse(GeneralSettings.PRIVACY_POLICY_URL);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        async private void btnRegister_Click(object sender, EventArgs e)
        {
            progress.Show();

            string sEmail = FindViewById<EditText>(Resource.Id.txtRegisterViewEmail).Text;
            string sCountryCode = FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNoCountry).Text;
            string sMobileNo = FindViewById<EditText>(Resource.Id.txtRegisterViewMobileNo).Text;
            if (sCountryCode.Contains("+"))
            {
                sCountryCode = sCountryCode.Replace("+", "");//SKA MAN TA BORT INLEDANDE NOLLOR OCKSÅ?
            }
            if (sMobileNo.StartsWith("0"))
            {
                sMobileNo = sMobileNo.Substring(1);
            }
            sMobileNo = sCountryCode + sMobileNo;
            //string sFirstName = FindViewById<EditText>(Resource.Id.txtRegisterViewFirstName).Text;
            //string sLastName = FindViewById<EditText>(Resource.Id.txtRegisterViewLastName).Text;
            //PERS NR
            string sPersNr = FindViewById<EditText>(Resource.Id.txtRegisterViewPersonalNumber).Text;
            string sPassword1 = FindViewById<EditText>(Resource.Id.txtRegisterViewPassword1).Text;
            string sPassword2 = FindViewById<EditText>(Resource.Id.txtRegisterViewPassword2).Text;
            bool bTermAndConditions = FindViewById<CheckBox>(Resource.Id.chbRegisterViewTerms).Checked;
            //bool bTerms = chk;
            if (validateFields(sEmail, sPassword1, sPassword2, bTermAndConditions))
            {
                var response = await RestAPI.RegisterUserWithPersonalNumber(sEmail,sPersNr,true,true,sMobileNo,sPassword1, appTokenModel.access_token);

                if (response == null)
                {
                    progress.Hide();

                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    AlertDialog ad = builder.Create();
                    ad.SetTitle(Lang.MESSAGE_HEADLINE);
                    ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    ad.SetMessage(Lang.UNEXPECTED_ERROR);
                    ad.SetButton(Lang.BUTTON_OK, (s, a) =>
                    {
                        //Skickar användaren till inloggningssidan
                        var intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    });

                    ad.Show();
                }
                else if (response != null && response is Customer)
                {
                    //Skickar användaren till inloggningssidan
                    progress.Hide();

                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    AlertDialog ad = builder.Create();
                    ad.SetTitle(Lang.MESSAGE_HEADLINE);
                    ad.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    ad.SetMessage(Lang.ACTIVATION_EMAIL_SENT);
                    ad.SetButton(Lang.BUTTON_OK, (s, a) =>
                    {
                        //Skickar användaren till inloggningssidan
                        var intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    });
                    ad.Show();

                }
                else if (response != null && response is AbalonErrors)
                {
                    Alert(Lang.ERROR_HEADLINE, ((AbalonErrors)response).message, Lang.BUTTON_OK);
                    progress.Hide();
                }
            }

            progress.Hide();
        }

        private bool validateFields(string sEmail, string sPassword1, string sPassword2, bool bTerms)
        {
            bool bValidates = true;

            if (!ObjectRepository.EmailValidator(sEmail))
            {
                Alert(Lang.ERROR_HEADLINE, Lang.ENTER_VALID_EMAIL, Lang.BUTTON_OK);
                bValidates = false;
            }

            if (sPassword1.Length < 1 || sPassword2.Length < 1)
            {
                Alert(Lang.ERROR_HEADLINE, Lang.ENTER_VALID_PASSWORD, Lang.BUTTON_OK);
                bValidates = false;
            }

            if (sPassword1 != sPassword2)
            {
                Alert(Lang.ERROR_HEADLINE, Lang.PASSWORD_NOT_MATCH, Lang.BUTTON_OK);
                bValidates = false;
            }

            if (!bTerms)
            {
                Alert(Lang.ERROR_HEADLINE, Lang.TERM_AND_CONDITIONS, Lang.BUTTON_OK);
                bValidates = false;
            }

            return bValidates;
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
    }
}