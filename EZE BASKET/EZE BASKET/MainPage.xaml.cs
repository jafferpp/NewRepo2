﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EZE_BASKET
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Shell
    {
       public bool a;
        public MainPage()
       {
            InitializeComponent();a = true;
        }

        protected override bool OnBackButtonPressed()
        {

            if (a)
            {
                a = false;
                OnBackButtonPressed();
            }


            return false ;
           
            
        }
    }
}