﻿using LibDatabase;
using LibDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WpfAppSimple
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private ObservableCollection<UserVM> users = new ObservableCollection<UserVM>();
        private readonly MyDataContext _myDataContext;
        int? page;
        const int pageSize = 10;
        int totalCount = 0;
        int totalPages = 0;
        public UsersWindow(MyDataContext myDataContext)
        {
            _myDataContext = myDataContext;
            InitializeComponent();
            //InitDataGrid();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //MessageBox.Show("ContentRendered");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Loaded");
        }

        private async Task InitDataGrid(IQueryable<UserEntity> query)
        {
            var cultureInfo = new CultureInfo("uk-UA");
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            totalCount=query.Count();
            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            int skip = (page ?? 0) * pageSize;
            var users = await query
                .OrderBy(x => x.Id)
                .Select(x => new UserVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    DateCreated = x.DateCreated != null ?
                    x.DateCreated.Value.ToString("dd MMMM yyyy HH:mm:ss", cultureInfo) : "",
                    Image = x.Image ?? "noimage.png"
                })
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            //MessageBox.Show("RunTime " + elapsedTime);

            labelTime.Content = "RunTime " + elapsedTime;
            labelInfo.Content = $"{skip}-{skip+pageSize}/{totalCount}";


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
            var query = ReadDataSearch();
            InitDataGrid(query);

        }
        private IQueryable<UserEntity> ReadDataSearch()
        {
            var query = _myDataContext.Users.AsQueryable();
            SearchUser search = new SearchUser();
            search.Name = txtName.Text;
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }
            if (cbIsImage.IsChecked == true)
                query = query.Where(x => x.Image != null);
            return query;
        }

        private void btnPev_Click(object sender, RoutedEventArgs e)
        {
            int p = (page ?? 0);
            if (p == 0)
                return;
            page = --p;

            var query = ReadDataSearch();
            InitDataGrid(query);

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int p = (page ?? 0);
            if (p >= totalPages)
                return;
            page=++p;

            var query = ReadDataSearch();
            InitDataGrid(query);
        }
    }
}
