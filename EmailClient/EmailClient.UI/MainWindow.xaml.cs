using System.Reflection;
using System.Windows;


namespace EmailClient.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
