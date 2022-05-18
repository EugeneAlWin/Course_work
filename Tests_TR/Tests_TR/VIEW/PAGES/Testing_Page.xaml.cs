namespace Tests_TR.VIEW.PAGES
{
    /// <summary>
    /// Логика взаимодействия для Testing_Page.xaml
    /// </summary>
    public partial class Testing_Page
    {
        public Testing_Page()
        {
            InitializeComponent();
            DataContext = new PagesViewModel();
        }

        private void TabItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PagesViewModel.Switch_Tab_By_TabItem.Execute(PagesViewModel.SelectedIndex[0]);
        }
    }
}