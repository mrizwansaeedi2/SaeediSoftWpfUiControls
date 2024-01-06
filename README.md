# ComboBoxAutoComplete WPF Control

ComboBoxAutoComplete is a custom WPF UserControl that enhances the functionality of the standard ComboBox by providing auto-complete and filtering features.

## Features
- Auto-complete functionality for easy data entry.
- Customizable filtering options.
- Sleek and user-friendly design.

## Getting Started

### Prerequisites
- .NET Framework
- Visual Studio (or any compatible IDE)

### Installation
1. Clone the repository.
2. Add the `ComboBoxAutoComplete` control to your WPF project.

### Usage
1. Include the `ComboBoxAutoComplete` control in your XAML.
   ```xml
   <local:ComboBoxAutoComplete
      ItemsSource="{Binding YourItemsSource}"
      DisplayMemberPath="YourDisplayMemberPath"
      SelectedValuePath="YourSelectedValuePath"
      FilterMode="YourFilterMode"
      FontSize="YourFontSize"
      FontWeight="YourFontWeight"
      SelectionChanged="YourSelectionChangedHandler"/>


### ItemsSource: 
- Bind it to your collection of data that you want to display in the ComboBoxAutoComplete.

### DisplayMemberPath: 
- Specify the property of each item in the ItemsSource that should be displayed in the ComboBoxAutoComplete.

### SelectedValuePath: 
- Specify the property of each item in the ItemsSource that should be used as the selected value.

### FilterMode: 
- Choose the filtering mode for the auto-complete feature (AutoCompleteFilterMode.None, AutoCompleteFilterMode.Equals, AutoCompleteFilterMode.Contains, AutoCompleteFilterMode.StartsWith).

### FontSize: 
- Set the font size of the ComboBoxAutoComplete.

### FontWeight: 
- Set the font weight of the ComboBoxAutoComplete.

### SelectionChanged: 
- Handle this event to perform actions when the selection in the ComboBoxAutoComplete changes.

- Customize the control by adjusting properties like ItemsSource, DisplayMemberPath, SelectedValuePath, FilterMode, FontSize, FontWeight, and handle the SelectionChanged event.