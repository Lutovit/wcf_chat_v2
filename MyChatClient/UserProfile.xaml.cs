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


namespace MyChatClient
{
    public enum sex 
    {
        Male,
        Female   
    }

    public class Sex 
    {
        public sex _sex { set; get; }

        public Sex() { }

        public Sex(sex s) 
        {
            _sex = s;        
        }

        public override string ToString() 
        {
            return _sex.ToString();
        }
    }
    
    

    /// <summary>
    /// Логика взаимодействия для UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {

        List<int> ageList;
        List<Sex> sexList = new List<Sex>
        {
            new Sex(sex.Male),
            new Sex(sex.Female)
        };

        public event EventHandler<AccauntArgs> onAccauntRegistration;
        public event EventHandler<AccauntArgs> onAccauntDelete;


        private List<int> ageListInit() 
        {
            List<int> l = new List<int>();
            for (int i = 1; i <= 120; i++) 
            {
                l.Add(i);          
            }

            return l;      
        }


        public UserProfile()
        {
            InitializeComponent();
            ageList = ageListInit();

            comboBoxAge.ItemsSource = ageList;
            comboBoxSex.ItemsSource = sexList;
        }


        private void exiteButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            OnAccauntRegistration();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            OnAccauntDelete();
        }



        private void OnAccauntRegistration() 
        {
            if (textBoxLogin.Text.Length >0 && textBoxPass.Text.Length > 0 && textBoxName.Text.Length > 0 && comboBoxAge.SelectedItem != null && comboBoxSex.SelectedItem != null)
            {
                AccauntArgs accarg = new AccauntArgs
                {
                    _login = textBoxLogin.Text,
                    _password = textBoxPass.Text,
                    _name = textBoxName.Text,
                    _age = (Int32)comboBoxAge.SelectedItem,
                    _sex = (Sex)comboBoxSex.SelectedItem
                    
                };
                onAccauntRegistration?.Invoke(this, accarg);
                Close();                
            }
            else 
            {
                MessageBox.Show("Your data is not correct!" );            
            }

        }


        private void OnAccauntDelete()
        {
            if (textBoxLogin.Text.Length > 0 && textBoxPass.Text.Length > 0 && textBoxName.Text.Length == 0 && comboBoxAge.SelectedItem == null && comboBoxSex.SelectedItem == null)
            {
                AccauntArgs accarg = new AccauntArgs(textBoxLogin.Text, textBoxPass.Text);
;
                onAccauntDelete?.Invoke(this, accarg);
                Close();
            }
            else
            {
                MessageBox.Show("Your data is not correct!");
            }
        }


    }
}
