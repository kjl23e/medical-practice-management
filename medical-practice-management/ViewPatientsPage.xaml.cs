using System;
using Microsoft.Maui.Controls;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2
{
    public partial class ViewPatientsPage : ContentPage
    {
        private string currentSort = "Name (A-Z)";

        public ViewPatientsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadPatients();
        }

        private void OnSortChanged(object sender, EventArgs e)
        {
            if (SortPicker.SelectedIndex != -1)
            {
                currentSort = SortPicker.SelectedItem.ToString();
                LoadPatients();
            }
        }

        private Patient[] GetSortedPatients()
        {
            Patient[] sorted = new Patient[DataManager.Patients.Length];
            for (int i = 0; i < DataManager.Patients.Length; i++)
            {
                sorted[i] = DataManager.Patients[i];
            }

            // Bubble sort
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                for (int j = 0; j < sorted.Length - i - 1; j++)
                {
                    bool swap = false;

                    if (currentSort == "Name (A-Z)")
                    {
                        if (string.Compare(sorted[j].Name, sorted[j + 1].Name) > 0)
                            swap = true;
                    }
                    else if (currentSort == "Name (Z-A)")
                    {
                        if (string.Compare(sorted[j].Name, sorted[j + 1].Name) < 0)
                            swap = true;
                    }
                    else if (currentSort == "Birthdate (Oldest)")
                    {
                        if (string.Compare(sorted[j].Birthdate, sorted[j + 1].Birthdate) > 0)
                            swap = true;
                    }
                    else if (currentSort == "Birthdate (Youngest)")
                    {
                        if (string.Compare(sorted[j].Birthdate, sorted[j + 1].Birthdate) < 0)
                            swap = true;
                    }

                    if (swap)
                    {
                        Patient temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }

            return sorted;
        }

        private bool IsMinor(string birthdate)
        {
            try
            {
                DateTime bday = DateTime.Parse(birthdate);
                int age = DateTime.Now.Year - bday.Year;
                if (DateTime.Now < bday.AddYears(age)) age--;
                return age < 18;
            }
            catch
            {
                return false;
            }
        }

        private void LoadPatients()
        {
            PatientsContainer.Clear();

            Patient[] patients = GetSortedPatients();

            for (int i = 0; i < patients.Length; i++)
            {
                Patient p = patients[i];

                // Find actual index in DataManager
                int actualIndex = -1;
                for (int j = 0; j < DataManager.Patients.Length; j++)
                {
                    if (DataManager.Patients[j] == p)
                    {
                        actualIndex = j;
                        break;
                    }
                }

                // Color code: minors in light pink
                Color bgColor = Colors.LightGray;
                if (IsMinor(p.Birthdate))
                {
                    bgColor = Color.FromRgb(255, 200, 220); // Light pink for minors
                }

                Frame patientFrame = new Frame
                {
                    BorderColor = Colors.Gray,
                    CornerRadius = 5,
                    Padding = 10,
                    BackgroundColor = bgColor
                };

                VerticalStackLayout content = new VerticalStackLayout { Spacing = 5 };

                content.Add(new Label { Text = "Name: " + p.Name, FontAttributes = FontAttributes.Bold });
                content.Add(new Label { Text = "Address: " + p.Address });
                content.Add(new Label { Text = "Birthdate: " + p.Birthdate });
                content.Add(new Label { Text = "Race: " + p.Race });
                content.Add(new Label { Text = "Gender: " + p.Gender });

                if (IsMinor(p.Birthdate))
                {
                    content.Add(new Label { Text = "⚠ MINOR (< 18 years old)", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold });
                }

                if (p.Notes.Length > 0)
                {
                    content.Add(new Label { Text = "Notes:", FontAttributes = FontAttributes.Bold });
                    for (int j = 0; j < p.Notes.Length; j++)
                    {
                        content.Add(new Label { Text = "  - " + p.Notes[j] });
                    }
                }

                HorizontalStackLayout buttons = new HorizontalStackLayout { Spacing = 10, Margin = new Thickness(0, 10, 0, 0) };

                Button addNoteBtn = new Button { Text = "Add Note", BackgroundColor = Colors.Blue };
                int capturedIndex = actualIndex;
                addNoteBtn.Clicked += (s, e) => OnAddNoteClicked(capturedIndex);

                Button editBtn = new Button { Text = "Edit", BackgroundColor = Colors.Orange };
                editBtn.Clicked += (s, e) => OnEditClicked(capturedIndex);

                Button deleteBtn = new Button { Text = "Delete", BackgroundColor = Colors.Red };
                deleteBtn.Clicked += (s, e) => OnDeleteClicked(capturedIndex);

                buttons.Add(addNoteBtn);
                buttons.Add(editBtn);
                buttons.Add(deleteBtn);
                content.Add(buttons);

                patientFrame.Content = content;
                PatientsContainer.Add(patientFrame);
            }
        }

        private async void OnAddNoteClicked(int index)
        {
            string note = await DisplayPromptAsync("Add Note", "Enter medical note (diagnosis/prescription):");
            if (!string.IsNullOrEmpty(note))
            {
                DataManager.Patients[index].Notes = DataManager.GrowArray(DataManager.Patients[index].Notes, note);
                LoadPatients();
            }
        }

        private async void OnEditClicked(int index)
        {
            Patient p = DataManager.Patients[index];

            string name = await DisplayPromptAsync("Edit Patient", "Name:", initialValue: p.Name);
            if (string.IsNullOrEmpty(name)) return;

            string address = await DisplayPromptAsync("Edit Patient", "Address:", initialValue: p.Address);
            string birthdate = await DisplayPromptAsync("Edit Patient", "Birthdate:", initialValue: p.Birthdate);
            string race = await DisplayPromptAsync("Edit Patient", "Race:", initialValue: p.Race);
            string gender = await DisplayPromptAsync("Edit Patient", "Gender:", initialValue: p.Gender);

            p.Name = name;
            p.Address = address ?? p.Address;
            p.Birthdate = birthdate ?? p.Birthdate;
            p.Race = race ?? p.Race;
            p.Gender = gender ?? p.Gender;

            LoadPatients();
        }

        private async void OnDeleteClicked(int index)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Delete this patient?", "Yes", "No");
            if (confirm)
            {
                DataManager.Patients = DataManager.RemoveFromArray(DataManager.Patients, index);
                LoadPatients();
            }
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}