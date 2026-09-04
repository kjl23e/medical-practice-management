using System;
using Microsoft.Maui.Controls;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2
{
    public partial class AddAppointmentPage : ContentPage
    {
        public AddAppointmentPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private void LoadData()
        {
            PatientPicker.Items.Clear();
            for (int i = 0; i < DataManager.Patients.Length; i++)
            {
                PatientPicker.Items.Add(DataManager.Patients[i].Name);
            }

            PhysicianPicker.Items.Clear();
            for (int i = 0; i < DataManager.Physicians.Length; i++)
            {
                PhysicianPicker.Items.Add(DataManager.Physicians[i].Name);
            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (DataManager.Patients.Length == 0 || DataManager.Physicians.Length == 0)
            {
                MessageLabel.Text = "Need at least 1 patient and 1 physician.";
                MessageLabel.TextColor = Colors.Red;
                return;
            }

            if (PatientPicker.SelectedIndex == -1 || PhysicianPicker.SelectedIndex == -1 ||
                DayPicker.SelectedIndex == -1 || TimePicker.SelectedIndex == -1 || RoomPicker.SelectedIndex == -1)
            {
                MessageLabel.Text = "Please fill all fields.";
                MessageLabel.TextColor = Colors.Red;
                return;
            }

            int pIndex = PatientPicker.SelectedIndex;
            int dIndex = PhysicianPicker.SelectedIndex;
            string day = DayPicker.SelectedItem.ToString();
            string time = TimePicker.SelectedItem.ToString();
            string room = RoomPicker.SelectedItem.ToString();

            // Check for physician double booking
            for (int i = 0; i < DataManager.Appointments.Length; i++)
            {
                if (DataManager.Appointments[i].Physician == DataManager.Physicians[dIndex] &&
                    DataManager.Appointments[i].Day == day &&
                    DataManager.Appointments[i].Time == time)
                {
                    MessageLabel.Text = "Physician already booked at that time.";
                    MessageLabel.TextColor = Colors.Red;
                    return;
                }
            }

            // Check for room double booking
            for (int i = 0; i < DataManager.Appointments.Length; i++)
            {
                if (DataManager.Appointments[i].Room == room &&
                    DataManager.Appointments[i].Day == day &&
                    DataManager.Appointments[i].Time == time)
                {
                    MessageLabel.Text = "Room already booked at that time.";
                    MessageLabel.TextColor = Colors.Red;
                    return;
                }
            }

            Appointment a = new Appointment();
            a.Patient = DataManager.Patients[pIndex];
            a.Physician = DataManager.Physicians[dIndex];
            a.Day = day;
            a.Time = time;
            a.Room = room;

            DataManager.Appointments = DataManager.GrowArray(DataManager.Appointments, a);

            MessageLabel.Text = "Appointment added successfully!";
            MessageLabel.TextColor = Colors.Green;

            // Clear selections
            PatientPicker.SelectedIndex = -1;
            PhysicianPicker.SelectedIndex = -1;
            DayPicker.SelectedIndex = -1;
            TimePicker.SelectedIndex = -1;
            RoomPicker.SelectedIndex = -1;
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}