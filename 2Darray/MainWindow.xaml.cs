using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WikiApplication;

namespace _2Darray
{

    public partial class MainWindow : Window
    {
        List<Information> Wikidata = new List<Information>(); 
        int index; 

        public MainWindow()
        {
            InitializeComponent();
            LoadComboBox();
        }

        // checks if radio btns are valid
        #region
        private bool ValidRadio()
        {
            return rdoLinear.IsChecked == true || rdoNonLinear.IsChecked == true;
        }
        #endregion

        // Add Function
        #region
        private void Add()
        {
            Information newInformation = new Information();

            newInformation.isLinear = rdoLinear.IsChecked == true;
            newInformation.isLinear = rdoNonLinear.IsChecked == true;

            if (ValidInputs())
            {
                newInformation.category = ComboCategory.SelectedItem?.ToString();
                newInformation.name = txtName.Text;
                newInformation.definition = txtDefiniton.Text;
                Wikidata.Add(newInformation);
            }
            else
            {
                MessageBox.Show("You are missing some information. Please fill out all fields");
            }
        }
        #endregion

        // ComboBox on Form Load Function 
        #region
        private void LoadComboBox()
        {
            try
            {
                string filePath = "Options.txt";

                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine("Array");
                        writer.WriteLine("List");
                        writer.WriteLine("Tree");
                        writer.WriteLine("Graph");
                        writer.WriteLine("Abstract");
                        writer.WriteLine("Hash");
                    }
                }

                string[] options = File.ReadAllLines(filePath);
                foreach (string option in options)
                {
                    ComboCategory.Items.Add(option);
                }

                ComboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception Caught: {ex.Message}");
                Close();
            }
        }
        #endregion

        // Duplicate chgecking
        #region
        private bool NameDuplicates()
        {
            if (Wikidata.Exists(e => e.name.Equals(txtName.Text)))
            {
                Display();
                Clear();
                return true;
            }
            return false; 
        }
        #endregion

        // Selected Radio Button
        #region
        private string SelectedRadioButton()
        {
            if (rdoLinear.IsChecked == true)
            {
                return "Linear Structure";
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                return "Non-Linear Structure";
            }
            else
            {
                return "";
            }
        }
        #endregion

        // Selected RadioBUtton Index
        #region
        private int SelectedRadioButtonIndex()
        {
            if (rdoLinear.IsChecked == true)
            {
                index = 0;
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                index = 1;
            }
            else
            {
                index = -1;
            }
            return index;
        }
        #endregion

        // Call Radio Buttons
        #region
        private void CheckRadioButton()
        {
            SelectedRadioButton();
            SelectedRadioButtonIndex();
        }
        #endregion

        // Delete Function
        #region
        private void DeleteItem()
        {
            if (lstView.SelectedItem != null)
            {
                MessageBoxResult userResult = MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
                if (userResult == MessageBoxResult.Yes)
                {
                    var selectedItem = lstView.SelectedItem;
                    lstView.Items.Remove(selectedItem);
                    Wikidata.Remove((Information)selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an entry to delete");
            }
        }

        #endregion

        // Edit Function
        #region
        private void EditItem()
        {
            int selectedIndex = lstView.SelectedIndex;

            if (selectedIndex != -1)
            {
                Information dataObject = (Information)lstView.SelectedItem;

                dataObject.name = txtName.Text;
                dataObject.category = ComboCategory.SelectedItem.ToString();
                dataObject.definition = txtDefiniton.Text;
                dataObject.isLinear = rdoLinear.IsChecked == true;
                dataObject.isLinear = rdoNonLinear.IsChecked == true;
            }
            else
            {
                Clear();
            }
        }
        #endregion

        // Sort/Display Function
        #region
        private void Display()
        {
            lstView.Items.Clear();
            Wikidata.Sort();
            foreach (var item in Wikidata)
            {
                lstView.Items.Add(item);
            }
        }
        #endregion

        // Binary Search Function
        #region
        private void BinarySearch()
        {
            string searchInput = txtSearch.Text;
            int index = Wikidata.FindIndex(e => e.name.Equals(searchInput));

            if (index >= 0)
            {
                lstView.SelectedIndex = -1;
                Information foundEntry = Wikidata[index];
                txtName.Text = foundEntry.name;
                ComboCategory.SelectedItem = foundEntry.category;
                txtDefiniton.Text = foundEntry.definition;
                rdoLinear.IsChecked = foundEntry.isLinear;
                rdoNonLinear.IsChecked = !foundEntry.isLinear;
            }
            else
            {
                lstView.SelectedIndex = -1;
                MessageBox.Show("The entry you are searching for doesn't exist");
                Clear();
            }
        }
        #endregion

        // Clear Function
        #region
        private void Clear()
        {
            txtName.Clear();
            txtDefiniton.Clear();
            txtSearch.Clear();
            ComboCategory.SelectedItem = null;
            rdoLinear.IsChecked = false;
            rdoNonLinear.IsChecked = false;
        }
        #endregion

        // Save Function
        #region
        private void SaveFile(List<Information> list)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                FileName = "wikiList.dat",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (dialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(dialog.FileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, list);
                }
            }
        }
        #endregion

        // Load Function
        #region
        private List<Information> LoadFile()
        {
            List<Information> list = new List<Information>();

            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Data File|*.dat"
            };

            if (dialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.OpenRead(dialog.FileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    list = (List<Information>)formatter.Deserialize(fileStream);
                }

                lstView.Items.Clear();
                Wikidata.Clear();
                foreach (Information info in list)
                {
                    lstView.Items.Add(info);
                    Wikidata.Add(info);
                }
            }

            return list;
        }

        #endregion

        // Check if all input boxes are valid or not
        #region
        private bool ValidInputs()
        {
            return !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtDefiniton.Text) &&
                   ComboCategory.SelectedIndex >= 0 &&
                   ValidRadio();
        }
        #endregion

        // Add Button
        #region
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameDuplicates())
            {
                Add();
                Display();
                Clear();
            }
            else
            {
                MessageBox.Show("Found a duplicate");
            }
        }
        #endregion

        // Selected Entry
        #region
        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Information selectedItem = (Information)lstView.SelectedItem;
            if (selectedItem == null)
                return;

            txtName.Text = selectedItem.name;
            txtDefiniton.Text = selectedItem.definition;
            ComboCategory.SelectedItem = selectedItem.category;
            rdoLinear.IsChecked = selectedItem.isLinear;
            rdoNonLinear.IsChecked = !selectedItem.isLinear;
        }
        #endregion

        // Delete Button
        #region
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem();
            Clear();
            Display();
        }
        #endregion

        // Edit Button
        #region
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameDuplicates() && ValidInputs())
            {
                EditItem();
                Clear();
                Display();
            }
            else
            {
                MessageBox.Show("Unable to edit please try again");
                Clear();
                Display();
            }
        }
        #endregion

        // Search Button
        #region
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            BinarySearch();
            txtSearch.Clear();
        }
        #endregion

        // Text Box Name double click
        #region
        private void txtName_doubleClick(object sender, MouseButtonEventArgs e)
        {
            lstView.SelectedIndex = -1;
            Clear();
        }
        #endregion

        //Save Click
        #region
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(Wikidata);
        }
        #endregion

        //Load Click
        #region
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            List<Information> loadedList = LoadFile();
        }
        #endregion

        //Closing Form
        #region
        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to SAVE before exitting the program?", "Exitting Program", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                SaveFile(Wikidata);
            }
        }
        #endregion

        //Unused
        #region
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDefiniton_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rdoNonLinear_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoLinear_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }

}
