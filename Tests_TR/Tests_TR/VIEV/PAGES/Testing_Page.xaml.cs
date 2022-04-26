using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tests_TR.VIEV.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Testing_Page.xaml
    /// </summary>
    public partial class Testing_Page : Page, INotifyPropertyChanged
    {
        public List<Text>? Texts { get; set; } = new() { new Text() { Textt = "привd" }, new Text() { Textt = "пок" } };
        public Testing_Page()
        {

            InitializeComponent();
            DataContext = Texts;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class Text : INotifyPropertyChanged
    {
        public string? Textt { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
