using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace _2Darray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public class ListEntry : IComparable<ListEntry>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Structure { get; set; }
        public string Definition { get; set; }
        public int CompareTo(ListEntry other)
        {
            //compare by Name
            return this.Name.CompareTo(other.Name);
        }

    }

    public partial class MainWindow : Window
    {

        List<List<ListEntry>> multiArray = new List<List<ListEntry>>();

        public MainWindow()
        {
            InitializeComponent();
            //remove following comment BEFORE LAUNCH to pre fill array for testing
            TestArray();
            Refresh();
        }

        //Pre fill array for testing
        #region
                private void TestArray()
                {

                }
        #endregion

        //Function for Add button EMPTY
        #region
        private void AddFun()
        {
           
        }
        #endregion

        //Function for Edit button EMPTY
        #region
        private void EditFun()
        {

            StatusBar("Item Edited");
        }
        #endregion

        //Function for Delete button EMPTY
        #region
        private void DeleteFun()
        {

        }
        #endregion

        //Function for Status Bar
        #region
        private void StatusBar(string msg)
        {
            statusBar.Items.Clear();
            statusBar.Items.Add(msg);
        }
        #endregion

        //Function for Clear textbox
        #region
        private void ClearFun()
        {
            txtName.Clear();
            txtCategory.Clear();
            txtStructure.Clear();
            txtDefiniton.Clear();
        }

        #endregion

        //Function for Display Data EMPTY
        #region
        private void DisplayFun(int index)
        {


        }
        #endregion

        //Function for Show Info EMPTY
        #region
        private void DisplayOutputFun()
        {

        }
        #endregion

        //Refresh listbox EMPTY
        #region
        private void Refresh()
        {
            DisplayOutputFun();
            ClearFun();
        }
        #endregion

        //Button functions EMPTY
        #region
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFun();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFun();
            Refresh();
        }

        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            SortFun();
            Refresh();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            BinSearchFun();
            txtSearch.Clear();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddFun();
            Refresh();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditFun();
            Refresh();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFun();
            Refresh();
        }
        #endregion

        //Function for Sort Button EMPTY
        #region
        private void SortFun()
        {

        }

        #endregion

        //Function for Swapping for bubblesort EMPTY
        #region
        private void Swap(int indx)
        {

        }
        #endregion

        //Function for Save Button EMPTY
        #region
        private void SaveFun()
        {

        }
        #endregion

        //Function for Load Button EMPTY
        #region
        private void LoadFun()
        {

        }
        #endregion

        //Function for Bin Search EMPTY
        #region
        private void BinSearchFun()
        {
            
        }
        #endregion

        //Text boxes changed
        #region
        private void txtStructure_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCategory_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        //Displays selected item in ListView in the text boxes
        #region
        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayFun(lstView.SelectedIndex);
        }
        #endregion

        private void txtDefiniton_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
