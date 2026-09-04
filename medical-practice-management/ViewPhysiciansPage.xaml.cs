using System;
using Microsoft.Maui.Controls;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2
{
    public partial class ViewPhysiciansPage : ContentPage
    {
        public ViewPhysiciansPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadPhysicians();
        }

        private void LoadPhysicians()
        {
            PhysiciansContainer.Clear();

            for (int i = 0; i < DataManager.Physicians.Length; i++)
            {
                Physician d = DataManager.Physicians[i];
                int index = i;

                Frame physicianFrame = new Frame
                {
                    BorderColor = Colors.Gray,
                    CornerRadius = 5,
                    Padding = 10,
                    BackgroundColor = Colors.LightBlue
                };

                VerticalStackLayout content = new VerticalStackLayout { Spacing = 5 };

                content.Add(new Label { Text = "Name: " + d.Name, FontAttributes = FontAttributes.Bold });
                content.Add(new Label { Text = "License: " + d.LicenseNumber });
                content.Add(new Label { Text = "Graduation: " + d.GraduationDate });
                content.Add(new Label { Text = "Specialization: " + d.Specialization });

                HorizontalStackLayout buttons = new HorizontalStackLayout { Spacing = 10, Margin = new Thickness(0, 10, 0, 0) };

                Button editBtn = new Button { Text = "Edit", BackgroundColor = Colors.Orange };
                editBtn.Clicked += (s, e) => OnEditClicked(index);

                Button deleteBtn = new Button { Text = "Delete", BackgroundColor = Colors.Red };
                deleteBtn.Clicked += (s, e) => OnDeleteClicked(index);

                buttons.Add(editBtn);
                buttons.Add(deleteBtn);
                content.Add(buttons);

                physicianFrame.Content = content;
                PhysiciansContainer.Add(physicianFrame);
            }
        }

        private async void OnEditClicked(int index)
        {
            Physician d = DataManager.Physicians[index];

            string name = await DisplayPromptAsync("Edit Physician", "Name:", initialValue: d.Name);
            if (string.IsNullOrEmpty(name)) return;

            string license = await DisplayPromptAsync("Edit Physician", "License Number:", initialValue: d.LicenseNumber);
            string graduation = await DisplayPromptAsync("Edit Physician", "Graduation Date:", initialValue: d.GraduationDate);
            string specialization = await DisplayPromptAsync("Edit Physician", "Specialization:", initialValue: d.Specialization);

            d.Name = name;
            d.LicenseNumber = license ?? d.LicenseNumber;
            d.GraduationDate = graduation ?? d.GraduationDate;
            d.Specialization = specialization ?? d.Specialization;

            LoadPhysicians();
        }

        private async void OnDeleteClicked(int index)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Delete this physician?", "Yes", "No");
            if (confirm)
            {
                DataManager.Physicians = DataManager.RemoveFromArray(DataManager.Physicians, index);
                LoadPhysicians();
            }
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}