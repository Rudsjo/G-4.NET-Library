﻿using Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Interaction logic for MyProfileControl.xaml
    /// </summary>
    public partial class MyProfileControl : UserControl
    {
        public MyProfileControl()
        {
            InitializeComponent();
            DataContext = IoC.CreateInstance<MyProfileControlViewModel>();
        }
    }
}
