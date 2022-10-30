using LibDatabase;
using LibDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppSimple.Models;

namespace WpfAppSimple
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private ObservableCollection<UserVM> users = new ObservableCollection<UserVM>();
        private readonly MyDataContext _myDataContext;
        public UsersWindow(MyDataContext myDataContext)
        {
            _myDataContext = myDataContext;
            InitializeComponent();
            //InitDataGrid();
        }
        private async Task InitDataGrid(IQueryable<UserEntity> query)
        {
            var cultureInfo = new CultureInfo("uk-UA");
            int threadId = Thread.CurrentThread.ManagedThreadId;

            var users = await query
                .OrderBy(x => x.Id)
                .Select(x=>new UserVM
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                DateCreated = x.DateCreated!=null ? 
                    x.DateCreated.Value.ToString("dd MMMM yyyy HH:mm:ss", cultureInfo) :""
            }).ToListAsync();

            threadId = Thread.CurrentThread.ManagedThreadId;

            dgUsers.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new UserVM
            {
                //Id = 1,
                Name="Матрос",
                Phone="3883 s8d8d8 asdljf 883883"
            });
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if(dgUsers.SelectedItem != null)
                if(dgUsers.SelectedItem is UserVM)
                {
                    var userVM = (UserVM)dgUsers.SelectedItem;
                    var user = _myDataContext.Users.SingleOrDefault(x => x.Id == userVM.Id);
                    if(user != null)
                    {
                        user.Name = "Оновили користувача :)";
                        _myDataContext.Users.Update(user);
                        _myDataContext.SaveChanges();
                        userVM.Name = user.Name;
                    }
                    //userVM.Name = "Оновили користувача :)";
                }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query = _myDataContext.Users.AsQueryable();
            SearchUser search = new SearchUser();
            search.Name = txtName.Text;
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }
            InitDataGrid(query);

        }
    }
}
