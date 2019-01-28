using JudoPayDotNet;
using JudoPayDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PaymentTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCheckOut_Clicked(object sender, EventArgs e)
        {
            var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, "vNJCgGYhlY1CSPCh", "9b9311eadf62a180d8e7f7c9224f831f6fae0dbdb15e61c29f52b85b33deb8a3");

            var cardPaymentModel = new CardPaymentModel
            {
                JudoId = "100699-396",

                // value of the payment

                Amount = 10,
                Currency = "USD",

                // card details
                CardNumber = "4976 0000 0000 3436",
                ExpiryDate = "12/20",
                CV2 = "452",

                // an identifier for your customer
                YourConsumerReference = "MyCustomer004",
            };


            client.Payments.Create(cardPaymentModel).ContinueWith(result =>
            {
                var paymentResult = result.Result;

                if (!paymentResult.HasError && paymentResult.Response.Result == "Success")
                {
                    string id = paymentResult.Response.ReceiptId.ToString();
                    //DisplayAlert("Payment  successful. Transaction Reference {0}", paymentResult.Response.ReceiptId.ToString(), "ok");
                    Console.WriteLine("Payment successful. Transaction Reference {0}", paymentResult.Response.ReceiptId);
                    LblID.Text = id;
                    DisplayAlert("Alert", id, "OK");
                }
            });
        }
    }
}
