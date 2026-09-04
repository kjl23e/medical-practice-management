using System;
using Microsoft.Maui.Controls;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2
{
    public partial class AddPatientPage : ContentPage
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.Name = NameEntry.Text;
            p.Address = AddressEntry.Text;
            p.Birthdate = BirthdateEntry.Text;
            p.Race = RaceEntry.Text;
            p.Gender = GenderEntry.Text;

            DataManager.Patients = DataManager.GrowArray(DataManager.Patients, p);

            MessageLabel.Text = "Patient added successfully!";
            MessageLabel.TextColor = Colors.Green;

            // Clear fields
            NameEntry.Text = "";
            AddressEntry.Text = "";
            BirthdateEntry.Text = "";
            RaceEntry.Text = "";
            GenderEntry.Text = "";
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}