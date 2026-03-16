using MinhasCompras.Helpers;

namespace MinhasCompras
{
  public partial class App : Application
  {
    static SQLiteDatabaseHelper _database;

    public static SQLiteDatabaseHelper Database
    {
      get
      {
        if (_database == null)
        {
          string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "compras.db3"
          );
          _database = new SQLiteDatabaseHelper(path);
        }

        return _database;
      }
    }

    public App()
    {
      InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      Window window = new(new NavigationPage(new Views.ListaProduto()))
      {
        Width = 400,
        MinimumWidth = 320,
        Height = 800,
        MinimumHeight = 480
      };
      return window;
    }
  }
}
