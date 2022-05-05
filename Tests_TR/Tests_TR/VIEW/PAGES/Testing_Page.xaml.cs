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

    }
}
