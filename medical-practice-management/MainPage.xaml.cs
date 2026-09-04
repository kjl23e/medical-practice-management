using System;
using Microsoft.Maui.Controls;

namespace Assignment2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddPatientClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AddPatientPage");
        }

        private void OnAddPhysicianClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AddPhysicianPage");
        }

        private void OnAddAppointmentClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AddAppointmentPage");
        }

        private void OnShowPatientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ViewPatientsPage");
        }

        private void OnShowPhysiciansClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ViewPhysiciansPage");
        }

        private void OnShowAppointmentsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ViewAppointmentsPage");
        }
    }
}