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
    public partial class Page1: ContentPage
    {

        int framecount = 0; MySqlDataReader reader; Grid grid = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Margin = 0, Padding = 0 };
        Label[] qty = new Label[50]; int qtycount = 0;
        Frame[] f = new Frame[50];
        Grid[] dr = new Grid[50]; Label commentlabel = new Label();
        Editor[] ed = new Editor[50];
        MySqlConnection con2 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");

        public Page1()
        {
            InitializeComponent();
            grid.ColumnSpacing = 0; grid.RowSpacing = 0; grid.Margin = 0; grid.Padding = 0;
            startbackgrounding();
        }
        private async void startbackgrounding()
        {
            Task<bool> task1 = new Task<bool>(firstcall);
            task1.Start();
            bool a = await task1;
            loading.IsVisible = false; gify2.IsVisible = false;
            if (reader.Read())
            {
                var lab = new Label() { FontSize = 20, TextColor = Color.Black, FontAttributes = FontAttributes.Bold };
                lab.Text = "#" + reader["name"].ToString();
                fisrtstack.Children.Add(lab);
                Task<Frame> task2 = new Task<Frame>(secondcall);
                task2.Start();
                Frame b = await task2;
                fisrtstack.Children.Add(b);
                Task<Grid> task3 = new Task<Grid>(thirdcall);
                task3.Start();
                Grid c = await task3;
                fisrtstack.Children.Add(c);
                Task<StackLayout> task4 = new Task<StackLayout>(fourthcall);
                task4.Start();
                StackLayout d = await task4;
                fisrtstack.Children.Add(d);
               
            }
        }
      

        StackLayout fourthcall()
        {
            StackLayout st1 = new StackLayout() { Padding = 0, Spacing = 0, Margin = 0, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.LightBlue };

            Button b1 = new Button(); Button b2 = new Button(); Button b3 = new Button();
            b1.Text = "Product Details"; b1.ClassId = reader["details"].ToString(); b2.Text = "Specifications"; b2.ClassId = reader["specification"].ToString(); b3.Text = "Refund policy"; b3.ClassId = reader["refund"].ToString();
            b1.FontSize = 11; b2.FontSize = 12; b3.FontSize = 12;
            b1.BackgroundColor = Color.LightBlue; b2.BackgroundColor = Color.LightBlue; b3.BackgroundColor = Color.LightBlue;
            b1.StyleId = framecount.ToString();
            b2.StyleId = framecount.ToString();
            b3.StyleId = framecount.ToString();
            b3.HorizontalOptions = LayoutOptions.End;
            b1.Clicked += new System.EventHandler(this.buttonclickj1);
            b2.Clicked += new System.EventHandler(this.buttonclickj2);
            b3.Clicked += new System.EventHandler(this.buttonclickj3);
            st1.Children.Add(b1); st1.Children.Add(b2); st1.Children.Add(b3);
            Label l = new Label() { FontSize = 14, TextColor = Color.Black };

            l.Text = "DETAILS: " + reader["details"].ToString();
            StackLayout framtack = new StackLayout() { Margin = 0, Spacing = 0, Padding = 0 };
            f[framecount] = new Frame() { BorderColor = Color.LightBlue, Padding = 0, Margin = 0, HorizontalOptions = LayoutOptions.Fill };
            f[framecount].Content = l;
            framtack.Children.Add(st1);
            framtack.Children.Add(f[framecount]);
            return framtack;

        }
        Grid thirdcall()
        {
            grid.BackgroundColor = Color.LightBlue;
            grid.RowDefinitions.Add(new RowDefinition { Height = 80 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 120 });
            StackLayout st = new StackLayout() { Spacing = 2, Margin = 0, Padding = 2, HeightRequest = 80 };
            Label lb = new Label() { FontSize = 15, FontAttributes = FontAttributes.Bold };
            lb.Text ="AED"+ reader["price"].ToString();
            Label lb2 = new Label() { FontSize = 12 }; lb2.Text = reader["description"].ToString();
            st.Children.Add(lb);
            st.Children.Add(lb2);
            StackLayout g = new StackLayout() { Padding = 0, Margin = 0, Spacing = 5, HorizontalOptions = LayoutOptions.Fill };

            Button b = new Button() { BackgroundColor = Color.DarkSlateBlue, TextColor = Color.White, CornerRadius = 10, Text = "Add to cart", HeightRequest = 40, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Start }; b.Clicked += new System.EventHandler(this.buttonclickb);
            b.StyleId = reader["bar_code"].ToString();
            b.ClassId = qtycount.ToString();
            StackLayout stk = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 5, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };
            Button b1 = new Button() { BackgroundColor = Color.DarkOrange, TextColor = Color.White, CornerRadius = 10, Text = "-", Padding = 0, HeightRequest = 35, FontSize = 9, HorizontalOptions = LayoutOptions.Start, WidthRequest = 50, VerticalOptions = LayoutOptions.Fill, ClassId = qtycount.ToString() };
            b1.Clicked += new System.EventHandler(this.buttonclickb1);
            qty[qtycount] = new Label() { FontSize = 12, Text = "0", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, };
            Button b2 = new Button() { BackgroundColor = Color.LimeGreen, TextColor = Color.White, CornerRadius = 10, Padding = 0, Text = "+", FontSize = 9, HeightRequest = 35, HorizontalOptions = LayoutOptions.End, WidthRequest = 50, VerticalOptions = LayoutOptions.Fill, ClassId = qtycount.ToString() };
            b2.Clicked += new System.EventHandler(this.buttonclickb2);
            stk.Children.Add(b1); stk.Children.Add(qty[qtycount]); stk.Children.Add(b2);
            g.Children.Add(b); g.Children.Add(stk);
            grid.Children.Add(st, 0, 0); grid.Children.Add(g, 1, 0);
            qtycount++;
            return grid;
        }
        Frame secondcall()
        {

            var img = new Image() { HorizontalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFill };
            var fr = new Frame() { HorizontalOptions = LayoutOptions.Fill, CornerRadius = 5, Padding = 0, Margin = 0, BorderColor = Color.LightBlue };

            byte[] imm = (byte[])reader["image"];

            img.Source = ImageSource.FromStream(() => new MemoryStream(imm));

            fr.Content = img;
            return fr;
        }
        bool firstcall()
        {
            MySqlConnection con1 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            string gh = "SELECT * FROM product_table WHERE bar_code='" + category.code + "'";
            MySqlCommand cmd = new MySqlCommand(gh, con1);
            con1.Open();
            reader = cmd.ExecuteReader();

            return true;
        }
        //  protected override void OnAppearing()
        //  {

        // }

        private void buttonclickbr(object sender, EventArgs e)
        {
            Button bm = (Button)sender;
            int r = Int32.Parse(bm.StyleId.ToString());
            Image image9 = new Image(); image9.Source = "defaultimage.png";
            StackLayout kl = new StackLayout() { Padding = 0, Spacing = 0, Margin = 0 };
            Label ll = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 15, Text = login.strl };
            Label lll = new Label() { FontSize = 11, Text = ed[r].Text };
            kl.Children.Add(ll); kl.Children.Add(lll);
            dr[r].Children.Add(image9, 0, 0); dr[r].Children.Add(kl, 1, 0);
        }
        private void buttonclickj1(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT details FROM product_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = "DETAILS: " + rer["details"].ToString();
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickj2(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT specification FROM product_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = "APECIFICATIONS: " + rer["specification"].ToString();//specification
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickj3(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT refund FROM product_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = "REFUND: " + rer["refund"].ToString();//refund
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickb(object sender, EventArgs e)//adto cart buton
        {
            Button k = (Button)sender;
            string s = k.ClassId;
            MySqlConnection conn2 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            //
            string str = "INSERT INTO cart(`mobile`,`code`,`name`,`price`,`qty`,`image`) SELECT @mob, `bar_code`, `name`, `price`, @qty, `image` FROM product_table WHERE bar_code='" + s + "'";
            int a = Int32.Parse(s);
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand(str, conn2);
            cmd.Parameters.Add("@mob", MySqlDbType.VarChar);
            cmd.Parameters["@mob"].Value = login.mobileno;
            cmd.Parameters.Add("@qty", MySqlDbType.VarChar);
            cmd.Parameters["@qty"].Value = qty[a].Text;

            cmd.ExecuteNonQuery();
            conn2.Close();
        }
        private void buttonclickb1(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.ClassId.ToString());
            int a = Int32.Parse(qty[asdf].Text);
            if (a != 0)
            {
                a--;
            }
            qty[asdf].Text = a.ToString();

        }
        private void buttonclickb2(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.ClassId.ToString());
            int a = Int32.Parse(qty[asdf].Text);

            a++;

            qty[asdf].Text = a.ToString();
        }
    }
}