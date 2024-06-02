//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Input;

//namespace AuoCompleteComboBoxV2
//{
//    public class SearchModel
//    {
//        public string DisplayField { get; set; }
//        public string StringFeild1 { get; set; }
//        public string StringFeild2 { get; set; }
//        public int? intField1 { get; set; }
//        public int? intField2 { get; set; }
//    }

//    public partial class AutoCompleteComboBox : UserControl
//    {
//        public static readonly DependencyProperty ItemsSourceProperty =
//            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<SearchModel>), typeof(AutoCompleteComboBox), new PropertyMetadata(null));

//        public ObservableCollection<SearchModel> ItemsSource
//        {
//            get { return (ObservableCollection<SearchModel>)GetValue(ItemsSourceProperty); }
//            set { SetValue(ItemsSourceProperty, value); }
//        }

//        public static readonly DependencyProperty DisplayMemberPathProperty =
//           DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(AutoCompleteComboBox), new PropertyMetadata(string.Empty));

//        public string DisplayMemberPath
//        {
//            get { return (string)GetValue(DisplayMemberPathProperty); }
//            set { SetValue(DisplayMemberPathProperty, value); }
//        }

//        public static readonly DependencyProperty SelectedValuePathProperty =
//            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(AutoCompleteComboBox), new PropertyMetadata(string.Empty));

//        public string SelectedValuePath
//        {
//            get { return (string)GetValue(SelectedValuePathProperty); }
//            set { SetValue(SelectedValuePathProperty, value); }
//        }

//        public static readonly DependencyProperty DelayProperty =
//            DependencyProperty.Register("Delay", typeof(int), typeof(AutoCompleteComboBox), new PropertyMetadata(500));

//        public int Delay
//        {
//            get { return (int)GetValue(DelayProperty); }
//            set { SetValue(DelayProperty, value); }
//        }

//        private Timer timer;

//        public AutoCompleteComboBox()
//        {
//            InitializeComponent();
//            DataContext = this;
//        }

//        private void cmbAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            // You can handle the selection change event here or expose a public event from the user control.
//        }

//        private void cbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        {
//            ICollectionView view = CollectionViewSource.GetDefaultView(cmbAutoComplete.Items);
//            if (e == null)
//            {
//                view.Filter = null;
//                return;
//            }

//            if (e.Text == string.Empty)
//            {
//                // If the text is cleared, reset the Timer and close the dropdown.
//                if (timer != null)
//                {
//                    timer.Dispose();
//                }
//                cmbAutoComplete.IsDropDownOpen = false;
//                return;
//            }

//            string searchText = cmbAutoComplete.Text + e.Text;

//            if (!string.IsNullOrEmpty(searchText))
//            {
//                // Stop the existing timer (if any) and start a new one
//                if (timer != null)
//                {
//                    timer.Dispose();
//                }
//                timer = new Timer(FilterComboBox, searchText, Delay, Timeout.Infinite);
//            }
//            else
//            {
//                view.Filter = null;
//                cmbAutoComplete.IsDropDownOpen = false;
//            }
//        }

//        private void FilterComboBox(object state)
//        {
//            string searchText = (string)state;

//            // Dispatch the filter operation to the UI thread
//            Dispatcher.Invoke(() =>
//            {
//                ICollectionView view = CollectionViewSource.GetDefaultView(cmbAutoComplete.Items);
//                view.Filter = item =>
//                {
//                    if (item is SearchModel lineItem)
//                    {
//                        cmbAutoComplete.SelectedIndex = -1;
//                        cmbAutoComplete.IsDropDownOpen = true;
//                        if (lineItem.intField1.HasValue && lineItem.intField1.Value.ToString().Equals(searchText))
//                        {
//                            return true;
//                        }
//                        else if (lineItem.intField2.HasValue && lineItem.intField2.Value.ToString().Equals(searchText))
//                        {
//                            return true;
//                        }
//                        else
//                        {
//                            return (lineItem.StringFeild1 != null && lineItem.StringFeild1.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
//                                   (lineItem.StringFeild2 != null && lineItem.StringFeild2.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
//                        }
//                    }
//                    return false;
//                };
//            });
//        }

//        private async void cbox_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                //AddToCart();
//                e.Handled = true;
//                return;
//            }
//            else if (e.Key == Key.Back)
//            {
//                if (string.IsNullOrEmpty(cmbAutoComplete.Text))
//                {
//                    cmbAutoComplete.SelectedItem = null;
//                    cmbAutoComplete.IsDropDownOpen = false;
//                    return;
//                }
//            }
//            else if (e.Key == Key.Escape)
//            {
//                cmbAutoComplete.SelectedItem = null;
//                cmbAutoComplete.IsDropDownOpen = false;
//                return;
//            }

//            await Task.Factory.StartNew(() => { Thread.Sleep(300); });
//            if (string.IsNullOrEmpty(cmbAutoComplete.Text))
//                cbox_PreviewTextInput(null, null);
//        }

//        private void cmbAutoComplete_KeyUp(object sender, KeyEventArgs e)
//        {
//            if (string.IsNullOrEmpty(cmbAuto