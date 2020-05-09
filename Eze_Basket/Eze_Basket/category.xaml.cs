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
    public partial class category : ContentPage
    {
        public static string code; int something = 0;
        
        
        MySqlDataReader reader;

        MySqlConnection con1 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        
       
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
        Label labe1 = new Label();
        Label labe2 = new Label();
        Label labe3 = new Label();
        Label l2 = new Label();
        Label[] mainlabelarray = new Label[100];

        ScrollView[] scrarray = new ScrollView[20]; int scrarraycount = 0;
        StackLayout[] stacklist1 = new StackLayout[20];

        string str; 
        
        private ScrollView scr = new ScrollView();
        private StackLayout stk = new StackLayout();
        private StackLayout stk2 = new StackLayout();

        public category()
        {
            InitializeComponent(); 
            tapGestureRecognizer1.Tapped += (sender, e) =>
            {
                Frame f = (Frame)sender;
                string frs = f.StyleId.ToString();
                int asdf = Int32.Parse(f.ClassId.ToString());
                scrarr(frs, asdf);

            };
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                Frame f1 = (Frame)sender;
                code = f1.StyleId.ToString();
                Shell.Current.Navigation.PushModalAsync(new Page1());
            };
            startbackgrounding();
        }
       // protected override void OnAppearing()
       // {
          //  base.OnAppearing();
           // startbackgrounding();
            
       // }
        private async void startbackgrounding()
        {
            Task<int> task1 = new Task<int>(firsttask);
            task1.Start();
            int catlen = await task1;
            gify2.IsVisible = false;
            int somecount = 0;
            goline:
            if (somecount < 1)
            {
                firststack.Children.Add(mainlabelarray[somecount]);
                
                Task<ScrollView> task2 = new Task<ScrollView>(secondtask);
                
                task2.Start();
            ScrollView som = await task2;
                firststack.Children.Add(som);
                Task<ScrollView> task3 = new Task<ScrollView>(thirdtask);
                task3.Start();
                ScrollView some = await task3;
                scrarraycount++;
                firststack.Children.Add(some);

                somecount++; something++;
                goto goline; }
            
        }

        private ScrollView thirdtask()
        {
            str = "SELECT * FROM product_table WHERE category='" + (mainlabelarray[something].Text) + "'" + " ORDER BY RAND()";
            MySqlCommand cmd = new MySqlCommand(str, con1);
            
            con1.Open();
            reader = cmd.ExecuteReader();

            scrarray[scrarraycount] = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                HeightRequest = 210
            };

            stk2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 210
            };
            
            scrarray[scrarraycount].Content = stk2;
            
            for (int i=0; i<4;i++)
            {if (reader.Read())
                { reader3call(); }
                
            }con1.Close(); return scrarray[scrarraycount];
        }

        private ScrollView secondtask()
        {
            con1.Open();

            str = "SELECT section FROM product_table WHERE category='" + (mainlabelarray[something].Text) + "'";
            MySqlCommand cmd = new MySqlCommand(str, con1);
            reader = cmd.ExecuteReader();
            stk = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 50
            };
            scr = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                HeightRequest = 50,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never
            };
            scr.Content = stk;
            
            
            while (reader.Read())
            {
                reader2call();
            }con1.Close();
            return scr;
        }

        private int firsttask()
        {
            con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT  category FROM product_table ORDER BY RAND()", con1);
            
            reader = cmd.ExecuteReader();int la = 0;
            while (reader.Read())
            {
                mainlabelarray[la] = new Label() { FontSize = 18, TextColor = Color.Brown, FontAttributes = FontAttributes.Bold };
                mainlabelarray[la].Text = reader["category"].ToString();  la++;
            }
            int s = mainlabelarray.Length;con1.Close();
            return s;

        }
        
        private void reader3call()
        {

            var ff = new Frame();
            ff.HeightRequest = 210; ff.WidthRequest = 113; ff.Margin = 0; ff.Padding = 0; ff.CornerRadius = 5;
            ff.BorderColor = Color.LightGray;

            ff.GestureRecognizers.Add(tapGestureRecognizer);
            ff.StyleId = reader["bar_code"].ToString();
            ff.ClassId = reader["name"].ToString();
            var ll = new StackLayout();
            ll.Padding = 0; ll.Spacing = 0; ll.Margin = 3; ll.HorizontalOptions = LayoutOptions.Start; ll.VerticalOptions = LayoutOptions.Start;
            Image imag1 = new Image(); imag1.Aspect = Aspect.AspectFill;imag1.WidthRequest = 113;imag1.HeightRequest = 150;
            
                byte[] imm = (byte[])reader["image"];

            imag1.Source = ImageSource.FromStream(() => new MemoryStream(imm));
            var labe1 = new Label(); var labe2 = new Label(); var labe3 = new Label();
            labe1.FontAttributes = FontAttributes.Bold; labe1.FontSize = 15;
            labe2.FontSize = 11; labe3.FontSize = 15; labe3.VerticalOptions = LayoutOptions.End; labe2.LineBreakMode = LineBreakMode.TailTruncation;
            labe3.LineBreakMode = LineBreakMode.TailTruncation; labe3.VerticalOptions = LayoutOptions.End;

            labe1.Text = reader["price"].ToString();
            labe2.Text = reader["description"].ToString();
            labe3.Text = "delivery 15 min";
            ll.Children.Add(imag1); ll.Children.Add(labe1); ll.Children.Add(labe2); ll.Children.Add(labe3);
            ff.Content = ll; stk2.Children.Add(ff); 

        }

        private void reader2call()
        {
            Image tempimg = new Image()
            {
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,Source="cart.png"
            };

            

            var l2 = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = reader["section"].ToString() + "  "
            };

            var fr1 = new Frame
            {
                CornerRadius = 50,
                HeightRequest = 50,
                Padding = 0,
                Margin = 0,
                BorderColor = Color.LightBlue
            };

            var fr2 = new Frame
            {
                CornerRadius = 45,
                HeightRequest = 45,
                WidthRequest = 45,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Padding = 0,
                Margin = 4,
                BorderColor = Color.Red
            };

            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 1,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            fr2.Content = tempimg; 
            st.Children.Add(fr2);
            st.Children.Add(l2);
            fr1.Content = st;
            fr1.GestureRecognizers.Add(tapGestureRecognizer1);
            fr1.ClassId = scrarraycount.ToString();
            fr1.StyleId = reader["section"].ToString();
            stk.Children.Add(fr1);
         }
       
        private void scrarr(string a, int b)
        {

            con1.Open();
            str = "SELECT * FROM product_table WHERE section = '" + a + "'";
           MySqlCommand cmd = new MySqlCommand(str, con1); reader = cmd.ExecuteReader();
            stk2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 210
            };
            scrarray[b].Content = stk2; 
            for (int iq = 0; iq < 4; iq++)
            {
                if (reader.Read())
                { reader3call(); }

            }
            con1.Close();
        }

    }
}