using Assignment2.Models;

namespace Assignment2.Services
{
    public static class DataManager
    {
        public static Patient[] Patients = new Patient[0];
        public static Physician[] Physicians = new Physician[0];
        public static Appointment[] Appointments = new Appointment[0];

        public static T[] GrowArray<T>(T[] oldArray, T newItem)
        {
            T[] newArray = new T[oldArray.Length + 1];
            for (int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }
            newArray[oldArray.Length] = newItem;
            return newArray;
        }

        public static T[] RemoveFromArray<T>(T[] oldArray, int index)
        {
            T[] newArray = new T[oldArray.Length - 1];
            int newIndex = 0;
            for (int i = 0; i < oldArray.Length; i++)
            {
                if (i != index)
                {
                    newArray[newIndex] = oldArray[i];
                    newIndex++;
                }
            }
            return newArray;
        }

        public static bool IsValidTime(string time)
        {
            string[] validTimes = {
                "8:00am","8:30am","9:00am","9:30am","10:00am","10:30am","11:00am","11:30am",
                "12:00pm","12:30pm","1:00pm","1:30pm","2:00pm","2:30pm","3:00pm","3:30pm",
                "4:00pm","4:30pm","5:00pm"
            };

            foreach (string t in validTimes)
            {
                if (time == t) return true;
            }
            return false;
        }
    }
}