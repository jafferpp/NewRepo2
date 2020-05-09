using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Eze_Basket
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cart : ContentPage
    {
        Grid grid = new Grid(); int a;
        MySqlDataReader reader;
        MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        public cart()
        {
            InitializeComponent();
            string cv = "SELECT * FROM cart WHERE mobile='" + login.mobileno + "'";
            MySqlCommand cmd = new MySqlCommand(cv, con);

            con.Open();
            reader = cmd.ExecuteReader();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                if (reader.Read())
                {
                    abc(); return true;
                }
                return false;
            });
        }
        void abc()
        {

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Image img = new Image() { Aspect = Aspect.AspectFit };
            img.Source = "defaultimage.png";
            StackLayout st = new StackLayout() { Padding = 0, Spacing = 0, Margin = 0 };
            Label lab1 = new Label();
            Label lab2 = new Label();
            Label lab3 = new Label();
            lab1.Text = reader["name"].ToString();
            lab2.Text = reader["price"].ToString();
            lab3.Text += "quantity=" + reader["qty"].ToString();
            st.Children.Add(lab1);
            st.Children.Add(lab2);
            st.Children.Add(lab3);
            grid.Children.Add(img, 0, a); grid.Children.Add(st, 1, a); a++;

        }

    }
}