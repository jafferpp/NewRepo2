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

namespace EZE_BASKET
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Category : ContentPage
{
        public static string code;
        bool isstatrt;bool a;bool b;bool c;bool d;bool ee;
        int rowcount;int reader3count=0;

        private MySqlDataReader reader1; private MySqlDataReader reader2; private MySqlDataReader reader3;
        MySqlConnection con1 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        MySqlConnection con2 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        MySqlConnection con3 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");

        MySqlCommand cmd1 = new MySqlCommand(); MySqlCommand cmd2 = new MySqlCommand(); MySqlCommand cmd3 = new MySqlCommand();
        private Frame[] bigframe = new Frame[6];
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
        Label labe1 = new Label();
        Label labe2 = new Label();
        Label labe3 = new Label();
        Label l2 = new Label();
        Label[] label1 = new Label[6];

        ScrollView[] scrarray = new ScrollView[20];int scrarraycount = 0;
        StackLayout[] stacklist1 = new StackLayout[20];

        private Label mainlabel = new Label() { FontSize = 18, TextColor = Color.Brown, FontAttributes = FontAttributes.Bold };
        int imagecount = 0; int scrollcount2; int scrollhide1 = 0; int bigframecount = 0; int scrollcount1 = 277;
        string str; int scrollhide2 = 0; int count; int i = 0;
        private StackLayout bigstacklayout = new StackLayout() { Padding = 0, Margin = 0, Spacing = 0 };
        private ScrollView scr = new ScrollView()
        {
            Orientation = ScrollOrientation.Horizontal,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never
        };
        private StackLayout stk = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = 0,
            Padding = 0,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            HeightRequest = 50
        };
        private ScrollView scr2 = new ScrollView()
        {
            Orientation = ScrollOrientation.Horizontal,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never
        };
        private StackLayout stk2 = new StackLayout()
        { Orientation = StackOrientation.Horizontal, Spacing = 0, Padding = 0, Margin = 0 };

        public Category()
        {
            InitializeComponent();isstatrt = true;
            Task task1 = Task.Factory.StartNew(() => one());
            Task task2 = Task.Factory.StartNew(() => two());
            Task task3 = Task.Factory.StartNew(() => three());
            Task.WaitAll(task1, task2, task3);
        }
        protected override void OnAppearing()
        {
            rowcount = 0; isstatrt = false; a = true; b = false; c = false; d = false; ee = false; scrarraycount = 0;
            
            cmd1 = new MySqlCommand("SELECT DISTINCT category FROM product_table ORDER BY RAND()", con1);
            reader1 = cmd1.ExecuteReader();
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
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                

                    return abc();
                
                
            });
        }
        private bool abc()
        {   
            if (a)
            {if (reader1.Read())
                {
                    mainlabel = new Label() { FontSize = 18, TextColor = Color.Brown, FontAttributes = FontAttributes.Bold };

                    mainlabelread(); a = false; b = true;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        firststack.Children.Add(mainlabel);
                    });
                    return true;
                }
                else { isstatrt = false; return false; }
            }
            if (b)
            {stk= new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 50
            };
                scr= new ScrollView()
                {
                    Orientation = ScrollOrientation.Horizontal,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Never
                };
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    scr.Content = stk;
                });
                firststack.Children.Add(scr);
                b = false;c = true;return true;
            }
            if (c)
            {
                if( reader2.Read())
                {
                    reader2call();reader3count++;
                    if (reader3count == 4)
                    {
                        c = false;
                    }
                    return true;
                }
                else {
                    reader3read();
                    c = false;d = true;rowcount++; return true; }
                
            }
            if(d)
            {
                scrarray[scrarraycount] = new ScrollView()
                {
                    Orientation = ScrollOrientation.Horizontal,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Never,HeightRequest=210
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
                firststack.Children.Add(scrarray[scrarraycount]);scrarraycount++;
                d = false;ee = true;return true;
            }
            if (ee)
            {
                if (reader3.Read())
                {
                    reader3call();return true;
                }
                else { ee = false;a = true; }
            }
            return true;
        }
        private void mainlabelread()
        {

            mainlabel.Text = reader1["category"].ToString();
            str = "SELECT DISTINCT section FROM product_table WHERE category='" + (mainlabel.Text) + "'";
            con2.Close(); con2.Open();
            cmd2 = new MySqlCommand(str, con2);
            reader2 = cmd2.ExecuteReader();
        }
        private void reader3read()
        {
            str = "SELECT * FROM product_table WHERE category='" + (mainlabel.Text) + "'" + " ORDER BY RAND()";
            con3.Close(); con3.Open();
            cmd3 = new MySqlCommand(str, con3);
            reader3 = cmd3.ExecuteReader();
        }
        private void reader3call()
        {

            var ff = new Frame();
            ff.HeightRequest = 210; ff.WidthRequest = 113; ff.Margin = 0; ff.Padding = 0; ff.CornerRadius = 5;
            ff.BorderColor = Color.LightGray;
            
            ff.GestureRecognizers.Add(tapGestureRecognizer);
            ff.StyleId= reader3["bar_code"].ToString();
            ff.ClassId = reader3["name"].ToString();
            var ll = new StackLayout();
            ll.Padding = 0; ll.Spacing = 0; ll.Margin = 3; ll.HorizontalOptions = LayoutOptions.Start; ll.VerticalOptions = LayoutOptions.Start;
            Image imag = new Image();imag.Aspect = Aspect.AspectFit;
            imag.Source = ImageSource.FromStream(() =>
            {
                byte[] imm = (byte[])reader3["image"];

                MemoryStream j = new MemoryStream(imm);
                return j;
            });
            var labe1 = new Label(); var labe2 = new Label(); var labe3 = new Label();
            labe1.FontAttributes = FontAttributes.Bold; labe1.FontSize = 15;
            labe2.FontSize = 11; labe3.FontSize = 15; labe3.VerticalOptions = LayoutOptions.End; labe2.LineBreakMode = LineBreakMode.TailTruncation;
            labe3.LineBreakMode = LineBreakMode.TailTruncation; labe3.VerticalOptions = LayoutOptions.End;

            labe1.Text = reader3["price"].ToString();
            labe2.Text = reader3["description"].ToString();
            labe3.Text = "delivery 15 min";
            ll.Children.Add(imag); ll.Children.Add(labe1); ll.Children.Add(labe2); ll.Children.Add(labe3);
            ff.Content = ll; stk2.Children.Add(ff); imag = null;

        }
        
    private void reader2call()
        {

            var tempimg = firstimage("defaultimage.png");

            var l2 = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = reader2["section"].ToString() + "  "
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
            fr2.Content = tempimg; tempimg = null;
            st.Children.Add(fr2);
            st.Children.Add(l2);
            fr1.Content = st;
            fr1.GestureRecognizers.Add(tapGestureRecognizer1);
            fr1.ClassId = scrarraycount.ToString();
            fr1.StyleId = reader2["section"].ToString();
            stk.Children.Add(fr1);

        }
        private Image firstimage(string s)
        {
            Image tempimg = new Image()
            {
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,
                Source = s
            };
            return tempimg;
        }
        private Image secondimage(string s)
        {
            Image imag = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = 108,
                HeightRequest = 140,
                Source = "defaultimage.png"
            };
            return imag;
        }
        private void one()
        {
            con1.Open();
        }
        private void two()
        {
            con2.Open();
        }
        private void three()
        {
            con3.Open();
        }
        private void scrarr(string a, int b)
        {
            
            con3.Close(); con3.Open();
            str = "SELECT * FROM product_table WHERE section = '" + a +"'";
            cmd3 = new MySqlCommand(str, con3); reader3 = cmd3.ExecuteReader();
            stk2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 210
            };
            scrarray[b].Content = stk2;reader3.Read();
            for(int iq =0;iq<3;iq++)
            {
                reader3.Read();
                reader3call();
                
            }
            
        }
        
    }
}