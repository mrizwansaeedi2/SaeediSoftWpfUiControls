using SaeediSoftWpfUiControls.Data;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SaeediSoftWpfUiControls
{
    /// <summary>
    /// Interaction logic for ComboBoxAutoComplete.xaml
    /// </summary>
    public class SearchModel
    {
        public Guid ValueFieldGuidId { get; set; }
        public Guid ValueFieldInt { get; set; }
        public string DisplayField { get; set; }
        public string StringFeild1 { get; set; }
        public string StringFeild2 { get; set; }
        public int? intField1 { get; set; }
        public int? intField2 { get; set; }
    }

    /// <summary>
    /// Interaction logic for ComboBoxAutoComplete.xaml
    /// </summary>
    public partial class ComboBoxAutoComplete : UserControl
    {
        public ComboBox Ctrl;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<SearchModel>), typeof(ComboBoxAutoComplete), new PropertyMetadata(null));

        public ObservableCollection<SearchModel> ItemsSource
        {
            get { return (ObservableCollection<SearchModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
           DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(ComboBoxAutoComplete), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SelectedItemProperty =
           DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboBoxAutoComplete), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(ComboBoxAutoComplete), new PropertyMetadata(string.Empty));

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty FilterModeProperty =
             DependencyProperty.Register("FilterMode", typeof(AutoCompleteFilterMode), typeof(ComboBoxAutoComplete), new PropertyMetadata(AutoCompleteFilterMode.None));

        public AutoCompleteFilterMode FilterMode
        {
            get { return (AutoCompleteFilterMode)GetValue(FilterModeProperty); }
            set { SetValue(FilterModeProperty, value); }
        }

        // ----------------------------------
        public static readonly DependencyProperty FontSizeProperty =
                DependencyProperty.Register("FontSize", typeof(double), typeof(ComboBoxAutoComplete), new PropertyMetadata(12.0));

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // ----------------------------------

        // ----------------------------------
        public static readonly DependencyProperty FontWeightProperty =
                DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ComboBoxAutoComplete), new PropertyMetadata(FontWeights.Normal));

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        // ------------00----------------------
        public static readonly DependencyProperty ShowBorderProperty =
    DependencyProperty.Register("ShowBorder", typeof(bool), typeof(ComboBoxAutoComplete), new PropertyMetadata(false, OnShowBorderChanged));

        public bool ShowBorder
        {
            get { return (bool)GetValue(ShowBorderProperty); }
            set { SetValue(ShowBorderProperty, value); }
        }

        // ------------00----------------------

        private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ComboBoxAutoComplete;
            control?.UpdateBorder();
        }

        private void UpdateBorder()
        {
            if (ShowBorder)
            {
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.BorderThickness = new Thickness(1);
            }
            else
            {
                border.BorderBrush = System.Windows.Media.Brushes.Transparent;
                border.BorderThickness = new Thickness(0);
            }
        }


        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
    "SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ComboBoxAutoComplete));

        public static readonly DependencyProperty SelectionChangedProperty =
    DependencyProperty.Register("SelectionChanged", typeof(RoutedEventHandler), typeof(ComboBoxAutoComplete));

        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

   

        private void cmbAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //RoutedEventArgs newEventArgs = new RoutedEventArgs(ComboBoxAutoComplete.SelectionChangedEvent);
            // RaiseEvent(newEventArgs);
            SelectedItem = cmbAutoComplete.SelectedItem;
            // Raise the SelectionChanged dependency property event
            //RoutedEventHandler handler = (RoutedEventHandler)GetValue(SelectionChangedProperty);
            //if (handler != null)
            //{
            //    handler.Invoke(this, e);
            //}

            // You can also handle the selection change event here as before.
            // ...
        }

        //public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public ComboBoxAutoComplete()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void cbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(cmbAutoComplete.Items);
            if (e == null)
            {
                view.Filter = null;
                return;
            }

            string searchText = cmbAutoComplete.Text + e.Text;

            if (!string.IsNullOrEmpty(searchText))
            {
                view.Filter = item =>
                {
                    if (item is SearchModel lineItem)
                    {
                        cmbAutoComplete.SelectedIndex = -1;
                        cmbAutoComplete.IsDropDownOpen = true;
                        if (lineItem.intField1.HasValue && lineItem.intField1.Value.ToString().Equals(searchText))
                        {
                            return true;
                        }
                        else if (lineItem.intField2.HasValue && lineItem.intField2.Value.ToString().Equals(searchText))
                        {
                            return true;
                        }
                        else
                        {
                            if (FilterMode == AutoCompleteFilterMode.Equals)
                            {
                                return (lineItem.StringFeild1 != null && lineItem.StringFeild1.Equals(searchText)) ||
                                   (lineItem.StringFeild2 != null && lineItem.StringFeild2.Equals(searchText));
                            }
                            else if (FilterMode == AutoCompleteFilterMode.Contains)
                            {
                                return (lineItem.StringFeild1 != null && lineItem.StringFeild1.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                   (lineItem.StringFeild2 != null && lineItem.StringFeild2.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
                            }
                            else if (FilterMode == AutoCompleteFilterMode.StartsWith)
                            {
                                return (lineItem.StringFeild1 != null && lineItem.StringFeild1.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)) ||
                                   (lineItem.StringFeild2 != null && lineItem.StringFeild2.StartsWith(searchText, StringComparison.OrdinalIgnoreCase));
                            }

                            return (lineItem.StringFeild1 != null && lineItem.StringFeild1.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                   (lineItem.StringFeild2 != null && lineItem.StringFeild2.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
                        }
                    }
                    return false;
                };
            }
            else
            {
                view.Filter = null;
                cmbAutoComplete.IsDropDownOpen = false;
            }
        }

        private async void cbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //AddToCart();
                e.Handled = true;
                return;
            }
            else if (e.Key == Key.Back)
            {
                if (string.IsNullOrEmpty(cmbAutoComplete.Text))
                {
                    cmbAutoComplete.SelectedItem = null;
                    cmbAutoComplete.IsDropDownOpen = false;
                    return;
                }
            }
            else if (e.Key == Key.Escape)
            {
                cmbAutoComplete.IsDropDownOpen = false;
                cmbAutoComplete.SelectedItem = null;
                return;
            }

            await Task.Factory.StartNew(() => { Thread.Sleep(300); });
            if (string.IsNullOrEmpty(cmbAutoComplete.Text))
                cbox_PreviewTextInput(null, null);

            //if (string.IsNullOrEmpty(cmbAutoComplete.Text))
            //{
            //    cmbAutoComplete.SelectedItem = null;
            //    cmbAutoComplete.IsDropDownOpen = false;
            //}
        }

        private void cmbAutoComplete_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbAutoComplete.Text))
            {
            }
            if (e.Key == Key.Back)
            {
                if (string.IsNullOrEmpty(cmbAutoComplete.Text))
                {
                    cmbAutoComplete.SelectedItem = null;
                    cmbAutoComplete.IsDropDownOpen = false;
                    return;
                }
            }
        }

        private Storyboard fadeInStoryboard;

        private void cmbAutoComplete_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (cmbAutoComplete.IsDropDownOpen)
            {
                // Start the fade-in animation when the dropdown opens
                fadeInStoryboard?.Begin(cmbAutoComplete);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fadeInStoryboard = (Storyboard)FindResource("FadeInAnimation");
            Ctrl = cmbAutoComplete;
            UpdateBorder();  // Ensure the border is updated when the control is loaded
        }

        private void cmbAutoComplete_Loaded(object sender, RoutedEventArgs e)
        {
            Ctrl = cmbAutoComplete;
        }

        private void cmbAutoComplete_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ShowBorder)
            {
                border.BorderBrush = System.Windows.Media.Brushes.Yellow;
                border.BorderThickness = new Thickness(1);
            }
        }

        private void cmbAutoComplete_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ShowBorder)
            {
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.BorderThickness = new Thickness(.5);
            }
        }
    }
}