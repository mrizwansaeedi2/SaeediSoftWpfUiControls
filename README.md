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
