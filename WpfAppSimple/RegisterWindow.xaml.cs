using LibDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region ComboBox
            MyComboBoxItem nodata = new MyComboBoxItem
            {
                Id = -1,
                Name = "Не вказано"
            };
            cbGender.Items.Add(nodata);

            MyComboBoxItem male = new MyComboBoxItem
            {
                Id = (int)Gender.Male,
                Name = "Чоловік"
            };
            cbGender.Items.Add(male);

            MyComboBoxItem female = new MyComboBoxItem
            {
                Id = (int)Gender.Female,
                Name = "Жінка"
            };
            cbGender.Items.Add(female);

            cbGender.SelectedIndex = 0;
            #endregion

            #region ListBox

            string[] profession = { "Менеджер", "Програміст", "Двірник", "Артилерист" };
            int i = 1;
            foreach (var item in profession)
            {
                MyComboBoxItem data = new MyComboBoxItem
                {
                    Id = i++,
                    Name = item
                };
                lbItems.Items.Add(data);
            }

            #endregion

            #region RadioButton 
            string[] products = { "Капуста", "Морква", "Горох" };
            foreach (var item in products)
            {
                RadioButton rb = new RadioButton() { Content = item };
                sp.Children.Add(rb);
            }
            //rbPosition.
            #endregion
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var index = cbGender.SelectedIndex;
            var item = (MyComboBoxItem)cbGender.Items[index];
            if(item.Id == -1)
            {
                MessageBox.Show("Вкажіть стать");
                return;
            }
            Gender gender = (Gender)item.Id;
            MessageBox.Show(gender.ToString());
            //if (lbItems.SelectedItem != null)
            //{
            //    var lb = (MyComboBoxItem)lbItems.SelectedItem;
            //    MessageBox.Show(lb.Name);
            //}

            if (lbItems.SelectedItems.Count>0)
            {
                string str = "";
                foreach (object data in lbItems.SelectedItems)
                {
                    var lb = (MyComboBoxItem)data;
                    str += $"{lb.Name}\t";
                }
                MessageBox.Show(str);
            }
        }
    }
}
