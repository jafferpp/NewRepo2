using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace EZE_BASKET
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class logincustomer : ContentPage
    {
        MemoryStream mn = new MemoryStream();
        byte[] imgs;
        public static string namestring;
        public static Image profile = new Image();
        public static string mobileno;
        public logincustomer()
        {
            InitializeComponent();

        }

        private void logincutomer(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//customer/home/");
        }

        private void signup(object sender, EventArgs e)
        {
            firststack.IsVisible = false;
            secondstack.IsVisible = true;

        }
        void countc()
        {
            MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            string str = "INSERT INTO registration(mobile,name,password) VALUES(@mob,@name,@password)";

            MySqlCommand cmd = new MySqlCommand(str, con);
            
            cmd.Parameters.Add("@mob", MySqlDbType.VarChar);
            cmd.Parameters["@mob"].Value = entry22.Text;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar);
            cmd.Parameters["@name"].Value = entry11.Text;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar);
            cmd.Parameters["@password"].Value = entry66.Text;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void cancel(object sender, EventArgs e)
        {
            secondstack.IsVisible = false;
            firststack.IsVisible = true;

        }

        private  void imgclick(object sender, EventArgs e)
        {
            //await CrossMedia.Current.Initialize();
            // if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
            // {
            //   await DisplayAlert("message", "photo capture not supported", "ok");
            //   return;
            //}
            //  else
            //  {


            //   var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //   {
            //       PhotoSize = PhotoSize.Small,
            //      CompressionQuality = 30,
            //      Name = DateTime.Now + "test.jpg",
            //      Directory = "my_images"


            //  });

            //  if (file == null)
            //  {
            //    return;
            //
            //  }
            // await DisplayAlert("file path", file.Path, "ok");
            // imgbutton.Source = ImageSource.FromStream(() =>
            // {
            //    var stream = file.GetStream();

            //    return stream;
            // });

            // file.GetStream().CopyTo(mn);
            // imgs = mn.ToArray();
        }
    

        private void sign_up(object sender, EventArgs e)
        {
            if (entry11.Text == "" && entry22.Text == "")
            {
                DisplayAlert("fill the mandatory fields", "name and mobile no", "yes");
            }
            else
            {
                if (entry66.Text == entry77.Text)
                {

                    countc();
                }
                else
                {
                    DisplayAlert("password not matching", "conform your password", "yes");

                }
            }
        }
    }
}