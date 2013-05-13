using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace CalorieCounter
{
    /// <summary>
    /// This class models the behavior of the calorie calculator application
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        int totalCalories = 0; 
        int scratchPadCalories = 0;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the generic function which computes the calories for
        /// each predefined button pressed.
        /// </summary>
        /// <param name="object sender"></param> the button which calls the function
        /// <param name="RoutedEventArgs e"></param> 
        private void CalorieButtonEventHandler(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            scratchPadCalories += Convert.ToInt32(clickedButton.Content);
            scratchPadTextBox.Text = scratchPadCalories.ToString();
        }

        /// <summary>
        /// This function adds the scratchpad calorie value to the total calorie value
        /// </summary>
        /// <param name="object sender"></param> the button which calls the function
        /// <param name="RoutedEventArgs e"></param>
        private void AddButtonEventHandler(object sender, RoutedEventArgs e)
        {
            scratchPadCalories = Convert.ToInt32(scratchPadTextBox.Text);
            totalCalories += scratchPadCalories;
            scratchPadTextBox.Text = "0";
            scratchPadCalories = 0;
            totalCaloriesTextBox.Text = totalCalories.ToString();
        }

        /// <summary>
        /// This button clears the scratchpad calorie value to 0.
        /// </summary>
        /// <param name="object sender"></param> the button which calls the function
        /// <param name="RoutedEventArgs e"></param>
        private void ClearButtonEventHandler(object sender, RoutedEventArgs e)
        {
            scratchPadTextBox.Text = "0";
            scratchPadCalories = 0;
        }
    }
}