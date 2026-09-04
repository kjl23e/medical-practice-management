using System;
using Microsoft.Maui.Controls;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2
{
    public partial class ViewAppointmentsPage : ContentPage
    {
        public ViewAppointmentsPage()
        {
            InitializeComponent();
            BackButton.Clicked += OnBackClicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentsContainer.Clear();

            if (DataManager.Appointments.Length == 0)
            {
                Label noData = new Label
                {
                    Text = "No appointments scheduled.",
                    FontSize = 16,
                    TextColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                AppointmentsContainer.Add(noData);
                return;
            }

            for (int i = 0; i < DataManager.Appointments.Length; i++)
            {
                Appointment a = DataManager.Appointments[i];
                int index = i;

                // Color code: today's appointments in light green
                Color bgColor = Colors.LightYellow;
                string today = DateTime.Now.DayOfWeek.ToString().ToUpper();
                if (a.Day == today)
                {
                    bgColor = Color.FromRgb(200, 255, 200);
                }

                Frame appointmentFrame = new Frame
                {
                    BorderColor = Colors.Gray,
                    CornerRadius = 5,
                    Padding = 10,
                    BackgroundColor = bgColor
                };

                VerticalStackLayout content = new VerticalStackLayout { Spacing = 5 };

                string dayTime = (a.Day ?? "No Day") + " at " + (a.Time ?? "No Time");
                string patientName = a.Patient != null ? (a.Patient.Name ?? "Unknown Patient") : "No Patient";
                string physicianName = a.Physician != null ? (a.Physician.Name ?? "Unknown Physician") : "No Physician";
                string room = a.Room ?? "No Room";

                content.Add(new Label
                {
                    Text = dayTime,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Colors.Black
                });
                content.Add(new Label
                {
                    Text = "Patient: " + patientName,
                    TextColor = Colors.Black
                });
                content.Add(new Label
                {
                    Text = "Physician: " + physicianName,
                    TextColor = Colors.Black
                });
                content.Add(new Label
                {
                    Text = "Room: " + room,
                    TextColor = Colors.Black
                });

                if (a.Diagnoses.Length > 0)
                {
                    content.Add(new Label { Text = "Diagnoses:", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black });
                    for (int j = 0; j < a.Diagnoses.Length; j++)
                    {
                        content.Add(new Label { Text = "  - " + a.Diagnoses[j], TextColor = Colors.Black });
                    }
                }

                if (a.Treatments.Length > 0)
                {
                    content.Add(new Label { Text = "Treatments:", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black });
                    double totalCost = 0;
                    for (int j = 0; j < a.Treatments.Length; j++)
                    {
                        content.Add(new Label { Text = "  - " + a.Treatments[j].Name + " ($" + a.Treatments[j].Cost + ")", TextColor = Colors.Black });
                        totalCost += a.Treatments[j].Cost;
                    }
                    content.Add(new Label { Text = "Total Cost: $" + totalCost, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black });
                }

                HorizontalStackLayout buttons = new HorizontalStackLayout { Spacing = 5, Margin = new Thickness(0, 10, 0, 0) };

                Button addDiagnosisBtn = new Button { Text = "Add Diagnosis", BackgroundColor = Colors.Blue, TextColor = Colors.White, FontSize = 12 };
                addDiagnosisBtn.Clicked += (s, e) => OnAddDiagnosisClicked(index);

                Button addTreatmentBtn = new Button { Text = "Add Treatment", BackgroundColor = Colors.Green, TextColor = Colors.White, FontSize = 12 };
                addTreatmentBtn.Clicked += (s, e) => OnAddTreatmentClicked(index);

                Button deleteBtn = new Button { Text = "Delete", BackgroundColor = Colors.Red, TextColor = Colors.White, FontSize = 12 };
                deleteBtn.Clicked += (s, e) => OnDeleteClicked(index);

                buttons.Add(addDiagnosisBtn);
                buttons.Add(addTreatmentBtn);
                buttons.Add(deleteBtn);
                content.Add(buttons);

                appointmentFrame.Content = content;
                AppointmentsContainer.Add(appointmentFrame);
            }
        }

        private async void OnAddDiagnosisClicked(int index)
        {
            string diagnosis = await DisplayPromptAsync("Add Diagnosis", "Enter diagnosis:");
            if (!string.IsNullOrEmpty(diagnosis))
            {
                DataManager.Appointments[index].Diagnoses = DataManager.GrowArray(DataManager.Appointments[index].Diagnoses, diagnosis);
                LoadAppointments();
            }
        }

        private async void OnAddTreatmentClicked(int index)
        {
            string name = await DisplayPromptAsync("Add Treatment", "Enter treatment name:");
            if (string.IsNullOrEmpty(name)) return;

            string costStr = await DisplayPromptAsync("Add Treatment", "Enter cost:");
            if (string.IsNullOrEmpty(costStr)) return;

            double cost;
            if (double.TryParse(costStr, out cost))
            {
                Treatment t = new Treatment();
                t.Name = name;
                t.Cost = cost;
                DataManager.Appointments[index].Treatments = DataManager.GrowArray(DataManager.Appointments[index].Treatments, t);
                LoadAppointments();
            }
            else
            {
                await DisplayAlert("Error", "Invalid cost entered.", "OK");
            }
        }

        private async void OnDeleteClicked(int index)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Delete this appointment?", "Yes", "No");
            if (confirm)
            {
                DataManager.Appointments = DataManager.RemoveFromArray(DataManager.Appointments, index);
                LoadAppointments();
            }
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}