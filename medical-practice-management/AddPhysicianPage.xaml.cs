using Assignment2.Models;
using Assignment2.Services;
using Microsoft.Maui.Controls;
using System;
using System.Runtime.Serialization;

namespace Assignment2
{
    public partial class AddPhysicianPage : ContentPage
    {
        public AddPhysicianPage()
        {
            InitializeComponent();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            Physician d = new Physician();
            d.Name = NameEntry.Text;
            d.LicenseNumber = LicenseEntry.Text;
            d.GraduationDate = GraduationEntry.Text;
            d.Specialization = SpecializationEntry.Text;

            DataManager.Physicians = DataManager.GrowArray(DataManager.Physicians, d);

            MessageLabel.Text = "Physician added successfully!";
            MessageLabel.TextColor = Colors.Green;

            // Clear fields
            NameEntry.Text = "";
            LicenseEntry.Text = "";
            GraduationEntry.Text = "";
            SpecializationEntry.Text = "";
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}