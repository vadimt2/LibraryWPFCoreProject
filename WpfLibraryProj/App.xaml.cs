using DryIoc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;
using WpfLibraryProj.Logic.AbstractItemService;
using WpfLibraryProj.Logic.BookService;
using WpfLibraryProj.Logic.JournalService;
using WpfLibraryProj.Logic.UserRentalService;
using WpfLibraryProj.Logic.UserService;
using WpfLibraryProj.ViewModels;
using WpfLibraryProj.ViewModels.Customer;
using WpfLibraryProj.ViewModels.Employee;
using WpfLibraryProj.ViewModels.Manage.Items;
using WpfLibraryProj.ViewModels.Manager;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.LibraryItems.AllCollection;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;
using WpfLibraryProj.Views.Users.Customer;
using WpfLibraryProj.Views.Users.Employee;
using WpfLibraryProj.Views.Users.Manager;

namespace WpfLibraryProj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            //using (var db = new LibraryContext())
            //{
            //    db.Database.Migrate();
            //}
        }

        protected override Window CreateShell()
        {
            var shell = Container.Resolve<MainWindowView>();
            return shell;
     
        }


        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Container.Resolve<LibraryContext>();

            ////////////////// Services


            containerRegistry.Register(typeof(INavigateAsync), typeof(Region));

            containerRegistry.Register<IAbsractItemService, AbsractItemService>();

            containerRegistry.Register<IUserService, UserService>();

            containerRegistry.Register<IBookService, BookService>();

            containerRegistry.Register<IJournalService, JournalService>();

            containerRegistry.Register<IUserRentalService, UserRentalService>();

   
            ////////////////// Repositories

            containerRegistry.Register<IRepository<AbstractItem>, Repository<AbstractItem>>();

            containerRegistry.Register<IRepository<User>, Repository<User>>();

            containerRegistry.Register<IRepository<Book>, Repository<Book>>();

            containerRegistry.Register<IRepository<Journal>, Repository<Journal>>();

            containerRegistry.Register<IRepository<CustomerRentalModel>, Repository<CustomerRentalModel>>();

            containerRegistry.Register<IRepository<ItemCollection>, Repository<ItemCollection>>();

            // App Services


            ////////////////// Window and ViewModel
            ///
            

            containerRegistry.RegisterForNavigation<MainWindowView, MainWindowViewModel>(Pages.MainWindowView);
           
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>(Pages.MainView);

            containerRegistry.RegisterForNavigation<SignupView, SignupViewModel>(Pages.SignupView);

            containerRegistry.RegisterForNavigation<CustomerMainView, CustomerMainViewModel>(Pages.CustomerMainView);

            containerRegistry.RegisterForNavigation<EmployeeMainView, EmployeeMainViewModel>(Pages.EmployeeMainView);

            containerRegistry.RegisterForNavigation<EmployeeAllStoreItemsView, EmployeeAllStoreItemsViewModel>(Pages.EmployeeAllStoreItemsView);

            containerRegistry.RegisterForNavigation<EmployeeAllUsersView, EmployeeAllUsersViewModel>(Pages.EmployeeAllUsersView);
            
            containerRegistry.RegisterForNavigation<ManagerMainView, ManagerMainViewModel>(Pages.ManagerMainView);

            containerRegistry.RegisterForNavigation<AddItemView, AddItemViewModel>(Pages.AddItemView);

            containerRegistry.RegisterForNavigation<AddBookView, AddBookViewModel>(Pages.AddBookView);

            containerRegistry.RegisterForNavigation<AddJournalView, AddJournalViewModel>(Pages.AddJournalView);

            containerRegistry.RegisterForNavigation<ManageAllItemsView, ManageAllItemsViewModel>(Pages.ManageAllItemsView);

            containerRegistry.RegisterForNavigation<EmptyPageView>(Pages.EmptyPageView);

            containerRegistry.RegisterForNavigation<ItemHasAddedView, ItemHasAddedViewModel>(Pages.ItemHasAddedView);

            containerRegistry.RegisterForNavigation<ViewAllItemsView, ViewAllItemsViewModel>(Pages.ViewAllItemsView);

            containerRegistry.RegisterForNavigation<BookView, BookViewModel>(Pages.BookView);

            containerRegistry.RegisterForNavigation<JournalView, JournalViewModel>(Pages.JournalView);

            containerRegistry.RegisterForNavigation<AllUsersView, AllUsersViewModel>(Pages.AllUsersView);

            containerRegistry.RegisterForNavigation<AddUserView, AddUserViewModel>(Pages.AddUserView);

            containerRegistry.RegisterForNavigation<CurrentBorrowedItemView, CurrentBorrowedItemViewModel>(Pages.CurrentBorrowedItemView);

            containerRegistry.RegisterForNavigation<HistoryOfRentedItemsView, HistoryOfRentedItemsViewModel>(Pages.HistoryOfRentedItemsView);


            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
        }


        ////////////////// DF of WPF MICROSOFT.
        /// <summary>
        /// Now In use !
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }


        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    var builder = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //    Configuration = builder.Build();

        //var serviceCollection = new ServiceCollection();
        //ConfigureServices(serviceCollection);

        //ServiceProvider = serviceCollection.BuildServiceProvider();

        //var mainWindow = ServiceProvider.GetRequiredService<MainView>();
        //mainWindow.Show();


        //using (var db = new LibraryContext())
        //{
        //    db.Database.Migrate();
        //}

        //}

        private void ConfigureServices(IServiceCollection services)
        {
          
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LibraryDataBase;Trusted_Connection=True;"));
            //services.AddScoped(typeof(IRepository<User>), typeof(Repository<User>));
            //services.AddScoped(typeof(IRepository<AbstractItem>), typeof(Repository<AbstractItem>));
            //services.AddScoped(typeof(IUserService), typeof(UserService));
            //services.AddScoped(typeof(IAbsractItemService), typeof(AbsractItemService));
            //services.AddScoped(typeof(MainView));
        }


    }
}
