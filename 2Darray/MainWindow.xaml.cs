using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace _2Darray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // initialize the stuff
        static int rows = 12;
        static int columns = 4;
        int pointer = 0;
        string[,] multiArray = new string[rows, columns];


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
                    multiArray[0, 0] = "Name A";
                    multiArray[0, 1] = "Category B";
                    multiArray[0, 2] = "Structure A";
                    multiArray[0, 3] = "Definition A";

                    multiArray[1, 0] = "Name B";
                    multiArray[1, 1] = "Category B";
                    multiArray[1, 2] = "Structure B";
                    multiArray[1, 3] = "Definition B";

                    multiArray[2, 0] = "Carrot C";
                    multiArray[2, 1] = "Category C";
                    multiArray[2, 2] = "Structure C";
                    multiArray[2, 3] = "Definition C";

                    multiArray[3, 0] = "Name C";
                    multiArray[3, 1] = "CategoryC";
                    multiArray[3, 2] = "Structure D";
                    multiArray[3, 3] = "Definition D";

                    pointer = 4;
                }
        #endregion

        //Function for Add button COMPLETE
        #region
        private void AddFun()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtName.Text) && (!string.IsNullOrWhiteSpace(txtCategory.Text)))
                {
                    if (pointer < rows)
                    {
                        multiArray[pointer, 0] = txtName.Text;
                        multiArray[pointer, 1] = txtCategory.Text;
                        multiArray[pointer, 2] = txtStructure.Text;
                        multiArray[pointer, 3] = txtDefiniton.Text;
                        pointer++;
                    }
                    else
                    {
                        MessageBox.Show("Array is full");
                    }
                    StatusBar("Item added");
                }
                else
                {
                    MessageBox.Show("Name and Catagory can not be null");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on read: " + ex.Message);
            }
        }
        #endregion
        
        //Function for Edit button COMPLETE
        #region
        private void EditFun()
        {
            int index = lstView.SelectedIndex;
            if (index >= 0)
            {
                multiArray[index, 0] = txtName.Text;
                multiArray[index, 1] = txtCategory.Text;
                multiArray[index, 2] = txtStructure.Text;
                multiArray[index, 3] = txtDefiniton.Text;
                lstView.Items.Clear();
                DisplayOutputFun();
            }
            StatusBar("Item Edited");
        }
        #endregion

        //Function for Delete button COMPLETE
        #region
        private void DeleteFun()
        {
            int index = lstView.SelectedIndex;
            if (index >= 0)
            {
                multiArray[index, 0] = null;
                multiArray[index, 1] = null;
                multiArray[index, 2] = null;
                multiArray[index, 3] = null;

                pointer--;
                SortFun();
                Refresh();
                StatusBar("Item deleted");
            }
            else
            {
                MessageBox.Show("No item is selected");
            }
        }
        #endregion

        //Function for Status Bar COMPLETE
        #region
        private void StatusBar(string msg)
        {
            statusBar.Items.Clear();
            statusBar.Items.Add(msg);
        }
        #endregion

        //Function for Clear textbox COMPLETE
        #region
        private void ClearFun()
        {
            txtName.Clear();
            txtCategory.Clear();
            txtStructure.Clear();
            txtDefiniton.Clear();
        }

        #endregion

        //Function for Display Data COMPLETE
        #region
        private void DisplayFun(int index)
        {
            if (index >= 0)
            {
                txtName.Text = multiArray[index, 0];
                txtCategory.Text = multiArray[index, 1];
                txtStructure.Text = multiArray[index, 2];
                txtDefiniton.Text = multiArray[index, 3];
            }

        }
        #endregion

        //Function for Show Info COMPLETE
        #region
        private void DisplayOutputFun()
        {
            try
            {
                lstView.Items.Clear();
                for (int x = 0; x < rows; x++)
                {
                    if (multiArray[x, 0] != null)
                    {
                        lstView.Items.Add("Name: " + multiArray[x, 0] + ", " + "Category: " + multiArray[x, 1]);
                    }
                }
            }
            catch (Exception ex) //this is the try catch triggering
            {
                MessageBox.Show("Error on read: " + ex.Message);
            }
        }
        #endregion

        //Refresh listbox COMPLETE
        #region
        private void Refresh()
        {
            DisplayOutputFun();
            ClearFun();
        }
        #endregion

        //Button functions COMPLETE
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

        //Function for Sort Button COMPLETE
        #region
        private void SortFun()
        {
            try
            {
                //basic Bubblesort 
                //outer loop
                //from start of array [0] to end of array [array.len]
                for (int x = 1; x < rows; x++)
                {
                    //inner loop
                    //from second of array [1] to second last of array [array.len -1]
                    for (int i = 0; i < rows - 1; i++)
                    {
                        //if null move to back
                        if (string.IsNullOrEmpty(multiArray[i, 0]))
                        {
                            Swap(i);
                        }
                        // else if not null and greater then first then Swap()
                        else if (!string.IsNullOrEmpty(multiArray[i + 1, 0]) &&
                                 string.Compare(multiArray[i, 0], multiArray[i + 1, 0]) == 1)
                        {
                            Swap(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on SortFun(): " + ex.Message);
            }
        }

        #endregion

        //Function for Swapping for bubblesort COMPLETE
        #region
        private void Swap(int indx)
        {
            try
            {
                //check if in bounds
                if (indx >= 0 && indx < rows - 1)
                {
                    string temp;
                    for (int i = 0; i < columns; i++)
                    {
                        //swaps spots in array
                        temp = multiArray[indx, i];
                        multiArray[indx, i] = multiArray[indx + 1, i];
                        multiArray[indx + 1, i] = temp;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on SwapFun(): " + ex.Message);
            }
        }
        #endregion

        //Function for Save Button COMPLETE
        #region
        private void SaveFun()
        {
            //SaveFile Dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*";
            saveFileDialog.Title = "Save binary file";
            saveFileDialog.FileName = "definitions.dat";
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            BinaryWriter bw;
            try
            {
                bw = new BinaryWriter(new FileStream("2D Array.dat", FileMode.Create));
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.Message + "\n Cannot append to file.");
                return;
            }
            try
            {
                for (int i = 0; i < multiArray.GetLength(0); i++)
                {
                    //dont save null values
                    if (multiArray[i, 0] != null)
                    {
                        bw.Write(multiArray[i, 0]);
                        bw.Write(multiArray[i, 1]);
                        bw.Write(multiArray[i, 2]);
                        bw.Write(multiArray[i, 3]);
                    }
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.Message + "\n Cannot write data to file.");
                return;
            }
            MessageBox.Show("Saved successfully");
            StatusBar("File saved successfully");
            bw.Close();
        }
        #endregion

        //Function for Load Button COMPLETE
        #region
        private void LoadFun()
        {
            //LoadFile Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog.Title = "Select binary file";
            openFileDialog.FileName = "definitions.dat";
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            BinaryReader reader;
            try
            {
                reader = new BinaryReader(new FileStream(openFileDialog.FileName, FileMode.Open));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on load: " + ex.Message);
                return;
            }
            lstView.Items.Clear();

            int row = 0;
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                try
                {
                    multiArray[row, 0] = reader.ReadString();
                    multiArray[row, 1] = reader.ReadString();
                    multiArray[row, 2] = reader.ReadString();
                    multiArray[row, 3] = reader.ReadString();

                    lstView.Items.Add("Name: " + multiArray[row, 0] + ", " + "Category: " + multiArray[row, 1]);
                    row++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on read: " + ex.Message);
                    break;
                }
            }
            StatusBar("File Opened");

            //big important
            reader.Close();


        }
        #endregion

        //Function for Bin Search COMPLETE
        #region
        private void BinSearchFun()
        {
            try
            {
                SortFun();
                Refresh();
                string x = txtSearch.Text;
                int upBound = pointer - 1;
                int lowBound = 0;
                int found = -1;

                //loop while until end of array
                while (lowBound <= upBound)
                {
                    //finds the mid point and compares to search index
                    int midpoint = lowBound + (upBound - lowBound) / 2;
                    int result = x.CompareTo(multiArray[midpoint, 0]);

                    //looks through array
                    if (result == 0)
                    {
                        found = midpoint;
                        break;
                    }
                    else if (result > 0)
                    {
                        lowBound = midpoint + 1;
                    }
                    else
                    {
                        upBound = midpoint - 1;
                    }
                    //prints if found or not
                }
                if (found == -1)
                {
                    MessageBox.Show("The value is NOT present");
                }
                else
                {
                    MessageBox.Show($"The value is present at index {found}, Name: {multiArray[found, 0]}, Category: {multiArray[found, 1]}");
                    lstView.SelectedIndex = found;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on read: " + ex.Message);
            }
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
